using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MrCoto.Ca.Application.Common.Exceptions;
using MrCoto.Ca.Application.Modules.GeneralModule.Users.Exceptions;
using MrCoto.Ca.Application.Modules.GeneralModule.Users.Services;
using MrCoto.Ca.Domain.Modules.GeneralModule.Users;
using MrCoto.Ca.Domain.Modules.GeneralModule.Users.Events;

namespace MrCoto.Ca.Application.Modules.GeneralModule.Users.Commands.Create
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, User>
    {
        private readonly IUowGeneral _uowGeneral;
        private readonly IPasswordService _passwordService;

        public CreateUserCommandHandler(IUowGeneral uowGeneral, IPasswordService passwordService)
        {
            _uowGeneral = uowGeneral;
            _passwordService = passwordService;
        }
        
        public async Task<User> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            if (await _uowGeneral.UserRepository.FindByEmail(request.Email) != null)
            {
                throw new ExistingAccountException($"El email '{request.Email}' ya ha sido tomado");
            }
            
            var tenant = await GetUserTenant(request.TenantCode);
            var role = await _uowGeneral.RoleRepository.FindByCode(request.RoleCode) ?? 
                       throw new EntityNotFoundException("Rol", request.RoleCode);
            var user = GetUserAccount(request, tenant, role);

            await _uowGeneral.UserRepository.Create(user);
            
            user.AddEvent(new UserRegistered() {User = user});

            await _uowGeneral.SaveChanges();
            
            return user;
        }

        private User GetUserAccount(CreateUserCommand request, Tenant tenant, Role role)
        {
            return new User()
            {
                Name = request.Name,
                Email = request.Email,
                Password = _passwordService.Hash(request.Password),
                TenantId = tenant.Id,
                Tenant = tenant,
                RoleId = role.Id,
                LoginAttempts = 0,
                DisabledAccountAt = null,
                LastLoginAt = null,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
        }

        private async Task<Tenant> GetUserTenant(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
            {
                var defaultTenant = await DefaultTenant();
                await _uowGeneral.TenantRepository.Create(defaultTenant);
                return defaultTenant;
            }

            return await _uowGeneral.TenantRepository.FindByCode(code) ??
                   throw new EntityNotFoundException("Organización", code);
        }

        private async Task<Tenant> DefaultTenant()
        {
            string code;
            do
            {
                code = Guid.NewGuid().ToString("n");
            } while (await _uowGeneral.TenantRepository.FindByCode(code) != null);
            
            return new Tenant()
            {
                Code = code,
                Name = "Mi Organización",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
        }
    }
}