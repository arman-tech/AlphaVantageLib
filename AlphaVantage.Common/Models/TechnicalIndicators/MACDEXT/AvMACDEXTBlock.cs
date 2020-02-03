namespace AlphaVantage.Common.Models.TechnicalIndicators.MACDEXT
{
    public class AvMACDEXTBlock : AvBlockAbs<AvMACDEXTBlock>
    {
        [AvPropertyName(ExtractPropertyName = "MACD")]
        public decimal MACD { get; set; }

        [AvPropertyName(ExtractPropertyName = "MACD_Hist")]
        public decimal MACDHist { get; set; }

        [AvPropertyName(ExtractPropertyName = "MACD_Signal")]
        public decimal MACDSignal { get; set; }

    }
}
