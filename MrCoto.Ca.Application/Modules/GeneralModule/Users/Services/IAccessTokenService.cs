using System.Threading.Tasks;
using MrCoto.Ca.Application.Modules.GeneralModule.Users.Services.Data;
using MrCoto.Ca.Domain.Modules.GeneralModule.Users;

namespace MrCoto.Ca.Application.Modules.GeneralModule.Users.Services
{
    public interface IAccessTokenService
    {
        public Task<AccessToken> GetAccessToken(User user);
    }
}