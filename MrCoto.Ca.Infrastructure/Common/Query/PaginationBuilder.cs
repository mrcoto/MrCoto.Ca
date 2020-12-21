using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.EntityFrameworkCore;
using MrCoto.Ca.Application.Common.Query.Request;
using MrCoto.Ca.Application.Common.Query.Response;

namespace MrCoto.Ca.Infrastructure.Common.Query
{
    public class PaginationBuilder : IPaginationBuilder
    {
        public readonly IHttpContextAccessor HttpContextAccessor;

        public PaginationBuilder(IHttpContextAccessor httpContextAccessor)
        {
            HttpContextAccessor = httpContextAccessor;
        }

        public async Task<PaginationResponse<TOutput>> Paginate<TOutput, TEntity>(
            PaginationRequest request,
            IQueryable<TEntity> query,
            IQueryable<TEntity> queryCount,
            Func<TEntity, TOutput> mapToOutput)
        {
            var total = await queryCount.CountAsync();
            var data = await query
                .Skip(request.Offset())
                .Take(request.Limit)
                .ToListAsync();
            var outputData = data.Select(mapToOutput).ToList();

            return Build(request, total, outputData);
        }
        
        public async Task<PaginationResponse<TOutput>> Paginate<TOutput, TEntity>(
            PaginationRequest request,
            IOrderedEnumerable<TEntity> query,
            IQueryable<TEntity> queryCount,
            Func<TEntity, TOutput> mapToOutput)
        {
            var total = await queryCount.CountAsync();
            var data = query
                .Skip(request.Offset())
                .Take(request.Limit)
                .ToList();
            var outputData = data.Select(mapToOutput).ToList();

            return Build(request, total, outputData);
        }
        
        public async Task<PaginationResponse<TOutput>> Paginate<TOutput, TEntity>(
            PaginationRequest request,
            List<TOutput> data,
            IQueryable<TEntity> queryCount)
        {
            var total = await queryCount.CountAsync();
            return Build(request, total, data);
        }

        private PaginationResponse<TOutput> Build<TOutput>(PaginationRequest request, int total, List<TOutput> data)
        {
            var lastPage = (int) Math.Ceiling(1.0 * total / request.Limit);
            var firstPageUrl = BuildUrl(request.Limit, 1);
            var lastPageUrl = BuildUrl(request.Limit, lastPage);
            var prevPageUrl = request.Page > 1 ? BuildUrl(request.Limit, request.Page - 1) : null;
            var nextPageUrl = request.Page == lastPage ? null : BuildUrl(request.Limit, request.Page + 1);
            var from = (request.Page - 1) * request.Limit + 1;
            return new PaginationResponse<TOutput>()
            {
                Total = data.Count > 0 ? total : 0,
                PerPage = request.Limit,
                CurrentPage = request.Page,
                LastPage = lastPage,
                FirstPageUrl = firstPageUrl,
                LastPageUrl = lastPageUrl,
                NextPageUrl = nextPageUrl,
                PrevPageUrl = prevPageUrl,
                From = from,
                To = from + data.Count - 1,
                Data = data
            };
        }

        private string BuildUrl(int limit, int page)
        {
            var url = HttpContextAccessor.HttpContext.Request.GetEncodedUrl();
            if (!url.Contains("page"))
            {
                url += (url.EndsWith("?") ? "" : "&") + "page=" + page;
            }

            if (!url.Contains("limit")) 
            {
                url += (url.EndsWith("?") ? "" : "&") + "limit=" + limit;
            }

            return url;
        }
    }
}