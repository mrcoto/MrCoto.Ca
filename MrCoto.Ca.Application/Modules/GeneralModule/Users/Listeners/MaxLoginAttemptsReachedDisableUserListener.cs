using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using MrCoto.Ca.Application.Common.CurrentUsers;
using MrCoto.Ca.Application.Common.Events;
using MrCoto.Ca.Application.Modules.GeneralModule.Users.Commands.DisableUser;
using MrCoto.Ca.Domain.Modules.GeneralModule.Constants;
using MrCoto.Ca.Domain.Modules.GeneralModule.Users.Events;

namespace MrCoto.Ca.Application.Modules.GeneralModule.Users.Listeners
{
    public class MaxLoginAttemptsReachedDisableUserListener : INotificationHandler<DomainEventNotification<MaxLoginAttemptsReached>>
    {
        private readonly IMediator _mediator;
        private readonly ICurrentUserService _currentUserService;
        private readonly ILogger<MaxLoginAttemptsReachedDisableUserListener> _logger;

        public MaxLoginAttemptsReachedDisableUserListener(IMediator mediator,
            ICurrentUserService currentUserService,
            ILogger<MaxLoginAttemptsReachedDisableUserListener> logger)
        {
            _mediator = mediator;
            _currentUserService = currentUserService;
            _logger = logger;
        }

        public async Task Handle(DomainEventNotification<MaxLoginAttemptsReached> notification, CancellationToken cancellationToken)
        {
            var user = notification.DomainEvent.User;
            _logger.LogInformation($"User {user.Name} reached max login attempts");

            var currentUserId = (await _currentUserService.CurrentUser())?.Id ?? user.Id;
            var command = new DisableUserCommand()
            {
                DisablementTypeId = GeneralConstants.DisablementBlockedByLoginTriesId,
                Observation = "",
                ToDisableId = user.Id,
                DisabledById = currentUserId
            };
            
            await _mediator.Send(command, cancellationToken);
        }
    }
}