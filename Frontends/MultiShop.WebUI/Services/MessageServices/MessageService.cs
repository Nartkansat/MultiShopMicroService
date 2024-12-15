using MultiShop.DtoLayer.DiscountDtos;
using MultiShop.DtoLayer.MessageDtos;
using System.Net.Http.Json;

namespace MultiShop.WebUI.Services.MessageServices
{
    public class MessageService : IMessageService
    {
        private readonly HttpClient _httpClient;
        public MessageService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CreateMessageAsync(CreateMessageDto createMessageDto)
        {
            var response = await _httpClient.PostAsJsonAsync("UserMessages", createMessageDto);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Hata oluştu: {response.StatusCode}, Mesaj: {errorContent}");
            }
        }

        public async Task<GetByIdMessageDto> GetByIdMessageAsync(int id)
        {
            var responseMessage = await _httpClient.GetAsync("http://localhost:5000/services/Message/UserMessages/GetByIdMessage?id=" + id);
            var values = await responseMessage.Content.ReadFromJsonAsync<GetByIdMessageDto>();
            return values;
        }

        public async Task<List<ResultInboxMessageDto>> GetInboxMessageAsync(string id)
        {
            var responseMessage = await _httpClient.GetAsync("http://localhost:5000/services/Message/UserMessages/GetMessageInbox?id=" + id);
            var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultInboxMessageDto>>();
            return values;
        }

        public async Task<List<ResultSendboxMessageDto>> GetSendboxMessageAsync(string id)
        {
            var responseMessage = await _httpClient.GetAsync("http://localhost:5000/services/Message/UserMessages/GetMessageSendbox?id=" + id);
            var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultSendboxMessageDto>>();
            return values;
        }

        public async Task<int> GetTotalMessageCountByReceiverId(string id)
        {
            var responseMessage = await _httpClient.GetAsync("UserMessages/GetTotalMessageCountByReceiverId?id=" + id);
            var values = await responseMessage.Content.ReadFromJsonAsync<int>();
            return values;
        }

        public async Task<HttpResponseMessage> UpdateMessageAsync(UpdateMessageDto updateMessageDto)
        {
            // PUT isteği ile veriyi güncelle
            var response = await _httpClient.PutAsJsonAsync("UserMessages", updateMessageDto);

            // API'den gelen cevabı kontrol et
            if (!response.IsSuccessStatusCode)
            {
                // Hata varsa, mesajı al
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Mesaj güncelleme hatası: {errorContent}");
            }

            // Başarılı durumda cevabı geri döndür
            return response;
        }

    }
}
