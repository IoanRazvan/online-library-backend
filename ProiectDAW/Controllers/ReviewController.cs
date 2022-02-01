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
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _service;

        public ReviewController(IReviewService service)
        {
            _service = service;
        }

        [HttpGet("{bookId}")]
        [Authorization]
        public async Task<IActionResult> GetBookReviewsPaged([FromRoute] Guid bookId, [FromQuery] int page, [FromQuery] int pageSize)
        {
           return Ok(await _service.FindByBookIdPaged(bookId, page, pageSize));
        }

        [HttpGet("has-reviewed/{bookId}")]
        [Authorization]
        public async Task<IActionResult> HasAlreadyReviewed([FromRoute] Guid bookId)
        {
            return Ok(await _service.ExistsByBookIdAndReviewerPrincipal(bookId));
        }

        [HttpPost("{bookId}")]
        public async Task<IActionResult> SaveReview([FromRoute] Guid bookId, [FromBody] ReviewDataDTO reviewData)
        {
            return Ok(await _service.Save(bookId, reviewData));
        }
    }
}
