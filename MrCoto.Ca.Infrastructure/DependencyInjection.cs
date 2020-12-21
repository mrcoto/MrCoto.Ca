using System;
using System.Text;
using FluentMigrator.Runner;
using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using MrCoto.Ca.Application.Common.Background;
using MrCoto.Ca.Application.Common.ClientDetection;
using MrCoto.Ca.Application.Common.CurrentUsers;
using MrCoto.Ca.Application.Common.Events;
using MrCoto.Ca.Application.Common.Mail;
using MrCoto.Ca.Application.Common.Renderers;
using MrCoto.Ca.Application.Common.Repositories;
using MrCoto.Ca.Infrastructure.Common.Background;
using MrCoto.Ca.Infrastructure.Common.ClientDetection;
using MrCoto.Ca.Infrastructure.Common.CurrentUsers;
using MrCoto.Ca.Infrastructure.Common.Events;
using MrCoto.Ca.Infrastructure.Common.Mail;
using MrCoto.Ca.Infrastructure.Common.Query;
using MrCoto.Ca.Infrastructure.Common.Renderers;
using MrCoto.Ca.Infrastructure.Common.Repositories;
using MrCoto.Ca.Infrastructure.Migrations;
using MrCoto.Ca.Infrastructure.Modules.GeneralModule.Configuration;

namespace MrCoto.Ca.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDetection();
            services.TryAddScoped<IHttpContextAccessor, HttpContextAccessor>();
            var connectionString = configuration.GetConnectionString("DB");
            services.AddDbContext<SimpleAgendaContext>(options => options
                .UseNpgsql(connectionString)
                .UseSnakeCaseNamingConvention());

            services.AddAuthorization(configuration);
            
            return services
                .AddScoped<IUow, Uow>()
                .AddScoped<ICurrentUserService, CurrentUserService>()
                .AddScoped<IDomainEventPublisher, DomainEventPublisher>()
                .AddScoped<IClientInfoDetection, ClientInfoDetection>()
                .AddScoped<IBackgroundJobService, BackgroundJobService>()
                .AddScoped<IViewRenderer, ViewRenderer>()
                .AddScoped<IMailService, MailService>()
                .AddScoped<IPaginationBuilder, PaginationBuilder>()
                .AddGeneral(configuration)
                .AddHangfire(connectionString)
                .AddEmail(configuration)
                .AddMigrations(connectionString);
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
        
        public static IServiceCollection AddHangfire(this IServiceCollection services, string connectionString)
        {
            services.AddHangfire(configuration => configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UsePostgreSqlStorage(connectionString, new PostgreSqlStorageOptions()
                {
                    QueuePollInterval = TimeSpan.FromSeconds(3),
                }));

            services.AddHangfireServer();

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
        
        public static IServiceCollection AddMigrations(this IServiceCollection services, string connectionString)
        {
            services
                .AddFluentMigratorCore()
                .ConfigureRunner(rb => 
                    rb.AddPostgres()
                        .WithGlobalConnectionString(connectionString)
                        .ScanIn(typeof(AddInitialTables).Assembly).For.Migrations()
                )
                .BuildServiceProvider(false);
            return services;
        }
    }
}