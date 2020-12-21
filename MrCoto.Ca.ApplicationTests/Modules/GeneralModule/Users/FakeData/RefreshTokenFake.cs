using System;
using Bogus;
using MrCoto.Ca.Domain.Modules.GeneralModule.Users;

namespace MrCoto.Ca.ApplicationTests.Modules.GeneralModule.Users.FakeData
{
    public class RefreshTokenFake
    {
        public readonly Faker<RefreshToken> Builder;
        public readonly Faker<User> UserFake;

        public RefreshTokenFake()
        {
            UserFake = new Faker<User>();
            var user = UserFake.Generate();
            
            Builder = new Faker<RefreshToken>("es")
                    .StrictMode(true)
                    .RuleFor(x => x.Id, f => (long)f.Random.Number(1, 10_000_000))
                    .RuleFor(x => x.Token, f => f.Random.String(1, 100))
                    .RuleFor(x => x.ExpiresAt, f => f.Date.Future())
                    .RuleFor(x => x.UserId, f => user.Id)
                    .RuleFor(x => x.User, f => user)
                    .RuleFor(x => x.TenantId, f => user.TenantId)
                    .RuleFor(x => x.DeletedAt, f => null)
                    .RuleFor(x => x.CreatedAt, f => DateTime.Now)
                    .RuleFor(x => x.UpdatedAt, f => DateTime.Now)
                ;
        }
    }
}