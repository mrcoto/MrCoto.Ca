using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MrCoto.Ca.Application.Modules.GeneralModule.Users.Repositories;
using MrCoto.Ca.Domain.Modules.GeneralModule.Users;
using MrCoto.Ca.Infrastructure.Common.Repositories;

namespace MrCoto.Ca.Infrastructure.Modules.GeneralModule.Users.Repositories
{
    public class UserRepository : Repository<User, long>, IUserRepository
    {
        public UserRepository(SimpleAgendaContext context) : base(context)
        {
        }

        public async Task<User> FindByEmail(string email)
        {
            return await Context.Users.FirstOrDefaultAsync(x => x.Email == email && x.DeletedAt == null);
        }
    }
}