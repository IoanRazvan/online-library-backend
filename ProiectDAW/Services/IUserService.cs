using ProiectDAW.DTOs;
using ProiectDAW.Models;
using ProiectDAW.Services.Generic;
using ProiectDAW.Services.Types;
using System.Threading.Tasks;

namespace ProiectDAW.Services
{
    public interface IUserService : IGenericService<User>
    {
        int BAD_CREDENTIALS { get; }
        int DISABLED_ACCOUNT { get; }

        Task<string> Create(DirectSigninUserDTO user);

        Task<AuthenticationResult> Authenticate(DirectLoginUserDTO user);

        Task<bool> ExistsByEmail(string email);

        UserDTO Find();

        Task<bool> Update(UserDTO userInformation);

        Task<Page<AdminEditableUserDTO>> FindAllExceptPrincipalPaged(int page, int pageSize, string q);

        bool IsPrincipal(User user);

        Task<AdminEditableUserDTO> PromoteUser(User user);
        Task<AdminEditableUserDTO> EnableUser(User user);
        Task<AdminEditableUserDTO> DisableUser(User user);
    }
}
