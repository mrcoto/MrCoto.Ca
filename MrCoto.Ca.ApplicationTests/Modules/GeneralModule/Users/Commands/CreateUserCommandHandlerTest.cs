using System.Threading.Tasks;
using Bogus;
using Moq;
using MrCoto.Ca.Application.Common.Exceptions;
using MrCoto.Ca.Application.Modules.GeneralModule;
using MrCoto.Ca.Application.Modules.GeneralModule.Users.Commands.Create;
using MrCoto.Ca.Application.Modules.GeneralModule.Users.Exceptions;
using MrCoto.Ca.Application.Modules.GeneralModule.Users.Services;
using MrCoto.Ca.ApplicationTests.Modules.GeneralModule.Users.FakeData;
using MrCoto.Ca.Domain.Modules.GeneralModule.Users;
using MrCoto.Ca.Domain.Modules.GeneralModule.Users.Events;
using Xunit;

namespace MrCoto.Ca.ApplicationTests.Modules.GeneralModule.Users.Commands
{
    public class CreateUserCommandHandlerTest
    {
        [Fact]
        public async Task ShouldThrow_ExistingAccountException()
        {
            var request = FakeRequest();
            var user = new UserFake()
                .Builder
                .RuleFor(x => x.Email, request.Email)
                .Generate();
            
            var uowGeneralMock = new Mock<IUowGeneral>();
            uowGeneralMock.Setup(x =>
                x.UserRepository.FindByEmail(request.Email)).ReturnsAsync(user);
            var passwordServiceMock = new Mock<IPasswordService>();
            
            var handler = new CreateUserCommandHandler(uowGeneralMock.Object, passwordServiceMock.Object);

            await Assert.ThrowsAsync<ExistingAccountException>(() =>
                handler.Handle(request, default));
        }
        
        [Fact]
        public async Task ShouldThrow_EntityNotFoundException_OnTenant()
        {
            var request = FakeRequest();
            
            var uowGeneralMock = new Mock<IUowGeneral>();
            uowGeneralMock.Setup(x =>
                x.UserRepository.FindByEmail(request.Email)).ReturnsAsync(default(User));
            uowGeneralMock.Setup(x =>
                x.TenantRepository.FindByCode(request.TenantCode)).ReturnsAsync(default(Tenant));
            var passwordServiceMock = new Mock<IPasswordService>();
            
            var handler = new CreateUserCommandHandler(uowGeneralMock.Object, passwordServiceMock.Object);

            await Assert.ThrowsAsync<EntityNotFoundException>(() =>
                handler.Handle(request, default));
        }
        
        [Fact]
        public async Task ShouldThrow_EntityNotFoundException_OnRole()
        {
            var request = FakeRequest();
            var tenant = new TenantFake().Builder
                .RuleFor(x => x.Code, f => request.TenantCode)
                .Generate();
            
            var uowGeneralMock = new Mock<IUowGeneral>();
            uowGeneralMock.Setup(x =>
                x.UserRepository.FindByEmail(request.Email)).ReturnsAsync(default(User));
            uowGeneralMock.Setup(x =>
                x.TenantRepository.FindByCode(request.TenantCode)).ReturnsAsync(tenant);
            uowGeneralMock.Setup(x =>
                x.RoleRepository.FindByCode(request.RoleCode)).ReturnsAsync(default(Role));
            var passwordServiceMock = new Mock<IPasswordService>();
            
            var handler = new CreateUserCommandHandler(uowGeneralMock.Object, passwordServiceMock.Object);

            await Assert.ThrowsAsync<EntityNotFoundException>(() =>
                handler.Handle(request, default));
        }
        
        [Fact]
        public async Task Should_CreateUser()
        {
            var request = FakeRequest();
            var tenant = new TenantFake().Builder
                .RuleFor(x => x.Code, f => request.TenantCode)
                .Generate();
            var role = new RoleFake().Builder
                .RuleFor(x => x.Code, f => request.RoleCode)
                .Generate();
            
            var uowGeneralMock = new Mock<IUowGeneral>();
            uowGeneralMock.Setup(x =>
                x.UserRepository.FindByEmail(request.Email)).ReturnsAsync(default(User));
            uowGeneralMock.Setup(x =>
                x.TenantRepository.FindByCode(request.TenantCode)).ReturnsAsync(tenant);
            uowGeneralMock.Setup(x =>
                x.RoleRepository.FindByCode(request.RoleCode)).ReturnsAsync(role);
            var passwordServiceMock = new Mock<IPasswordService>();
            
            var handler = new CreateUserCommandHandler(uowGeneralMock.Object, passwordServiceMock.Object);

            var user = await handler.Handle(request, default);
            
            Assert.NotNull(user);
            Assert.NotNull(user.FindEvent(typeof(UserRegistered)));
            uowGeneralMock.Verify(x => x.UserRepository.Create(user), Times.Once);
            uowGeneralMock.Verify(x => x.SaveChanges(), Times.Once);
        }
        
        [Fact]
        public async Task Should_CreateUser_WithDefaultTenant()
        {
            var request = FakeRequest();
            request.TenantCode = string.Empty;
            var role = new RoleFake().Builder
                .RuleFor(x => x.Code, f => request.RoleCode)
                .Generate();
            
            var uowGeneralMock = new Mock<IUowGeneral>();
            uowGeneralMock.Setup(x =>
                x.UserRepository.FindByEmail(request.Email)).ReturnsAsync(default(User));
            uowGeneralMock.Setup(x =>
                x.TenantRepository.FindByCode(It.IsAny<string>())).ReturnsAsync(default(Tenant));
            uowGeneralMock.Setup(x =>
                x.RoleRepository.FindByCode(request.RoleCode)).ReturnsAsync(role);
            var passwordServiceMock = new Mock<IPasswordService>();
            
            var handler = new CreateUserCommandHandler(uowGeneralMock.Object, passwordServiceMock.Object);

            var user = await handler.Handle(request, default);
            
            Assert.NotNull(user);
            Assert.NotNull(user.FindEvent(typeof(UserRegistered)));
            uowGeneralMock.Verify(x => x.TenantRepository.Create(It.IsAny<Tenant>()));
            uowGeneralMock.Verify(x => x.UserRepository.Create(user), Times.Once);
            uowGeneralMock.Verify(x => x.SaveChanges(), Times.Once);
        }

        private CreateUserCommand FakeRequest()
        {
            return new Faker<CreateUserCommand>()
                .RuleFor(x => x.Name, f => f.Name.FullName())
                .RuleFor(x => x.Email, f => f.Internet.Email())
                .RuleFor(x => x.TenantCode, f => f.Random.String(1, 10))
                .RuleFor(x => x.RoleCode, f => f.Random.String(1, 10))
                .RuleFor(x => x.Password, f => f.Random.String(6, 20))
                .Generate();
        }
    }
}