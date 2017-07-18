using System;
using System.Globalization;

namespace Common.Extensions
{
    public static class StringExtension
    {
        public static int ToInt(this string numberInString)
        {
            return int.Parse(numberInString);
        }

        public static long ToLong(this string numberInString)
        {
            return long.Parse(numberInString);
        }

        public static float ToFloat(this string numberInString)
        {
            return float.Parse(numberInString);
        }

        public static DateTime ToDateTime(this string timeInString, string format)
        {
            if (!string.IsNullOrEmpty(timeInString))
                return DateTime.ParseExact(timeInString, format, CultureInfo.InvariantCulture);
            else
                return DateTime.MinValue;
        }
    }
}
