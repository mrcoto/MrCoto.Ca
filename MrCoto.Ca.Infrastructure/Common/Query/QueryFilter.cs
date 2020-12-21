using System;
using System.Linq.Expressions;
using MrCoto.Ca.Application.Common.Query.Filtering.Bag;

namespace MrCoto.Ca.Infrastructure.Common.Query
{
    public abstract class QueryFilter<TEntity>
    {
        public abstract Expression<Func<TEntity, bool>> Apply(Expression<Func<TEntity, bool>> expression, FilterParam filterParam);
    }
}