using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.ProductDtos;
using MultiShop.WebUI.Services.CatalogServices.ProductServices;
using Newtonsoft.Json;

namespace MultiShop.WebUI.ViewComponents.DefaultViewComponents
{
    public class _FeatureProductDefaultComponentPartial:ViewComponent
    {
        private readonly IProductService _productService;
        private readonly IHttpClientFactory _httpClientFactory;
        public _FeatureProductDefaultComponentPartial(IHttpClientFactory httpClientFactory, IProductService productService)
        {
            _httpClientFactory = httpClientFactory;
            _productService = productService;
        }


        public async Task<IViewComponentResult> InvokeAsync()
        {
            var values = await _productService.GetAllProductAsync();

            return View(values);

            //var client = _httpClientFactory.CreateClient();
            //var responseMessage = await client.GetAsync("http://localhost:7070/api/Products");

            //if (responseMessage.IsSuccessStatusCode)
            //{
            //    var jsonData = await responseMessage.Content.ReadAsStringAsync(); // gelen veriyi string formatta oku
            //    var values = JsonConvert.DeserializeObject<List<ResultProductDto>>(jsonData); //api'den gelen json formatındaki veriyi string e çevirmek için.

            //    return View(values);

            //}

            //return View();
        }
    }
}
