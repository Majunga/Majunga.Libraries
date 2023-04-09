using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace Majunga.Libraries.Auth
{
    public class JwtMessageHandler : DelegatingHandler
    {
        private readonly Uri _allowedBaseAddress;
        private readonly JwtAuthenticationStateProvider _loginStateService;

        public JwtMessageHandler(Uri allowedBaseAddress, JwtAuthenticationStateProvider loginStateService)
        {
            _allowedBaseAddress = allowedBaseAddress;
            _loginStateService = loginStateService;
        }

        protected override HttpResponseMessage Send(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return SendAsync(request, cancellationToken).Result;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var uri = request.RequestUri;
            var isSelfApiAccess = _allowedBaseAddress.IsBaseOf(uri!);

            if (isSelfApiAccess)
            {
                var token = await _loginStateService.Token();
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token ?? string.Empty);
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
