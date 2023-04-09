using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace Majunga.Libraries.SessionStorage
{
    public class SessionStorage : IStorageRepository
    {
        private readonly IJSRuntime _jSRuntime;

        public SessionStorage(IJSRuntime jSRuntime)
        {
            _jSRuntime = jSRuntime;
        }

        public async Task<T?> GetAsync<T>(string key)
        {
            try
            {
                return await _jSRuntime.InvokeAsync<T>("getSessionItemAsync", key);
            }
            catch (Exception)
            {
                await _jSRuntime.InvokeVoidAsync("console.error", "Failed to get session, this is usually due to not including '_content/Majunga.Libraries.SessionStorage/js/SessionStorage.js' at the bottom of your index.html");
                throw;
            }
        }

        public async Task SetAsync<T>(string key, T value)
        {
            try
            {
                await _jSRuntime.InvokeVoidAsync("setSessionItemAsync", key, value);
            }
            catch (Exception)
            {
                await _jSRuntime.InvokeVoidAsync("console.error", "Failed to set session, this is usually due to not including '_content/Majunga.Libraries.SessionStorage/js/SessionStorage.js' at the bottom of your index.html");
                throw;
            }
        }

        public async Task RemoveAsync(string key)
        {
            try
            {
                await _jSRuntime.InvokeVoidAsync("removeSessionItemAsync", key);
            }
            catch (Exception)
            {
                await _jSRuntime.InvokeVoidAsync("console.error", "Failed to remove session, this is usually due to not including '_content/Majunga.Libraries.SessionStorage/js/SessionStorage.js' at the bottom of your index.html");
                throw;
            }
        }

        public async Task ClearAsync()
        {
            try
            {
                await _jSRuntime.InvokeVoidAsync("clearSessionAsync");
            }
            catch (Exception)
            {
                await _jSRuntime.InvokeVoidAsync("console.error", "Failed to clear session, this is usually due to not including '_content/Majunga.Libraries.SessionStorage/js/SessionStorage.js' at the bottom of your index.html");
                throw;
            }
        }
    }
}
