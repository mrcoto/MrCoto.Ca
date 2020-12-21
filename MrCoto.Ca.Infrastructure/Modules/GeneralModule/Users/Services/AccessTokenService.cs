using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using MrCoto.Ca.Application.Modules.GeneralModule.Users.Services;
using MrCoto.Ca.Application.Modules.GeneralModule.Users.Services.Data;
using MrCoto.Ca.Domain.Modules.GeneralModule.Users;

namespace MrCoto.Ca.Infrastructure.Modules.GeneralModule.Users.Services
{
    public class AccessTokenService : IAccessTokenService
    {
        private readonly string _secretKey;

        public AccessTokenService(string secretKey)
        {
            _secretKey = secretKey;
        }
        
        public Task<AccessToken> GetAccessToken(User user)
        {
            var key = Encoding.ASCII.GetBytes(_secretKey);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
            };

            var expiresAt = DateTime.Now.AddDays(1);
            var tokenDescription = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = expiresAt.ToUniversalTime(),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            
            var tokenHandler = new JwtSecurityTokenHandler();
            var createdToken = tokenHandler.CreateToken(tokenDescription);
            var token = tokenHandler.WriteToken(createdToken);

            return Task.FromResult(new AccessToken() { Token = token, ExpiresAt = expiresAt });
        }
    }
}