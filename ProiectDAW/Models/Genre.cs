using ProiectDAW.Models.Base;
using System.Collections.Generic;

namespace ProiectDAW.Models
{
    public class Genre : BaseEntity
    {
        public string Title { get; set; }

        public ICollection<Book> Books { get; set; }
    }
}
