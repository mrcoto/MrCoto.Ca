using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
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
using MrCoto.Ca.Infrastructure.Common.CurrentUsers;
using MrCoto.Ca.Infrastructure.Common.Events;
using MrCoto.Ca.Infrastructure.Common.Renderers;
using MrCoto.Ca.Infrastructure.Common.Repositories;
using MrCoto.Ca.Infrastructure.Modules.GeneralModule.Configuration;
using MrCoto.Ca.WebApiTests.Fake;

namespace MrCoto.Ca.WebApiTests.Configuration
{
    public static class FakeInfrastructureDependencyInjection
    {
        public static IServiceCollection AddFakeInfrastructure(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDetection();
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddAuthorization(configuration);

            return services
                .AddScoped<IUow, Uow>()
                .AddScoped<ICurrentUserService, CurrentUserService>()
                .AddScoped<IDomainEventPublisher, DomainEventPublisher>()
                .AddScoped<IClientInfoDetection, FakeClientInfoDetection>()
                .AddScoped<IBackgroundJobService, FakeBackgroundService>()
                .AddScoped<IViewRenderer, ViewRenderer>()
                .AddScoped<IMailService, FakeMailService>()
                .AddGeneral(configuration);
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
        
    }
}