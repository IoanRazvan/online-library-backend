using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using ProiectDAW.DTOs;
using ProiectDAW.Models;
using ProiectDAW.Repositories;
using ProiectDAW.Services.Generic;
using ProiectDAW.Services.Types;
using ProiectDAW.Utilities.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ProiectDAW.Services
{
    public class BookService : GenericService<Book>, IBookService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        private readonly IHostEnvironment _hostEnvironment;

        private string GenerateFileName(string uploadedFileName)
        {
            return _httpContextAccessor.GetPrincipal().Id + "_" + new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds() + "_" + Path.GetFileName(uploadedFileName);
        }

        private string GenerateFilePath(string uploadedFileName)
        {
            string fileName = GenerateFileName(uploadedFileName);
            string uploads = Path.Combine(_hostEnvironment.ContentRootPath, "uploads");
            return Path.Combine(uploads, fileName);
        }

        public BookService(IBookRepository repo, IMapper mapper, IHttpContextAccessor httpContextAccessor, IHostEnvironment hostEnvironment) : base(repo)
        {
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _hostEnvironment = hostEnvironment;
        }

        private async Task<string> SavePdfContent(IFormFile content)
        {
            var filePath = GenerateFilePath(content.FileName);
            using (Stream fileStream = new FileStream(filePath, FileMode.Create))
            {
                await content.CopyToAsync(fileStream);
            }
            return filePath;
        }

        public async Task<BookUploadsResponseDTO> AddBook(BookUploadsRequestDTO bookDTO)
        {
            Book book = _mapper.Map<Book>(bookDTO);
            book.Uploader = _httpContextAccessor.GetPrincipal();
            try
            {
                book.FilePath = await SavePdfContent(bookDTO.Content);
            }
            catch
            {
                return null;
            }
            if (!await Create(book))
                return null;
            return _mapper.Map<BookUploadsResponseDTO>(book);
        }

        public bool BookWasUploadedByPrincipal(Book book)
        {
            return _httpContextAccessor.GetPrincipal().Id.Equals(book.UploaderId);
        }

        public async Task<BookUploadsResponseDTO> Update(BookUploadsRequestDTO bookDTO)
        {
            Book book = await Find(bookDTO.Id);
            Book mappedDTO = _mapper.Map<Book>(bookDTO);
            UpdateBookFields(book, mappedDTO);
            string oldFilePath = book.FilePath;
            if (bookDTO.Content != null)
                try
                {
                    book.FilePath = await SavePdfContent(bookDTO.Content);
                }
                catch
                {
                    return null;
                }
            bool success = await Update(book);
            if (!success)
                return null;
            if (bookDTO.Content != null)
                File.Delete(oldFilePath);
            return _mapper.Map<BookUploadsResponseDTO>(book);
        }

        private void UpdateBookFields(Book book, Book mappedDTO)
        {
            book.Title = mappedDTO.Title;
            book.AuthorName = mappedDTO.AuthorName;
            book.CoverUrl = mappedDTO.CoverUrl;
            book.Description = mappedDTO.Description;
            // remove genres that are no longer present on the book
            foreach (var oldGenre in book.Genres)
            {
                if (mappedDTO.Genres.FirstOrDefault(newGenre => newGenre.Id.Equals(oldGenre.Id)) == null)
                    book.Genres.Remove(oldGenre);
            }
            // add new genres not yet present on the book
            foreach (var newGenre in mappedDTO.Genres)
            {
                if (book.Genres.FirstOrDefault(oldGenre => oldGenre.Id.Equals(newGenre.Id)) == null)
                    book.Genres.Add(newGenre);
            }
        }

        public async Task<Page<BookUploadsResponseDTO>> FindByUploaderAndTitlePaged(string title, int pageSize, int page)
        {
            List<Book> result = await ((IBookRepository)_repo).FindByUploaderIdAndTitlePaged(_httpContextAccessor.GetPrincipal().Id, title, pageSize, page);
            int totalRecords = await ((IBookRepository)_repo).CountByUploaderIdAndTitle(_httpContextAccessor.GetPrincipal().Id, title);
            List<BookUploadsResponseDTO> mapped = _mapper.Map<List<Book>, List<BookUploadsResponseDTO>>(result);
            int lastPage = ComputeLastPage(totalRecords, pageSize);
            return new Page<BookUploadsResponseDTO>
            {
                CurrentPageNumber = page,
                LastPageNumber = lastPage,
                Result = mapped,
                Query = title,
                PageSize = pageSize
            };
        }

        private static int ComputeLastPage(int totalRecords, int pageSize)
        {
            int result = (int)Math.Ceiling((double)totalRecords / pageSize) - 1;
            return result < 0 ? 0 : result;
        }

        public override async Task<bool> Delete(Book book)
        {
            bool deleteResult = await base.Delete(book);
            if (!deleteResult)
                return false;
            File.Delete(book.FilePath);
            return true;
        }

        public async Task<Book> FindAsNoTracking(Guid id)
        {
            return await ((IBookRepository)_repo).FindByIdAsNoTracking(id);
        }
    }
}
