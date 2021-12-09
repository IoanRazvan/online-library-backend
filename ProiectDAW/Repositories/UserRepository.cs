using Microsoft.EntityFrameworkCore;
using ProiectDAW.Data;
using ProiectDAW.Models;
using System.Linq;
using System.Threading.Tasks;

namespace ProiectDAW.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(NgReadingContext context) : base(context)
        {
        }

        public async Task<bool> ExistsByEmail(string email)
        {
            return await _table.AsNoTracking().Where(user => user.Email.Equals(email)).AnyAsync();
        }
    }
}
