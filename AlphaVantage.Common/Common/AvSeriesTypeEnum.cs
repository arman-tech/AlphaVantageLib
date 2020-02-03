using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaVantage.Common
{
    public class AvSeriesTypeEnum : Enumeration
    {
        public static AvSeriesTypeEnum Undefined = new AvSeriesTypeEnum(0, "__UNKNOWN__");
        public static AvSeriesTypeEnum Close = new AvSeriesTypeEnum(1, "close");
        public static AvSeriesTypeEnum Open = new AvSeriesTypeEnum(2, "open");
        public static AvSeriesTypeEnum High = new AvSeriesTypeEnum(3, "high");
        public static AvSeriesTypeEnum Low = new AvSeriesTypeEnum(4, "low");


        public AvSeriesTypeEnum(int id, string name) : base(id, name) { }
        public AvSeriesTypeEnum() : base(0, "__UNKNOWN__") { }

        public static AvSeriesTypeEnum FromName(string name)
        {
            return FromDisplayName<AvSeriesTypeEnum>(name);
        }
    }
}
