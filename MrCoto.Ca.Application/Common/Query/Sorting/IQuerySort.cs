using MrCoto.Ca.Application.Common.Query.Sorting.Bag;

namespace MrCoto.Ca.Application.Common.Query.Sorting
{
    public interface IQuerySort
    {
        public void Apply(SortParam sortParam);
    }
}