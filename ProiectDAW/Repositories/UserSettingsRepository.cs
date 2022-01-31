using ProiectDAW.Data;
using ProiectDAW.Models;
using ProiectDAW.Repositories.Generic;

namespace ProiectDAW.Repositories
{
    public class UserSettingsRepository : GenericRepository<UserSettings>, IUserSettingsRepository
    {
        public UserSettingsRepository(NgReadingContext context) : base(context)
        {
        }
    }
}
