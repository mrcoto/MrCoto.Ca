using System.Threading.Tasks;
using McMaster.Extensions.CommandLineUtils;
using MrCoto.Ca.AppCli.Common;
using MrCoto.Ca.AppCli.Modules.GeneralModule.Users;

namespace MrCoto.Ca.AppCli.Modules.GeneralModule
{
    [Command(Name = "general")]
    [Subcommand(typeof(UserCmd))]
    public class GeneralCmd : ICommandable
    {
        public Task OnExecute()
        {
            return Task.CompletedTask;
        }
    }
}