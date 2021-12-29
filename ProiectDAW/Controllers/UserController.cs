using Microsoft.AspNetCore.Mvc;
using ProiectDAW.DTOs;
using ProiectDAW.Services;
using ProiectDAW.Services.Types;
using System.Threading.Tasks;

namespace ProiectDAW.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] DirectSigninUserDTO userDTO)
        {
            if (await _service.ExistsByEmail(userDTO.Email))
                return BadRequest(ErrorBody.FromMessage("Email is already registred!"));
            var token = await _service.Register(userDTO);
            if (token == null)
                return BadRequest(ErrorBody.FromMessage("Unable to register new user!"));

            return Ok(new { token = token });
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Login([FromBody] DirectLoginUserDTO userDTO)
        {
            var token = await _service.Authenticate(userDTO);
            if (token == null)
                return BadRequest(ErrorBody.FromMessage("Bad Credentials!"));

            return Ok(new { token = token });
        }
    }
}
