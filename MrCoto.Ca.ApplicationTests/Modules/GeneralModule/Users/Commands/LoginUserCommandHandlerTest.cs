using System.Threading.Tasks;
using Bogus;
using Moq;
using MrCoto.Ca.Application.Modules.GeneralModule;
using MrCoto.Ca.Application.Modules.GeneralModule.Users.Commands.Login;
using MrCoto.Ca.Application.Modules.GeneralModule.Users.Exceptions;
using MrCoto.Ca.Application.Modules.GeneralModule.Users.Services;
using MrCoto.Ca.ApplicationTests.Modules.GeneralModule.Users.FakeData;
using MrCoto.Ca.Domain.Modules.GeneralModule.Constants;
using MrCoto.Ca.Domain.Modules.GeneralModule.Users;
using MrCoto.Ca.Domain.Modules.GeneralModule.Users.Events;
using Xunit;

namespace MrCoto.Ca.ApplicationTests.Modules.GeneralModule.Users.Commands
{
    public class LoginUserCommandHandlerTest
    {
        [Fact]
        public async Task ShouldThrow_InvalidAccountException_OnNotFoundUser()
        {
            var request = FakeRequest();

            var uowGeneralMock = new Mock<IUowGeneral>();
            uowGeneralMock.Setup(x =>
                x.UserRepository.FindByEmail(request.Email)).ReturnsAsync(default(User));
            var passwordServiceMock = new Mock<IPasswordService>();
            var accessTokenServiceMock = new Mock<IAccessTokenService>();
            var refreshTokenService = new RefreshTokenService(uowGeneralMock.Object);

            var handler = new LoginUserCommandHandler(uowGeneralMock.Object, passwordServiceMock.Object,
                accessTokenServiceMock.Object, refreshTokenService);

            await Assert.ThrowsAsync<InvalidAccountException>(() =>
                handler.Handle(request, default));
        }

        [Fact]
        public async Task ShouldThrow_InvalidAccountException_OnDisabledAccount()
        {
            var request = FakeRequest();
            var user = new UserFake().Builder
                .RuleFor(x => x.DisabledAccountAt, f => f.Date.Recent())
                .Generate();

            var uowGeneralMock = new Mock<IUowGeneral>();
            uowGeneralMock.Setup(x =>
                x.UserRepository.FindByEmail(request.Email)).ReturnsAsync(user);
            var passwordServiceMock = new Mock<IPasswordService>();
            var accessTokenServiceMock = new Mock<IAccessTokenService>();
            var refreshTokenService = new RefreshTokenService(uowGeneralMock.Object);

            var handler = new LoginUserCommandHandler(uowGeneralMock.Object, passwordServiceMock.Object,
                accessTokenServiceMock.Object, refreshTokenService);

            await Assert.ThrowsAsync<InvalidAccountException>(() =>
                handler.Handle(request, default));
        }

        [Fact]
        public async Task ShouldThrow_LoginMaxAttemptsReachedException()
        {
            var request = FakeRequest();
            var loginMaxAttempts = new LoginMaxAttemptFake().Builder.Generate();
            var user = new UserFake().Builder
                .RuleFor(x => x.Email, f => request.Email)
                .RuleFor(x => x.LoginAttempts, f => loginMaxAttempts.MaxAttempts + 1)
                .Generate();

            var uowGeneralMock = new Mock<IUowGeneral>();
            uowGeneralMock.Setup(x =>
                x.UserRepository.FindByEmail(request.Email)).ReturnsAsync(user);
            uowGeneralMock.Setup(x =>
                x.LoginMaxAttemptRepository.Find(GeneralConstants.DefaultId)).ReturnsAsync(loginMaxAttempts);
            var passwordServiceMock = new Mock<IPasswordService>();
            var accessTokenServiceMock = new Mock<IAccessTokenService>();
            var refreshTokenService = new RefreshTokenService(uowGeneralMock.Object);

            var handler = new LoginUserCommandHandler(uowGeneralMock.Object, passwordServiceMock.Object,
                accessTokenServiceMock.Object, refreshTokenService);

            await Assert.ThrowsAsync<LoginMaxAttemptsReachedException>(() =>
                handler.Handle(request, default));
        }
        
        [Fact]
        public async Task ShouldThrow_InvalidAccountException_OnPassword()
        {
            var request = FakeRequest();
            var loginMaxAttempts = new LoginMaxAttemptFake().Builder.Generate();
            var user = new UserFake().Builder
                .RuleFor(x => x.Email, f => request.Email)
                .Generate();

            var uowGeneralMock = new Mock<IUowGeneral>();
            uowGeneralMock.Setup(x =>
                x.UserRepository.FindByEmail(request.Email)).ReturnsAsync(user);
            uowGeneralMock.Setup(x =>
                x.LoginMaxAttemptRepository.Find(GeneralConstants.DefaultId)).ReturnsAsync(loginMaxAttempts);
            var passwordServiceMock = new Mock<IPasswordService>();
            passwordServiceMock.Setup(x =>
                x.Verify(request.Password, user.Password)).Returns(false);
            var accessTokenServiceMock = new Mock<IAccessTokenService>();
            var refreshTokenService = new RefreshTokenService(uowGeneralMock.Object);

            var handler = new LoginUserCommandHandler(uowGeneralMock.Object, passwordServiceMock.Object,
                accessTokenServiceMock.Object, refreshTokenService);

            await Assert.ThrowsAsync<InvalidAccountException>(() =>
                handler.Handle(request, default));

            Assert.Equal(1, user.LoginAttempts);
            Assert.Null(user.FindEvent(typeof(MaxLoginAttemptsReached)));
            uowGeneralMock.Verify(x => x.SaveChanges(), Times.Once);
        }

        [Fact]
        public async Task ShouldThrow_InvalidAccountException_OnPassword_And_RaiseEvent_MaxLoginAttemptsReached()
        {
            var request = FakeRequest();
            var loginMaxAttempts = new LoginMaxAttemptFake().Builder.Generate();
            var user = new UserFake().Builder
                .RuleFor(x => x.LoginAttempts, f => loginMaxAttempts.MaxAttempts - 1)
                .RuleFor(x => x.Email, f => request.Email)
                .Generate();

            var uowGeneralMock = new Mock<IUowGeneral>();
            uowGeneralMock.Setup(x =>
                x.UserRepository.FindByEmail(request.Email)).ReturnsAsync(user);
            uowGeneralMock.Setup(x =>
                x.LoginMaxAttemptRepository.Find(GeneralConstants.DefaultId)).ReturnsAsync(loginMaxAttempts);
            var passwordServiceMock = new Mock<IPasswordService>();
            passwordServiceMock.Setup(x =>
                x.Verify(request.Password, user.Password)).Returns(false);
            var accessTokenServiceMock = new Mock<IAccessTokenService>();
            var refreshTokenService = new RefreshTokenService(uowGeneralMock.Object);

            var handler = new LoginUserCommandHandler(uowGeneralMock.Object, passwordServiceMock.Object,
                accessTokenServiceMock.Object, refreshTokenService);

            await Assert.ThrowsAsync<InvalidAccountException>(() =>
                handler.Handle(request, default));

            Assert.Equal(loginMaxAttempts.MaxAttempts, user.LoginAttempts);
            Assert.NotNull(user.FindEvent(typeof(MaxLoginAttemptsReached)));
            uowGeneralMock.Verify(x => x.SaveChanges(), Times.Once);
        }
        
        [Fact]
        public async Task Should_LoginUser()
        {
            var request = FakeRequest();
            var loginMaxAttempts = new LoginMaxAttemptFake().Builder.Generate();
            var user = new UserFake().Builder
                .RuleFor(x => x.Email, f => request.Email)
                .Generate();

            var uowGeneralMock = new Mock<IUowGeneral>();
            uowGeneralMock.Setup(x =>
                x.UserRepository.FindByEmail(request.Email)).ReturnsAsync(user);
            uowGeneralMock.Setup(x =>
                x.LoginMaxAttemptRepository.Find(GeneralConstants.DefaultId)).ReturnsAsync(loginMaxAttempts);
            uowGeneralMock.Setup(x =>
                x.RefreshTokenRepository.Create(It.IsAny<RefreshToken>())).ReturnsAsync(It.IsAny<RefreshToken>());
            var passwordServiceMock = new Mock<IPasswordService>();
            passwordServiceMock.Setup(x =>
                x.Verify(request.Password, user.Password)).Returns(true);
            var accessTokenServiceMock = new Mock<IAccessTokenService>();
            var refreshTokenService = new RefreshTokenService(uowGeneralMock.Object);

            var handler = new LoginUserCommandHandler(uowGeneralMock.Object, passwordServiceMock.Object,
                accessTokenServiceMock.Object, refreshTokenService);

            var response = await handler.Handle(request, default);

            Assert.NotNull(response);
            Assert.NotNull(user.FindEvent(typeof(UserLogged)));
            accessTokenServiceMock.Verify(x => x.GetAccessToken(user), Times.Once);
            uowGeneralMock.Verify(x => x.UserRepository.Update(user), Times.Once);
            uowGeneralMock.Verify(x => x.SaveChanges(), Times.Once);
        }

        private LoginUserCommand FakeRequest()
        {
            return new Faker<LoginUserCommand>()
                .RuleFor(x => x.Email, f => f.Internet.Email())
                .RuleFor(x => x.Password, f => f.Random.String(6, 20))
                .Generate();
        }
    }
}