using ProiectDAW.Models;
using ProiectDAW.Repositories.Generic;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProiectDAW.Repositories
{
    public interface IGenreRepository : IGenericRepository<Genre>
    {
        Task<List<Genre>> FindAll();

        Task<List<Genre>> FindByBookId(Guid id);
    }
}
