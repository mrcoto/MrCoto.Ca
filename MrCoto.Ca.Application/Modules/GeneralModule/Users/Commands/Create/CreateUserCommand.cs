using MediatR;
using MrCoto.Ca.Domain.Modules.GeneralModule.Users;

namespace MrCoto.Ca.Application.Modules.GeneralModule.Users.Commands.Create
{
    public class CreateUserCommand : IRequest<User>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string TenantCode { get; set; }
        public string RoleCode { get; set; }
        public string Password { get; set; }
    }
}