using System;

namespace MultiShop.IdentityServer.Tools
{
    public interface ITokenResponseViewModel
    {
        DateTime ExpireDate { get; set; }
        string Token { get; set; }
    }
}