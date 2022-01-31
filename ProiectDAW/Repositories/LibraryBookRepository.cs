using ProiectDAW.Data;
using ProiectDAW.Models;
using ProiectDAW.Repositories.Generic;
using System.Collections.Generic;

namespace ProiectDAW.Repositories
{
    public class LibraryBookRepository : GenericRepository<LibraryBook>, ILibraryBookRepository
    {
        public LibraryBookRepository(NgReadingContext contex) : base(contex)
        {
        }

        public override async void Create(LibraryBook entity)
        {
            await _table.AddAsync(entity);
        }

        public async void CreateRange(IEnumerable<LibraryBook> entities)
        {
            await _table.AddRangeAsync(entities);
        }

        public void DeleteRange(IEnumerable<LibraryBook> entities)
        {
            _table.RemoveRange(entities);
        }
    }
}
