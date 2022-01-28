using ProiectDAW.DTOs;
using ProiectDAW.Models;
using System.Threading.Tasks;

namespace ProiectDAW.Services
{
    public interface IUserService : IGenericService<User>
    {
        Task<string> Create(DirectSigninUserDTO user);

        Task<string> Authenticate(DirectLoginUserDTO user);

        Task<bool> ExistsByEmail(string email);

        UserDTO Find();

        Task<bool> Update(UserDTO userInformation);
    }
}
