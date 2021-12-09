using ProiectDAW.Models;
using System.Threading.Tasks;

namespace ProiectDAW.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<bool> ExistsByEmail(string email);
    }
}
