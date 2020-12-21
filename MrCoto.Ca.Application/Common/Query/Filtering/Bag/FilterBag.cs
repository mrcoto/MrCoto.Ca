using System;
using System.Collections.Generic;
using System.Linq;

namespace MrCoto.Ca.Application.Common.Query.Filtering.Bag
{
    public class FilterBag
    {
        public readonly List<FilterParam> Params;

        public FilterBag()
        {
            Params = new List<FilterParam>();
        }

        public FilterBag Add(FilterParam filterParam)
        {
            Params.Add(filterParam);
            return this;
        }

        public FilterBag Add(string param, FilterOperator op, string value)
        {
            Params.Add(new FilterParam(param, op, value));
            return this;
        }

        public FilterBag Remove(string param, FilterOperator op)
        {
            Params.RemoveAll(x => x.Operator == op);
            return this;
        }

        public FilterParam FindByIndex(int index)
        {
            try
            {
                return Params[index];
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool IsPresent(string param, FilterOperator op)
        {
            return Params.Any(x => x.Param == param && x.Operator == op);
        }

        public bool IsPresent(FilterParam filterParam) => IsPresent(filterParam.Param, filterParam.Operator);
        
    }
}