namespace MrCoto.Ca.Application.Common.Query.Request
{
    public class PaginationRequest
    {
        public int Page;
        public int Limit;
        
        public int Offset() => (Page - 1) * Limit;
    }
}