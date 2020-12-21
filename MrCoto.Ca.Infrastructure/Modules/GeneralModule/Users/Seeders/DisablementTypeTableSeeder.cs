using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MrCoto.Ca.Application.Modules.GeneralModule;
using MrCoto.Ca.Domain.Modules.GeneralModule.Constants;
using MrCoto.Ca.Domain.Modules.GeneralModule.Users;
using MrCoto.Ca.Infrastructure.Common.Seeders;

namespace MrCoto.Ca.Infrastructure.Modules.GeneralModule.Users.Seeders
{
    public class DisablementTypeTableSeeder : ITableSeeder
    {
        private readonly IUowGeneral _uowGeneral;

        public DisablementTypeTableSeeder(IUowGeneral uowGeneral)
        {
            _uowGeneral = uowGeneral;
        }
        
        public async Task Seed()
        {
            var now = DateTime.Now;
            var disablementTypes = new List<DisablementType>()
            {
                new DisablementType()
                {
                    Id = GeneralConstants.DisablementBlockedByUserId,
                    Description = "Bloqueado de Administración",
                    CreatedAt = now,
                    UpdatedAt = now
                },
                new DisablementType()
                {
                    Id = GeneralConstants.DisablementBlockedByLoginTriesId,
                    Description = "Se ha superado la cantidad de intentos de inicio de sesión",
                    CreatedAt = now,
                    UpdatedAt = now
                },
                new DisablementType()
                {
                    Id = GeneralConstants.DisablementUnblockedId,
                    Description = "Cuenta Desbloqueada",
                    CreatedAt = now,
                    UpdatedAt = now
                },
            };

            foreach (var disablementType in disablementTypes)
            {
                var existing = await _uowGeneral.DisablementTypeRepository.Find(disablementType.Id);
                if (existing == null)
                {
                    await _uowGeneral.DisablementTypeRepository.Create(disablementType);
                }
                else
                {
                    existing.Description = disablementType.Description;
                    existing.UpdatedAt = DateTime.Now;
                    await _uowGeneral.DisablementTypeRepository.Update(disablementType);
                }
            }
            
            await _uowGeneral.SaveChanges();
        }
    }
}