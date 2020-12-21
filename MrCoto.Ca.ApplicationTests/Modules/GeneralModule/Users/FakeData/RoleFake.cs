using Bogus;
using MrCoto.Ca.Domain.Modules.GeneralModule.Users;

namespace MrCoto.Ca.ApplicationTests.Modules.GeneralModule.Users.FakeData
{
    public class RoleFake
    {
        public readonly Faker<Role> Builder;

        public RoleFake()
        {
            Builder = new Faker<Role>("es")
                    .StrictMode(true)
                    .RuleFor(x => x.Id, f => (long)f.Random.Number(1, 10_000_000))
                    .RuleFor(x => x.Code, f => f.Name.FullName())
                    .RuleFor(x => x.Name, f => f.Name.FullName())
                    .RuleFor(x => x.DeletedAt, f => null)
                ;
        }
    }
}