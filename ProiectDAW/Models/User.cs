using ProiectDAW.Models.Base;
using System;
using System.Collections.Generic;

namespace ProiectDAW.Models
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public UserSettings UserSettings { get; set; }

        public DirectLoginUser DirectLoginUser { get; set; }

        public ICollection<Book> Uploads { get; set; }

        public ICollection<Review> Reviews { get; set; }

        public ICollection<Library> Libraries { get; set; }
    }
}
