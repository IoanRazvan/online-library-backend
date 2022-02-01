using System;

namespace ProiectDAW.DTOs
{
    public class PostedReviewDTO : ReviewDataDTO
    {
        public string ReviewerFirstName { get; set; }

        public string ReviewerLastName { get; set; }

        public Guid BookId { get; set; }

        public DateTime DateOfPosting { get; set; }
    }
}
