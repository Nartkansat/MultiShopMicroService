using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Message.Dtos;
using MultiShop.Message.Services;

namespace MultiShop.Message.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserMessagesController : ControllerBase
    {
        private readonly IUserMessageService _userMessageService;

        public UserMessagesController(IUserMessageService userMessageService)
        {
            _userMessageService = userMessageService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllMessage()
        {
            var values = await _userMessageService.GetAllMessageAsync();
            return Ok(values);
        }

        [HttpPost]
        public async Task<ActionResult> CreateMessageAsync(CreateMessageDto createMessageDto)
        {
            await _userMessageService.CreateMessageAsync(createMessageDto);
            return Ok("Mesaj Başarıyla Eklendi");
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteMessageAsync(int id)
        {
            await _userMessageService.DeleteMessageAsync(id);
            return Ok("Mesaj Başarıyla Silindi");
        }
        [HttpPut]
        public async Task<ActionResult> UpdateMessageAsync(UpdateMessageDto updateMessageDto)
        {
            await _userMessageService.UpdateMessageAsync(updateMessageDto);

            return Ok("Mesaj Başarıyla Güncellendi");
        }
        [HttpGet("GetMessageSendbox")]
        public async Task<ActionResult> GetMessageSendbox(string id)
        {
            var values = await _userMessageService.GetSendboxMessageAsync(id);
            return Ok(values);
        }

        [HttpGet("GetMessageInbox")]
        public async Task<ActionResult> GetMessageInbox(string id)
        {
            var values = await _userMessageService.GetInboxMessageAsync(id);
            return Ok(values);
        }

        [HttpGet("GetByIdMessage")]
        public async Task<ActionResult> GetByIdMessage(int id)
        {
            var values = await _userMessageService.GetByIdMessageAsync(id);
            return Ok(values);
        }

        [HttpGet("GetTotalMessageCount")]
        public async Task<ActionResult> GetTotalMessageCount()
        {
            int values = await _userMessageService.GetTotalMessageCount();
            return Ok(values);
        }

        [HttpGet("GetTotalMessageCountByReceiverId")]
        public async Task<ActionResult> GetTotalMessageCountByReceiverId(string id)
        {
            int values = await _userMessageService.GetTotalMessageCountByReceiverId(id);
            return Ok(values);
        }



    }
}
