using System.Collections.Generic;
using System.Threading.Tasks;
using MrCoto.Ca.Domain.Common.Entities;

namespace MrCoto.Ca.Application.Common.Repositories
{
    public interface IRepository<TEntity, TId> 
        where TEntity : Entity<TId>
    {
        public Task<TEntity> Create(TEntity entity);
        public Task<TEntity> Update(TEntity entity);
        public Task<TEntity> Delete(TEntity entity);
        public Task<TEntity> Find(TId id);
        public Task<List<TEntity>> Find(List<TId> idList);
    }
}