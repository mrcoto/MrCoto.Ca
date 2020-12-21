using System;
using System.Threading.Tasks;
using MrCoto.Ca.Application.Modules.GeneralModule.Users.Services.Data;
using MrCoto.Ca.Domain.Modules.GeneralModule.Users;

namespace MrCoto.Ca.Application.Modules.GeneralModule.Users.Services
{
    public class RefreshTokenService
    {
        private readonly IUowGeneral _uowGeneral;

        public RefreshTokenService(IUowGeneral uowGeneral)
        {
            _uowGeneral = uowGeneral;
        }

        public async Task<RefreshTokenData> GetRefreshToken(User user)
        {
            var refreshToken = new RefreshToken()
            {
                Token = Guid.NewGuid().ToString(),
                ExpiresAt = DateTime.Now.AddYears(1),
                UserId = user.Id,
                User = user,
                TenantId = user.TenantId,
                DeletedAt = null,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            await _uowGeneral.RefreshTokenRepository.Create(refreshToken);
            
            return new RefreshTokenData() { Token = refreshToken.Token, ExpiresAt = refreshToken.ExpiresAt };
        }
    }
}