using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MrCoto.Ca.Application.Modules.GeneralModule.Users.Repositories;
using MrCoto.Ca.Domain.Modules.GeneralModule.Users;
using MrCoto.Ca.Infrastructure.Common.Repositories;

namespace MrCoto.Ca.Infrastructure.Modules.GeneralModule.Users.Repositories
{
    public class RoleRepository : Repository<Role, long>, IRoleRepository
    {
        public RoleRepository(SimpleAgendaContext context) : base(context)
        {
        }

        public async Task<Role> FindByCode(string code)
        {
            return await Context.Roles.FirstOrDefaultAsync(x => x.Code == code && x.DeletedAt == null);
        }
    }
}