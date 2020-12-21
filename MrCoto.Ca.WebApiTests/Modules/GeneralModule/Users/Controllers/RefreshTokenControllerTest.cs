using System.Threading.Tasks;
using MrCoto.Ca.Application.Modules.GeneralModule.Users.Commands.RefreshToken;
using MrCoto.Ca.Application.Modules.GeneralModule.Users.Commands.RefreshToken.Response;
using MrCoto.Ca.WebApiTests.Configuration;
using MrCoto.Ca.WebApiTests.Modules.GeneralModule.Users.FakeData;
using Xunit;

namespace MrCoto.Ca.WebApiTests.Modules.GeneralModule.Users.Controllers
{
    public class RefreshTokenControllerTest : IClassFixture<WebAppFactory>
    {
        private readonly WebAppFactory _factory;

        public RefreshTokenControllerTest(WebAppFactory factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Should_RefreshToken()
        {
            var client = _factory.CreateClient();
            
            var user = await _factory.Util.GetUser();
            var refreshToken = new RefreshTokenFake(user).Builder.Generate();
            await _factory.Util.SaveEntity(refreshToken);
            
            var request = new RefreshTokenCommand(){ Token = refreshToken.Token };
            var json = _factory.Util.AsJsonContent(request);
            var response = await client.PostAsync("api/refresh_token", json);

            response.EnsureSuccessStatusCode();
            var responseMessage = await response.Content.ReadAsStringAsync();
            var refreshTokenResponse = _factory.Util.Deserialize<RefreshTokenResponse>(responseMessage);
            
            Assert.NotNull(refreshTokenResponse);
            Assert.NotNull(refreshTokenResponse.AccessToken);
            Assert.NotEmpty(refreshTokenResponse.AccessToken.Token);
            Assert.NotNull(refreshTokenResponse.RefreshToken);
            Assert.NotEmpty(refreshTokenResponse.AccessToken.Token);
        }
    }
}