using System.Text.RegularExpressions;

namespace MrCoto.Ca.Domain.Common.Extensions
{
    public static class StringExtensions
    {
        public static bool IsHexColor(this string value)
        {
            return !string.IsNullOrWhiteSpace(value) &&
                   Regex.IsMatch(value, "^[a-fA-F0-9]{6}$");
        }

        public static bool IsChileanCellphone(this string value)
        {
            return !string.IsNullOrWhiteSpace(value) &&
                   Regex.IsMatch(value, @"^\+56 9 [9876543]\d{7}$");
        }
    }
}