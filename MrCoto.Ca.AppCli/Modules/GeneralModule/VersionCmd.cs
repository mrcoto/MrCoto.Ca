using System;
using System.Reflection;
using System.Threading.Tasks;
using McMaster.Extensions.CommandLineUtils;
using MrCoto.Ca.AppCli.Common;

namespace MrCoto.Ca.AppCli.Modules.GeneralModule
{
    [Command(Name = "version", Description = "Versión de la aplicación")]
    [HelpOption]
    public class VersionCmd : ICommandable
    {
        public Task OnExecute()
        {
            var version = typeof(Program).Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion;
            Console.WriteLine(version);
            return Task.CompletedTask;
        }
    }
}