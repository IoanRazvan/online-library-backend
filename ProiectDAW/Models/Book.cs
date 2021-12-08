using ProiectDAW.Models.Base;
using System;
using System.Collections.Generic;

namespace ProiectDAW.Models
{
    public class Book : BaseEntity
    {
        public string Title { get; set; }

        public string Description { get; set; }
        
        public byte[] Content { get; set; }

        public User Uploader { get; set; }
        
        public Guid? UploaderId { get; set; }

        public ICollection<Review> Reviews { get; set; }

        public ICollection<LibraryBook> LibraryBooks { get; set; }

        public ICollection<BookGenre> BookGenres { get; set; }
    }
}
