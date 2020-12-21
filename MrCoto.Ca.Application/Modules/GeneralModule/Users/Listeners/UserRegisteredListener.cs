using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using MrCoto.Ca.Application.Common.Events;
using MrCoto.Ca.Application.Common.Mail;
using MrCoto.Ca.Application.Common.Mail.Data;
using MrCoto.Ca.Application.Modules.GeneralModule.Users.Mails.Register;
using MrCoto.Ca.Domain.Modules.GeneralModule.Users.Events;

namespace MrCoto.Ca.Application.Modules.GeneralModule.Users.Listeners
{
    public class UserRegisteredListener : INotificationHandler<DomainEventNotification<UserRegistered>>
    {
        private readonly ILogger<UserRegisteredListener> _logger;
        private readonly IMailService _mailService;

        public UserRegisteredListener(ILogger<UserRegisteredListener> logger, IMailService mailService)
        {
            _logger = logger;
            _mailService = mailService;
        }

        public async Task Handle(DomainEventNotification<UserRegistered> notification, CancellationToken cancellationToken)
        {
            var user = notification.DomainEvent.User;
            _logger.LogInformation($"User {user.Name} Registered");
            
            var registerData = new RegisterMailData() { Username = user.Name };
            var templateData = new MailTemplateData(user.Email, "Bienvenido a Clean Architecture Example!");
            await _mailService.Enqueue(templateData, typeof(IRegisterMail), registerData);
        }
    }
}