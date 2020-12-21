using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MrCoto.Ca.Application.Modules.GeneralModule.Users.Repositories;
using MrCoto.Ca.Domain.Modules.GeneralModule.Users;
using MrCoto.Ca.Infrastructure.Common.Repositories;

namespace MrCoto.Ca.Infrastructure.Modules.GeneralModule.Users.Repositories
{
    public class UserLoginRepository : Repository<UserLogin, long>, IUserLoginRepository
    {
        public UserLoginRepository(SimpleAgendaContext context) : base(context)
        {
        }

        public async Task<UserLogin> LastUserLogin(long userId)
        {
            return await Context.UserLogins
                .OrderByDescending(x => x.CreatedAt)
                .FirstOrDefaultAsync(x => x.UserId == userId && x.DeletedAt == null);
        }
    }
}