using Microsoft.EntityFrameworkCore;
using ProiectDAW.Data;
using ProiectDAW.Models;
using ProiectDAW.Repositories.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ProiectDAW.Repositories
{
    public class ReviewRepository : PagedRepository<Review>, IReviewRepository
    {
        public ReviewRepository(NgReadingContext context) : base(context)
        {
        }

        public override async void Create(Review entity)
        {
            await _table.AddAsync(entity);
        }

        public async Task<bool> ExistsByBookIdAndReviewerId(Guid bookId, Guid reviewerId)
        {
            return await _table.AnyAsync(review => review.BookId.Equals(bookId) && review.ReviewerId.Equals(reviewerId));
        }

        public override async Task<List<Review>> FindByPredicatePaged(Expression<Func<Review, bool>> predicate, int page, int pageSize)
        {
            return await _table.Include(review => review.Reviewer)
                               .Where(predicate)
                               .OrderByDescending(review => review.DateOfPosting)
                               .Skip(page * pageSize)
                               .Take(pageSize)
                               .ToListAsync();
        }
    }
}
