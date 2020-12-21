using System;
using Bogus;
using MrCoto.Ca.Application.Common.CurrentUsers.Response;

namespace MrCoto.Ca.ApplicationTests.Common.CurrentUsers.FakeData
{
    public class CurrentTenantFake
    {
        public readonly Faker<CurrentTenant> Builder;

        public CurrentTenantFake()
        {
            Builder = new Faker<CurrentTenant>("es")
                    .StrictMode(true)
                    .RuleFor(x => x.Id, f => (long)f.Random.Number(1, 10_000_000))
                    .RuleFor(x => x.Code, f => f.Random.String(1, 40))
                    .RuleFor(x => x.Name, f => f.Random.String(1, 80))
                ;
        }
    }
}