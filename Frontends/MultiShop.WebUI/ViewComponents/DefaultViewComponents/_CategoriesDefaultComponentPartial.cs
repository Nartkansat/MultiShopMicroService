using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.CategoryDtos;
using MultiShop.WebUI.Services.CatalogServices.CategoryServices;
using Newtonsoft.Json;

namespace MultiShop.WebUI.ViewComponents.DefaultViewComponents
{
    public class _CategoriesDefaultComponentPartial:ViewComponent
    {
        private readonly ICategoryService _categoryService;
        private readonly IHttpClientFactory _httpClientFactory;

        public _CategoriesDefaultComponentPartial(IHttpClientFactory httpClientFactory, ICategoryService categoryService)
        {
            _httpClientFactory = httpClientFactory;
            _categoryService = categoryService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var values = await _categoryService.GetAllCategoryAsync();

            return View(values);

            //var client = _httpClientFactory.CreateClient();
            //var responseMessage = await client.GetAsync("http://localhost:7070/api/Categories");

            //if (responseMessage.IsSuccessStatusCode)
            //{
            //    var jsonData = await responseMessage.Content.ReadAsStringAsync(); // gelen veriyi string formatta oku
            //    var values = JsonConvert.DeserializeObject<List<ResultCategoryDto>>(jsonData); //api'den gelen json formatındaki veriyi string e çevirmek için.

            //    return View(values);

            //}

            //return View();


        }

      
    }
}
