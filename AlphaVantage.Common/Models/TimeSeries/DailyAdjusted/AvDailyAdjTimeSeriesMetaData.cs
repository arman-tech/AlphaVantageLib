using System;

namespace AlphaVantage.Common.Models.TimeSeries.DailyAdjusted
{
    public class AvDailyAdjTimeSeriesMetaData : AvMetaDataAbs<AvDailyAdjTimeSeriesMetaData>
    {
        public AvDailyAdjTimeSeriesMetaData()
        {
            base.Type = AvMetaDataTypeEnum.StockTimeSeries;
            base.Function = AvFunctionEnum.DailyAdjusted;
        }

        [AvPropertyName(ExtractPropertyName = "1. Information")]
        public string Information { get; set; }

        [AvPropertyName(ExtractPropertyName = "2. Symbol")]
        public override string Symbol { get; set; }

        [AvPropertyName(ExtractPropertyName = "3. Last Refreshed")]
        public override DateTime LastRefreshed { get; set; }

        [AvPropertyName(ExtractPropertyName = "4. Output Size")]
        public AvOutputSizeEnum OutputSize { get; set; }

        [AvPropertyName(ExtractPropertyName = "5. Time Zone")]
        public override TimeZoneInfo TimeZone { get; set; }

    }
}