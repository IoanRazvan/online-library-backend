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
    public class LibraryRepository : GenericRepository<Library>, ILibraryRepository
    {
        public LibraryRepository(NgReadingContext context) : base(context)
        {
        }

        public async Task<List<Library>> FindLibrariesByPredicate(Expression<Func<Library, bool>> predicate)
        {
            return await _table.Where(predicate)
                               .ToListAsync();
        }

        public void RemoveAssignments(List<LibraryBook> assignments)
        {
            _context.LibrariesBooks.RemoveRange(assignments);
        }

        public void AddAssignments(List<LibraryBook> assignments)
        {
            _context.LibrariesBooks.AddRange(assignments);
        }
    }
}
