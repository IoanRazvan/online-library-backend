using ProiectDAW.Data;
using ProiectDAW.Models;


namespace ProiectDAW.Repositories
{
    public class UserSettingsRepository : GenericRepository<UserSettings>, IUserSettingsRepository
    {
        public UserSettingsRepository(NgReadingContext context) : base(context)
        {
        }
    }
}
