using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProiectDAW.DTOs
{
    public class ManyToManyUpdateDTO
    {
        public Guid EntityId { get; set; }

        public List<Guid> Removed { get; set; }

        public List<Guid> Added { get; set; }
    }
}
