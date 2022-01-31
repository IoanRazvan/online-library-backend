using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProiectDAW.Models
{
    public class Review
    {
        public Guid BookId { get; set; }

        public Book Book { get; set; }

        public Guid ReviewerId { get; set; }

        public User Reviewer { get; set; }

        public float Score { get; set; }

        public string Comment { get; set; }

        public DateTime DateOfPosting { get; set; }
    }
}
