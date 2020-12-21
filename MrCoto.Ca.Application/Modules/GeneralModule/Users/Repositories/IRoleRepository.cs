using System.Threading.Tasks;
using MrCoto.Ca.Application.Common.Repositories;
using MrCoto.Ca.Domain.Modules.GeneralModule.Users;

namespace MrCoto.Ca.Application.Modules.GeneralModule.Users.Repositories
{
    public interface IRoleRepository : IRepository<Role, long>
    {
        public Task<Role> FindByCode(string code);
    }
}