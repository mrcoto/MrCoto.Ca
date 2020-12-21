using System.Collections.Generic;
using System.Linq;

namespace MrCoto.Ca.AppCli.Common.Utils
{
    public class PrinterTableHelper
    {
        private readonly PrinterHelper _printerHelper;

        public PrinterTableHelper(PrinterHelper printerHelper)
        {
            _printerHelper = printerHelper;
        }

        public void PrintTable(List<string> headers, List<List<string>> data)
        {
            var slackSpaces = 2;
            var maxLens = new List<int>();
            for(var i = 0; i < headers.Count; i++)
            {
                var header = headers[i];
                var rows = data.Select(x => x[i]).ToList();
                var maxLen = MaxLen(header, rows) + slackSpaces;
                maxLens.Add(maxLen);
            }

            PrintRows(headers, data, maxLens);
        }

        private void PrintRows(List<string> headers, List<List<string>> data, List<int> maxLens)
        {
            var separatorLine = GetSeparatorLine(maxLens);
            _printerHelper.Print(separatorLine);
            PrintRow(headers, maxLens);
            _printerHelper.Print(separatorLine);
            foreach (var row in data)
            {
                PrintRow(row, maxLens);
                _printerHelper.Print(separatorLine);
            }
        }

        private string GetSeparatorLine(List<int> maxLens)
        {
            var columns = "+";
            maxLens.ForEach((maxLen) => columns += new string('-', maxLen) + "+");
            return columns;
        }

        private void PrintRow(List<string> row, List<int> maxLens)
        {
            var columns = "|";
            for (int i = 0; i < maxLens.Count; i++)
            {
                var maxLen = maxLens[i];
                columns += " " + row[i] + new string(' ', maxLen - row[i].Length - 1) + "|";
            }

            _printerHelper.Print(columns);
        }

        private int MaxLen(string header, List<string> rows)
        {
            var maxLen = header.Length;
            rows.ForEach((row) =>
            {
                if (row.Length > maxLen)
                {
                    maxLen = row.Length;
                }
            });
            return maxLen;
        }
    }
}