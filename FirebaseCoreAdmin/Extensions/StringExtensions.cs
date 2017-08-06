
namespace FirebaseCoreAdmin.Extensions
{
    using System;

    public static class StringExtensions
    {
        public static string TrimSlashes(this string str)
        {
            if (String.IsNullOrWhiteSpace(str))
            {
                return "";
            }
            char[] trimedCharacters = new char[] { '/', '\\' };
            return str.Trim(trimedCharacters);
        }
    }
}
