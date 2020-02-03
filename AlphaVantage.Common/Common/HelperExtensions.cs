using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace AlphaVantage.Common.Common
{
    public static class HelperExtensions
    {
        /// <summary>
        /// Gets the iso8601 week of year.
        /// <para>https://blogs.msdn.microsoft.com/shawnste/2006/01/24/iso-8601-week-of-year-format-in-microsoft-net/</para>
        /// </summary>
        /// <param name="time">The time.</param>
        /// <returns></returns>
        public static int GetIso8601WeekOfYear(this DateTime time)
        {
            // Need a calendar.  Culture's irrelevent since we specify start day of week
            var cal = CultureInfo.InvariantCulture.Calendar;

            // Seriously cheat.  If its Monday, Tuesday or Wednesday, then it'll 
            // be the same week# as whatever Thursday, Friday or Saturday are,
            // and we always get those right
            DayOfWeek day = cal.GetDayOfWeek(time);
            if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
            {
                time = time.AddDays(3);
            }

            // Return the week of our adjusted day
            return cal.GetWeekOfYear(time, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }

        public static DateTime FirstDateOfWeekISO8601(this DateTime time)
        {
            return FirstDateOfWeekISO8601(time.Year, time.GetIso8601WeekOfYear());
        }

        /// <summary>
        /// The first date of the week under ISO8601
        /// <para>https://stackoverflow.com/a/9064954</para>
        /// </summary>
        /// <param name="year">The year.</param>
        /// <param name="weekOfYear">The week of year.</param>
        /// <returns></returns>
        private static DateTime FirstDateOfWeekISO8601(int year, int weekOfYear)
        {
            DateTime jan1 = new DateTime(year, 1, 1);
            int daysOffset = DayOfWeek.Thursday - jan1.DayOfWeek;

            // Use first Thursday in January to get first week of the year as
            // it will never be in Week 52/53
            DateTime firstThursday = jan1.AddDays(daysOffset);
            var cal = CultureInfo.CurrentCulture.Calendar;
            int firstWeek = cal.GetWeekOfYear(firstThursday, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

            var weekNum = weekOfYear;
            // As we're adding days to a date in Week 1,
            // we need to subtract 1 in order to get the right date for week #1
            if (firstWeek == 1)
            {
                weekNum -= 1;
            }

            // Using the first Thursday as starting week ensures that we are starting in the right year
            // then we add number of weeks multiplied with days
            var result = firstThursday.AddDays(weekNum * 7);

            // Subtract 3 days from Thursday to get Monday, which is the first weekday in ISO8601
            return result.AddDays(-3);
        }
    }
}
