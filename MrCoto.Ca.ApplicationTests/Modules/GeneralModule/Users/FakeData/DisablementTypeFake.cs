using System;
using Bogus;
using MrCoto.Ca.Domain.Modules.GeneralModule.Users;

namespace MrCoto.Ca.ApplicationTests.Modules.GeneralModule.Users.FakeData
{
    public class DisablementTypeFake
    {
        public readonly Faker<DisablementType> Builder;

        public DisablementTypeFake()
        {
            Builder = new Faker<DisablementType>("es")
                    .StrictMode(true)
                    .RuleFor(x => x.Id, f => (long)f.Random.Number(1, 10_000_000))
                    .RuleFor(x => x.Description, f => f.Random.String(1, 40))
                    .RuleFor(x => x.DeletedAt, f => null)
                    .RuleFor(x => x.CreatedAt, f => DateTime.Now)
                    .RuleFor(x => x.UpdatedAt, f => DateTime.Now)
                ;
        }
    }
}