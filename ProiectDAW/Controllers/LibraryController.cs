using Microsoft.AspNetCore.Mvc;
using ProiectDAW.DTOs;
using ProiectDAW.Security.Attributes;
using ProiectDAW.Services;
using System;
using System.Threading.Tasks;

namespace ProiectDAW.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibraryController : ControllerBase
    {
        private readonly ILibraryService _service;
        public LibraryController(ILibraryService service)
        {
            _service = service;
        }

        [HttpGet("")]
        [Authorization]
        public async Task<IActionResult> GetLibraries([FromQuery] Guid bookId)
        {
            return Ok(new {
               Libraries = await _service.FindLibrariesByOwner(),
               SelectedLibraries = await _service.FindLibrariesByOwnerAndBook(bookId)
           });
        }

        [HttpPost("")]
        [Authorization]
        public async Task<IActionResult> UpdateLibraryBook([FromBody]ManyToManyUpdateDTO newLibraryAssignments)
        {
            var result = await _service.UpdateLibraryBook(newLibraryAssignments);
            if (result == null)
                return BadRequest();
            return Ok(result);
        }
    }
}
