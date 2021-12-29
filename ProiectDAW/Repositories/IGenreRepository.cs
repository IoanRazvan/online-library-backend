using ProiectDAW.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProiectDAW.Repositories
{
    public interface IGenreRepository : IGenericRepository<Genre>
    {
        Task<List<Genre>> FindAll();
    }
}
