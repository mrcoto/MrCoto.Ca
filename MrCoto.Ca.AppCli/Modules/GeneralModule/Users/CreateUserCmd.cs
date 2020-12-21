using System;
using System.Threading.Tasks;
using McMaster.Extensions.CommandLineUtils;
using MediatR;
using MrCoto.Ca.AppCli.Common;
using MrCoto.Ca.AppCli.Common.Utils;
using MrCoto.Ca.Application.Modules.GeneralModule.Users.Commands.Create;
using MrCoto.Ca.Domain.Modules.GeneralModule.Constants;

namespace MrCoto.Ca.AppCli.Modules.GeneralModule.Users
{
    [Command(Name = "create", Description = "Registra un usuario en la aplicación")]
    public class CreateUserCmd : ICommandable
    {
        private readonly CommandUtil _commandUtil;
        private readonly IMediator _mediator;

        public CreateUserCmd(CommandUtil commandUtil, IMediator mediator)
        {
            _commandUtil = commandUtil;
            _mediator = mediator;
        }

        public async Task OnExecute()
        {
            _commandUtil.PrintTitle("Creación de Usuario");
            var name = _commandUtil.PromptString("Nombre:");
            var email =_commandUtil.PromptString("Email:");
            var password = _commandUtil.PromptPassword("Contraseña:");
            
            var command = new CreateUserCommand()
            {
                Name = name,
                Email = email,
                Password = password,
                TenantCode = "",
                RoleCode = GeneralConstants.RoleUser,
            };
            
            try
            {
                var user = await _mediator.Send(command);
                _commandUtil.Print($"Usuario Creado: {user.Id}", ConsoleColor.Green);
            }
            catch (Exception e)
            {
                _commandUtil.PrintException(e);
            }
        }
    }
}