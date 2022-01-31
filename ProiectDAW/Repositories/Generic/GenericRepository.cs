using Microsoft.EntityFrameworkCore;
using ProiectDAW.Data;
using System.Threading.Tasks;

namespace ProiectDAW.Repositories.Generic
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        protected readonly DbSet<TEntity> _table;
        protected readonly NgReadingContext _context;

        public GenericRepository(NgReadingContext context)
        {
            _context = context;
            _table = context.Set<TEntity>();
        }

        public virtual void Create(TEntity entity)
        {
            _table.Attach(entity);
        }

        public virtual async Task<bool> Save()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public virtual void Update(TEntity entity)
        {
            _table.Update(entity);
        }

        public virtual void Delete(TEntity entity)
        {
            _table.Remove(entity);
        }

        public virtual async Task<TEntity> FindById(object id)
        {
            return await _table.FindAsync(id);
        }
    }
}
