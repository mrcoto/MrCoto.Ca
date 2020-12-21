using System;
using System.Threading.Tasks;
using FluentMigrator.Runner;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MrCoto.Ca.Infrastructure.Modules.GeneralModule.Configuration;

namespace MrCoto.Ca.WebApi
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            var logger = host.Services.GetRequiredService<ILogger<Program>>();
            try
            {
                using (var scope = host.Services.CreateScope())
                {
                    logger.LogInformation("Inicializando Migraciones");
                    var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
                    runner.MigrateUp();
                }
                
                logger.LogInformation("Generando Semillas");

                using (var scopeSeed = host.Services.CreateScope())
                {
                    logger.LogInformation("SEEDS del Módulo General");
                    var generalSeeder = scopeSeed.ServiceProvider.GetRequiredService<GeneralModuleSeeder>();
                    await generalSeeder.Run();
                }

            }
            catch (Exception exception)
            {
                logger.LogError(exception, exception.Message);
                throw;
            }
            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}