using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaVantage.Common.Common
{
    public class AvMovingAverageTypeEnum : Enumeration
    {
        public static AvFunctionEnum SMA = new AvFunctionEnum(0, "Simple Moving Average (SMA)");
        public static AvFunctionEnum EMA = new AvFunctionEnum(1, "Exponential Moving Average (EMA)");
        public static AvFunctionEnum WMA = new AvFunctionEnum(2, "Weighted Moving Average (WMA)");
        public static AvFunctionEnum DEMA = new AvFunctionEnum(3, "Double Exponential Moving Average (DEMA)");
        public static AvFunctionEnum TEMA = new AvFunctionEnum(4, "Triple Exponential Moving Average (TEMA)");
        public static AvFunctionEnum TRIMA = new AvFunctionEnum(5, "Triangular Moving Average (TRIMA)");
        public static AvFunctionEnum T3MA = new AvFunctionEnum(6, "T3 Moving Average");
        public static AvFunctionEnum KAMA = new AvFunctionEnum(7, "Kaufman Adaptive Moving Average (KAMA)");
        public static AvFunctionEnum MAMA = new AvFunctionEnum(8, "MESA Adaptive Moving Average (MAMA)");

        public AvMovingAverageTypeEnum(int id, string name) : base(id, name) { }
        public AvMovingAverageTypeEnum() : base(0, "Simple Moving Average (SMA)") { }
        public static AvMovingAverageTypeEnum FromName(string name)
        {
            return FromDisplayName<AvMovingAverageTypeEnum>(name);
        }

    }
}
