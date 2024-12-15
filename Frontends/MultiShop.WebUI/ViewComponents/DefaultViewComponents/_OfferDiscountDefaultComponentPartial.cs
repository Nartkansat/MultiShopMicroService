using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.OfferDiscountDtos;
using MultiShop.WebUI.Services.CatalogServices.OfferDiscountServices;
using Newtonsoft.Json;

namespace MultiShop.WebUI.ViewComponents.DefaultViewComponents
{
    public class _OfferDiscountDefaultComponentPartial:ViewComponent
    {
        private readonly IOfferDiscountService _offerDiscountService;
        private readonly IHttpClientFactory _httpClientFactory;

        public _OfferDiscountDefaultComponentPartial(IHttpClientFactory httpClientFactory, IOfferDiscountService offerDiscountService)
        {
            _httpClientFactory = httpClientFactory;
            _offerDiscountService = offerDiscountService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var values = await _offerDiscountService.GetAllOfferDiscountAsync();

            return View(values);

            //var client = _httpClientFactory.CreateClient();
            //var responseMessage = await client.GetAsync("http://localhost:7070/api/OfferDiscounts");

            //if (responseMessage.IsSuccessStatusCode)
            //{
            //    var jsonData = await responseMessage.Content.ReadAsStringAsync(); // gelen veriyi string formatta oku
            //    var values = JsonConvert.DeserializeObject<List<ResultOfferDiscountDto>>(jsonData); //api'den gelen json formatındaki veriyi string e çevirmek için.

            //    return View(values);

            //}

            //return View();


        }

    }
}
