using Majunga.Libraries.Infrastructure.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Majunga.Libraries.Infrastructure.Repositories
{
    public interface IGenericEfRepository<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
    {
        public Task Create(TEntity entity);
        public Task<List<TEntity>> Read();
        public Task<TEntity?> Read(TKey id, bool tracked = true);
        public Task Update(TKey id, TEntity entity);
        public Task Delete(TKey id);
    }
}
