using ProiectDAW.Models.Base;
using System;

namespace ProiectDAW.Models
{
    public class UserSettings : BaseEntity
    {
        public bool RememberPageNumber { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }

        public readonly static UserSettings DEFAULT_SETTINGS = new() { RememberPageNumber = false };
    }
}
