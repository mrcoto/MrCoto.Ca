using System.Collections.Generic;
using System.Threading.Tasks;
using MrCoto.Ca.Domain.Common.Entities;

namespace MrCoto.Ca.Application.Common.Repositories
{
    public interface ITenantRepository<TEntity, TId> : IRepository<TEntity, TId>
        where TEntity : Entity<TId>
    {
        public Task<TEntity> Find(TId id, long tenantId);
        public Task<List<TEntity>> Find(List<TId> idList, long tenantId);
    }
}