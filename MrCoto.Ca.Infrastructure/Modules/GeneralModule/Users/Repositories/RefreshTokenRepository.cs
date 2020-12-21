using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MrCoto.Ca.Application.Modules.GeneralModule.Users.Repositories;
using MrCoto.Ca.Domain.Modules.GeneralModule.Users;
using MrCoto.Ca.Infrastructure.Common.Repositories;

namespace MrCoto.Ca.Infrastructure.Modules.GeneralModule.Users.Repositories
{
    public class RefreshTokenRepository : Repository<RefreshToken, long>, IRefreshTokenRepository
    {
        public RefreshTokenRepository(CaContext context) : base(context)
        {
        }

        public async Task<RefreshToken> FindByToken(string token)
        {
            return await Context.RefreshTokens.FirstOrDefaultAsync(
                x => x.Token == token && x.DeletedAt == null);
        }
    }
}