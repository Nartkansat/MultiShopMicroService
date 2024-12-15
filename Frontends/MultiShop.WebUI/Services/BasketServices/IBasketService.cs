using MultiShop.DtoLayer.BasketDtos;

namespace MultiShop.WebUI.Services.BasketServices
{
    public interface IBasketService
    {
        Task<BasketTotalDto> GetBasket();
        Task SaveBasket(BasketTotalDto basketTotalDto);

        //sepeti komple silme işlevi için
        Task DeleteBasket(string userId);

        //sepete yeni öğe
        Task AddBasketItem(BasketItemDto basketItemDto);

        //sepetteki öğeyi silme, ürün id ye göre silme işlemi yapılmalı
        Task<bool> RemoveBasketItem(string productId);

    }
}
