using System.Linq;

namespace Jello
{
    public static class StringExtensions
    {
        public static string ToReadableOrList(this string[] strings)
        {
            if (!strings.Any()) return "";
            string firstPart;
            string lastPart;
            if (strings.Count() > 2)
            {
                firstPart = string.Join(",", strings.Take(strings.Count() - 1));
            }
            else
            {
                firstPart = strings[0];
            }
            return firstPart + " or " + strings.Last();
        }
    }
}