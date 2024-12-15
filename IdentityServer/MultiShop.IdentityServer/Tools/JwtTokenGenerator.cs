using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MultiShop.IdentityServer.Tools
{
    public class JwtTokenGenerator
    {

        // Token oluşturmak için kullanılan statik metod
        public static TokenResponseViewModel GenerateToken(GetCheckAppUserViewModel model)
        {
            // Kullanıcının token'a eklenecek "claim" (hak) listesi
            var claims = new List<Claim>();

            // Eğer kullanıcının rol bilgisi boş değilse, Role claim'i ekleniyor
            if (!string.IsNullOrWhiteSpace(model.Role))
            {
                claims.Add(new Claim(ClaimTypes.Role, model.Role));
            }
            else
            {
                // Rol bilgisi yoksa, kullanıcının ID'sini içeren NameIdentifier claim'i ekleniyor
                claims.Add(new Claim(ClaimTypes.NameIdentifier, model.Id));
            }

            // Eğer kullanıcının kullanıcı adı varsa, bu da bir claim olarak ekleniyor
            if (!string.IsNullOrWhiteSpace(model.Username))
            {
                claims.Add(new Claim("Username", model.Username));
            }

            // Token'ı imzalamak için kullanılan güvenlik anahtarı oluşturuluyor
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtTokenDefaults.Key));

            // HMAC SHA-256 algoritması kullanılarak imzalama bilgileri ayarlanıyor
            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Token'ın geçerlilik süresi ayarlanıyor (JwtTokenDefaults.Expire gün kadar)
            var expireDate = DateTime.UtcNow.AddDays(JwtTokenDefaults.Expire);

            // JWT token oluşturuluyor ve gerekli parametreler ayarlanıyor
            JwtSecurityToken token = new JwtSecurityToken(issuer: JwtTokenDefaults.ValidIssuer,
                audience: JwtTokenDefaults.ValidAudience,
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: expireDate,
                signingCredentials: signingCredentials);

            // JWT tokenını yazdırmak için bir handler oluşturuluyor
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

            // Oluşturulan token, geçerlilik süresiyle birlikte döndürülüyor
            return new TokenResponseViewModel(handler.WriteToken(token), expireDate);
        }
    }
}
