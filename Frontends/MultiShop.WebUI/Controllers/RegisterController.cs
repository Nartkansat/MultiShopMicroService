using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.IdentityDtos.RegisterDtos;
using Newtonsoft.Json;
using System.Text;

namespace MultiShop.WebUI.Controllers
{
    public class RegisterController : Controller
    {
		private readonly IHttpClientFactory _httpClientFactory;

		public RegisterController(IHttpClientFactory httpClientFactory)
		{
			_httpClientFactory = httpClientFactory;
		}


		[HttpGet]
        public IActionResult Index()
        {
            return View();
        }

		[HttpPost]
		public async Task<IActionResult> Index(CreateRegisterDto createRegisterDto)
		{
			// şifre oluşturken 123456aA* gibi formatta olsun.

			if(createRegisterDto.Password == createRegisterDto.ConfirmPassword)
			{
				var client = _httpClientFactory.CreateClient();
				var jsonData = JsonConvert.SerializeObject(createRegisterDto); // json formatına dönüştürmek için serialize
				StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json"); //content olarak atadık, neyi kontrol atadık,hangi dil, ve hangi mediator
				var responseMessage = await client.PostAsync("http://localhost:5001/api/Registers", stringContent);
				if (responseMessage.IsSuccessStatusCode)
				{
					return RedirectToAction("Index", "Login");
				}
			}

			
			return View();
		}
	}
}
