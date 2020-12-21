using System;
using Bogus;
using MrCoto.Ca.Domain.Modules.GeneralModule.Users;

namespace MrCoto.Ca.ApplicationTests.Modules.GeneralModule.Users.FakeData
{
    public class UserFake
    {
        public readonly Faker<User> Builder;
        public readonly TenantFake TenantFake;
        public readonly RoleFake RoleFake;
        
        public UserFake()
        {
            TenantFake = new TenantFake();
            var tenant = TenantFake.Builder.Generate();
            
            RoleFake = new RoleFake();
            var role = RoleFake.Builder.Generate();
            
            Builder = new Faker<User>("es")
                .StrictMode(true)
                .RuleFor(x => x.Id, f => (long)f.Random.Number(1, 10_000_000))
                .RuleFor(x => x.Name, f => f.Name.FullName())
                .RuleFor(x => x.Email, f => f.Internet.Email())
                .RuleFor(x => x.Password, f => f.Random.String(6, 20))
                .RuleFor(x => x.TenantId, f => tenant.Id)
                .RuleFor(x => x.Tenant, f => tenant)
                .RuleFor(x => x.RoleId, f => role.Id)
                .RuleFor(x => x.Role, f => role)
                .RuleFor(x => x.LoginAttempts, f => 0)
                .RuleFor(x => x.DisabledAccountAt, f => null)
                .RuleFor(x => x.LastLoginAt, f => null)
                .RuleFor(x => x.LastLoginAt, f => null)
                .RuleFor(x => x.DeletedAt, f => null)
                .RuleFor(x => x.CreatedAt, f => DateTime.Now)
                .RuleFor(x => x.UpdatedAt, f => DateTime.Now)
                ;
        }
    }
}