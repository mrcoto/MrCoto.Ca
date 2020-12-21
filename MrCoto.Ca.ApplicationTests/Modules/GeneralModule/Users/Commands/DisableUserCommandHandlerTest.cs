using System.Threading.Tasks;
using Bogus;
using Moq;
using MrCoto.Ca.Application.Common.Mail;
using MrCoto.Ca.Application.Common.Mail.Data;
using MrCoto.Ca.Application.Modules.GeneralModule;
using MrCoto.Ca.Application.Modules.GeneralModule.Users.Commands.DisableUser;
using MrCoto.Ca.Application.Modules.GeneralModule.Users.Exceptions;
using MrCoto.Ca.Application.Modules.GeneralModule.Users.Mails.Disablement;
using MrCoto.Ca.ApplicationTests.Modules.GeneralModule.Users.FakeData;
using MrCoto.Ca.Domain.Modules.GeneralModule.Users;
using Xunit;

namespace MrCoto.Ca.ApplicationTests.Modules.GeneralModule.Users.Commands
{
    public class DisableUserCommandHandlerTest
    {
        [Fact]
        public async Task ShouldThrow_AlreadyDisabledAccountException()
        {
            var request = FakeRequest();
            var user = new UserFake().Builder
                .RuleFor(x => x.Id, f => request.ToDisableId)
                .RuleFor(x => x.DisabledAccountAt, f => f.Date.Recent())
                .Generate();
            
            var uowGeneralMock = new Mock<IUowGeneral>();
            uowGeneralMock.Setup(x => x.UserRepository.Find(user.Id)).ReturnsAsync(user);
            var mailServiceMock = new Mock<IMailService>();
            
            var handler = new DisableUserCommandHandler(uowGeneralMock.Object, mailServiceMock.Object);

            await Assert.ThrowsAsync<AlreadyDisabledAccountException>(() => 
                handler.Handle(request, default));
        }
        
        [Fact]
        public async Task Should_GenerateDisablementOrEnablement()
        {
            var request = FakeRequest();
            var user = new UserFake().Builder
                .RuleFor(x => x.Id, f => request.ToDisableId)
                .Generate();
            var disablementType = new DisablementTypeFake().Builder
                .RuleFor(x => x.Id, f => request.DisablementTypeId)
                .Generate();
            
            var uowGeneralMock = new Mock<IUowGeneral>();
            uowGeneralMock.Setup(x => x.UserRepository.Find(user.Id)).ReturnsAsync(user);
            uowGeneralMock.Setup(x => x.DisablementTypeRepository.Find(disablementType.Id)).ReturnsAsync(disablementType);
            uowGeneralMock.Setup(x => x.UserDisablementRepository.Create(It.IsAny<UserDisablement>())).ReturnsAsync(It.IsAny<UserDisablement>());
            var mailServiceMock = new Mock<IMailService>();
            
            var handler = new DisableUserCommandHandler(uowGeneralMock.Object, mailServiceMock.Object);

            var response = await handler.Handle(request, default);
            
            Assert.NotNull(user.DisabledAccountAt);
            Assert.Equal(user.Id, response.UserId);
            uowGeneralMock.Verify(x => x.UserRepository.Update(user), Times.Once);
            uowGeneralMock.Verify(x => 
                x.UserDisablementRepository.Create(It.IsAny<UserDisablement>()), Times.Once);
            uowGeneralMock.Verify(x => x.SaveChanges(), Times.Once);
            mailServiceMock.Verify(x => 
                x.Enqueue(It.IsAny<MailTemplateData>(), typeof(IDisablementMail), It.IsAny<DisablementMailData>()),
                Times.Once);
        }
        
        private DisableUserCommand FakeRequest()
        {
            return new Faker<DisableUserCommand>("es")
                .StrictMode(true)
                .RuleFor(x => x.DisablementTypeId, f => f.Random.Number(1, 3))
                .RuleFor(x => x.Observation, f => f.Random.String(1, 200))
                .RuleFor(x => x.ToDisableId, f => f.Random.Number(1, 10))
                .RuleFor(x => x.DisabledById, f => f.Random.Number(1, 10))
                ;
        }
        
    }
}