using MrCoto.Ca.Application.Common.Events;
using MrCoto.Ca.Application.Common.Repositories;
using MrCoto.Ca.Application.Modules.GeneralModule;
using MrCoto.Ca.Application.Modules.GeneralModule.Users.Repositories;
using MrCoto.Ca.Domain.Modules.GeneralModule.Users;
using MrCoto.Ca.Infrastructure.Common.Repositories;
using MrCoto.Ca.Infrastructure.Modules.GeneralModule.Users.Repositories;

namespace MrCoto.Ca.Infrastructure.Modules.GeneralModule.Configuration
{
    public class UowGeneral : Uow, IUowGeneral
    {
        public IRepository<DisablementType, long> DisablementTypeRepository => new Repository<DisablementType, long>(Context);
        public IRepository<UserDisablement, long> UserDisablementRepository => new Repository<UserDisablement, long>(Context);
        public IRepository<LoginMaxAttempt, long> LoginMaxAttemptRepository => new Repository<LoginMaxAttempt, long>(Context);
        public IUserLoginRepository UserLoginRepository => new UserLoginRepository(Context);
        public IRefreshTokenRepository RefreshTokenRepository => new RefreshTokenRepository(Context);
        public IUserRepository UserRepository => new UserRepository(Context);
        public ITenantRepository TenantRepository => new TenantRepository(Context);
        public IRoleRepository RoleRepository => new RoleRepository(Context);

        public UowGeneral(SimpleAgendaContext context, IDomainEventPublisher eventPublisher) : base(context, eventPublisher)
        {
        }
    }
}