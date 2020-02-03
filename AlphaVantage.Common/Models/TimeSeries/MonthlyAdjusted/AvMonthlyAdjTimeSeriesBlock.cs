using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaVantage.Common.Models.TimeSeries.MonthlyAdjusted
{
    public class AvMonthlyAdjTimeSeriesBlock : AvBlockAbs<AvMonthlyAdjTimeSeriesBlock>
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
    }
}
