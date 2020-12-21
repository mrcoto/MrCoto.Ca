using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MrCoto.Ca.Application.Common.Repositories;
using MrCoto.Ca.Domain.Common.Entities;

namespace MrCoto.Ca.Infrastructure.Common.Repositories
{
    public class TenantRepository<TEntity, TId> : Repository<TEntity, TId>, ITenantRepository<TEntity, TId>
        where TEntity : Entity<TId>, ITenantable, new()
    {
        public TenantRepository(CaContext context) : base(context)
        {
        }

        public async Task<TEntity> Find(TId id, long tenantId)
        {
            var entity = await Context.Set<TEntity>().FirstOrDefaultAsync(x => 
                x.Id.Equals(id) && x.TenantId == tenantId);
            if (entity is ISoftDeletable softDeletable && softDeletable.DeletedAt != null)
            {
                return null;
            }

            return entity;
        }

        public async Task<List<TEntity>> Find(List<TId> idList, long tenantId)
        {
            var entities = await Context.Set<TEntity>().Where(x => 
                idList.Contains(x.Id) && x.TenantId == tenantId)
                .ToListAsync();

            return entities.Where(x => !(x is ISoftDeletable) || 
                                       x is ISoftDeletable sd && sd.DeletedAt == null).ToList();
        }
    }
}