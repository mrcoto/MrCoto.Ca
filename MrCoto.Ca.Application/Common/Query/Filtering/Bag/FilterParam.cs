namespace MrCoto.Ca.Application.Common.Query.Filtering.Bag
{
    public class FilterParam
    {
        public readonly string Param;
        public readonly FilterOperator Operator;
        public readonly string Value;

        public FilterParam(string param, FilterOperator op, string value)
        {
            Param = param;
            Operator = op;
            Value = value;
        }
        
    }
}