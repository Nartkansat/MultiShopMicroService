using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.BrandDtos;
using MultiShop.WebUI.Services.CatalogServices.BrandServices;
using Newtonsoft.Json;

namespace MultiShop.WebUI.ViewComponents.DefaultViewComponents
{
    public class _VendorDefaultComponentPartial:ViewComponent
    {
        private readonly IBrandService _brandService;
        private readonly IHttpClientFactory _httpClientFactory;

        public _VendorDefaultComponentPartial(IHttpClientFactory httpClientFactory, IBrandService brandService)
        {
            _httpClientFactory = httpClientFactory;
            _brandService = brandService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var values = await _brandService.GetAllBrandAsync();

            return View(values);

            //var client = _httpClientFactory.CreateClient();
            //var responseMessage = await client.GetAsync("http://localhost:7070/api/Brands");
            //if (responseMessage.IsSuccessStatusCode)
            //{
            //    var jsonData = await responseMessage.Content.ReadAsStringAsync();
            //    var values = JsonConvert.DeserializeObject<List<ResultBrandDto>>(jsonData);
            //    return View(values);
            //}
            //return View();
        }

    }
}
