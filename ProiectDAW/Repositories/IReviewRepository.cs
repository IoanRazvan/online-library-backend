using ProiectDAW.Models;
using ProiectDAW.Repositories.Generic;
using System;
using System.Threading.Tasks;

namespace ProiectDAW.Repositories
{
    public interface IReviewRepository : IPagedRepository<Review>
    {
        Task<bool> ExistsByBookIdAndReviewerId(Guid bookId, Guid reviewerId);
    }
}
