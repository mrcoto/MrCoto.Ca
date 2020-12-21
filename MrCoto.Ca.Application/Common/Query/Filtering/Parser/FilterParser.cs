using System.Linq;
using System.Text.RegularExpressions;
using MrCoto.Ca.Application.Common.Query.Filtering.Bag;

namespace MrCoto.Ca.Application.Common.Query.Filtering.Parser
{
    public class FilterParser
    {
        private const string FilterRegex = "([a-zA-Z\\-_]+)=([a-zA-Z]+):([\\p{L}|\\d.\\-_ :]+)";

        public FilterBag Parse(string pattern)
        {
            var bag = new FilterBag();
            var candidates = pattern.Split("&").ToList();
            candidates.ForEach(candidate =>
            {
                FilterParam filterParam;
                if (IsFilterParam(candidate) && !bag.IsPresent(filterParam = Convert(candidate)))
                {
                    bag.Add(filterParam);
                }
            });
            return bag;
        }

        private bool IsFilterParam(string candidate) => Regex.IsMatch(candidate, FilterRegex);

        private FilterParam Convert(string candidate)
        {
            var match = Regex.Match(candidate, FilterRegex);
            return new FilterParam(
                match.Groups[1].Value,
                FilterOperatorFactory.Of(match.Groups[2].Value),
                match.Groups[3].Value
                );
        }
    }
}