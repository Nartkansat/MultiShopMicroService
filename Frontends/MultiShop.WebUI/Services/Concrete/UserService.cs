using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using MultiShop.WebUI.Models;
using MultiShop.WebUI.Services.Interfaces;

namespace MultiShop.WebUI.Services.Concrete
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<UserDetailViewModel> GetUserInfo()
        {
            try
            {
                var response = await _httpClient.GetAsync("/api/users/getuserinfo");

                if (response.IsSuccessStatusCode)
                {
                    var userInfo = await response.Content.ReadFromJsonAsync<UserDetailViewModel>();

                    if (userInfo != null)
                    {
                        return userInfo;
                    }
                    else
                    {
                        throw new Exception("Kullanıcı bilgisi boş döndü.");
                    }
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized) // 401 kontrolü
                {
                    return null;
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Sunucu hatası: {response.StatusCode}, İçerik: {errorContent}");
                }
            }
            catch (Exception ex)
            {
                // Hata loglama ya da işlem yapma
                throw new Exception($"Kullanıcı bilgisi alınırken bir hata oluştu: {ex.Message}", ex);
            }
        }
    }

}
