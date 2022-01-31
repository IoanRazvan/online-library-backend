using ProiectDAW.DTOs;
using ProiectDAW.Models;
using ProiectDAW.Repositories.Generic;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ProiectDAW.Repositories
{
    public interface IBookRepository : IPagedRepository<Book>
    {
        Task<List<Book>> FindByPredicatePaged(Expression<Func<Book, bool>> predicate, int pageSize, int page, BookOrder order);

        Task<Book> FindByIdAsNoTracking(Guid id);

        Task<int> Count();

        Task<object> FindBookWithDetails(Guid id);
    }
}
