using static System.Char;

namespace EvidenceZakazekWebApp.Extensions
{
    public static class StringExtensions
    {
        public static string LowercaseFirstLetter(this string str)
        {
            return ToLowerInvariant(str[0]) + str.Substring(1);
        }
    }
}