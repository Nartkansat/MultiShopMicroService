using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.SpecialOfferDtos;
using MultiShop.WebUI.Services.CatalogServices.SpecialOfferServices;
using Newtonsoft.Json;

namespace MultiShop.WebUI.ViewComponents.DefaultViewComponents
{
    public class _SpecialOfferComponentPartial:ViewComponent
    {
        private readonly ISpecialOfferService _specialOfferService;
        private readonly IHttpClientFactory _httpClientFactory;

        public _SpecialOfferComponentPartial(IHttpClientFactory httpClientFactory, ISpecialOfferService specialOfferService)
        {
            _httpClientFactory = httpClientFactory;
            _specialOfferService = specialOfferService;
        }


        public async Task<IViewComponentResult> InvokeAsync()
        {

            var values = await _specialOfferService.GetAllSpecialOfferAsync();

            return View(values);

            //var client = _httpClientFactory.CreateClient();
            //var responseMessage = await client.GetAsync("http://localhost:7070/api/SpecialOffers");

            //if (responseMessage.IsSuccessStatusCode)
            //{
            //    var jsonData = await responseMessage.Content.ReadAsStringAsync(); // gelen veriyi string formatta oku
            //    var values = JsonConvert.DeserializeObject<List<ResultSpecialOfferDto>>(jsonData); //api'den gelen json formatındaki veriyi string e çevirmek için.

            //    return View(values);

            //}

            //return View();
        }
    }
}
