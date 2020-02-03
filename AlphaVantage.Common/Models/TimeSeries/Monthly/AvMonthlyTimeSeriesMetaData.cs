﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaVantage.Common.Models.TimeSeries.Monthly
{
    public class AvMonthlyTimeSeriesMetaData : AvMetaDataAbs<AvMonthlyTimeSeriesMetaData>
    {
        public AvMonthlyTimeSeriesMetaData()
        {
            base.Type = AvMetaDataTypeEnum.StockTimeSeries;
            base.Function = AvFunctionEnum.Monthly;
        }

        [AvPropertyName(ExtractPropertyName = "1. Information")]
        public string Information { get; set; }

        [AvPropertyName(ExtractPropertyName = "2. Symbol")]
        public override string Symbol { get; set; }

        [AvPropertyName(ExtractPropertyName = "3. Last Refreshed")]
        public override DateTime LastRefreshed { get; set; }

        [AvPropertyName(ExtractPropertyName = "4. Time Zone")]
        public override TimeZoneInfo TimeZone { get; set; }
    }
}
