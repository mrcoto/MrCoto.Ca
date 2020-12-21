using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MrCoto.Ca.AppCli.Common.Background;
using MrCoto.Ca.AppCli.Common.ClientDetection;
using MrCoto.Ca.AppCli.Common.CurrentUsers;
using MrCoto.Ca.AppCli.Common.Query;
using MrCoto.Ca.Application;
using MrCoto.Ca.Application.Common.Background;
using MrCoto.Ca.Application.Common.ClientDetection;
using MrCoto.Ca.Application.Common.CurrentUsers;
using MrCoto.Ca.Application.Common.Events;
using MrCoto.Ca.Application.Common.Mail;
using MrCoto.Ca.Application.Common.Renderers;
using MrCoto.Ca.Application.Common.Repositories;
using MrCoto.Ca.Infrastructure;
using MrCoto.Ca.Infrastructure.Common.Events;
using MrCoto.Ca.Infrastructure.Common.Mail;
using MrCoto.Ca.Infrastructure.Common.Query;
using MrCoto.Ca.Infrastructure.Common.Renderers;
using MrCoto.Ca.Infrastructure.Common.Repositories;
using MrCoto.Ca.Infrastructure.Modules.GeneralModule.Configuration;

namespace MrCoto.Ca.AppCli
{
    public static class DependencyInjection
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddApplication();
            services.ConfigureConsoleInfrastructure(configuration);
            return services;
        }

        private static IServiceCollection ConfigureConsoleInfrastructure(this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DB");
            services.AddDbContext<CaContext>(options => options
                .UseNpgsql(connectionString)
                .UseSnakeCaseNamingConvention(), ServiceLifetime.Singleton);

            services.AddAuthorization(configuration);
            
            return services
                .AddSingleton<IUow, Uow>()
                .AddSingleton<ICurrentUserService, ConsoleCurrentUserService>()
                .AddSingleton<IDomainEventPublisher, DomainEventPublisher>()
                .AddSingleton<IClientInfoDetection, ConsoleClientInfoDetection>()
                .AddSingleton<IBackgroundJobService, ConsoleBackgroundJobService>()
                .AddSingleton<IViewRenderer, ViewRenderer>()
                .AddSingleton<IMailService, MailService>()
                .AddSingleton<IPaginationBuilder, ConsolePaginationBuilder>()
                .AddGeneral(configuration)
                .AddEmail(configuration);
        }

        private static IServiceCollection AddAuthorization(this IServiceCollection services,
            IConfiguration configuration)
        {
            var key = Encoding.ASCII.GetBytes(configuration.GetValue<string>("SecretKey"));
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
            return services;
        }
        
        public static IServiceCollection AddEmail(this IServiceCollection services, IConfiguration configuration)
        {
            var host = configuration["Mail:Host"];
            var port = configuration.GetValue<int>("Host:Port");
            var from = configuration["Mail:From"];
            var fromName = configuration["Mail:FromName"];
            var username = configuration["Mail:Username"];
            var password = configuration["Mail:Password"];
            
            services
                .AddFluentEmail(from, fromName)
                .AddSmtpSender(host, port, username, password);

            return services;
        }
        
    }
}