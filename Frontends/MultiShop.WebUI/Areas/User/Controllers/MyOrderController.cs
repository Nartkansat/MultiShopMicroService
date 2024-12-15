using Microsoft.AspNetCore.Mvc;
using MultiShop.WebUI.Services.Interfaces;
using MultiShop.WebUI.Services.OrderServices.OrderOderingServices;

namespace MultiShop.WebUI.Areas.User.Controllers
{
    [Area("User")]
    public class MyOrderController : Controller
    {
        private readonly IOrderOderingService _orderOderingService;
        private readonly IUserService _userService;

        public MyOrderController(IOrderOderingService orderOderingService, IUserService userService)
        {
            _orderOderingService = orderOderingService;
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.directory1 = "Anasayfa";
            ViewBag.directory2 = "Kullanıcı";
            ViewBag.directory3 = "Siparişler";

            var user = await _userService.GetUserInfo();

            if (user == null)
            {
                return RedirectToAction("SignIn", "Login");
            }


            var values = await _orderOderingService.GetOrderingByUserId(user.Id);

            return View(values);
        }
    }
}
