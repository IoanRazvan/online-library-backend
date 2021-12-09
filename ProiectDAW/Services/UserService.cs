using ProiectDAW.Models;
using ProiectDAW.Repositories;
using ProiectDAW.Services.Generic;
using System.Threading.Tasks;

namespace ProiectDAW.Services
{
    public class UserService : GenericService<User>, IUserService
    {
        public UserService(IUserRepository repo) : base(repo)
        {
        }

        public async Task<bool> ExistsByEmail(string email)
        {
            return await ((IUserRepository)_repo).ExistsByEmail(email);
        }
    }
}
