using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CommentDtos;
using MultiShop.WebUI.Services.CommentServices;
using Newtonsoft.Json;

namespace MultiShop.WebUI.ViewComponents.ProductDetailViewComponents
{
    public class _ProductDetailReviewComponentPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ICommentService _commentService;
        public _ProductDetailReviewComponentPartial(IHttpClientFactory httpClientFactory, ICommentService commentService = null)
        {
            _httpClientFactory = httpClientFactory;
            _commentService = commentService;
        }


        public async Task<IViewComponentResult> InvokeAsync(string id)
        {
            var values = await _commentService.CommentListByProductId(id);
            return View(values);
            //var client = _httpClientFactory.CreateClient();
            //var responseMessage = await client.GetAsync("https://localhost:7075/api/Comments/CommentListByProductId?id=" + id);

            //if (responseMessage.IsSuccessStatusCode)
            //{
            //    var jsonData = await responseMessage.Content.ReadAsStringAsync(); // gelen veriyi string formatta oku
            //    var values = JsonConvert.DeserializeObject<List<ResultCommentDto>>(jsonData); //api'den gelen json formatındaki veriyi string e çevirmek için.

            //    return View(values);

            //}

            //return View();
        }
    }
}
