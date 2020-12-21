using System.Threading.Tasks;
using MrCoto.Ca.Domain.Common.Events;

namespace MrCoto.Ca.Application.Common.Events
{
    public interface IDomainEventPublisher
    {
        Task Publish(DomainEvent domainEvent);
    }
}