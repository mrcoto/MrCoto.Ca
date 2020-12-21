using System.Threading.Tasks;
using MrCoto.Ca.Application.Modules.GeneralModule.Users.Commands.Login;
using MrCoto.Ca.Application.Modules.GeneralModule.Users.Commands.Login.Response;
using MrCoto.Ca.Domain.Modules.GeneralModule.Constants;
using MrCoto.Ca.WebApiTests.Configuration;
using Xunit;

namespace MrCoto.Ca.WebApiTests.Modules.GeneralModule.Users.Controllers
{
    public class LoginUserControllerTest : IClassFixture<WebAppFactory>
    {
        private readonly WebAppFactory _factory;

        public LoginUserControllerTest(WebAppFactory factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Should_LoginUser()
        {
            var client = _factory.CreateClient();
            
            var request = new LoginUserCommand()
            {
                Email = GeneralConstants.DefaultUser,
                Password = GeneralConstants.DefaultPassword
            };
            var json = _factory.Util.AsJsonContent(request);
            var response = await client.PostAsync("/api/login", json);

            response.EnsureSuccessStatusCode();
            var responseMessage = await response.Content.ReadAsStringAsync();

            var loginResponse = _factory.Util.Deserialize<LoginUserResponse>(responseMessage);
            
            Assert.NotNull(loginResponse);
            Assert.NotNull(loginResponse.AccessToken);
            Assert.NotEmpty(loginResponse.AccessToken.Token);
            Assert.NotNull(loginResponse.RefreshToken);
            Assert.NotEmpty(loginResponse.RefreshToken.Token);
        }
    }
}