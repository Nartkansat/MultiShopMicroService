using Microsoft.AspNetCore.Mvc;
using MultiShop.WebUI.Services.CatalogServices.CategoryServices;
using MultiShop.WebUI.Services.CommentServices;
using MultiShop.WebUI.Services.StatisticServices.CatalogStatisticServices;
using MultiShop.WebUI.Services.StatisticServices.DiscountStatisticServices;
using MultiShop.WebUI.Services.StatisticServices.MessageStatisticServices;
using MultiShop.WebUI.Services.StatisticServices.UserStatisticServices;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class StatisticController : Controller
    {
        private readonly ICatalogStatisticService _catalogStatisticService;
        private readonly IUserStatisticService _userStatisticService;
        private readonly ICommentService _commentService;
        private readonly IDiscountStatisticService _discountStatisticService;
        private readonly IMessageStatisticService _messageStatisticService;

        public StatisticController(ICatalogStatisticService catalogStatisticService, IUserStatisticService userStatisticService, ICommentService commentService, IDiscountStatisticService discountStatisticService, IMessageStatisticService messageStatisticService)
        {
            _catalogStatisticService = catalogStatisticService;
            _userStatisticService = userStatisticService;
            _commentService = commentService;
            _discountStatisticService = discountStatisticService;
            _messageStatisticService = messageStatisticService;
        }

        public async Task<IActionResult> Index()
        {
            var brandCount = await _catalogStatisticService.GetBrandCount();
            ViewBag.ToplamMarka = brandCount;

            var categoryCount = await _catalogStatisticService.GetCategoryCount();
            ViewBag.CategoryCount = categoryCount;

            var productCount = await _catalogStatisticService.GetProductCount();
            ViewBag.ProductCount = productCount;

            var maxPriceProduct = await _catalogStatisticService.GetMaxPriceProductName();
            ViewBag.MaxPriceProduct = maxPriceProduct;

            var minPriceProduct = await _catalogStatisticService.GetMinPriceProductName();
            ViewBag.MinPriceProduct = minPriceProduct;

            //var avgPriceProduct = await _catalogStatisticService.GetProductAvgPrice();
            //ViewBag.AvgPriceProduct = avgPriceProduct;

            var userCount = await _userStatisticService.GetUsercount();
            ViewBag.UserCount = userCount;

            var totalCommentCount = await _commentService.GetTotalCommentCount();
            ViewBag.TotalCommentCount = totalCommentCount;

            var activeCommentCount = await _commentService.GetActiveCommentCount();
            ViewBag.ActiveCommentCount = activeCommentCount;

            var passiveCommentCount = await _commentService.GetPassiveCommentCount();
            ViewBag.PassiveCommentCount = passiveCommentCount;

            var discountCouponCount = await _discountStatisticService.GetDiscountCouponCount();
            ViewBag.DiscountCouponCount = discountCouponCount;

            var totalMessageCount = await _messageStatisticService.GetTotalMessageCount();
            ViewBag.MessageCount = totalMessageCount;

            return View();
        }
    }
}
