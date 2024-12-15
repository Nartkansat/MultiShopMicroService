using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.AboutDtos;
using MultiShop.WebUI.Services.CatalogServices.AboutServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;

namespace MultiShop.WebUI.ViewComponents.UILayoutViewComponents
{
    public class _FooterUILayoutComponentPartial:ViewComponent
    {
        private readonly IAboutService _aboutService;

        private readonly IHttpClientFactory _httpClientFactory;

        public _FooterUILayoutComponentPartial(IHttpClientFactory httpClientFactory, IAboutService aboutService)
        {
            _httpClientFactory = httpClientFactory;
            _aboutService = aboutService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var values = await _aboutService.GetAllAboutAsync();

            return View(values);

            //string token = "";
            //using (var httpClient = new HttpClient())
            //{
            //    var request = new HttpRequestMessage
            //    {
            //        RequestUri = new Uri("http://localhost:5001/connect/token"),
            //        Method = HttpMethod.Post,
            //        Content = new FormUrlEncodedContent(new Dictionary<string, string>
            //        {
            //            {"client_id","MultiShopVisitorID" },
            //            {"client_secret","multishopsecret" },
            //            {"grant_type","client_credentials" }
            //        })
            //    };

            //    using (var response = await httpClient.SendAsync(request))
            //    {
            //        if (response.IsSuccessStatusCode)
            //        {
            //            var content = await response.Content.ReadAsStringAsync();
            //            var tokenResponse = JObject.Parse(content);
            //            token = tokenResponse["access_token"].ToString();
            //        }
            //    }
            //}

            //var client = _httpClientFactory.CreateClient();
            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            //var responseMessage = await client.GetAsync("http://localhost:7070/api/Abouts");
            //if (responseMessage.IsSuccessStatusCode)
            //{
            //    var jsonData = await responseMessage.Content.ReadAsStringAsync();
            //    var values = JsonConvert.DeserializeObject<List<ResultAboutDto>>(jsonData);
            //    return View(values);
            //}
            //return View();

        }
    }
}
