using System;

namespace AlphaVantage.Common.Models.TimeSeries.IntraDay
{
    public class AvIntraDayTimeSeriesMetaData : AvMetaDataAbs<AvIntraDayTimeSeriesMetaData>
    {
        public AvIntraDayTimeSeriesMetaData()
        {
            base.Type = AvMetaDataTypeEnum.StockTimeSeries;
            base.Function = AvFunctionEnum.InteraDay;
        }

        [AvPropertyName(ExtractPropertyName = "1. Information")]
        public string Information { get; set; }

        [AvPropertyName(ExtractPropertyName = "2. Symbol")]
        public override string Symbol { get; set; }

        [AvPropertyName(ExtractPropertyName = "3. Last Refreshed")]
        public override DateTime LastRefreshed { get; set; }

        [AvPropertyName(ExtractPropertyName = "4. Interval")]
        public AvIntervalEnum Interval { get; set; }

        [AvPropertyName(ExtractPropertyName = "5. Output Size")]
        public AvOutputSizeEnum OutputSize { get; set; }

        [AvPropertyName(ExtractPropertyName = "6. Time Zone")]
        public override TimeZoneInfo TimeZone { get; set; }

    }
}
