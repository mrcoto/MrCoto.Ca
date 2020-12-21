using MrCoto.Ca.Application.Modules.GeneralModule.Users.Services.Data;

namespace MrCoto.Ca.Application.Modules.GeneralModule.Users.Commands.RefreshToken.Response
{
    public class RefreshTokenResponse
    {
        public AccessToken AccessToken { get; set; }
        public RefreshTokenData RefreshToken { get; set; }
    }
}