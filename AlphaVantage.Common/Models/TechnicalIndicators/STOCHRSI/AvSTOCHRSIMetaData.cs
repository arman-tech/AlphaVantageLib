﻿using System;

namespace AlphaVantage.Common.Models.TechnicalIndicators.STOCHRSI
{
    public class AvSTOCHRSIMetaData : AvMetaDataAbs<AvSTOCHRSIMetaData>
    {
        public AvSTOCHRSIMetaData()
        {
            base.Type = AvMetaDataTypeEnum.TechnicalIndicators;
            base.Function = AvFunctionEnum.STOCHRSI;
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

        [AvPropertyName(ExtractPropertyName = "6.1: FastK Period")]
        public int FastKPeriod { get; set; }

        [AvPropertyName(ExtractPropertyName = "6.2: FastD Period")]
        public int FastDPeriod { get; set; }

        [AvPropertyName(ExtractPropertyName = "6.3: FastD MA Type")]
        public int FastDMAType { get; set; }

        [AvPropertyName(ExtractPropertyName = "7: Series Type")]
        public AvSeriesTypeEnum SeriesType { get; set; }

        [AvPropertyName(ExtractPropertyName = "8: Time Zone")]
        public override TimeZoneInfo TimeZone { get => base.TimeZone; set => base.TimeZone = value; }
    }
}
