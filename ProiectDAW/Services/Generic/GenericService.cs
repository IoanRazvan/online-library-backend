using ProiectDAW.Repositories;
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

        public async Task<bool> Create(TEntity entity)
        {
            _repo.Create(entity);
            return await _repo.SaveAsync();
        }

        public async Task<bool> Delete(TEntity entity)
        {
            _repo.Delete(entity);
            return await _repo.SaveAsync();
        }

        public async Task<TEntity> Retrieve(object id)
        {
            return await _repo.FindByIdAsync(id);
        }

        public Task<bool> Update(TEntity entity)
        {
            _repo.Update(entity);
            return _repo.SaveAsync();
        }
    }
}
