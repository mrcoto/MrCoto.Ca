using MrCoto.Ca.Application.Common.Query.Filtering.Bag;
using MrCoto.Ca.Application.Common.Query.Filtering.Parser;
using Xunit;

namespace MrCoto.Ca.ApplicationTests.Common.Query.Filtering
{
    public class FilterParserTest
    {
        [Theory]
        [InlineData(0, "price", FilterOperator.Gte, "10")]
        [InlineData(1, "price", FilterOperator.Lte, "100.25")]
        [InlineData(2, "money", FilterOperator.Eq, "USD")]
        [InlineData(3, "region", FilterOperator.Neq, "Coquimbo")]
        [InlineData(4, "date", FilterOperator.Lt, "2020-10-09T16:56:00")]
        [InlineData(5, "search", FilterOperator.Pattern, "Jo")]
        [InlineData(6, "enie", FilterOperator.Pattern, "ña")]
        public void Should_ContainFilterParam(int index, string param, FilterOperator op, string value)
        {
            var pattern = "price=gte:10&price=lte:100.25&money=eq:USD&region=neq:Coquimbo" +
                          "&date=lt:2020-10-09T16:56:00&search=pattern:Jo" +
                          "&enie=pattern:ña";
            var parser = new FilterParser();

            var bag = parser.Parse(pattern);
            var filterParam = bag.FindByIndex(index);
            Assert.NotNull(filterParam);
            Assert.Equal(param, filterParam.Param);
            Assert.Equal(op, filterParam.Operator);
            Assert.Equal(value, filterParam.Value);
        }
    }
}