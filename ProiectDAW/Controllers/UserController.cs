using Microsoft.AspNetCore.Mvc;
using ProiectDAW.DTOs;
using ProiectDAW.Security.Attributes;
using ProiectDAW.Services;
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

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] DirectSigninUserDTO userDTO)
        {
            var registerResult = await _service.Register(userDTO);
            if (registerResult.IsError)
                return BadRequest(registerResult.Error);

            return Ok(new { token = registerResult.Token });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] DirectLoginUserDTO userDTO)
        {
            var authenticationResult = await _service.Authenticate(userDTO);
            if (authenticationResult.IsError)
                return BadRequest(authenticationResult.Error);

            return Ok(new { token = authenticationResult.Token });
        }

        [HttpGet("authorizedHello")]
        [Authorization]
        public IActionResult AuthorizedHello()
        {
            return Ok("Hello");
        }

    }
}
