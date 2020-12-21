using System;
using System.Collections.Generic;
using System.Linq;

namespace MrCoto.Ca.Application.Common.Query.Sorting.Bag
{
    public class SortBag
    {
        public readonly List<SortParam> Params;

        public SortBag()
        {
            Params = new List<SortParam>();
        }

        public SortBag Add(SortParam sortParam)
        {
            Params.Add(sortParam);
            return this;
        }

        public SortBag Add(string param, SortOperator op) => Add(new SortParam(param, op));

        public SortBag Remove(string param)
        {
            Params.RemoveAll(x => x.Param == param);
            return this;
        }

        public SortBag Remove(string param, SortOperator op)
        {
            Params.RemoveAll(x => x.Param == param && x.Operator == op);
            return this;
        }

        public SortParam FindByIndex(int index)
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

        public bool IsPresent(string param, SortOperator op)
        {
            return Params.Any(x => x.Param == param && x.Operator == op);
        }

        public bool IsPresent(SortParam sortParam) => IsPresent(sortParam.Param, sortParam.Operator);
        
    }
}