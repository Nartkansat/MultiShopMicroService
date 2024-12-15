using MultiShop.Basket.Dtos;

namespace MultiShop.Basket.Services
{
    public interface IBasketService
    {
        //kullanıcı id bilgisine göre sepet verilerini getir
        Task<BasketTotalDto> GetBasket(string userId);
        Task SaveBasket(BasketTotalDto basketTotalDto);
        Task DeleteBasket(string userId);

    }
}
