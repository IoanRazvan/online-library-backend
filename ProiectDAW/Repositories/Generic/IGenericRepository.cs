using System.Threading.Tasks;

namespace ProiectDAW.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        void Create(TEntity entity);

        Task<bool> Save();

        void Delete(TEntity entity);

        void Update(TEntity entity);

        Task<TEntity> FindById(object id);
    }
}
