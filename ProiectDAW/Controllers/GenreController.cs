using Microsoft.AspNetCore.Mvc;
using ProiectDAW.Security.Attributes;
using ProiectDAW.Services;
using System.Threading.Tasks;

namespace ProiectDAW.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly IGenreService _service;

        public GenreController(IGenreService service) {
            _service = service;
        }

        [HttpGet]
        [Authorization]
        public async Task<IActionResult> GetGenres()
        {
            return Ok(await _service.FindAll());
        }
    }
}
