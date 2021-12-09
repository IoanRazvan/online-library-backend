using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProiectDAW.DTOs;
using ProiectDAW.Models;
using ProiectDAW.Services;
using System.Threading.Tasks;

namespace ProiectDAW.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;
        private readonly IMapper _mapper;

        public UserController(IUserService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] DirectSigninUserDTO userDTO)
        {
            if (await _service.ExistsByEmail(userDTO.Email))
                return BadRequest(new { email = "Email is already registered in our platform" });

            User u = _mapper.Map<User>(userDTO);
            await _service.Create(u);
            return Ok();
        }
    }
}
