using MediatR;
using MrCoto.Ca.Application.Modules.GeneralModule.Users.Commands.RefreshToken.Response;

namespace MrCoto.Ca.Application.Modules.GeneralModule.Users.Commands.RefreshToken
{
    public class RefreshTokenCommand : IRequest<RefreshTokenResponse>
    {
        public string Token { get; set; }
    }
}