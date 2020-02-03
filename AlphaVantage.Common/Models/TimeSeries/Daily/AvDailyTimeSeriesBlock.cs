namespace AlphaVantage.Common.Models.TimeSeries.Daily
{
    public class AvDailyTimeSeriesBlock : AvBlockAbs<AvDailyTimeSeriesBlock>
    {

        [AvPropertyName(ExtractPropertyName = "1. open")]
        public decimal Open { get; set; }

        [AvPropertyName(ExtractPropertyName = "2. high")]
        public decimal High { get; set; }

        [AvPropertyName(ExtractPropertyName = "3. low")]
        public decimal Low { get; set; }

        [AvPropertyName(ExtractPropertyName = "4. close")]
        public decimal Close { get; set; }

        [AvPropertyName(ExtractPropertyName = "5. volume")]
        public ulong Volume { get; set; }
    }
}
