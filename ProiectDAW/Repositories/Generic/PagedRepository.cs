using Microsoft.EntityFrameworkCore;
using ProiectDAW.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ProiectDAW.Repositories.Generic
{
    public abstract class PagedRepository<TEntity> : GenericRepository<TEntity>, IPagedRepository<TEntity> where TEntity : class
    {
        public PagedRepository(NgReadingContext context) : base(context)
        {
        }

        public async Task<int> CountByPredicate(Expression<Func<TEntity, bool>> predicate)
        {
            return await _table.Where(predicate)
                               .CountAsync();
        }

        public abstract Task<List<TEntity>> FindByPredicatePaged(Expression<Func<TEntity, bool>> predicate, int page, int pageSize);
    }
}
