using ProiectDAW.DTOs;
using ProiectDAW.Models;
using ProiectDAW.Services.Types;
using System;
using System.Threading.Tasks;

namespace ProiectDAW.Services
{
    public interface IBookService : IGenericService<Book>
    {
        Task<BookUploadsResponseDTO> AddBook(BookUploadsRequestDTO bookDTO);

        bool BookWasUploadedByPrincipal(Book book);

        Task<BookUploadsResponseDTO> Update(BookUploadsRequestDTO bookDTO);

        Task<Page<BookUploadsResponseDTO>> FindByUploaderAndTitlePaged(string title, int pageSize, int page);

        Task<Book> FindAsNoTracking(Guid id);
    }
}
