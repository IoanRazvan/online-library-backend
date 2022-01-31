using Microsoft.AspNetCore.Mvc;
using ProiectDAW.DTOs;
using ProiectDAW.Security.Attributes;
using ProiectDAW.Services;
using ProiectDAW.Services.Types;
using System;
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
            var token = await _service.Create(userDTO);
            if (token == null)
                return BadRequest(ErrorBody.FromMessage("Unable to register new user!"));

            return Ok(new { token = token });
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Login([FromBody] DirectLoginUserDTO userDTO)
        {
            var result = await _service.Authenticate(userDTO);
            if (result.IsError)
            {
                if (result.ErrorType == _service.BAD_CREDENTIALS)
                    return BadRequest(ErrorBody.FromMessage("Bad Credentials!"));
                else
                    return BadRequest(ErrorBody.FromMessage("This account has been disabled!"));
            }
            return Ok(new { token = result.Token});
        }

        [HttpGet]
        [Authorization]
        public IActionResult GetProfileInformation()
        {
            return Ok(_service.Find());
        }

        [HttpPut]
        [Authorization]
        public async Task<IActionResult> UpdateProfileInformation([FromBody] UserDTO userInformation)
        {
            // TODO add email check
            if (!await _service.Update(userInformation))
                return BadRequest();
            return Ok();

        }

        [HttpGet("admin")]
        [Authorization("Admin")]
        public async Task<IActionResult> GetUsers([FromQuery] int page, [FromQuery] int pageSize, [FromQuery] string q)
        {
            return Ok(await _service.FindAllExceptPrincipalPaged(page, pageSize, q ?? ""));
        }

        [HttpPut("admin")]
        [Authorization("Admin")]
        public async Task<IActionResult> EditUser([FromBody] UserEventDTO userEvent)
        {
            var user = await _service.Find(userEvent.Id);

            if (user == null)
                return NotFound();
            else if (user.UserRole.Equals("Admin") || _service.IsPrincipal(user))
                return Unauthorized();
            AdminEditableUserDTO updatedUser;
            switch (userEvent.Operation)
            {
                case "promote":
                    updatedUser = await _service.PromoteUser(user);
                    break;
                case "disable":
                    updatedUser = await _service.DisableUser(user);
                    break;
                case "enable":
                    updatedUser = await _service.EnableUser(user);
                    break;
                default:
                    return BadRequest();
            }
            if (updatedUser == null)
                return BadRequest();
            return Ok(updatedUser);
        }
    }
}
