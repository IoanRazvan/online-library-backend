
using Microsoft.EntityFrameworkCore;
using ProiectDAW.Data;
using ProiectDAW.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ProiectDAW.Repositories
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        public BookRepository(NgReadingContext context) : base(context)
        {
        }

        public async Task<Book> FindByIdAsNoTracking(Guid id)
        {
            return await _table.AsNoTracking().FirstOrDefaultAsync(book => book.Id.Equals(id));
        }

        public override async Task<Book> FindById(object id)
        {
            return await _table.Include(book => book.Genres).FirstOrDefaultAsync(book => book.Id.Equals(id));
        }

        public async Task<List<Book>> FindByPredicatePaged(Expression<Func<Book, bool>> predicate, int pageSize, int page, BookOrder order)
        {
            var intermediaryQuery = _table.Where(predicate)
                                          .Include(book => book.Genres);
            IOrderedQueryable<Book> query;
            if (order == BookOrder.UPLOAD_TIME_DESCENDING)
                query = intermediaryQuery.OrderByDescending(book => book.TimeOfUpload);
            else
                query = intermediaryQuery.OrderBy(book => book.TimeOfUpload);

            return await query.Skip(pageSize * page)
                              .Take(pageSize)
                              .ToListAsync();
        }

        public async Task<int> Count()
        {
            return await _table.CountAsync();
        }

        public async Task<int> CountByPredicate(Expression<Func<Book, bool>> predicate)
        {
            return await _table.Where(predicate)
                               .CountAsync();
        }
    }
}
