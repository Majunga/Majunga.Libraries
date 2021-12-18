using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Majunga.Libraries.Client.HttpRepositories
{
    public interface IGenericRepository<T, TKey>
        where T : class
    {
        public Task<DataResult<List<T>>> Get();
        public Task<DataResult<T>> Get(TKey id);
        public Task<DataResult<T>> Post(T T);
        public Task<Result> Put(TKey id, T T);
        public Task<Result> Delete(TKey id);
    }

    public class GenericRepository<T, TKey> : IGenericRepository<T, TKey>
        where T : class
    {
        protected readonly HttpClient HttpClient;
        protected readonly JsonSerializerOptions Options;
        protected readonly string UrlPath;

        public GenericRepository(HttpClient httpClient, string controller)
        {
            HttpClient = httpClient;
            Options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            UrlPath = $"{controller}";
        }

        /// <summary>
        /// Get List of Data from API
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="HttpRequestException"></exception>
        /// <exception cref="JsonException"></exception>
        /// <exception cref="NotSupportedException"></exception>
        public async virtual Task<DataResult<List<T>>> Get()
        {
            var response = await HttpClient.GetAsync(UrlPath);
            var responseContent = await response.Content.ReadAsStringAsync();

            var result = response.IsSuccessStatusCode ? JsonSerializer.Deserialize<List<T>>(responseContent, Options) : new List<T>();
            return new DataResult<List<T>>(response.IsSuccessStatusCode, response.StatusCode, result);
        }

        /// <summary>
        /// Get Data from API
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="HttpRequestException"></exception>
        /// <exception cref="JsonException"></exception>
        /// <exception cref="NotSupportedException"></exception>
        public async virtual Task<DataResult<T>> Get(TKey id)
        {
            var response = await HttpClient.GetAsync($"{UrlPath}/{id}");
            var responseContent = await response.Content.ReadAsStringAsync();

            var result = response.IsSuccessStatusCode ? JsonSerializer.Deserialize<T>(responseContent, Options) : null;
            return new DataResult<T>(response.IsSuccessStatusCode, response.StatusCode, result);
        }

        /// <summary>
        /// Post Data to API
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="HttpRequestException"></exception>
        /// <exception cref="JsonException"></exception>
        /// <exception cref="NotSupportedException"></exception>
        public async virtual Task<DataResult<T>> Post(T T)
        {
            var json = JsonSerializer.Serialize(T, Options);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await HttpClient.PostAsync($"{UrlPath}", content);
            var responseContent = await response.Content.ReadAsStringAsync();

            var result = response.IsSuccessStatusCode ? JsonSerializer.Deserialize<T>(responseContent, Options) : null;

            return new DataResult<T>(response.IsSuccessStatusCode, response.StatusCode, result);
        }

        /// <summary>
        /// Put Data to API
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="HttpRequestException"></exception>
        /// <exception cref="NotSupportedException"></exception>
        public async virtual Task<Result> Put(TKey id, T T)
        {
            var json = JsonSerializer.Serialize(T, Options);
            var content = new StringContent(json);
            var response = await HttpClient.PutAsync($"{UrlPath}/{id}", content);

            return new Result(response.IsSuccessStatusCode, response.StatusCode);
        }

        /// <summary>
        /// Delete Data from API
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="HttpRequestException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public async virtual Task<Result> Delete(TKey id)
        {
            var response = await HttpClient.DeleteAsync($"{UrlPath}/{id}");

            return new Result(response.IsSuccessStatusCode, response.StatusCode);
        }
    }
}
