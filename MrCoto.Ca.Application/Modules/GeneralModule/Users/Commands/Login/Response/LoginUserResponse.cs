using MrCoto.Ca.Application.Modules.GeneralModule.Users.Services.Data;

namespace MrCoto.Ca.Application.Modules.GeneralModule.Users.Commands.Login.Response
{
    public class LoginUserResponse
    {
        public AccessToken AccessToken { get; set; }
        public RefreshTokenData RefreshToken { get; set; }
    }
}