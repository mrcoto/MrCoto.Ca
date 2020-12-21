using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MrCoto.Ca.Application.Common.Repositories;
using MrCoto.Ca.Domain.Common.Entities;

namespace MrCoto.Ca.Infrastructure.Common.Repositories
{
    public class Repository<TEntity, TId> : IRepository<TEntity, TId> where TEntity : Entity<TId>, new()
    {
        protected readonly CaContext Context;

        public Repository(CaContext context)
        {
            Context = context;
        }
        
        public async Task<TEntity> Create(TEntity entity)
        {
            await Context.Set<TEntity>().AddAsync(entity);
            return entity;
        }

        public Task<TEntity> Update(TEntity entity)
        {
            // EfCore Automatically handle entity
            return Task.FromResult(entity);
        }

        public Task<TEntity> Delete(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
            return Task.FromResult(entity);
        }

        public async Task<TEntity> Find(TId id)
        {
            var entity = await Context.Set<TEntity>().FindAsync(id);
            if (entity is ISoftDeletable softDeletable && softDeletable.DeletedAt != null)
            {
                return null;
            }

            return entity;
        }

        public async Task<List<TEntity>> Find(List<TId> idList)
        {
            var entities = await Context.Set<TEntity>().Where(x => idList.Contains(x.Id))
                .ToListAsync();

            return entities.Where(x => !(x is ISoftDeletable) || 
                                       x is ISoftDeletable sd && sd.DeletedAt == null).ToList();
        }
    }
}