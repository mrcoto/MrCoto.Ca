using MrCoto.Ca.Application.Common.Repositories;
using MrCoto.Ca.Application.Modules.GeneralModule.Users.Repositories;
using MrCoto.Ca.Domain.Modules.GeneralModule.Users;

namespace MrCoto.Ca.Application.Modules.GeneralModule
{
    public interface IUowGeneral : IUow
    {
        public IRepository<DisablementType, long> DisablementTypeRepository { get; }
        public IRepository<UserDisablement, long> UserDisablementRepository { get; }
        public IRepository<LoginMaxAttempt, long> LoginMaxAttemptRepository { get; }
        public IUserLoginRepository UserLoginRepository { get; }
        public IRefreshTokenRepository RefreshTokenRepository { get; }
        public IUserRepository UserRepository { get; }
        public ITenantRepository TenantRepository { get; }
        public IRoleRepository RoleRepository { get; }
    }
}