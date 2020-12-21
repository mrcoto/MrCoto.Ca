using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using MrCoto.Ca.Domain.Common.Events;

namespace MrCoto.Ca.Domain.Common.Entities
{
    public abstract class Entity<T>
    {
        public T Id { get; set; }

        private List<DomainEvent> _domainEvents;
        public ImmutableList<DomainEvent> DomainEvents => _domainEvents.ToImmutableList();
        
        public Entity()
        {
            Id = default;
            _domainEvents = new List<DomainEvent>();
        }

        public void AddEvent(DomainEvent domainEvent) => _domainEvents.Add(domainEvent);
        public void RemoveEvent(DomainEvent domainEvent) => _domainEvents.Remove(domainEvent);

        public DomainEvent FindEvent(Type domainEventType) =>
            _domainEvents.FirstOrDefault(x => x.GetType() == domainEventType);

        public void ClearEvents() => _domainEvents.Clear();

    }
}