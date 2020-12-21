using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MrCoto.Ca.Application.Common.Query.Request;
using MrCoto.Ca.Application.Common.Query.Response;

namespace MrCoto.Ca.Infrastructure.Common.Query
{
    public interface IPaginationBuilder
    {
        public Task<PaginationResponse<TOutput>> Paginate<TOutput, TEntity>(
            PaginationRequest request,
            IQueryable<TEntity> query,
            IQueryable<TEntity> queryCount,
            Func<TEntity, TOutput> mapToOutput);

        public Task<PaginationResponse<TOutput>> Paginate<TOutput, TEntity>(
            PaginationRequest request,
            IOrderedEnumerable<TEntity> query,
            IQueryable<TEntity> queryCount,
            Func<TEntity, TOutput> mapToOutput);

        public Task<PaginationResponse<TOutput>> Paginate<TOutput, TEntity>(
            PaginationRequest request,
            List<TOutput> data,
            IQueryable<TEntity> queryCount);

    }
}