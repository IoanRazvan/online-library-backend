
using Microsoft.EntityFrameworkCore;
using ProiectDAW.Data;
using ProiectDAW.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProiectDAW.Repositories
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        public BookRepository(NgReadingContext context) : base(context)
        {
        }

        public async Task<int> Count()
        {
            return await _table.CountAsync();
        }

        public async Task<int> CountByUploaderIdAndTitle(Guid uploaderId, string title)
        {
            return await _table.Where(book => book.UploaderId.Equals(uploaderId) && book.Title.ToLower().Contains(title.ToLower()))
                                .CountAsync();
        }

        public async Task<List<Book>> FindByUploaderId(Guid uploaderId)
        {
            return await _table.Where(book => book.UploaderId.Equals(uploaderId))
                                .Include(book => book.Genres)
                                .ToListAsync();
        }

        public async Task<List<Book>> FindByUploaderIdAndTitlePaged(Guid uploaderId, string title, int pageSize, int page)
        {
            return await _table.Where(book => book.UploaderId.Equals(uploaderId) && book.Title.ToLower().Contains(title.ToLower()))
                                .Include(book => book.Genres)
                                .Skip(pageSize * page)
                                .Take(pageSize)
                                .ToListAsync();
        }

        public async Task<Book> FindByIdAsNoTracking(Guid id)
        {
            return await _table.AsNoTracking().FirstOrDefaultAsync(book => book.Id.Equals(id));
        }

        public override async Task<Book> FindById(object id)
        {
            return await _table.Include(book => book.Genres).FirstOrDefaultAsync(book => book.Id.Equals(id));
        }
    }
}
