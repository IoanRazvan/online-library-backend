using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProiectDAW.DTOs
{
    public class BookUploadsResponseDTO : BookDTO
    {
        public List<GenreDTO> Genres { get; set; }
    }
}
