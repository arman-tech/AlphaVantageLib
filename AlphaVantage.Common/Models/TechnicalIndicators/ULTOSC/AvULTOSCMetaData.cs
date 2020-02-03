using System;

namespace AlphaVantage.Common.Models.TechnicalIndicators.ULTOSC
{
    public class AvULTOSCMetaData : AvMetaDataAbs<AvULTOSCMetaData>
    {
        public AvULTOSCMetaData()
        {
            base.Type = AvMetaDataTypeEnum.TechnicalIndicators;
            base.Function = AvFunctionEnum.ULTOSC;
        }
        
        [AvPropertyName(ExtractPropertyName = "1: Symbol")]
        public override string Symbol { get; set; }

        [AvPropertyName(ExtractPropertyName = "2: Indicator")]
        public string Indicator { get; set; }

        [AvPropertyName(ExtractPropertyName = "3: Last Refreshed")]
        public override DateTime LastRefreshed { get => base.LastRefreshed; set => base.LastRefreshed = value; }

        [AvPropertyName(ExtractPropertyName = "4: Interval")]
        public AvIntervalEnum Interval { get; set; }

        [AvPropertyName(ExtractPropertyName = "5.1: Time Period 1")]
        public int TimePeriodOne { get; set; }

        [AvPropertyName(ExtractPropertyName = "5.2: Time Period 2")]
        public int TimePeriodTwo { get; set; }

        [AvPropertyName(ExtractPropertyName = "5.3: Time Period 3")]
        public int TimePeriodThree { get; set; }

        [AvPropertyName(ExtractPropertyName = "6: Time Zone")]
        public override TimeZoneInfo TimeZone { get => base.TimeZone; set => base.TimeZone = value; }
    }
}
