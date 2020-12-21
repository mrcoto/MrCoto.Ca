using Bogus;
using MrCoto.Ca.Application.Common.CurrentUsers.Response;

namespace MrCoto.Ca.ApplicationTests.Common.CurrentUsers.FakeData
{
    public class CurrentRoleFake
    {
        public readonly Faker<CurrentRole> Builder;

        public CurrentRoleFake()
        {
            Builder = new Faker<CurrentRole>("es")
                    .StrictMode(true)
                    .RuleFor(x => x.Id, f => (long)f.Random.Number(1, 10_000_000))
                    .RuleFor(x => x.Code, f => f.Random.String(1, 40))
                    .RuleFor(x => x.Name, f => f.Random.String(1, 80))
                ;
        }
    }
}