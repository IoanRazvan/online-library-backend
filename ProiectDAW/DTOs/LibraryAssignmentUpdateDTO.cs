using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProiectDAW.DTOs
{
    public class LibraryAssignmentUpdateDTO
    {
        public Guid BookId { get; set; }

        public List<Guid> Removed { get; set; }

        public List<Guid> Added { get; set; }
    }
}
