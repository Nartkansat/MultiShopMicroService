using Microsoft.AspNetCore.Mvc;

namespace MultiShop.WebUI.Controllers
{
    public class PaymentController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.directory1 = "Anasayfa";
            ViewBag.directory2 = "Siparişler";
            ViewBag.directory3 = "Ödeme";
            return View();
        }
    }
}
