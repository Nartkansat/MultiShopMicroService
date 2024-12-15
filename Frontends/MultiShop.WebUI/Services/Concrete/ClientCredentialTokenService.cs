using IdentityModel.AspNetCore.AccessTokenManagement;
using IdentityModel.Client;
using Microsoft.Extensions.Options;
using MultiShop.DtoLayer.IdentityDtos.LoginDtos;
using MultiShop.WebUI.Services.Interfaces;
using MultiShop.WebUI.Settings;

namespace MultiShop.WebUI.Services.Concrete
{
    public class ClientCredentialTokenService : IClientCredentialTokenService
    {
        private readonly ServiceApiSettings _serviceApiSettings;
        private readonly HttpClient _httpClient;
        private readonly IClientAccessTokenCache _clientAccessTokenCache;
        private readonly ClientSettings _clientSettings;

        public ClientCredentialTokenService(IOptions<ServiceApiSettings> serviceApiSettings, HttpClient httpClient, IClientAccessTokenCache clientAccessTokenCache, IOptions<ClientSettings> clientSettings)
        {
            _serviceApiSettings = serviceApiSettings.Value;
            _httpClient = httpClient;
            _clientAccessTokenCache = clientAccessTokenCache;
            _clientSettings = clientSettings.Value;
        }

        public async Task<string> GetToken()
        {
            // Önce mevcut bir tokenın cache'de olup olmadığını kontrol eder
            var currentToken = await _clientAccessTokenCache.GetAsync("multishoptoken");

            // Eğer cache'de token varsa, mevcut access tokenını döner
            if (currentToken != null)
            {
                return currentToken.AccessToken;
            }

            // IdentityServer üzerinde keşif işlemi yaparak token endpoint bilgisi alınır
            var discoveryEndPoint = await _httpClient.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest
            {
                // Identity Server adresi
                Address = _serviceApiSettings.IdentityServerUrl, 
                Policy = new DiscoveryPolicy
                {
                    RequireHttps = false,
                }
            });

            // Client credentials grant request ayarlanıyor
            var clientCredentialTokenRequest = new ClientCredentialsTokenRequest
            {
                ClientId = _clientSettings.MultiShopVisitorClient.ClientId,
                ClientSecret = _clientSettings.MultiShopVisitorClient.ClientSecret,
                Address = discoveryEndPoint.TokenEndpoint
            };

            // İstek yapılarak yeni token alınıyor
            var newToken = await _httpClient.RequestClientCredentialsTokenAsync(clientCredentialTokenRequest);
            // Alınan yeni token, cache'e ekleniyor ve geçerlilik süresi ile ayarlanıyor
            await _clientAccessTokenCache.SetAsync("multishoptoken", newToken.AccessToken, newToken.ExpiresIn);

            return newToken.AccessToken;
        }
    }
}
