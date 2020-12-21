namespace MrCoto.Ca.Application.Common.Query.Sorting.Bag
{
    public class SortParam
    {
        public readonly string Param;
        public readonly SortOperator Operator;

        public SortParam(string param, SortOperator sortOperator)
        {
            Param = param;
            Operator = sortOperator;
        }
        
    }
}