using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MrCoto.Ca.Application.Common.Query;
using MrCoto.Ca.Application.Common.Query.Request;
using MrCoto.Ca.Application.Common.Query.Response;
using MrCoto.Ca.Application.Common.Query.Sorting.Bag;
using MrCoto.Ca.Domain.Common.Entities;
using MrCoto.Ca.Infrastructure.Common.Query.Extensions;

namespace MrCoto.Ca.Infrastructure.Common.Query
{
    public abstract class BaseQuery<TOutput, TEntity> where TEntity : Entity<long>, new()
    {
        protected readonly SimpleAgendaContext Context;
        protected readonly IPaginationBuilder PaginationBuilder;
        
        public BaseQuery(SimpleAgendaContext context, IPaginationBuilder paginationBuilder)
        {
            Context = context;
            PaginationBuilder = paginationBuilder;
        }
        
        protected abstract IQueryable<TEntity> Query();
        protected abstract Expression<Func<TEntity, bool>> PreFilter(Expression<Func<TEntity, bool>> expression, QueryBag queryBag);
        protected abstract Expression<Func<TEntity, bool>> PostFilter(Expression<Func<TEntity, bool>> expression, QueryBag queryBag);
        protected abstract Dictionary<string, QueryFilter<TEntity>> FilterMap();
        protected abstract Dictionary<string, QuerySort<TEntity>> SortMap();
        protected abstract TOutput Map(TEntity entity);
        
        public async Task<PaginationResponse<TOutput>> Paginated(PaginationRequest request, QueryBag queryBag)
        {
            Expression<Func<TEntity, bool>> expression = x => true;

            expression = PreFilter(expression, queryBag);
            expression = ApplyFilters(expression, queryBag);
            expression = PostFilter(expression, queryBag);
            
            var query = Query();
            var queryCount = Query();
            query = query.Where(expression);
            queryCount = queryCount.Where(expression);
            if (queryBag.SortBag.Params.Count > 0)
            {
                query = ApplySorts(query, queryBag);
            }
            return await PaginationBuilder.Paginate(request, query, queryCount, Map);
        }
        
        public async Task<TOutput> Find(long id, QueryBag queryBag)
        {
            Expression<Func<TEntity, bool>> expression = x => true;
            expression = expression.And(x => x.Id == id);
            expression = PreFilter(expression, queryBag);
            expression = PostFilter(expression, queryBag);
            var result = await Query().Where(expression).FirstOrDefaultAsync();
            return result != null ? Map(result) : default;
        }
        
        protected Expression<Func<TEntity, bool>> ApplyFilters(Expression<Func<TEntity, bool>> expression, 
            QueryBag queryBag)
        {
            var map = FilterMap();
            queryBag.FilterBag.Params.ForEach(filterParam =>
            {
                if (!map.ContainsKey(filterParam.Param))
                {
                    throw new ArgumentException($"Filtro '{filterParam.Param}' no soportado");
                }

                var filter = map[filterParam.Param];
                expression = filter.Apply(expression, filterParam);
            });
            return expression;
        }
        
        protected IOrderedQueryable<TEntity> ApplySorts(IQueryable<TEntity> query, QueryBag queryBag)
        {
            var map = SortMap();
            var sortParams = queryBag.SortBag.Params;
            var sortParam = sortParams[0];
            if (!map.ContainsKey(sortParam.Param))
            {
                throw new ArgumentException($"Sort '{sortParam.Param}' no soportado");
            }
            var sort = map[sortParam.Param];
            var queryOrder = sortParam.Operator.IsAsc() ? 
                query.OrderBy(sort.Apply()) : 
                query.OrderByDescending(sort.Apply());

            for (var i = 1; i < sortParams.Count; i++)
            {
                sortParam = sortParams[i];
                if (!map.ContainsKey(sortParam.Param))
                {
                    throw new ArgumentException($"Sort '{sortParam.Param}' no soportado");
                }
                sort = map[sortParam.Param];
                queryOrder = sortParam.Operator.IsAsc() ? 
                    queryOrder.ThenBy(sort.Apply()) :
                    queryOrder.ThenByDescending(sort.Apply());
            }

            return queryOrder;
        }
    }
}