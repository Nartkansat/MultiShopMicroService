using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.MessageDtos;
using MultiShop.WebUI.Services.Interfaces;
using MultiShop.WebUI.Services.MessageServices;

namespace MultiShop.WebUI.Areas.User.Controllers
{
    [Area("User")]
    public class MessageController : Controller
    {
        private readonly IMessageService _messageService;
        private readonly IUserService _userService;
        public MessageController(IMessageService messageService, IUserService userService)
        {
            _messageService = messageService;
            _userService = userService;
        }

        public async Task<IActionResult> Inbox()
        {
            ViewBag.directory1 = "Anasayfa";
            ViewBag.directory2 = "Kullanıcı";
            ViewBag.directory3 = "Gelen Mesajlar";

            var user = await _userService.GetUserInfo();

            var values = await _messageService.GetInboxMessageAsync(user.Id);

            return View(values);
        }

        public async Task<IActionResult> Sendbox()
        {
            ViewBag.directory1 = "Anasayfa";
            ViewBag.directory2 = "Kullanıcı";
            ViewBag.directory3 = "Gönderilen Mesajlar";


            var user = await _userService.GetUserInfo();

            var values = await _messageService.GetSendboxMessageAsync(user.Id);

            return View(values);
        }

        [HttpGet]
        public async Task<IActionResult> SendMessage()
        {
            ViewBag.directory1 = "Anasayfa";
            ViewBag.directory2 = "Kullanıcı";
            ViewBag.directory3 = "Mesaj Gönder";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage(CreateMessageDto createMessageDto)
        {
            var user = await _userService.GetUserInfo();

            createMessageDto.IsRead = false;
            createMessageDto.MessageDate = DateTime.UtcNow;
            createMessageDto.SenderId = user.Id;
            createMessageDto.ReceiverId = "52acc89e-a9a2-492b-9a2f-d683d12b756d";

            await _messageService.CreateMessageAsync(createMessageDto);

            return RedirectToAction("Index", "Default");

        }



        [HttpGet]
        public async Task<IActionResult> UpdateIsReadMessage(int id)
        {
            // Mesajı ID ile al
            var message = await _messageService.GetByIdMessageAsync(id);

            // Mesaj bulunduysa
            if (message != null)
            {
                // DTO oluştur ve IsRead'yi true yap
                var updateDto = new UpdateMessageDto
                {
                    UserMessageID = message.UserMessageID,
                    MessageDate = message.MessageDate,
                    SenderId = message.SenderId,
                    MessageDetail = message.MessageDetail,
                    Subject = message.Subject,
                    ReceiverId = message.ReceiverId,
                    IsRead = true
                };

                // Mesajı güncelle
                var response = await _messageService.UpdateMessageAsync(updateDto);

                // API çağrısının başarılı olup olmadığını kontrol et
                if (!response.IsSuccessStatusCode)
                {
                    // Hata varsa, hata mesajını kullanıcıya bildir
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Mesaj güncelleme hatası: {errorContent}");
                }
            }

            // Mesaj başarıyla güncellenirse gelen kutusuna yönlendir
            return RedirectToAction("Inbox", "Message", new { area = "User" });
        }



    }
}
