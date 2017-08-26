using System;

namespace Common.Extensions
{
    public static class DateTimeExtension
    {
        public static DateTime ChangeDate(this DateTime time, DateTime baseDate)
        {
            return new DateTime(baseDate.Year, baseDate.Month, baseDate.Day, time.Hour, time.Minute, time.Second);
            //return baseDate.Date + time.TimeOfDay;
        }
    }
}
