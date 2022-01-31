using Microsoft.EntityFrameworkCore;
using ProiectDAW.Data;
using ProiectDAW.Models;
using ProiectDAW.Repositories.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ProiectDAW.Repositories
{
    public class UserRepository : PagedRepository<User>, IUserRepository
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

        public override async Task<List<User>> FindByPredicatePaged(Expression<Func<User, bool>> predicate, int page, int pageSize)
        {
            return await _table.Where(predicate)
                               .Skip(page * pageSize)
                               .Take(pageSize)
                               .ToListAsync();
        }
    }
}
