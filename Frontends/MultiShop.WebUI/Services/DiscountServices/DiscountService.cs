using MultiShop.DtoLayer.DiscountDtos;

namespace MultiShop.WebUI.Services.DiscountServices
{
    public class DiscountService : IDiscountService
    {
        private readonly HttpClient _httpClient;

        public DiscountService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<GetDiscountCode> GetDiscountCode(string code)
        {
            var responseMessage = await _httpClient.GetAsync($"discounts/GetCodeDetailByCode/{code}");
            var values = await responseMessage.Content.ReadFromJsonAsync<GetDiscountCode>();
            return values;
        }

        public async Task<int> GetDiscountCouponCountRate(string code)
        {
            var responseMessage = await _httpClient.GetAsync("http://localhost:7071/api/Discounts/GetDiscountCouponCountRate?code=" + code);
            var values = await responseMessage.Content.ReadFromJsonAsync<int>();
            return values;
        }
    }
}
