using Microsoft.EntityFrameworkCore;
using ProiectDAW.Data;
using System.Threading.Tasks;

namespace ProiectDAW.Repositories
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

        public void Create(TEntity entity)
        {
            _table.Add(entity);
        }

        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Update(TEntity entity)
        {
            _table.Update(entity);
        }

        public void Delete(TEntity entity)
        {
            _table.Remove(entity);
        }

        public async Task<TEntity> FindByIdAsync(object id)
        {
            return await _table.FindAsync(id);
        }
    }
}
