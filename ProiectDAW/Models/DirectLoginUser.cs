using System;
using System.ComponentModel.DataAnnotations;

namespace ProiectDAW.Models
{
    public class DirectLoginUser
    {
        public string PasswordHash { get; set; }

        [Key]
        public Guid UserId { get; set; }

        public User User { get; set; }
    }
}
