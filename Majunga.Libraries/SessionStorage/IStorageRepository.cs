using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace Majunga.Libraries.SessionStorage
{
    public interface IStorageRepository
    {
        Task<T?> GetAsync<T>(string key);
        Task SetAsync<T>(string key, T value);
        Task RemoveAsync(string key);
        Task ClearAsync();
    }
}