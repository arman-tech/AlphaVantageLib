using System;

namespace AlphaVantage.Common.Models.TechnicalIndicators.ADOSC
{
    public class AvADOSCMetaData : AvMetaDataAbs<AvADOSCMetaData>
    {
        public AvADOSCMetaData()
        {
            base.Type = AvMetaDataTypeEnum.TechnicalIndicators;
            base.Function = AvFunctionEnum.ADOSC;
        }
        
        [AvPropertyName(ExtractPropertyName = "1: Symbol")]
        public override string Symbol { get; set; }

        [AvPropertyName(ExtractPropertyName = "2: Indicator")]
        public string Indicator { get; set; }

        [AvPropertyName(ExtractPropertyName = "3: Last Refreshed")]
        public override DateTime LastRefreshed { get => base.LastRefreshed; set => base.LastRefreshed = value; }

        [AvPropertyName(ExtractPropertyName = "4: Interval")]
        public AvIntervalEnum Interval { get; set; }

        [AvPropertyName(ExtractPropertyName = "5.1: FastK Period")]
        public int FastKPeriod { get; set; }

        [AvPropertyName(ExtractPropertyName = "5.2: SlowK Period")]
        public int SlowKPeriod { get; set; }

        [AvPropertyName(ExtractPropertyName = "6: Time Zone")]
        public override TimeZoneInfo TimeZone { get => base.TimeZone; set => base.TimeZone = value; }
    }
}
