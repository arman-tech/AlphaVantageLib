using System;

namespace AlphaVantage.Common.Models.TechnicalIndicators.KAMA
{
    public class AvKAMAMetaData : AvMetaDataAbs<AvKAMAMetaData>
    {
        public AvKAMAMetaData()
        {
            base.Type = AvMetaDataTypeEnum.TechnicalIndicators;
            base.Function = AvFunctionEnum.KAMA;
        }
        
        [AvPropertyName(ExtractPropertyName = "1: Symbol")]
        public override string Symbol { get; set; }

        [AvPropertyName(ExtractPropertyName = "2: Indicator")]
        public string Indicator { get; set; }

        [AvPropertyName(ExtractPropertyName = "3: Last Refreshed")]
        public override DateTime LastRefreshed { get => base.LastRefreshed; set => base.LastRefreshed = value; }

        [AvPropertyName(ExtractPropertyName = "4: Interval")]
        public AvIntervalEnum Interval { get; set; }

        [AvPropertyName(ExtractPropertyName = "5: Time Period")]
        public int TimePeriod { get; set; }

        [AvPropertyName(ExtractPropertyName = "6: Series Type")]
        public AvSeriesTypeEnum SeriesType { get; set; }

        [AvPropertyName(ExtractPropertyName = "7: Time Zone")]
        public override TimeZoneInfo TimeZone { get => base.TimeZone; set => base.TimeZone = value; }
    }
}
