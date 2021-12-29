using ProiectDAW.Models.Base;
using System;
using System.Collections.Generic;

namespace ProiectDAW.Models
{
    public class Book : BaseEntity
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string FilePath { get; set; }
        
        public string CoverUrl { get; set; }

        public string AuthorName { get; set; }

        public User Uploader { get; set; }
        
        public Guid? UploaderId { get; set; }

        public ICollection<Review> Reviews { get; set; }

        public ICollection<Library> LibraryBooks { get; set; }

        public ICollection<Genre> Genres { get; set; }
    }
}
