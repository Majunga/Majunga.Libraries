using Majunga.Libraries.Infrastructure.Entities;
using System.Threading.Tasks;

namespace Majunga.Libraries.Infrastructure.Repositories
{
    public interface IGenericHttpRepository<TEntity, TKey>
        where TEntity : IEntity<TKey>
    {
        public Task<ApiResponse> Get();
        public Task<ApiResponse> Get(TKey id);
        public Task<ApiResponse> Post(TEntity TEntity);
        public Task<ApiResponse> Put(TEntity TEntity);
        public Task<ApiResponse> Delete(TKey id);
    }
}
