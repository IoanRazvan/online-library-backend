using ProiectDAW.Models.Base;
using System;

namespace ProiectDAW.Models
{
    public class UserSettings : BaseEntity
    {
        public bool RememberPageNumber { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }

        public static UserSettings GetDefaultSettings()
        {
            return new UserSettings() { RememberPageNumber = false };
        }
    }
}
