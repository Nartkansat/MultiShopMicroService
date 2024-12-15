using System.Security.Claims;

namespace MultiShop.Basket.LoginService
{
    public class LoginService : ILoginService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LoginService(IHttpContextAccessor contextAccessor)
        {
            _httpContextAccessor = contextAccessor;
        }


        // sub key aracılığı ile tokenı yakalayıp id'yi alacaz.  // sonradan değiştirildi hata veriyordu.
        public string GetUserId => _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    }
}
