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

        public async Task<User> FindByEmail(string email)
        {
            return await _table.AsNoTracking().Include(user => user.DirectLoginUser).FirstOrDefaultAsync(user => user.Email.Equals(email));
        }

        public override async Task<User> FindById(object id)
        {
            return await _table.Include(user => user.UserSettings)
                               .Where(user => user.Id.Equals(id))
                               .FirstOrDefaultAsync();                        
        }
    }
}
