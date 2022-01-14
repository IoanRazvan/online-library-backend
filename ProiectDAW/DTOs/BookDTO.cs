using System;
using System.Collections.Generic;

namespace ProiectDAW.DTOs
{
    public class BookDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string CoverUrl { get; set; }
        public string AuthorName { get; set; }
        public DateTime TimeOfUpload { get; set; }
    }
}
