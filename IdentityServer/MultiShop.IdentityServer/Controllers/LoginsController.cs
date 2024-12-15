using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MultiShop.IdentityServer.Dtos;
using MultiShop.IdentityServer.Models;
using MultiShop.IdentityServer.Tools;
using System.Threading.Tasks;

namespace MultiShop.IdentityServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginsController : ControllerBase
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public LoginsController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> UserLogin(UserLoginDto LoginDto)
        {
            // PasswordSignInAsync parametleri => user,password, isPersistent, lockoutInFailure
            // isPersistent = giriş yapılan kişi hatırlansın mı,
            // lockoutInFailure = db'de AppNetUsers'ta LockoutEnd,LockoutEnabled, AccessFailedCount kısımlarının değerleri her yanlış girişte artıyor.
            // eğer bu sayı 5 olursa, kullanıcı 5 dk sistemden uzaklaştırılıyor.

            var result = await _signInManager.PasswordSignInAsync(LoginDto.Username, LoginDto.Password, false, false);
            var user = await _userManager.FindByNameAsync(LoginDto.Username);

            if (result.Succeeded)
            {
                GetCheckAppUserViewModel model = new GetCheckAppUserViewModel();
                model.Username = LoginDto.Username;
                model.Id = user.Id;
                var token = JwtTokenGenerator.GenerateToken(model);
                return Ok(token);

                //return Ok("Kullanıcı Girişi Başarılı");
            }
            else
            {
                return BadRequest(result);

            }
        }
    }
}
