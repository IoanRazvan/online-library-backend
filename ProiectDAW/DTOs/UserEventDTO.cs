using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProiectDAW.DTOs
{
    public class UserEventDTO
    {
        [RegularExpression(@"promote|disable|enable")]
        public string Operation { get; set; }

        public Guid Id { get; set; }
    }
}
