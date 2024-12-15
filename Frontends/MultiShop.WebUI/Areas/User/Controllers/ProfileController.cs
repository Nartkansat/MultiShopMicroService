using Microsoft.AspNetCore.Mvc;

namespace MultiShop.WebUI.Areas.User.Controllers
{
    [Area("User")]
    public class ProfileController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.directory1 = "Anasayfa";
            ViewBag.directory2 = "Kullanıcı";
            ViewBag.directory3 = "Profil Bilgileri";
            return View();
        }
    }
}
