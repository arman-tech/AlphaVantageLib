using System;

namespace AlphaVantage.Common.Models.TechnicalIndicators.MACDEXT
{
    public class AvMACDEXTMetaData : AvMetaDataAbs<AvMACDEXTMetaData>
    {
        public AvMACDEXTMetaData()
        {
            base.Type = AvMetaDataTypeEnum.TechnicalIndicators;
            base.Function = AvFunctionEnum.MACDEXT;
        }
        
        [AvPropertyName(ExtractPropertyName = "1: Symbol")]
        public override string Symbol { get; set; }

        [AvPropertyName(ExtractPropertyName = "2: Indicator")]
        public string Indicator { get; set; }

        [AvPropertyName(ExtractPropertyName = "3: Last Refreshed")]
        public override DateTime LastRefreshed { get => base.LastRefreshed; set => base.LastRefreshed = value; }

        [AvPropertyName(ExtractPropertyName = "4: Interval")]
        public AvIntervalEnum Interval { get; set; }

        [AvPropertyName(ExtractPropertyName = "5.1: Fast Period")]
        public int FastPeriod { get; set; }

        [AvPropertyName(ExtractPropertyName = "5.2: Slow Period")]
        public int SlowPeriod { get; set; }

        [AvPropertyName(ExtractPropertyName = "5.3: Signal Period")]
        public int SignalPeriod { get; set; }

        [AvPropertyName(ExtractPropertyName = "5.4: Fast MA Type")]
        public int FastMAType { get; set; }

        [AvPropertyName(ExtractPropertyName = "5.5: Slow MA Type")]
        public int SlowMAType { get; set; }

        [AvPropertyName(ExtractPropertyName = "5.6: Signal MA Type")]
        public int SignalMAType { get; set; }
        
        [AvPropertyName(ExtractPropertyName = "6: Series Type")]
        public AvSeriesTypeEnum SeriesType { get; set; }

        [AvPropertyName(ExtractPropertyName = "7: Time Zone")]
        public override TimeZoneInfo TimeZone { get => base.TimeZone; set => base.TimeZone = value; }
    }
}
