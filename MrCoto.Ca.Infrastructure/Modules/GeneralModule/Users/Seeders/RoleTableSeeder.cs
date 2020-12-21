using System.Collections.Generic;
using System.Threading.Tasks;
using MrCoto.Ca.Application.Modules.GeneralModule;
using MrCoto.Ca.Domain.Modules.GeneralModule.Users;
using MrCoto.Ca.Infrastructure.Common.Seeders;
using MrCoto.Ca.Domain.Modules.GeneralModule.Constants;

namespace MrCoto.Ca.Infrastructure.Modules.GeneralModule.Users.Seeders
{
    public class RoleTableSeeder : ITableSeeder
    {
        private readonly IUowGeneral _uowGeneral;

        public RoleTableSeeder(IUowGeneral uowGeneral)
        {
            _uowGeneral = uowGeneral;
        }
        
        public async Task Seed()
        {
            var roles = new List<Role>()
            {
                new Role(){ Id = 1, Code = GeneralConstants.RoleSuperAdmin, Name = "Super Administrador"},
                new Role(){ Id = 2, Code = GeneralConstants.RoleUser, Name = "Usuario"}
            };

            foreach (var role in roles)
            {
                var existing = await _uowGeneral.RoleRepository.Find(role.Id);
                if (existing == null)
                {
                    await _uowGeneral.RoleRepository.Create(role);
                }
                else
                {
                    existing.Name = role.Name;
                    await _uowGeneral.RoleRepository.Update(role);
                }
            }
            
            await _uowGeneral.SaveChanges();
        }
    }
}