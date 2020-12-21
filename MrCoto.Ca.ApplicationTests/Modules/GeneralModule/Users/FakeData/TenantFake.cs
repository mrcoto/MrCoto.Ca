using System;
using Bogus;
using MrCoto.Ca.Domain.Modules.GeneralModule.Users;

namespace MrCoto.Ca.ApplicationTests.Modules.GeneralModule.Users.FakeData
{
    public class TenantFake
    {
        public readonly Faker<Tenant> Builder;

        public TenantFake()
        {
            Builder = new Faker<Tenant>("es")
                    .StrictMode(true)
                    .RuleFor(x => x.Id, f => (long)f.Random.Number(1, 10_000_000))
                    .RuleFor(x => x.Code, f => f.Name.FullName())
                    .RuleFor(x => x.Name, f => f.Name.FullName())
                    .RuleFor(x => x.DeletedAt, f => null)
                    .RuleFor(x => x.CreatedAt, f => DateTime.Now)
                    .RuleFor(x => x.UpdatedAt, f => DateTime.Now)
                ;
        }
    }
}