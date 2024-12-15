namespace MultiShop.WebUI.Services.Interfaces
{
    public interface IClientCredentialTokenService
    {
        Task<String> GetToken();
    }
}
