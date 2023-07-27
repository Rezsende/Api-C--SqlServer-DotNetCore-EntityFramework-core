
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using c_.Entities;
using Microsoft.IdentityModel.Tokens;

namespace c_.Services
{
    public class TokenService
    {
        public static object GenerateToken(User user)
        {
            var chave = Encoding.ASCII.GetBytes(Chave.Secret);
            var tokenConfig = new SecurityTokenDescriptor
            {
               Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
               {
                    new Claim("UserID", user.Id.ToString()),
               }),
               Expires = DateTime.UtcNow.AddHours(1),
               SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(chave), SecurityAlgorithms.HmacSha256Signature),
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenConfig);
            var tokenString = tokenHandler.WriteToken(token);

            
            return tokenString;
            // return new { token = tokenString};
        
        }
    }
}
