using System.Threading.Tasks;
using MrCoto.Ca.Application.Common.Repositories;
using MrCoto.Ca.Domain.Modules.GeneralModule.Users;

namespace MrCoto.Ca.Application.Modules.GeneralModule.Users.Repositories
{
    public interface IUserLoginRepository : IRepository<UserLogin, long>
    {
        public Task<UserLogin> LastUserLogin(long userId);
    }
}