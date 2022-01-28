using Microsoft.AspNetCore.Mvc;
using ProiectDAW.DTOs;
using ProiectDAW.Models;
using ProiectDAW.Security.Attributes;
using ProiectDAW.Services;
using System;
using System.Threading.Tasks;

namespace ProiectDAW.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _service;

        public BookController(IBookService service)
        {
            _service = service;
        }

        [HttpPost]
        [Authorization]
        public async Task<IActionResult> CreateBook([FromForm] BookUploadsRequestDTO bookDTO)
        {
            var book = await _service.Create(bookDTO);
            if (book == null)
                return BadRequest(new { Message = "Couldn't save book" });
            return Ok(book);
        }

        [HttpGet("content/{id}")]
        [Authorization]
        public async Task<IActionResult> GetBookContent([FromRoute] Guid id)
        {
            var book = await _service.Find(id);
            if (book == null)
                return NotFound();
            return new PhysicalFileResult(book.FilePath, "application/pdf");
        }

        [HttpDelete("{id}")]
        [Authorization]
        public async Task<IActionResult> DeleteBook([FromRoute] Guid id)
        {
            var book = await _service.Find(id);
            if (book == null)
                return NotFound();
            if (!_service.IsUploadedByPrincipal(book))
                return Unauthorized();
            var success = await _service.Delete(book);
            if (!success)
                return BadRequest();
            return Ok();
        }

        [HttpPut("{id}")]
        [Authorization]
        public async Task<IActionResult> UpdateBook([FromForm] BookUploadsRequestDTO bookDTO, [FromRoute] Guid id)
        {
            Book book = await _service.FindAsNoTracking(id);
            if (book == null)
                return NotFound();
            if (!_service.IsUploadedByPrincipal(book))
                return Unauthorized();
            bookDTO.Id = book.Id;
            BookUploadsResponseDTO updated = await _service.Update(bookDTO);
            if (updated == null)
                return BadRequest();
            return Ok(updated);
        }

        [HttpGet("uploads")]
        [Authorization]
        public async Task<IActionResult> GetBooksByUploaderAndTitle([FromQuery] int pageSize, [FromQuery] int page, [FromQuery] string q)
        {
            if (q == null)
                q = "";
            return Ok(await _service.FindByUploaderAndTitlePaged(q, pageSize, page, BookOrder.UPLOAD_TIME_ASCENDING));
        }

        [HttpGet("")]
        [Authorization]
        public async Task<IActionResult> GetBooks([FromQuery] int pageSize, [FromQuery]int page, [FromQuery]int order, [FromQuery]string field, [FromQuery]string q)
        {
            if (field == null || field.Equals("title"))
                return Ok(await _service.FindByTitlePaged(q ?? "", pageSize, page, (BookOrder)order));
            else
                return Ok(await _service.FindByAuthorPaged(q ?? "", pageSize, page, (BookOrder)order));
        }

        [HttpGet("{id}")]
        [Authorization]
        public async Task<IActionResult> GetBook([FromRoute] Guid id)
        {
            BookDetailsDTO book = await _service.FindWithDetails(id);
            if (book == null)
                return NotFound();
            else return Ok(book);
        }
    }
}
