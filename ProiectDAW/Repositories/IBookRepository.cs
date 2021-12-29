using ProiectDAW.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProiectDAW.Repositories
{
    public interface IBookRepository : IGenericRepository<Book>
    {
        Task<List<Book>> FindByUploaderId(Guid uploaderId);

        Task<List<Book>> FindByUploaderIdAndTitlePaged(Guid uploaderId, string title, int pageSize, int page);

        Task<int> Count();

        Task<int> CountByUploaderIdAndTitle(Guid uploaderId, string title);

        Task<Book> FindByIdAsNoTracking(Guid id);
    }
}
