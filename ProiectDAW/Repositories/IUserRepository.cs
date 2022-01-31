using ProiectDAW.Models;
using ProiectDAW.Repositories.Generic;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ProiectDAW.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<bool> ExistsByEmail(string email);

        Task<User> FindByEmail(string email);

        Task<List<User>> FindByPredicatePaged(Expression<Func<User, bool>> predicate, int page, int pageSize);

        Task<int> CountByPredicate(Expression<Func<User, bool>> predicate);
    }
}
