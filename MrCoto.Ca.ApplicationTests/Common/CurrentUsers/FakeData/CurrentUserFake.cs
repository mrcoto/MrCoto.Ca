using Bogus;
using MrCoto.Ca.Application.Common.CurrentUsers.Response;

namespace MrCoto.Ca.ApplicationTests.Common.CurrentUsers.FakeData
{
    public class CurrentUserFake
    {
        public readonly Faker<CurrentUser> Builder;
        public readonly CurrentTenantFake CurrentTenantFake;
        public readonly CurrentRoleFake CurrentRoleFake;

        public CurrentUserFake()
        {
            CurrentTenantFake = new CurrentTenantFake();
            var tenant = CurrentTenantFake.Builder.Generate();
            
            CurrentRoleFake = new CurrentRoleFake();
            var role = CurrentRoleFake.Builder.Generate();
            
            Builder = new Faker<CurrentUser>("es")
                    .StrictMode(true)
                    .RuleFor(x => x.Id, f => (long)f.Random.Number(1, 10_000_000))
                    .RuleFor(x => x.Name, f => f.Name.FullName())
                    .RuleFor(x => x.Email, f => f.Internet.Email())
                    .RuleFor(x => x.Role, f => role)
                    .RuleFor(x => x.Tenant, f => tenant)
                ;
        }
    }
}