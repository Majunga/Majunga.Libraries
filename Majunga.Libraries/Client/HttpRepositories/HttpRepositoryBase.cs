using Majunga.Libraries.Infrastructure;
using Majunga.Libraries.Infrastructure.Entities;
using Majunga.Libraries.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Majunga.Libraries.Client.HttpRepositories
{
    public abstract class HttpRepositoryBase
    {
        protected readonly HttpClient HttpClient;
        protected readonly JsonSerializerOptions Options;
        protected readonly string BaseUriPath;

        public HttpRepositoryBase(HttpClient httpClient, string baseUriPath)
        {
            HttpClient = httpClient;
            Options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            BaseUriPath = baseUriPath;
        }

        public async virtual Task<HttpResponseMessage> SendAsync(HttpRequestMessage message)
        {
            var response = await HttpClient.SendAsync(message);
            return response;
        }

        protected abstract void HandleFailedRequests(ApiResponse apiResponse);

        protected virtual async Task<ApiResponse> GetApiResponse(HttpResponseMessage response)
        {
            var reponseContent = await response.Content.ReadAsStringAsync();

            if (string.IsNullOrWhiteSpace(reponseContent) == false && response.Content.Headers.ContentType?.MediaType == "application/json")
            {
                return JsonSerializer.Deserialize<ApiResponse>(reponseContent, Options) ?? ApiResponse.Create(response.StatusCode);
            }

            return ApiResponse.Create(response.StatusCode);
        }
        
        protected virtual StringContent CreateHttpContent<T>(T TEntity)
        {
            var json = JsonSerializer.Serialize(TEntity, Options);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            return content;
        }
    }
}
