using System.Threading.Tasks;
using MediatR;
using MrCoto.Ca.Application.Modules.GeneralModule;
using MrCoto.Ca.Application.Modules.GeneralModule.Users.Commands.Create;
using MrCoto.Ca.Infrastructure.Common.Seeders;
using MrCoto.Ca.Domain.Modules.GeneralModule.Constants;

namespace MrCoto.Ca.Infrastructure.Modules.GeneralModule.Users.Seeders
{
    public class UserTableSeeder : ITableSeeder
    {
        private readonly IUowGeneral _uowGeneral;
        private readonly IMediator _mediator;

        public UserTableSeeder(IUowGeneral uowGeneral, IMediator mediator)
        {
            _uowGeneral = uowGeneral;
            _mediator = mediator;
        }

        public async Task Seed()
        {
            var email = GeneralConstants.DefaultUser;
            if (await _uowGeneral.UserRepository.FindByEmail(email) != null)
            {
                return;
            }
            
            var command = new CreateUserCommand()
            {
                Name = "Super Administrador",
                Email = email,
                Password = GeneralConstants.DefaultPassword,
                RoleCode = GeneralConstants.RoleSuperAdmin
            };
            await _mediator.Send(command);
        }
    }
}