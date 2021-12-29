using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace ProiectDAW.DTOs
{
    public class BookUploadsRequestDTO : BookDTO
    {
        public string Genres { get; set; }
        public IFormFile Content { get; set; }
    }
}
