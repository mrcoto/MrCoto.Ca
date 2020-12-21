using System;
using System.Threading.Tasks;
using MrCoto.Ca.Application.Modules.GeneralModule;
using MrCoto.Ca.Domain.Modules.GeneralModule.Constants;
using MrCoto.Ca.Domain.Modules.GeneralModule.Users;
using MrCoto.Ca.Infrastructure.Common.Seeders;

namespace MrCoto.Ca.Infrastructure.Modules.GeneralModule.Users.Seeders
{
    public class LoginMaxAttemptTableSeeder : ITableSeeder
    {
        private readonly IUowGeneral _uowGeneral;

        public LoginMaxAttemptTableSeeder(IUowGeneral uowGeneral)
        {
            _uowGeneral = uowGeneral;
        }
        
        public async Task Seed()
        {
            var loginMaxAttempt = await _uowGeneral.LoginMaxAttemptRepository.Find(GeneralConstants.DefaultId);
            if (loginMaxAttempt == null)
            {
                loginMaxAttempt = new LoginMaxAttempt()
                {
                    Id = GeneralConstants.DefaultId,
                    MaxAttempts = GeneralConstants.DefaultMaxLoginAttempts,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };
                await _uowGeneral.LoginMaxAttemptRepository.Create(loginMaxAttempt);
                await _uowGeneral.SaveChanges();
            }
        }
    }
}