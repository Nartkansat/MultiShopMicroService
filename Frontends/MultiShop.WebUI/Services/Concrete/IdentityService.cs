using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using MultiShop.DtoLayer.IdentityDtos.LoginDtos;
using MultiShop.WebUI.Services.Interfaces;
using MultiShop.WebUI.Settings;
using System.Security.Claims;

namespace MultiShop.WebUI.Services.Concrete
{
    public class IdentityService : IIdentityService
    {
        // Kimlik doğrulama için kullanılan HttpClient nesnesi
        private readonly HttpClient _httpClient;

        // HTTP bağlamını (context) almak için kullanılan IHttpContextAccessor
        private readonly IHttpContextAccessor _contextAccessor;

        // OAuth 2.0 client ayarlarını tutar
        private readonly ClientSettings _clientSettings;

        // Servis API ayarlarını tutar (IdentityServer URL vb.)
        private readonly ServiceApiSettings _serviceApiSettings;

        public IdentityService(HttpClient httpClient, IHttpContextAccessor contextAccessor, IOptions<ClientSettings> clientSettings, IOptions<ServiceApiSettings> serviceApiSettings)
        {
            _httpClient = httpClient;
            _contextAccessor = contextAccessor;
            _clientSettings = clientSettings.Value;
            _serviceApiSettings = serviceApiSettings.Value;
        }


        public async Task<bool> SignIn(SignInDto signInDto)
        {
            // IdentityServer'dan token endpoint bilgilerini almak için discovery document çağrısı
            var discoveryEndPoint = await _httpClient.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest
            {
                Address = _serviceApiSettings.IdentityServerUrl,
                Policy = new DiscoveryPolicy
                {
                    RequireHttps = false,
                }
            });

            // Kullanıcı adı ve şifre ile token talebi oluşturma
            var passwordTokenRequest = new PasswordTokenRequest
            {
                ClientId = _clientSettings.MultiShopManagerClient.ClientId,
                ClientSecret = _clientSettings.MultiShopManagerClient.ClientSecret,
                UserName = signInDto.Username,
                Password = signInDto.Password,
                Address = discoveryEndPoint.TokenEndpoint
            };

            // Password grant ile token talebi
            var token = await _httpClient.RequestPasswordTokenAsync(passwordTokenRequest);

            // Kullanıcı bilgilerini almak için UserInfo endpoint talebi
            var userInfoRequest = new UserInfoRequest
            {
                Token = token.AccessToken,
                Address = discoveryEndPoint.UserInfoEndpoint,
            };

            // Kullanıcı bilgileri alınıyor
            var userValues = await _httpClient.GetUserInfoAsync(userInfoRequest);

            // ClaimsIdentity oluşturma: kimlik doğrulama bilgilerinin saklanması için kullanılır
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(userValues.Claims, CookieAuthenticationDefaults.AuthenticationScheme, "name", "role");

            // Kullanıcı oturumu açma bilgilerini içeren ClaimsPrincipal oluşturma
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            // Oturum özellikleri ayarlanıyor
            var authenticationProperties = new AuthenticationProperties();

            // Token bilgilerini AuthenticationProperties'e ekleme
            authenticationProperties.StoreTokens(new List<AuthenticationToken>()
            {
                new AuthenticationToken
                {
                    Name = OpenIdConnectParameterNames.AccessToken, // Erişim tokenı
                    Value = token.AccessToken,
                },
                new AuthenticationToken
                {
                    Name = OpenIdConnectParameterNames.RefreshToken, // Yenileme tokenı
                    Value = token.RefreshToken,
                },
                new AuthenticationToken
                {
                    Name = OpenIdConnectParameterNames.ExpiresIn,
                    Value = DateTime.Now.AddSeconds(token.ExpiresIn).ToString() // Token geçerlilik süresi
                }
            });

            // Oturum kalıcı değil
            authenticationProperties.IsPersistent = false;

            await _contextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal, authenticationProperties);

            return true;
        }


        public async Task<bool> GetRefreshToken()
        {
            try
            {
                var discoveryEndPoint = await _httpClient.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest
                {
                    Address = _serviceApiSettings.IdentityServerUrl,
                    Policy = new DiscoveryPolicy
                    {
                        RequireHttps = false,
                    }
                });

                var refreshToken = await _contextAccessor.HttpContext.GetTokenAsync(OpenIdConnectParameterNames.RefreshToken);

                RefreshTokenRequest refreshTokenRequest = new()
                {
                    ClientId = _clientSettings.MultiShopManagerClient.ClientId,
                    ClientSecret = _clientSettings.MultiShopManagerClient.ClientSecret,
                    RefreshToken = refreshToken,
                    Address = discoveryEndPoint.TokenEndpoint
                };

                var token = await _httpClient.RequestRefreshTokenAsync(refreshTokenRequest);

                // Eğer token null ise kullanıcıyı login sayfasına yönlendir
                if (token == null || token.IsError)
                {
                    _contextAccessor.HttpContext.Response.Redirect("/Login/Index");
                    await Task.CompletedTask;
                    return false;

                }

                var authenticationToken = new List<AuthenticationToken>()
                {
                    new AuthenticationToken
                    {
                        Name = OpenIdConnectParameterNames.AccessToken,
                        Value = token.AccessToken,
                    },
                    new AuthenticationToken
                    {
                        Name = OpenIdConnectParameterNames.RefreshToken,
                        Value = token.RefreshToken,
                    },
                    new AuthenticationToken
                    {
                        Name = OpenIdConnectParameterNames.ExpiresIn,
                        Value = DateTime.Now.AddSeconds(token.ExpiresIn).ToString()
                    }
                };

                var result = await _contextAccessor.HttpContext.AuthenticateAsync();

                // Yeni tokenları oturum özelliklerine ekler ve günceller
                var properties = result.Properties;
                properties.StoreTokens(authenticationToken);
                await _contextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, result.Principal, properties);

                return true;
            }
            catch (Exception ex)
            {
                // Hata durumunda login sayfasına yönlendir
                _contextAccessor.HttpContext.Response.Redirect("/Login/Index");
                await Task.CompletedTask;
                return false;

            }
        }

    }
}
