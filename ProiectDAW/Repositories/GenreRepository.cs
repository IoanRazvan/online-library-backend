using Microsoft.EntityFrameworkCore;
using ProiectDAW.Data;
using ProiectDAW.Models;
using ProiectDAW.Repositories.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProiectDAW.Repositories
{
    public class GenreRepository : GenericRepository<Genre>, IGenreRepository
    {
        public GenreRepository(NgReadingContext _context) : base(_context)
        {
        }

        public async Task<List<Genre>> FindAll()
        {
            return await _table.ToListAsync();
        }

        public async Task<List<Genre>> FindByBookId(Guid id)
        {
            return await _table.Include(g => g.Books)
                               .Where(g => g.Books.Any(b => b.Id.Equals(id)))
                               .ToListAsync();
        }
    }
}
