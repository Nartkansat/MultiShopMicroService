using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.CategoryDtos;
using MultiShop.WebUI.Services.CatalogServices.CategoryServices;
using Newtonsoft.Json;
using System.Text;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Category")]
    public class CategoryController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ICategoryService _categoryService;

        public CategoryController(IHttpClientFactory httpClientFactory, ICategoryService categoryService)
        {
            _httpClientFactory = httpClientFactory;
            _categoryService = categoryService;
        }

        void CategoryViewBagList()
        {
            ViewBag.v0 = "Kategori İşlemleri";
            ViewBag.v1 = "Anasayfa";
            ViewBag.v2 = "Kategoriler";
            ViewBag.v3 = "Kategori Listesi";
        }


        [Route("Index")]
        public async Task<IActionResult> Index()
        {

            CategoryViewBagList();

            #region Eski Erişim Yöntemi
            //var client = _httpClientFactory.CreateClient();
            //var responseMessage = await client.GetAsync("http://localhost:7070/api/Categories");

            //if (responseMessage.IsSuccessStatusCode)
            //{
            //    var jsonData = await responseMessage.Content.ReadAsStringAsync(); // gelen veriyi string formatta oku
            //    var values = JsonConvert.DeserializeObject<List<ResultCategoryDto>>(jsonData); //api'den gelen json formatındaki veriyi string e çevirmek için.

            //    return View(values);

            //}
            #endregion

            var values = await _categoryService.GetAllCategoryAsync();

            return View(values);
        }

        [HttpGet]
        [Route("CreateCategory")]
        public IActionResult CreateCategory()
        {
            CategoryViewBagList();

            return View();
        }


        [HttpPost]
        [Route("CreateCategory")]
        public async Task<IActionResult> CreateCategory(CreateCategoryDto createCategoryDto)
        {
            #region Eski Create İşlevi

            //var client = _httpClientFactory.CreateClient();
            //var jsonData = JsonConvert.SerializeObject(createCategoryDto); // json formatına dönüştürmek için serialize
            //StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json"); //content olarak atadık, neyi kontrol atadık,hangi dil, ve hangi mediator
            //var responseMessage = await client.PostAsync("http://localhost:7070/api/Categories", stringContent);
            //if (responseMessage.IsSuccessStatusCode)
            //{
            //    return RedirectToAction("Index", "Category", new { area = "Admin" });
            //}

            #endregion

            await _categoryService.CreateCategoryAsync(createCategoryDto);
            return RedirectToAction("Index", "Category", new { area = "Admin" });

        }


        // [HttpDelete] olmayacak burada. 
        [Route("DeleteCategory/{id}")]
        public async Task<IActionResult> DeleteCategory(string id)
        {
            await _categoryService.DeleteCategoryAsync(id);

            return RedirectToAction("Index", "Category", new { area = "Admin" });


            #region Eski Delete İşlevi
            //var client = _httpClientFactory.CreateClient();
            //var responseMessage = await client.DeleteAsync("http://localhost:7070/api/Categories?id=" + id);
            //if (responseMessage.IsSuccessStatusCode)
            //{
            //    return RedirectToAction("Index", "Category", new { area = "Admin" });

            //}

            #endregion

        }


        [Route("UpdateCategory/{id}")]
        [HttpGet]
        public async Task<IActionResult> UpdateCategory(string id)
        {
            CategoryViewBagList();

            var values = await _categoryService.GetByIdCategoryAsync(id);
            return View(values);


            #region Eski Update İşlevi
            //var client = _httpClientFactory.CreateClient();
            //var responseMessage = await client.GetAsync("http://localhost:7070/api/Categories/" + id);

            //if (responseMessage.IsSuccessStatusCode)
            //{
            //    var jsonData = await responseMessage.Content.ReadAsStringAsync();
            //    var values = JsonConvert.DeserializeObject<UpdateCategoryDto>(jsonData);
            //    return View(values);
            //}
            #endregion


        }


        [Route("UpdateCategory/{id}")]
        [HttpPost]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryDto updateCategoryDto)
        {
            await _categoryService.UpdateCategoryAsync(updateCategoryDto);

            return RedirectToAction("Index", "Category", new { area = "Admin" });


            #region Eski Update Post İşlevi

            //var client = _httpClientFactory.CreateClient();
            //var jsonData = JsonConvert.SerializeObject(updateCategoryDto);
            //StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

            //var responseMessage = await client.PutAsync("http://localhost:7070/api/Categories/", stringContent);

            //if (responseMessage.IsSuccessStatusCode)
            //{
            //    return RedirectToAction("Index", "Category", new { area = "Admin" });
            //}

            #endregion

        }


    }

}
