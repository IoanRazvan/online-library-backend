using ProiectDAW.DTOs;
using ProiectDAW.Models;
using ProiectDAW.Services.Generic;
using ProiectDAW.Services.Types;
using System;
using System.Threading.Tasks;

namespace ProiectDAW.Services
{
    public interface IReviewService : IGenericService<Review>
    {
        Task<Page<PostedReviewDTO>> FindByBookIdPaged(Guid bookId, int page, int pageSize);

        Task<bool> ExistsByBookIdAndReviewerPrincipal(Guid bookId);

        Task<PostedReviewDTO> Save(Guid bookId, ReviewDataDTO reviewData);
    }
}