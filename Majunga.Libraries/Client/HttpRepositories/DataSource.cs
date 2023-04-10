using Majunga.Libraries.Infrastructure;
using System;
using System.Threading.Tasks;

namespace Majunga.Libraries.Client.HttpRepositories
{
    public class DataSource<TKey, TModel>
    {
        public Func<TModel, Task<ApiResponse>>? Post { get; set; }
        public Func<Task<ApiResponse>>? GetAll { get; set; }
        public Func<TKey, Task<ApiResponse>>? Get { get; set; }
        public Func<TModel, Task<ApiResponse>>? Put { get; set; }
        public Func<TKey, Task<ApiResponse>>? Delete { get; set; }
    }
}
