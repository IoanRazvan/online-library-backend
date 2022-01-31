using ProiectDAW.Repositories;
using ProiectDAW.Repositories.Generic;
using System.Threading.Tasks;

namespace ProiectDAW.Services.Generic
{
    public class GenericService<TEntity> : IGenericService<TEntity> where TEntity : class
    {
        protected readonly IGenericRepository<TEntity> _repo;

        public GenericService(IGenericRepository<TEntity> repo)
        {
            _repo = repo;
        }

        public async virtual Task<bool> Create(TEntity entity)
        {
            _repo.Create(entity);
            return await _repo.Save();
        }

        public async virtual Task<bool> Delete(TEntity entity)
        {
            _repo.Delete(entity);
            return await _repo.Save();
        }

        public async virtual Task<TEntity> Find(object id)
        {
            return await _repo.FindById(id);
        }

        public virtual Task<bool> Update(TEntity entity)
        {
            _repo.Update(entity);
            return _repo.Save();
        }
    }
}
