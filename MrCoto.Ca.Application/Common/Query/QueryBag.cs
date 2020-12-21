using MrCoto.Ca.Application.Common.Query.Filtering.Bag;
using MrCoto.Ca.Application.Common.Query.Filtering.Parser;
using MrCoto.Ca.Application.Common.Query.Request;
using MrCoto.Ca.Application.Common.Query.Sorting.Bag;
using MrCoto.Ca.Application.Common.Query.Sorting.Parser;

namespace MrCoto.Ca.Application.Common.Query
{
    public class QueryBag
    {
        public UserData UserData { get; set; }

        public readonly FilterBag FilterBag;

        public readonly SortBag SortBag;

        private QueryBag(string queryString, UserData userData)
        {
            UserData = userData;
            var filterParser = new FilterParser();
            FilterBag = filterParser.Parse(queryString);
            var sortParser = new SortParser();
            SortBag = sortParser.Parse(queryString);
        }
        
        public static QueryBag Of(string queryString) => new QueryBag(queryString, new UserData());
        public static QueryBag Of(string queryString, UserData userData) => new QueryBag(queryString, userData);
        
    }
}