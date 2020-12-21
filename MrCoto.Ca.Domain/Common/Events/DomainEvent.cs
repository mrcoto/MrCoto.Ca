using System;

namespace MrCoto.Ca.Domain.Common.Events
{
    public abstract class DomainEvent
    {
        public readonly DateTime RegisteredAt;
        public readonly DateTime RegisteredAtUtc;

        public DomainEvent()
        {
            RegisteredAt = DateTime.Now;
            RegisteredAtUtc = DateTime.UtcNow;
        }
    }
}