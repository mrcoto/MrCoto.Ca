using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MrCoto.Ca.Application.Modules.GeneralModule.Users.Commands.RefreshToken.Response;
using MrCoto.Ca.Application.Modules.GeneralModule.Users.Exceptions;
using MrCoto.Ca.Application.Modules.GeneralModule.Users.Services;

namespace MrCoto.Ca.Application.Modules.GeneralModule.Users.Commands.RefreshToken
{
    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, RefreshTokenResponse>
    {
        private readonly IUowGeneral _uowGeneral;
        private readonly IAccessTokenService _accessTokenService;
        private readonly RefreshTokenService _refreshTokenService;

        public RefreshTokenCommandHandler(
            IUowGeneral uowGeneral,
            IAccessTokenService accessTokenService,
            RefreshTokenService refreshTokenService
        )
        {
            _uowGeneral = uowGeneral;
            _accessTokenService = accessTokenService;
            _refreshTokenService = refreshTokenService;
        }
        
        public async Task<RefreshTokenResponse> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var refreshToken = await _uowGeneral.RefreshTokenRepository.FindByToken(request.Token)
                               ?? throw new InvalidRefreshTokenException();

            if (refreshToken.IsExpired())
            {
                throw new InvalidRefreshTokenException("El token de refresco ya ha expirado");
            }

            var user = refreshToken.User;
            var accessToken = await _accessTokenService.GetAccessToken(user);
            var newRefreshToken = await _refreshTokenService.GetRefreshToken(user);
            
            await _uowGeneral.RefreshTokenRepository.Delete(refreshToken);
            
            await _uowGeneral.SaveChanges();
            
            return new RefreshTokenResponse() { AccessToken = accessToken, RefreshToken = newRefreshToken };
        }
    }
}