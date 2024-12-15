using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.FeatureSliderDtos;
using MultiShop.WebUI.Services.CatalogServices.FeatureServices;
using MultiShop.WebUI.Services.CatalogServices.FeatureSliderServices;
using Newtonsoft.Json;

namespace MultiShop.WebUI.ViewComponents.DefaultViewComponents
{
    public class _CarouselDefaultComponentPartial:ViewComponent
    {
        private readonly IFeatureSliderService _featureSliderService;
        private readonly IHttpClientFactory _httpClientFactory;

        public _CarouselDefaultComponentPartial(IHttpClientFactory httpClientFactory, IFeatureSliderService featureSliderService)
        {
            _httpClientFactory = httpClientFactory;
            _featureSliderService = featureSliderService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var values = await _featureSliderService.GetAllFeatureSliderAsync();

            return View(values);
            //var client = _httpClientFactory.CreateClient();
            //var responseMessage = await client.GetAsync("http://localhost:7070/api/Featuresliders");
            //if (responseMessage.IsSuccessStatusCode)
            //{
            //    var jsonData = await responseMessage.Content.ReadAsStringAsync();
            //    var values = JsonConvert.DeserializeObject<List<ResultFeatureSliderDto>>(jsonData);
            //    return View(values);
            //}
            //return View();
        }
    }
}
