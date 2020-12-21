using System;
using Bogus;
using MrCoto.Ca.Domain.Modules.GeneralModule.Users;

namespace MrCoto.Ca.WebApiTests.Modules.GeneralModule.Users.FakeData
{
    public class RefreshTokenFake
    {
        public readonly Faker<RefreshToken> Builder;

        public RefreshTokenFake(User user)
        {
            Builder = new Faker<RefreshToken>("es")
                    .StrictMode(true)
                    .Ignore(x => x.Id)
                    .RuleFor(x => x.Token, f => Guid.NewGuid().ToString())
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