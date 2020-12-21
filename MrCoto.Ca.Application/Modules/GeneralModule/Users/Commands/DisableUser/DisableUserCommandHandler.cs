using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MrCoto.Ca.Application.Common.Mail;
using MrCoto.Ca.Application.Common.Mail.Data;
using MrCoto.Ca.Application.Modules.GeneralModule.Users.Exceptions;
using MrCoto.Ca.Application.Modules.GeneralModule.Users.Mails.Disablement;
using MrCoto.Ca.Domain.Modules.GeneralModule.Constants;
using MrCoto.Ca.Domain.Modules.GeneralModule.Users;

namespace MrCoto.Ca.Application.Modules.GeneralModule.Users.Commands.DisableUser
{
    public class DisableUserCommandHandler : IRequestHandler<DisableUserCommand, UserDisablement>
    {
        private readonly IUowGeneral _uowGeneral;
        private readonly IMailService _mailService;

        public DisableUserCommandHandler(IUowGeneral uowGeneral, IMailService mailService)
        {
            _uowGeneral = uowGeneral;
            _mailService = mailService;
        }

        public async Task<UserDisablement> Handle(DisableUserCommand request, CancellationToken cancellationToken)
        {
            var userToDisable = await _uowGeneral.UserRepository.Find(request.ToDisableId);
            if (userToDisable.HasDisabledAccount())
            {
                throw new AlreadyDisabledAccountException();
            }
            
            userToDisable.DisabledAccountAt = DateTime.Now;
            await _uowGeneral.UserRepository.Update(userToDisable);

            var disablement = await GenerateDisablement(userToDisable, request);
            
            await _uowGeneral.SaveChanges();

            await SendDisablementMail(userToDisable, disablement);

            return disablement;
        }

        private async Task SendDisablementMail(User user, UserDisablement userDisablement)
        {
            var disablementMailData = new DisablementMailData()
            {
                DisablementTypeId = userDisablement.DisablementTypeId, 
                DisablementType = userDisablement.DisablementType.Description,
                Username = user.Name,
                Observation = userDisablement.Observation
            };
            var subject = userDisablement.DisablementTypeId == GeneralConstants.DisablementUnblockedId
                ? "Su cuenta ha sido habilitada"
                : "Su cuenta ha sido deshabilitada";
            var mailData = new MailTemplateData(user.Email, subject);
            await _mailService.Enqueue(mailData, typeof(IDisablementMail), disablementMailData);
        }

        private async Task<UserDisablement> GenerateDisablement(User user, DisableUserCommand request)
        {
            var disablementType = await _uowGeneral.DisablementTypeRepository.Find(request.DisablementTypeId);
            var disablement = new UserDisablement()
            {
                UserId = user.Id,
                DisablementTypeId = request.DisablementTypeId,
                DisablementType = disablementType,
                Observation = request.Observation,
                AuthUserId = request.DisabledById,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
            await _uowGeneral.UserDisablementRepository.Create(disablement);
            return disablement;
        }
    }
}