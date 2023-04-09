using Majunga.Libraries.Infrastructure;
using System;
using System.Threading.Tasks;

namespace Majunga.Libraries.Client.HttpRepositories
{
    public class DataSource<TKey, TModel>
    {
        public Func<TModel, Task<ApiResponse>>? Create { get; set; }
        public Func<Task<ApiResponse>>? ReadAll { get; set; }
        public Func<TKey, Task<ApiResponse>>? Read { get; set; }
        public Func<TKey, TModel, Task<ApiResponse>>? Update { get; set; }
        public Func<TKey, Task<ApiResponse>>? Delete { get; set; }
    }
}
