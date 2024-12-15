using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.ContactDtos;
using MultiShop.DtoLayer.CommentDtos;
using MultiShop.WebUI.Services.CatalogServices.ContactServices;
using Newtonsoft.Json;
using System.Text;

namespace MultiShop.WebUI.Controllers
{
    public class ContactController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IContactService _contactService;

        public ContactController(IHttpClientFactory httpClientFactory, IContactService contactService)
        {
            _httpClientFactory = httpClientFactory;
            _contactService = contactService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.directory1 = "Anasayfa";
            ViewBag.directory2 = "İletişim";
            ViewBag.directory3 = "Bize Ulaşın";
            return View();  
        }

        [HttpPost]
        public async Task<IActionResult> Index(CreateContactDto createContactDto)
        {
            createContactDto.SendDate = DateTime.Now;
            createContactDto.IsRead = false;
            await _contactService.CreateContactAsync(createContactDto);
           
            return RedirectToAction("Index", "Default");

            //var client = _httpClientFactory.CreateClient();
            //var jsonData = JsonConvert.SerializeObject(createContactDto); // json formatına dönüştürmek için serialize
            //StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json"); //content olarak atadık, neyi kontrol atadık,hangi dil, ve hangi mediator
            //var responseMessage = await client.PostAsync("http://localhost:7070/api/Contacts", stringContent);
            //if (responseMessage.IsSuccessStatusCode)
            //{
            //    return RedirectToAction("Index", "Default");
            //}
            //return View();
        }
    }
}
