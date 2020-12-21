using MrCoto.Ca.Domain.Common.Events;

namespace MrCoto.Ca.Domain.Modules.GeneralModule.Users.Events
{
    public class MaxLoginAttemptsReached : DomainEvent
    {
        public User User { get; set; }
    }
}