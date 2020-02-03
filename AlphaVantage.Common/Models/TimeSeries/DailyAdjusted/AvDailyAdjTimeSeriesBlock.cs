namespace AlphaVantage.Common.Models.TimeSeries.DailyAdjusted
{
    public class AvDailyAdjTimeSeriesBlock : AvBlockAbs<AvDailyAdjTimeSeriesBlock>
    {

        [AvPropertyName(ExtractPropertyName = "1. open")]
        public decimal Open { get; set; }

        [AvPropertyName(ExtractPropertyName = "2. high")]
        public decimal High { get; set; }

        [AvPropertyName(ExtractPropertyName = "3. low")]
        public decimal Low { get; set; }

        [AvPropertyName(ExtractPropertyName = "4. close")]
        public decimal Close { get; set; }

        [AvPropertyName(ExtractPropertyName = "5. adjusted close")]
        public decimal AdjustedClose { get; set; }

        [AvPropertyName(ExtractPropertyName = "6. volume")]
        public ulong Volume { get; set; }

        [AvPropertyName(ExtractPropertyName = "7. dividend amount")]
        public decimal DividendAmount { get; set; }

        [AvPropertyName(ExtractPropertyName = "8. split coefficient")]
        public decimal SplitCofficient { get; set; }

    }
}