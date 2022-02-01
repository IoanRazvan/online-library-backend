using AutoMapper;
using Microsoft.AspNetCore.Http;
using ProiectDAW.DTOs;
using ProiectDAW.Models;
using ProiectDAW.Repositories;
using ProiectDAW.Services.Generic;
using ProiectDAW.Services.Types;
using ProiectDAW.Utilities.Extensions;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ProiectDAW.Services
{
    public class ReviewService : GenericService<Review>, IReviewService
    {
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContext;

        public ReviewService(IReviewRepository repo, IMapper mapper, IHttpContextAccessor httpContext) : base(repo)
        {
            _mapper = mapper;
            _httpContext = httpContext;
        }

        public async Task<Page<PostedReviewDTO>> FindByBookIdPaged(Guid bookId, int page, int pageSize)
        {
            var result =  await ((IReviewRepository)_repo).FindByPredicatePaged(HasBookId(bookId), page, pageSize);
            var count = await ((IReviewRepository)_repo).CountByPredicate(HasBookId(bookId));
            return new Page<PostedReviewDTO>
            {
                Result = _mapper.Map<List<Review>, List<PostedReviewDTO>>(result),
                LastPageNumber = PageUtils.ComputeLastPage(count, pageSize),
                CurrentPageNumber = page,
                PageSize = pageSize
            };
        }

        private static Expression<Func<Review, bool>> HasBookId(Guid bookId)
        {
            return review => review.BookId.Equals(bookId);
        }

        public async Task<bool> ExistsByBookIdAndReviewerPrincipal(Guid bookId)
        {
            var principalId = _httpContext.GetPrincipal().Id;
            return await ((IReviewRepository)_repo).ExistsByBookIdAndReviewerId(bookId, principalId);
        }

        public async Task<PostedReviewDTO> Save(Guid bookId, ReviewDataDTO reviewData)
        {
            var reviewerId = _httpContext.GetPrincipal().Id;
            Review newReview = new()
            {
                BookId = bookId,
                ReviewerId = reviewerId,
                Score = reviewData.Score,
                Comment = reviewData.Comment,
                DateOfPosting = DateTime.UtcNow
            };
            // TODO check return of create
            await base.Create(newReview);
            return _mapper.Map<PostedReviewDTO>(newReview);
        }
    }
}
