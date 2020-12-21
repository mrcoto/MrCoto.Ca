using MrCoto.Ca.Application.Common.Query.Sorting.Bag;
using MrCoto.Ca.Application.Common.Query.Sorting.Parser;
using Xunit;

namespace MrCoto.Ca.ApplicationTests.Common.Query.Sorting
{
    public class SortParserTest
    {
        [Theory]
        [InlineData(0, "firstName", SortOperator.Asc)]
        [InlineData(1, "updatedAt", SortOperator.Desc)]
        [InlineData(2, "age", SortOperator.Asc)]
        [InlineData(3, "createdAt", SortOperator.Asc)]
        public void Should_ContaintSortParam(int index, string param, SortOperator op)
        {
            var pattern = "sort=+firstName,-updatedAt, age,createdAt";
            var parser = new SortParser();

            var bag = parser.Parse(pattern);

            var sortParam = bag.FindByIndex(index);
            Assert.NotNull(sortParam);
            Assert.Equal(param, sortParam.Param);
            Assert.Equal(op, sortParam.Operator);
        }
        
    }
}