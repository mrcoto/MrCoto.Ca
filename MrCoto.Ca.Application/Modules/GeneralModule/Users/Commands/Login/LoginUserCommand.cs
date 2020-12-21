using MediatR;
using MrCoto.Ca.Application.Modules.GeneralModule.Users.Commands.Login.Response;

namespace MrCoto.Ca.Application.Modules.GeneralModule.Users.Commands.Login
{
    public class LoginUserCommand : IRequest<LoginUserResponse>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}