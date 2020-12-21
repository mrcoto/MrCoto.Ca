using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using MrCoto.Ca.Application.Common.Query.Sorting.Bag;

namespace MrCoto.Ca.Application.Common.Query.Sorting.Parser
{
    public class SortParser
    {
        private const string SortRegex = "^\\??sort=([ +\\-]?[\\w.\\-_ :]+,)*[ +\\-]?[\\w.\\-_ :]+$";

        public SortBag Parse(string pattern)
        {
            var bag = new SortBag();
            var candidates = pattern.Split("&").ToList();
            var candidate = candidates
                .Where(IsSortParam)
                .FirstOrDefault();
            if (!string.IsNullOrWhiteSpace(candidate))
            {
                Extract(candidate).ForEach(sortParam => bag.Add(sortParam));
            }
            return bag;
        }

        private bool IsSortParam(string candidate) => Regex.IsMatch(candidate, SortRegex);

        private List<SortParam> Extract(string candidate)
        {
            var sortParamList = new List<SortParam>();
            var tokens = candidate.Split("=")[1].Split(",").ToList();
            tokens.ForEach(token =>
            {
                var sortParam = Convert(token);
                sortParamList.Add(sortParam);
            });
            return sortParamList;
        }

        private SortParam Convert(string token)
        {
            var opToken = token.Substring(0, 1);
            if (opToken.StartsWith(SortOperator.Asc.Value()) || opToken.StartsWith(SortOperator.Desc.Value()))
            {
                return new SortParam(token.Substring(1), SortOperatorFactory.Of(opToken));
            } else if (opToken == " ")
            {
                return new SortParam(token.Substring(1), SortOperator.Asc);
            }
            return new SortParam(token, SortOperator.Asc);
        }
    }
}