using ProiectDAW.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProiectDAW.Models
{
    public class Library : BaseEntity
    {
        public string Name { get; set; }

        public string Color { get; set; }
        
        public Guid OwnerId { get; set; }

        public User Owner { get; set; }

        public ICollection<LibraryBook> Books { get; set; }
    }
}
