
using MultiShop.WebUI.Services.Interfaces;
using System.Net;
using System.Net.Http.Headers;

namespace MultiShop.WebUI.Handlers
{
    public class ClientCredentialTokenHandler:DelegatingHandler
    {
        private readonly IClientCredentialTokenService _clientCredentialTokenService;

        public ClientCredentialTokenHandler(IClientCredentialTokenService clientCredentialTokenService)
        {
            _clientCredentialTokenService = clientCredentialTokenService;
        }

        // Her HTTP isteği gönderildiğinde çalıştırılır.
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // Client credential token alır ve `Authorization` başlığına ekler.
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer",await _clientCredentialTokenService.GetToken());

            // İsteği gönderir ve yanıtı alır.
            var response = await base.SendAsync(request, cancellationToken);

            // Eğer yanıt `Unauthorized` dönerse (401), hata işlemi yapılabilir.
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                //hata mesajı
            }
            return response;

        }
    }
}
