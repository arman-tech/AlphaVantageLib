    using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaVantage.Common
{
    public class AvIntervalEnum : Enumeration
    {
        /*
         * Required: interval
         * Time interval between two consecutive data points in the time series. 
         * The following values are supported: 1min, 5min, 15min, 30min, 60min, daily, weekly, monthly
         * 
         * */
        public static AvIntervalEnum Undefined = new AvIntervalEnum(0, "__UNKNOWN__");
        public static AvIntervalEnum OneMin = new AvIntervalEnum(1, "1min");
        public static AvIntervalEnum FiveMin = new AvIntervalEnum(2, "5min");
        public static AvIntervalEnum FifteenMin = new AvIntervalEnum(3, "15min");
        public static AvIntervalEnum ThirtyMin = new AvIntervalEnum(4, "30min");
        public static AvIntervalEnum SixtyMin = new AvIntervalEnum(5, "60min");
        public static AvIntervalEnum Daily = new AvIntervalEnum(6, "daily");
        public static AvIntervalEnum Weekly = new AvIntervalEnum(7, "weekly");
        public static AvIntervalEnum Monthly = new AvIntervalEnum(8, "monthly");
        public static AvIntervalEnum Default = new AvIntervalEnum(9, "default");

        public AvIntervalEnum(int id, string name) : base(id, name) { }
        public AvIntervalEnum() : base(0, "__UNKNOWN__") { }

        public static AvIntervalEnum FromName(string name)
        {
            return FromDisplayName<AvIntervalEnum>(name);
        }


    }
}
