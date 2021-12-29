using AutoMapper;
using ProiectDAW.DTOs;
using ProiectDAW.Models;
using ProiectDAW.Repositories;
using ProiectDAW.Services.Generic;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProiectDAW.Services
{
    public class GenreService : GenericService<Genre>, IGenreService
    {
        private readonly IMapper _mapper;
        public GenreService(IGenreRepository _repo, IMapper mapper) : base(_repo)
        {
            _mapper = mapper;
        }

        public async Task<List<GenreDTO>> FindAll()
        {
            return _mapper.Map<List<Genre>, List<GenreDTO>>(await ((IGenreRepository)_repo).FindAll());
        }
    }
}
