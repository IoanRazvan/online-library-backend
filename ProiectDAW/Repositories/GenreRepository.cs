using Microsoft.EntityFrameworkCore;
using ProiectDAW.Data;
using ProiectDAW.Models;
using System.Collections.Generic;
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
    }
}
