using ProiectDAW.Models;
using System.Threading.Tasks;

namespace ProiectDAW.Services
{
    public interface IUserService : IGenericService<User>
    {
        Task<bool> ExistsByEmail(string email);
    }
}
