using MediatR;
using MrCoto.Ca.Application.Common.CurrentUsers.Response;
using MrCoto.Ca.Domain.Common.Events;

namespace MrCoto.Ca.Application.Common.Events
{
    public class DomainEventNotification<TDomainEvent> : INotification where TDomainEvent : DomainEvent
    {
        public DomainEventNotification(TDomainEvent domainEvent)
        {
            DomainEvent = domainEvent;
        }
        
        public DomainEventNotification(TDomainEvent domainEvent, CurrentUser currentUser)
        {
            DomainEvent = domainEvent;
            CurrentUser = currentUser;
        }

        public TDomainEvent DomainEvent { get; }
        public CurrentUser CurrentUser { get; set; }
    }
}