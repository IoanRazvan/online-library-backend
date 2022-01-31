using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ProiectDAW.Repositories.Generic
{
    public interface IPagedRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        Task<List<TEntity>> FindByPredicatePaged(Expression<Func<TEntity, bool>> predicate, int page, int pageSize);

        Task<int> CountByPredicate(Expression<Func<TEntity, bool>> predicate);
    }
}
