using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProiectDAW.Security.Attributes;
using ProiectDAW.Services;
using System;
using System.Collections.Generic;
using System.Linq;
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
            return Ok(
           new
           {
               Libraries = await _service.FindLibrariesOfPrincipal(),
               SelectedLibraries = await _service.FindLibrariesOfPrincipalThatContainBook(bookId)
           });
        }
    }
}
