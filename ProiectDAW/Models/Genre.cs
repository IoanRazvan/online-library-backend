using ProiectDAW.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProiectDAW.Models
{
    public class Genre : BaseEntity
    {
        public string GenreTitle { get; set; }

        public ICollection<BookGenre> BookGenres { get; set; }
    }
}
