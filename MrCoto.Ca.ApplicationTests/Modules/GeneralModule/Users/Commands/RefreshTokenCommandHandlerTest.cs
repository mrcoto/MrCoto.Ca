using System.Threading.Tasks;
using Bogus;
using Moq;
using MrCoto.Ca.Application.Modules.GeneralModule;
using MrCoto.Ca.Application.Modules.GeneralModule.Users.Commands.RefreshToken;
using MrCoto.Ca.Application.Modules.GeneralModule.Users.Exceptions;
using MrCoto.Ca.Application.Modules.GeneralModule.Users.Services;
using MrCoto.Ca.ApplicationTests.Modules.GeneralModule.Users.FakeData;
using MrCoto.Ca.Domain.Modules.GeneralModule.Users;
using Xunit;

namespace MrCoto.Ca.ApplicationTests.Modules.GeneralModule.Users.Commands
{
    public class RefreshTokenCommandHandlerTest
    {
        [Fact]
        public async Task ShouldThrow_InvalidRefreshTokenException_OnNotFoundRefreshToken()
        {
            var request = FakeRequest();
            
            var uowGeneralMock = new Mock<IUowGeneral>();
            uowGeneralMock.Setup(x => x.RefreshTokenRepository.FindByToken(It.IsAny<string>()))
                .ReturnsAsync(default(RefreshToken));
            var accessTokenService = new Mock<IAccessTokenService>();
            var refreshTokenService = new RefreshTokenService(uowGeneralMock.Object);
            
            var handler = new RefreshTokenCommandHandler(
                uowGeneralMock.Object, accessTokenService.Object, refreshTokenService);

            await Assert.ThrowsAsync<InvalidRefreshTokenException>(() => 
                handler.Handle(request, default));
        }
        
        [Fact]
        public async Task ShouldThrow_InvalidRefreshTokenException_OnExpiredRefreshToken()
        {
            var request = FakeRequest();
            var refreshTokenFakeBuilder = new RefreshTokenFake().Builder;
            refreshTokenFakeBuilder.RuleFor(x => x.ExpiresAt, f => f.Date.Past());
            var refreshToken = refreshTokenFakeBuilder.Generate();
            
            var uowGeneralMock = new Mock<IUowGeneral>();
            uowGeneralMock.Setup(x => x.RefreshTokenRepository.FindByToken(request.Token))
                .ReturnsAsync(refreshToken);
            var accessTokenService = new Mock<IAccessTokenService>();
            var refreshTokenService = new RefreshTokenService(uowGeneralMock.Object);
            
            var handler = new RefreshTokenCommandHandler(
                uowGeneralMock.Object, accessTokenService.Object, refreshTokenService);

            await Assert.ThrowsAsync<InvalidRefreshTokenException>(() => 
                handler.Handle(request, default));
        }
        
        [Fact]
        public async Task Should_RefreshToken()
        {
            var request = FakeRequest();
            var refreshToken = new RefreshTokenFake().Builder.Generate();
            
            var uowGeneralMock = new Mock<IUowGeneral>();
            uowGeneralMock.Setup(x => x.RefreshTokenRepository.FindByToken(request.Token))
                .ReturnsAsync(refreshToken);
            var accessTokenService = new Mock<IAccessTokenService>();
            var refreshTokenService = new RefreshTokenService(uowGeneralMock.Object);
            
            var handler = new RefreshTokenCommandHandler(
                uowGeneralMock.Object, accessTokenService.Object, refreshTokenService);

            var response = await handler.Handle(request, default);
            
            Assert.NotNull(response);
            accessTokenService.Verify(x => x.GetAccessToken(refreshToken.User), Times.Once);
            uowGeneralMock.Verify(x => 
                x.RefreshTokenRepository.Create(It.IsNotIn(refreshToken)), Times.Once);
            uowGeneralMock.Verify(x => 
                x.RefreshTokenRepository.Delete(refreshToken), Times.Once);
            uowGeneralMock.Verify(x => x.SaveChanges(), Times.Once);
        }
        
        private RefreshTokenCommand FakeRequest()
        {
            return (new Faker<RefreshTokenCommand>("es"))
                .StrictMode(true)
                .RuleFor(x => x.Token, f => f.Random.String(1, 100))
                .Generate();
        }
        
    }
}