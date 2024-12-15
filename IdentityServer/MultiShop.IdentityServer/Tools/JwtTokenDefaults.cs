namespace MultiShop.IdentityServer.Tools
{
    public class JwtTokenDefaults
    {
        public const string ValidAudience = "http://localhost"; //dinleyici
        public const string ValidIssuer = "http://localhost"; //yayınlayıcı
        public const string Key = "MultiShop.0102030405Asp.NetCore.Synccase.EcommerceSite*/-+";
        public const int Expire = 60; // geçerlilik süresi 60 dk

    }
}
