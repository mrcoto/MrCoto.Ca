using System.Threading.Tasks;
using MrCoto.Ca.Infrastructure.Common.Seeders;
using MrCoto.Ca.Infrastructure.Modules.GeneralModule.Users.Seeders;

namespace MrCoto.Ca.Infrastructure.Modules.GeneralModule.Configuration
{
    public class GeneralModuleSeeder : IModuleSeeder
    {
        private readonly RoleTableSeeder _roleTableSeeder;
        private readonly UserTableSeeder _userTableSeeder;
        private readonly DisablementTypeTableSeeder _disablementTypeTableSeeder;
        private readonly LoginMaxAttemptTableSeeder _loginMaxAttemptTableSeeder;

        public GeneralModuleSeeder(
            RoleTableSeeder roleTableSeeder,
            UserTableSeeder userTableSeeder,
            DisablementTypeTableSeeder disablementTypeTableSeeder,
            LoginMaxAttemptTableSeeder loginMaxAttemptTableSeeder
            )
        {
            _roleTableSeeder = roleTableSeeder;
            _userTableSeeder = userTableSeeder;
            _disablementTypeTableSeeder = disablementTypeTableSeeder;
            _loginMaxAttemptTableSeeder = loginMaxAttemptTableSeeder;
        }
        
        public async Task Run()
        {
            await _disablementTypeTableSeeder.Seed();
            await _loginMaxAttemptTableSeeder.Seed();
            await _roleTableSeeder.Seed();
            await _userTableSeeder.Seed();
        }
    }
}