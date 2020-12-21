using System;

namespace MrCoto.Ca.Application.Common.Query.Sorting.Bag
{
    public enum SortOperator
    {
        Asc,
        Desc
    }

    public class SortOperatorFactory
    {
        public static SortOperator Of(string value)
        {
            foreach (SortOperator op in Enum.GetValues(typeof(SortOperator)))
            {
                if (op.Value() == value)
                {
                    return op;
                }
            }
            throw new ArgumentException($"Operador '{value}' no soportado");
        }
    }
    
    public static class SortOperatorMethods
    {
        public static bool IsAsc(this SortOperator op) => op == SortOperator.Asc;
        public static bool IsDesc(this SortOperator op) => op == SortOperator.Desc;

        public static string Value(this SortOperator op) => op switch
        {
            SortOperator.Asc => "+",
            SortOperator.Desc => "-",
            _ => ""
        };
    }
}