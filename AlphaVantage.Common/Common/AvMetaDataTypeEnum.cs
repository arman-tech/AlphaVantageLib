using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaVantage.Common
{
    public class AvMetaDataTypeEnum : Enumeration
    {
        public static AvMetaDataTypeEnum Undefined = new AvMetaDataTypeEnum(0, "__UNKNOWN__");
        public static AvMetaDataTypeEnum StockTimeSeries = new AvMetaDataTypeEnum(1, "Stock_Time_Series");
        public static AvMetaDataTypeEnum TechnicalIndicators = new AvMetaDataTypeEnum(2, "Technical_Indicators");
        public AvMetaDataTypeEnum(int id, string name) : base(id, name) { }
        public AvMetaDataTypeEnum() : base(0, "__UNKNOWN__") { }

        public static AvMetaDataTypeEnum FromName(string name)
        {
            return FromDisplayName<AvMetaDataTypeEnum>(name);
        }
    }
}
