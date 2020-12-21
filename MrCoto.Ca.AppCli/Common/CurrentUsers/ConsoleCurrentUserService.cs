using System.Threading.Tasks;
using MrCoto.Ca.Application.Common.CurrentUsers;
using MrCoto.Ca.Application.Common.CurrentUsers.Response;
using MrCoto.Ca.Domain.Modules.GeneralModule.Users;

namespace MrCoto.Ca.AppCli.Common.CurrentUsers
{
    public class ConsoleCurrentUserService : ICurrentUserService
    {
        public Task<CurrentUser> CurrentUser()
        {
            return Task.FromResult(default(CurrentUser));
        }

        public Task<CurrentUser> SetCurrentUser(long id)
        {
            return Task.FromResult(default(CurrentUser));
        }
        
        private CurrentUser Map(User user)
        {
            var currentRole = new CurrentRole()
            {
                Id = user.RoleId,
                Code = user.Role.Code,
                Name = user.Role.Name
            };
            var currentTenant = new CurrentTenant()
            {
                Id = user.TenantId,
                Code = user.Tenant.Code,
                Name = user.Tenant.Name
            };
            return new CurrentUser()
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.Name,
                Role = currentRole,
                Tenant = currentTenant
            };
        }
    }
}