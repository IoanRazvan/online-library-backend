using ProiectDAW.Models;
using ProiectDAW.Repositories.Generic;
using System.Collections.Generic;

namespace ProiectDAW.Repositories
{
    public interface ILibraryBookRepository : IGenericRepository<LibraryBook>
    {
        void CreateRange(IEnumerable<LibraryBook> entities);

        void DeleteRange(IEnumerable<LibraryBook> entities);
    }
}
