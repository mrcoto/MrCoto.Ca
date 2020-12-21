using System.Threading.Tasks;
using MrCoto.Ca.Application.Common.Repositories;
using MrCoto.Ca.Domain.Modules.GeneralModule.Users;

namespace MrCoto.Ca.Application.Modules.GeneralModule.Users.Repositories
{
    public interface ITenantRepository : IRepository<Tenant, long>
    {
        public Task<Tenant> FindByCode(string code);
    }
}