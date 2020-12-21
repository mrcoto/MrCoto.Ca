using System.Threading.Tasks;
using McMaster.Extensions.CommandLineUtils;
using MrCoto.Ca.AppCli.Common;

namespace MrCoto.Ca.AppCli.Modules.GeneralModule.Users
{
    [Command(Name = "user")]
    [Subcommand(
        typeof(CreateUserCmd),
        typeof(ListUserCmd)
    )]
    public class UserCmd : ICommandable
    {
        public Task OnExecute()
        {
            return Task.CompletedTask;
        }
    }
}