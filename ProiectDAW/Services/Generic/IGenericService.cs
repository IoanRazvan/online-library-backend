using System.Threading.Tasks;

namespace ProiectDAW.Services.Generic
{
    public interface IGenericService<TEntity> where TEntity : class
    {
        Task<bool> Create(TEntity entity);

        Task<bool> Delete(TEntity entity);

        Task<bool> Update(TEntity entity);

        Task<TEntity> Find(object id);
    }
}
