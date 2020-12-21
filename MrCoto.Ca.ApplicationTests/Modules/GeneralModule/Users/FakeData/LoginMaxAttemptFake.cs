using System;
using Bogus;
using MrCoto.Ca.Domain.Modules.GeneralModule.Users;

namespace MrCoto.Ca.ApplicationTests.Modules.GeneralModule.Users.FakeData
{
    public class LoginMaxAttemptFake
    {
        public readonly Faker<LoginMaxAttempt> Builder;
        public readonly int DefaultMaxAttempts = 3;

        public LoginMaxAttemptFake()
        {
            Builder = new Faker<LoginMaxAttempt>("es")
                    .StrictMode(true)
                    .RuleFor(x => x.Id, f => (long)f.Random.Number(1, 10_000_000))
                    .RuleFor(x => x.MaxAttempts, f => f.Random.Number(2, 10))
                    .RuleFor(x => x.CreatedAt, f => DateTime.Now)
                    .RuleFor(x => x.UpdatedAt, f => DateTime.Now)
                ;
        }
    }
}