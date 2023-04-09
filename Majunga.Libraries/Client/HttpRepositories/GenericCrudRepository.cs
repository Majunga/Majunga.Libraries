﻿using Majunga.Libraries.Infrastructure;
using Majunga.Libraries.Infrastructure.Entities;
using Majunga.Libraries.Infrastructure.Repositories;
using System.Net.Http;
using System.Threading.Tasks;

namespace Majunga.Libraries.Client.HttpRepositories
{
    public abstract class GenericCrudRepository<TEntity, TKey> : HttpRepositoryBase, IGenericHttpRepository<TEntity, TKey>
        where TEntity : IEntity<TKey>
    {
        protected GenericCrudRepository(HttpClient httpClient, string baseUriPath) : base(httpClient, baseUriPath)
        {
        }

        public async virtual Task<ApiResponse> Get()
        {
            var response = await SendAsync(new HttpRequestMessage(HttpMethod.Get, BaseUriPath));
            var result = await GetApiResponse(response);
            return result;
        }

        public async virtual Task<ApiResponse> Get(TKey id)
        {
            var response = await SendAsync(new HttpRequestMessage(HttpMethod.Get, $"{BaseUriPath}/{id}"));
            var result = await GetApiResponse(response);
            return result;
        }

        public async virtual Task<ApiResponse> Post(TEntity TEntity)
        {
            var content = CreateHttpContent(TEntity);
            var request = new HttpRequestMessage(HttpMethod.Post, BaseUriPath)
            {
                Content = content
            };
            var response = await SendAsync(request);

            var result = await GetApiResponse(response);
            return result;
        }

        public async virtual Task<ApiResponse> Put(TEntity TEntity)
        {
            var content = CreateHttpContent(TEntity);
            var request = new HttpRequestMessage(HttpMethod.Put, $"{BaseUriPath}/{TEntity.Id}")
            {
                Content = content
            };

            var response = await SendAsync(request);

            var result = await GetApiResponse(response);
            return result;
        }

        public async virtual Task<ApiResponse> Delete(TKey id)
        {
            var response = await SendAsync(new HttpRequestMessage(HttpMethod.Delete, $"{BaseUriPath}/{id}"));
            var result = await GetApiResponse(response);
            return result;
        }
    }
}
