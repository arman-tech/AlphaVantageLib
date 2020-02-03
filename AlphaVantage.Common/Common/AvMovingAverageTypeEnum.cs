using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaVantage.Common
{
    public class AvMovingAverageTypeEnum : Enumeration
    {
        /*
         * Integers 0 - 8 are accepted with the following mappings. 
         * 0 = Simple Moving Average (SMA), 
         * 1 = Exponential Moving Average (EMA), 
         * 2 = Weighted Moving Average (WMA), 
         * 3 = Double Exponential Moving Average (DEMA), 
         * 4 = Triple Exponential Moving Average (TEMA), 
         * 5 = Triangular Moving Average (TRIMA), 
         * 6 = T3 Moving Average (T3MA), 
         * 7 = Kaufman Adaptive Moving Average (KAMA), 
         * 8 = MESA Adaptive Moving Average (MAMA). 
         * 
         */
        public static AvMovingAverageTypeEnum SMA = new AvMovingAverageTypeEnum(0, "SMA");
        public static AvMovingAverageTypeEnum EMA = new AvMovingAverageTypeEnum(1, "EMA");
        public static AvMovingAverageTypeEnum WMA = new AvMovingAverageTypeEnum(2, "WMA");
        public static AvMovingAverageTypeEnum DEMA = new AvMovingAverageTypeEnum(3, "DEMA");
        public static AvMovingAverageTypeEnum TEMA = new AvMovingAverageTypeEnum(4, "TEMA");
        public static AvMovingAverageTypeEnum TRIMA = new AvMovingAverageTypeEnum(5, "TRIMA");
        public static AvMovingAverageTypeEnum T3MA = new AvMovingAverageTypeEnum(6, "T3MA");
        public static AvMovingAverageTypeEnum KAMA = new AvMovingAverageTypeEnum(7, "KAMA");
        public static AvMovingAverageTypeEnum MAMA = new AvMovingAverageTypeEnum(8, "MAMA");

        public AvMovingAverageTypeEnum(int id, string name) : base(id, name) { }
        public AvMovingAverageTypeEnum() : base(0, "__UNKNOWN__") { }

        public static AvMovingAverageTypeEnum FromName(string name)
        {
            return FromDisplayName<AvMovingAverageTypeEnum>(name);
        }
    }
}
