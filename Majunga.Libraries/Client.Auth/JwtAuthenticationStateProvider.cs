using Majunga.Libraries.SessionStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Majunga.Libraries.Auth
{
    public class JwtAuthenticationStateProvider : AuthenticationStateProvider
    {
        public const string TokenName = "access_token";
        private readonly IStorageRepository _sessionStorage;

        public JwtAuthenticationStateProvider(IStorageRepository sessionStorage)
        {
            _sessionStorage = sessionStorage;
        }

        public async Task<string?> Token() => await _sessionStorage.GetAsync<string>(TokenName);


        /// <summary>
        /// Login the user with a given JWT token.
        /// </summary>
        /// <param name="jwt">The JWT token.</param>
        public async Task Login(string jwt)
        {
            await _sessionStorage.SetAsync(TokenName, jwt);
            var token = new JwtSecurityToken(jwt);
            var principal = GetClaimsPrincipal(token!);
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(principal)));
        }

        /// <summary>
        /// Logout the current user.
        /// </summary>
        public async Task Logout()
        {
            await _sessionStorage.RemoveAsync(TokenName);
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(new ClaimsPrincipal())));
        }

        /// <inheritdoc/>
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var jwt = await _sessionStorage.GetAsync<string>(TokenName);
            var principal = new ClaimsPrincipal();

            if (string.IsNullOrEmpty(jwt) == false)
            {
                var token = new JwtSecurityToken(jwt);
                principal = GetClaimsPrincipal(token!);
            }

            return new AuthenticationState(principal);
        }

        private ClaimsPrincipal GetClaimsPrincipal(JwtSecurityToken jwtToken)
        {
            if (jwtToken == null) return new ClaimsPrincipal();

            var claimIdentity = new ClaimsIdentity(jwtToken.Claims, "jwt");
            var principal = new ClaimsPrincipal(new[] { claimIdentity });

            return principal;
        }

    }
}
