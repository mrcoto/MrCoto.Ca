using System;

namespace MrCoto.Ca.Application.Common.Query.Filtering.Bag
{
    public enum FilterOperator
    {
        Lt,
        Lte,
        Gt,
        Gte,
        Eq,
        Neq,
        Pattern
    }

    public class FilterOperatorFactory
    {
        public static FilterOperator Of(string value)
        {
            foreach (FilterOperator op in Enum.GetValues(typeof(FilterOperator)))
            {
                if (op.ToString().ToLower() == value.ToLower())
                {
                    return op;
                }
            }

            throw new ArgumentException($"Operador '{value}' no soportado");
        }
    }
    
}