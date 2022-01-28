using ProiectDAW.DTOs;
using ProiectDAW.Models;
using System.Threading.Tasks;

namespace ProiectDAW.Services
{
    public interface IUserSettingsService : IGenericService<UserSettings>
    {
        Task<bool> Update(UserSettingsDTO userSettings);
    }
}
