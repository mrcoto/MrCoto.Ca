using System.Threading.Tasks;
using MrCoto.Ca.Application.Common.Query.Request;
using MrCoto.Ca.Application.Common.Query.Response;

namespace MrCoto.Ca.Application.Common.Query
{
    public interface IQuery<TOutput>
    {
        public Task<PaginationResponse<TOutput>> Paginated(PaginationRequest request, QueryBag queryBag);
        public Task<TOutput> Find(long id, QueryBag queryBag);
    }
}