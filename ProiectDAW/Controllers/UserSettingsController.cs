using Microsoft.AspNetCore.Mvc;
using ProiectDAW.DTOs;
using ProiectDAW.Services;
using System.Threading.Tasks;

namespace ProiectDAW.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserSettingsController : ControllerBase
    {
        private readonly IUserSettingsService _service;
        public UserSettingsController(IUserSettingsService service)
        {
            _service = service;
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUserSettings([FromBody] UserSettingsDTO userSettings)
        {
            if (!await _service.Update(userSettings))
                return BadRequest();
            return Ok();
        }
    }
}
