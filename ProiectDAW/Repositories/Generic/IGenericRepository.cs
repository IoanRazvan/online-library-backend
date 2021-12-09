using System.Threading.Tasks;

namespace ProiectDAW.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        void Create(TEntity entity);

        Task<bool> SaveAsync();

        void Delete(TEntity entity);

        void Update(TEntity entity);

        Task<TEntity> FindByIdAsync(object id);
    }
}
