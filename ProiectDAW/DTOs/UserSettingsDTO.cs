using System;
using System.ComponentModel.DataAnnotations;

namespace ProiectDAW.DTOs
{
    public class UserSettingsDTO
    {
        public bool RememberPageNumber { get; set; }

        [Range(5, 10)]
        public int NumberOfRecentBooks { get; set; }
    }
}
