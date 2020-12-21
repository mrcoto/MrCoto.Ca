using MrCoto.Ca.Domain.Common.Events;

namespace MrCoto.Ca.Domain.Modules.GeneralModule.Users.Events
{
    public class UserRegistered : DomainEvent
    {
        public User User { get; set; }
    }
}