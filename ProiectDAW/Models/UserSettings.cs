using ProiectDAW.Models.Base;

namespace ProiectDAW.Models
{
    public class UserSettings : BaseEntity
    {
        public bool RememberPageNumber { get; set; }

        public User User { get; set; }

        public readonly static UserSettings DEFAULT_SETTINGS = new() { RememberPageNumber = false };
    }
}
