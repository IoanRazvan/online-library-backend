
using Microsoft.EntityFrameworkCore;
using ProiectDAW.Data;
using ProiectDAW.Models;
using ProiectDAW.Repositories.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ProiectDAW.Repositories
{
    public class BookRepository : PagedRepository<Book>, IBookRepository
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

        public override async Task<List<Book>> FindByPredicatePaged(Expression<Func<Book, bool>> predicate, int pageSize, int page) 
        {
            return await FindByPredicatePaged(predicate, pageSize, page, BookOrder.UPLOAD_TIME_ASCENDING);
        }

        public async Task<int> Count()
        {
            return await _table.CountAsync();
        }

        public async Task<object> FindBookWithDetails(Guid id)
        {
            return await 
            (
                from book in _table
                join review in _context.Reviews
                on book equals review.Book into group1
                from subBook in group1.DefaultIfEmpty()
                where (book.Id.Equals(id))
                group subBook by new { book.Id, book.Uploader.FirstName, book.Uploader.LastName, book.Title, book.Description, book.AuthorName, book.CoverUrl } into group2
                select new { 
                    group2.Key.Id,
                    group2.Key.Title,
                    group2.Key.CoverUrl,
                    group2.Key.Description,
                    group2.Key.AuthorName,
                    UploadedBy = group2.Key.FirstName + " " + group2.Key.LastName,
                    RatingCount = group2.Count(review => review != null),
                    MeanRating = group2.Average(review => review == null ? 0 : review.Score)
                }
             ).FirstOrDefaultAsync();
        }
    }
}
