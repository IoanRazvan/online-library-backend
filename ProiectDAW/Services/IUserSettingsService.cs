using ProiectDAW.DTOs;
using ProiectDAW.Models;
using ProiectDAW.Services.Generic;
using System.Threading.Tasks;

namespace ProiectDAW.Services
{
    public interface IUserSettingsService : IGenericService<UserSettings>
    {
        Task<bool> Update(UserSettingsDTO userSettings);
    }
}
