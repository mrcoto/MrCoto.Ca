using System.Threading.Tasks;
using MrCoto.Ca.Application.Common.CurrentUsers.Exceptions;
using MrCoto.Ca.Application.Common.CurrentUsers.Response;
using MrCoto.Ca.Domain.Common.Entities;

namespace MrCoto.Ca.Application.Common.CurrentUsers
{
    public interface ICurrentUserService
    {
        public Task<CurrentUser> CurrentUser();
        
        public Task<CurrentUser> SetCurrentUser(long id);

        public async Task<CurrentTenant> CurrentTenant() => (await CurrentUser())?.Tenant;

        public async Task<long> TenantId() => (await CurrentTenant())?.Id ?? 0L;

        public async Task<bool> HasSameTenant(ITenantable tenantable)
        {
            var currentUser = await CurrentUser();
            return currentUser != null && Equals(tenantable.TenantId, currentUser.Tenant.Id);
        }

        public async Task CheckSameTenant(ITenantable tenantable)
        {
            if (!await HasSameTenant(tenantable))
            {
                throw new TenantException();
            }
        }
    }
}