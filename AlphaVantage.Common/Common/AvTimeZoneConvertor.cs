using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaVantage.Common
{
    public static class AvTimeZoneConvertor
    {
        public static TimeZoneInfo AvTimeZone(string avTimeZone)
        {
            switch(avTimeZone)
            {
                case "US/Eastern":          // AdjDailyTimeSeries
                case "US/Eastern Time":     // BBANDS
                    return TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
                default:
                    return TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
            }
        }
    }
}
