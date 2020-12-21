using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MrCoto.Ca.Application.Common.Events;
using MrCoto.Ca.Application.Common.Repositories;
using MrCoto.Ca.Domain.Common.Entities;

namespace MrCoto.Ca.Infrastructure.Common.Repositories
{
    public class Uow : IUow
    {
        protected readonly SimpleAgendaContext Context;
        private readonly IDomainEventPublisher _eventPublisher;

        public Uow(SimpleAgendaContext context, IDomainEventPublisher eventPublisher)
        {
            Context = context;
            _eventPublisher = eventPublisher;
        }

        public async Task<int> SaveChanges()
        {
            foreach (EntityEntry<ISoftDeletable> entry in Context.ChangeTracker.Entries<ISoftDeletable>())
            {
                switch (entry.State)
                {
                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        entry.CurrentValues["DeletedAt"] = DateTime.Now;
                        break;
                }
            }

            var result = await Context.SaveChangesAsync();
            
            await DispatchEvents();
            
            return result;
        }
        
        private async Task DispatchEvents()
        {
            var entities = Context.ChangeTracker.Entries<Entity<long>>()
                .Select(x => x.Entity)
                .ToList();

            foreach (var entity in entities)
            {
                var domainEvents = entity.DomainEvents;
                entity.ClearEvents();
                foreach (var domainEvent in domainEvents)
                {
                    await _eventPublisher.Publish(domainEvent);
                }
            }
            
        }
        
    }
}