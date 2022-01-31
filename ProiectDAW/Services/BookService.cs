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
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ProiectDAW.Services
{
    public class BookService : GenericService<Book>, IBookService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        private readonly IHostEnvironment _hostEnvironment;
        private readonly IGenreRepository _genreRepo;

        public BookService(IBookRepository repo, IGenreRepository genreRepo, IMapper mapper, IHttpContextAccessor httpContextAccessor, IHostEnvironment hostEnvironment) : base(repo)
        {
            _genreRepo = genreRepo;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _hostEnvironment = hostEnvironment;
        }

        public bool IsUploadedByPrincipal(Book book)
        {
            return _httpContextAccessor.GetPrincipal().Id.Equals(book.UploaderId);
        }

        public async Task<BookUploadsResponseDTO> Create(BookUploadsRequestDTO bookDTO)
        {
            Book book = _mapper.Map<Book>(bookDTO);
            book.TimeOfUpload = DateTime.Now;
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

        private async Task<string> SavePdfContent(IFormFile content)
        {
            var filePath = GenerateFilePath(content.FileName);
            using (Stream fileStream = new FileStream(filePath, FileMode.Create))
            {
                await content.CopyToAsync(fileStream);
            }
            return filePath;
        }

        private string GenerateFilePath(string uploadedFileName)
        {
            string fileName = GenerateFileName(uploadedFileName);
            string uploads = Path.Combine(_hostEnvironment.ContentRootPath, "uploads");
            return Path.Combine(uploads, fileName);
        }

        private string GenerateFileName(string uploadedFileName)
        {
            return _httpContextAccessor.GetPrincipal().Id + "_" + new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds() + "_" + Path.GetFileName(uploadedFileName);
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

        private static void UpdateBookFields(Book book, Book mappedDTO)
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

        public override async Task<bool> Delete(Book book)
        {
            bool deleteResult = await base.Delete(book);
            if (!deleteResult)
                return false;
            File.Delete(book.FilePath);
            return true;
        }

        public async Task<Page<BookUploadsResponseDTO>> FindByUploaderAndTitlePaged(string title, int pageSize, int page, BookOrder order)
        {
            return await FindByPredicatePaged(IsUploadedByAndHasTitle(_httpContextAccessor.GetPrincipal().Id, title), pageSize, page, title, order);
        }

        private static Expression<Func<Book, bool>> IsUploadedByAndHasTitle(Guid uploaderId, string title)
        {
            return book => book.UploaderId.Equals(uploaderId) && book.Title.ToLower().Contains(title.ToLower());
        }

        public async Task<Page<BookUploadsResponseDTO>> FindByTitlePaged(string title, int pageSize, int page, BookOrder order)
        {
            return await FindByPredicatePaged(HasTitle(title), pageSize, page, $"title={title}", order);
        }

        private static Expression<Func<Book, bool>> HasTitle(string title)
        {
            return book => book.Title.ToLower().Contains(title.ToLower());
        }

        public async Task<Page<BookUploadsResponseDTO>> FindByAuthorPaged(string author, int pageSize, int page, BookOrder order)
        {
            return await FindByPredicatePaged(HasAuthor(author), pageSize, page, $"author={author}", order);
        }

        private static Expression<Func<Book, bool>> HasAuthor(string author)
        {
            return book => book.AuthorName.ToLower().Contains(author.ToLower());
        }

        private async Task<Page<BookUploadsResponseDTO>> FindByPredicatePaged(Expression<Func<Book, bool>> predicate, int pageSize, int page, string query, BookOrder order)
        {
            List<Book> result = await ((IBookRepository)_repo).FindByPredicatePaged(predicate, pageSize, page, order);
            int totalRecords = await ((IBookRepository)_repo).CountByPredicate(predicate);
            List<BookUploadsResponseDTO> mapped = _mapper.Map<List<Book>, List<BookUploadsResponseDTO>>(result);
            int lastPage = PageUtils.ComputeLastPage(totalRecords, pageSize);
            return new Page<BookUploadsResponseDTO>
            {
                CurrentPageNumber = page,
                LastPageNumber = lastPage,
                Result = mapped,
                Query = query,
                Order = (int)order,
                PageSize = pageSize
            };
        }

        public async Task<Book> FindAsNoTracking(Guid id)
        {
            return await ((IBookRepository)_repo).FindByIdAsNoTracking(id);
        }

        public async Task<BookDetailsDTO> FindWithDetails(Guid id)
        {
            dynamic obj = await ((IBookRepository)_repo).FindBookWithDetails(id);
            if (obj == null)
                return null;
            return new BookDetailsDTO {
                Id = obj.Id,
                Title = obj.Title,
                CoverUrl = obj.CoverUrl,
                Genres = _mapper.Map<List<GenreDTO>>(await _genreRepo.FindByBookId(id)),
                AuthorName = obj.AuthorName,
                Description = obj.Description,
                UploadedBy = obj.UploadedBy,
                MeanRating = obj.MeanRating,
                RatingCount = obj.RatingCount
            };  
        }
    }
}
