using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProiectDAW.Models
{
    public class BookGenre
    {
        public Guid BookId { get; set; }

        public Book Book { get; set; }

        public Guid GenreId { get; set; }

        public Genre Genre { get; set; }
    }
}
