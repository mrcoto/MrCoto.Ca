using System.Collections.Generic;

namespace MrCoto.Ca.Application.Common.Query.Response
{
    public class PaginationResponse<TOutput>
    {
        public int Total { get; set; }
        public int PerPage { get; set; }
        public int CurrentPage { get; set; }
        public int LastPage { get; set; }
        public string FirstPageUrl { get; set; }
        public string LastPageUrl { get; set; }
        public string NextPageUrl { get; set; }
        public string PrevPageUrl { get; set; }
        public int From { get; set; }
        public int To { get; set; }
        public List<TOutput> Data { get; set; }
    }
}