using ProiectDAW.Models;
using ProiectDAW.Repositories.Generic;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ProiectDAW.Repositories
{
    public interface IUserRepository : IPagedRepository<User>
    {
        Task<bool> ExistsByEmail(string email);

        Task<User> FindByEmail(string email);
    }
}
