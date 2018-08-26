using System;
using System.Linq;

namespace ContactManager.Common.Extensions
{
    public static class StringExtensions
    {
        public static string RemoveNonNumericChars(this string str)
        {
            return new string(str.Where(char.IsDigit).ToArray());
        }

        public static string ToPhoneNumberFormat(this string str)
        {
            return string.Format($"{{0:{Constants.Constants.ConstantPhoneDisplayFormat}}}", Convert.ToInt64(str));
        }
    }
}
