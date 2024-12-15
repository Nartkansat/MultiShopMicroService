using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MultiShop.DtoLayer.CatalogDtos.CategoryDtos;
using MultiShop.DtoLayer.CatalogDtos.ProductDtos;
using MultiShop.WebUI.Services.CatalogServices.CategoryServices;
using MultiShop.WebUI.Services.CatalogServices.ProductServices;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{

    [Area("Admin")]
    [Route("Admin/Product")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public ProductController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }
        void ProductViewBagList()
        {
            ViewBag.v0 = "Ürün İşlemleri";
            ViewBag.v1 = "Anasayfa";
            ViewBag.v2 = "Ürünler";
            ViewBag.v3 = "Ürün Listesi";
        }


        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            ProductViewBagList();

            var values = await _productService.GetAllProductAsync();

            return View(values);
        }

        [Route("ProductListWithCategory")]
        public async Task<IActionResult> ProductListWithCategory()
        {
            ProductViewBagList();

            return View();

            #region Eski Kod
            //var client = _httpClientFactory.CreateClient();
            //var responseMessage = await client.GetAsync("http://localhost:7070/api/Products/ProductListWithCategory");

            //if (responseMessage.IsSuccessStatusCode)
            //{
            //    var jsonData = await responseMessage.Content.ReadAsStringAsync(); // gelen veriyi string formatta oku
            //    var values = JsonConvert.DeserializeObject<List<ResultProductWithCategoryDto>>(jsonData); //api'den gelen json formatındaki veriyi string e çevirmek için.

            //    return View(values);

            //}

            //return View();

            #endregion


        }


        [HttpGet]
        [Route("CreateProduct")]
        public async Task<IActionResult> CreateProduct()
        {

            ProductViewBagList();

            var values = await _categoryService.GetAllCategoryAsync();
            List<SelectListItem> categoryValues = (from x in values
                                                   select new SelectListItem
                                                   {
                                                       Text = x.CategoryName,
                                                       Value = x.CategoryID
                                                   }).ToList();
            ViewBag.CategoryValues = categoryValues;
            return View();

            #region eski kod

            //var client = _httpClientFactory.CreateClient();
            //var responseMessage = await client.GetAsync("http://localhost:7070/api/Categories");
            //var jsonData = await responseMessage.Content.ReadAsStringAsync();
            //var values = JsonConvert.DeserializeObject<List<ResultCategoryDto>>(jsonData);

            //// uı tarafına ürün eklerken kategorileri taşımak için. 
            //List<SelectListItem> categoryValues = (from x in values
            //                                       select new SelectListItem
            //                                       {
            //                                           Text = x.CategoryName,
            //                                           Value = x.CategoryID
            //                                       }).ToList();

            //ViewBag.CategoryValues = categoryValues;

            //return View();

            #endregion


        }

        [HttpPost]
        [Route("CreateProduct")]
        public async Task<IActionResult> CreateProduct(CreateProductDto createProductDto)
        {
            await _productService.CreateProductAsync(createProductDto);
            return RedirectToAction("Index", "Product", new { area = "Admin" });
        }

        [Route("DeleteProduct/{id}")]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            await _productService.DeleteProductAsync(id);

            return RedirectToAction("Index", "Product", new { area = "Admin" });
        }


        [Route("UpdateProduct/{id}")]
        [HttpGet]
        public async Task<IActionResult> UpdateProduct(string id)
        {
            ProductViewBagList();

            var values = await _categoryService.GetAllCategoryAsync();
            List<SelectListItem> categoryValues = (from x in values
                                                   select new SelectListItem
                                                   {
                                                       Text = x.CategoryName,
                                                       Value = x.CategoryID
                                                   }).ToList();
            ViewBag.CategoryValues = categoryValues;

            var productValues = await _productService.GetByIdProductAsync(id);
            return View(productValues);

            #region Eski Kod
            //var client1 = _httpClientFactory.CreateClient();
            //var responseMessage1 = await client1.GetAsync("http://localhost:7070/api/Categories");
            //var jsonData1 = await responseMessage1.Content.ReadAsStringAsync();
            //var values1 = JsonConvert.DeserializeObject<List<ResultCategoryDto>>(jsonData1);

            //// uı tarafına ürün eklerken kategorileri taşımak için. 
            //List<SelectListItem> categoryValues1 = (from x in values1
            //                                        select new SelectListItem
            //                                        {
            //                                            Text = x.CategoryName,
            //                                            Value = x.CategoryID
            //                                        }).ToList();

            //ViewBag.CategoryValues = categoryValues1;



            //var client = _httpClientFactory.CreateClient();
            //var responseMessage = await client.GetAsync("http://localhost:7070/api/Products/" + id);

            //if (responseMessage.IsSuccessStatusCode)
            //{
            //    var jsonData = await responseMessage.Content.ReadAsStringAsync();
            //    var values = JsonConvert.DeserializeObject<UpdateProductDto>(jsonData);
            //    return View(values);
            //}
            //return View();

            #endregion

        }


        [Route("UpdateProduct/{id}")]
        [HttpPost]
        public async Task<IActionResult> UpdateProduct(UpdateProductDto updateProductDto)
        {

            await _productService.UpdateProductAsync(updateProductDto);

            return RedirectToAction("Index", "Product", new { area = "Admin" });

        }



    }
}
