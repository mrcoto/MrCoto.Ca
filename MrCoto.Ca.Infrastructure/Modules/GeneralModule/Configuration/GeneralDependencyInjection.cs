using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MrCoto.Ca.Application.Modules.GeneralModule;
using MrCoto.Ca.Application.Modules.GeneralModule.Users.Mails.Disablement;
using MrCoto.Ca.Application.Modules.GeneralModule.Users.Mails.NewLogin;
using MrCoto.Ca.Application.Modules.GeneralModule.Users.Mails.Register;
using MrCoto.Ca.Application.Modules.GeneralModule.Users.Query.Default;
using MrCoto.Ca.Application.Modules.GeneralModule.Users.Services;
using MrCoto.Ca.Infrastructure.Modules.GeneralModule.Users.Mails;
using MrCoto.Ca.Infrastructure.Modules.GeneralModule.Users.Query.Default;
using MrCoto.Ca.Infrastructure.Modules.GeneralModule.Users.Seeders;
using MrCoto.Ca.Infrastructure.Modules.GeneralModule.Users.Services;

namespace MrCoto.Ca.Infrastructure.Modules.GeneralModule.Configuration
{
    public static class GeneralDependencyInjection
    {
        public static IServiceCollection AddGeneral(this IServiceCollection services, IConfiguration configuration)
        {
            return services
                .AddScoped<IUowGeneral, UowGeneral>()
                .AddTransient<IPasswordService, PasswordService>()
                .AddTransient<RefreshTokenService>()
                .AddTransient<IAccessTokenService, AccessTokenService>(sp => 
                    new AccessTokenService(configuration.GetValue<string>("SecretKey")))
                .AddMails()
                .AddQueries()
                .AddSeeders();
        }
        
        private static IServiceCollection AddQueries(this IServiceCollection services)
        {
            return services
                    .AddScoped<IUserQuery, UserQuery>()
                ;
        }
        
        private static IServiceCollection AddMails(this IServiceCollection services)
        {
            return services
                .AddTransient<IRegisterMail, RegisterMailModel>()
                .AddTransient<INewLoginMail, NewLoginMailModel>()
                .AddTransient<IDisablementMail, DisablementMailModel>();
        }
        
        private static IServiceCollection AddSeeders(this IServiceCollection services)
        {
            return services
                .AddScoped<RoleTableSeeder>()
                .AddScoped<UserTableSeeder>()
                .AddScoped<DisablementTypeTableSeeder>()
                .AddScoped<LoginMaxAttemptTableSeeder>()
                .AddScoped<GeneralModuleSeeder>();
        }
    }
}