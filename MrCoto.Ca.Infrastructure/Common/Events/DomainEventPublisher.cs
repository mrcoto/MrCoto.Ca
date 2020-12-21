using System;
using System.Threading.Tasks;
using MediatR;
using MrCoto.Ca.Application.Common.CurrentUsers;
using MrCoto.Ca.Application.Common.Events;
using MrCoto.Ca.Domain.Common.Events;

namespace MrCoto.Ca.Infrastructure.Common.Events
{
    public class DomainEventPublisher : IDomainEventPublisher
    {
        private readonly IMediator _mediator;
        private readonly ICurrentUserService _currentUserService;

        public DomainEventPublisher(IMediator mediator, ICurrentUserService currentUserService)
        {
            _mediator = mediator;
            _currentUserService = currentUserService;
        }
        
        public async Task Publish(DomainEvent domainEvent)
        {
            var currentUser = await _currentUserService.CurrentUser();

            var notificationType = typeof(DomainEventNotification<>);
            Type[] notificationArgs = {domainEvent.GetType()};
            var notificationBuild = notificationType.MakeGenericType(notificationArgs);
            var notification = Activator.CreateInstance(notificationBuild, domainEvent, currentUser)!;

            await _mediator.Publish(new DomainEventNotification<DomainEvent>(domainEvent, currentUser));
            await _mediator.Publish(notification);
        }
    }
}