using ProiectDAW.DTOs;
using ProiectDAW.Models;
using ProiectDAW.Services.Generic;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProiectDAW.Services
{
    public interface IGenreService : IGenericService<Genre>
    {
        Task<List<GenreDTO>> FindAll();
    }
}
