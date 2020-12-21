using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MrCoto.Ca.AppCli.Common.Utils;
using MrCoto.Ca.AppCli.Modules.GeneralModule;

namespace MrCoto.Ca.AppCli
{
    [Subcommand(
        typeof(GeneralCmd),
        typeof(VersionCmd)
        )]
    public class Program
    {
        public static Task<int> Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Path.GetDirectoryName(Assembly.GetEntryAssembly()?.Location))
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();    
            
            var builder = new HostBuilder()
                .ConfigureServices((context, services) => {
                    services.AddSingleton(PhysicalConsole.Singleton);
                    services.AddSingleton<PrinterTableHelper>();
                    services.AddSingleton<PrinterHelper>();
                    services.AddSingleton<PromptHelper>();
                    services.AddSingleton<CommandUtil>();
                    services.ConfigureServices(configuration);
                });
            
            return builder.RunCommandLineApplicationAsync<Program>(args);
        }
        
        private void OnExecute()
        {
            Console.WriteLine($"=====================");
            Console.WriteLine($"    MRCOTO CA CLI    ");
            Console.WriteLine($"=====================");
        }
        
    }
}