using IdentityModel.AspNetCore.AccessTokenManagement;
using IdentityModel.Client;
using Microsoft.Extensions.Options;

namespace WatchStoreApp.UI.Services
{
    public class ClientCredentialTokenService : IClientCredentialTokenService
    {
        private readonly ServiceApiSettings _serviceApiSettings;
        private readonly ClientSettings _clientSettings;
        private readonly HttpClient _client;
        private readonly IClientAccessTokenCache _clientAccessTokenCache;

        public ClientCredentialTokenService(IOptions<ServiceApiSettings> serviceApiSettings, IOptions<ClientSettings> clientSettings, IClientAccessTokenCache clientAccessTokenCache, HttpClient client)
        {
            _serviceApiSettings = serviceApiSettings.Value;
            _clientSettings = clientSettings.Value;
            _clientAccessTokenCache = clientAccessTokenCache;
            _client = client;
        }

        public async Task<string> GetToken()
        {
            var currentToken = await _clientAccessTokenCache.GetAsync("WebClientToken", default);
            if (currentToken != null)
            {
                return currentToken.AccessToken;
            }
            var disco = await _client.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest()
            {
                Address = _serviceApiSettings.IdentityBaseUrl,
                Policy = new DiscoveryPolicy { RequireHttps = false }
            });
            if (disco.IsError)
            {
                throw disco.Exception;
            }
            var cleintCredentialTokenRequest = new ClientCredentialsTokenRequest
            {
                ClientId = _clientSettings.WebClient.ClientId,
                ClientSecret = _clientSettings.WebClient.ClientSecret,
                Address = disco.TokenEndpoint
            };

            var newToken = await _client.RequestClientCredentialsTokenAsync(cleintCredentialTokenRequest);
            if (newToken.IsError)
            {
                throw newToken.Exception;
            }
            await _clientAccessTokenCache.SetAsync("WebClientToken", newToken.AccessToken, newToken.ExpiresIn, default);
            return newToken.AccessToken;
        }
    }
}
