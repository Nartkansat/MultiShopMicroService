using MultiShop.DtoLayer.DiscountDtos;

namespace MultiShop.WebUI.Services.DiscountServices
{
    public interface IDiscountService
    {
        Task<GetDiscountCode> GetDiscountCode(string code);
        Task<int> GetDiscountCouponCountRate(string code);
    }
}
