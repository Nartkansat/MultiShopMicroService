
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using MultiShop.WebUI.Services.Interfaces;
using System.Net;
using System.Net.Http.Headers;

namespace MultiShop.WebUI.Handlers
{
    // Bu ResourceOwnerPasswordTokenHandler sınıfı, HTTP talepleri için bir DelegatingHandler görevi görür ve
    // kimlik doğrulaması için erişim tokenını (AccessToken) otomatik olarak ekler.
    // Eğer erişim tokenı geçersiz veya süresi dolmuşsa, yenileme tokenı (RefreshToken) ile yeniden bir token alır ve isteği tekrar gönderir.

    public class ResourceOwnerPasswordTokenHandler : DelegatingHandler
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IIdentityService _identityService;
        public ResourceOwnerPasswordTokenHandler(IHttpContextAccessor contextAccessor, IIdentityService identityService)
        {
            _contextAccessor = contextAccessor;
            _identityService = identityService;
        }

        // Her HTTP isteği gönderildiğinde çalıştırılır.
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // Mevcut access token'ı alır.
            var accessToken = await _contextAccessor.HttpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken);

            // Access token'ı Authorization başlığına ekler.
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            // İsteği gönderir ve yanıtı alır.
            var response = await base.SendAsync(request, cancellationToken);

            // Eğer yanıt `Unauthorized` dönerse (401), token yenileme işlemi yapılır.
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                // Token yenileme işlemi yapılır.
                var tokenResponse = await _identityService.GetRefreshToken();

                // Eğer yenileme başarılıysa, yeni token'ı tekrar Authorization başlığına ekleyip isteği tekrar gönderir.
                if (tokenResponse != null)
                {
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                    response = await base.SendAsync(request, cancellationToken);

                }
                else
                {
                    // Kullanıcıyı login sayfasına yönlendir
                    _contextAccessor.HttpContext.Response.Redirect("/Login/Index");
                }
            }

            // Yenileme işleminden sonra hala `Unauthorized` alınıyorsa hata mesajı eklenebilir.
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                // hata mesajı
                _contextAccessor.HttpContext.Response.Redirect("/Login/Index");
            }
            return response;
        }
    }
}
