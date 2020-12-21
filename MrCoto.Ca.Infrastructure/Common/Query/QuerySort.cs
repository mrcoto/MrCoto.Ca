using System;
using System.Linq.Expressions;

namespace MrCoto.Ca.Infrastructure.Common.Query
{
    public abstract class QuerySort<TEntity>
    {
        public abstract Expression<Func<TEntity, object>> Apply();

    }

}