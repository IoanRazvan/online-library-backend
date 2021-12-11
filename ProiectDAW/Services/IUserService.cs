using ProiectDAW.DTOs;
using ProiectDAW.Models;
using ProiectDAW.Services.Types;
using System.Threading.Tasks;

namespace ProiectDAW.Services
{
    public interface IUserService : IGenericService<User>
    {
        Task<AuthenticationResult> Register(DirectSigninUserDTO user);

        Task<AuthenticationResult> Authenticate(DirectLoginUserDTO user);
    }
}
