using Majunga.Libraries.Auth;
using Majunga.Libraries.Infrastructure;
using Majunga.Libraries.SessionStorage;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Majunga.Libraries.Auth.HttpRepositories
{
    public abstract class HttpAuthRepositoryBase
    {
        protected readonly HttpClient HttpClient;
        protected readonly JsonSerializerOptions Options;
        protected readonly string BaseUriPath;
        protected readonly IStorageRepository SessionStorage;

        public HttpAuthRepositoryBase(HttpClient httpClient, string baseUriPath, IStorageRepository sessionStorage)
        {
            HttpClient = httpClient;
            Options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            BaseUriPath = baseUriPath;
            SessionStorage = sessionStorage;
        }

        public async virtual Task<HttpResponseMessage> SendAsync(HttpRequestMessage message)
        {
            if (await IsAboutToExpired())
            {
                var token = await Refresh();

                await SessionStorage.SetAsync(JwtAuthenticationStateProvider.TokenName, token);
            }

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

        private async Task<string> Refresh()
        {
            var response = await HttpClient.PostAsync($"api/account/refresh", null);

            var result = await GetApiResponse(response);

            if (result.IsSuccessStatusCode)
            {
                return result.GetData<string>() ?? string.Empty;
            }

            return string.Empty;
        }

        private async Task<bool> IsAboutToExpired()
        {
            var token = await SessionStorage.GetAsync<string>(JwtAuthenticationStateProvider.TokenName);

            if (string.IsNullOrWhiteSpace(token)) return false;


            var jwt = new JwtSecurityToken(token);
            var exp = jwt.ValidTo;
            var now = DateTime.UtcNow;
            var diff = exp - now;

            return diff.TotalMinutes < 10;
        }
    }
}
