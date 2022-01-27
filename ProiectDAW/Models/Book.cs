using ProiectDAW.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProiectDAW.Models
{
    public enum BookOrder
    {
        UPLOAD_TIME_ASCENDING = 1,
        UPLOAD_TIME_DESCENDING = 2
    }

    public class Book : BaseEntity
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string FilePath { get; set; }
        
        public string CoverUrl { get; set; }

        public string AuthorName { get; set; }
        
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime TimeOfUpload { get; set; }

        public User Uploader { get; set; }
        
        public Guid? UploaderId { get; set; }

        public ICollection<Review> Reviews { get; set; }

        public ICollection<LibraryBook> Libraries { get; set; }

        public ICollection<Genre> Genres { get; set; }
    }
}
