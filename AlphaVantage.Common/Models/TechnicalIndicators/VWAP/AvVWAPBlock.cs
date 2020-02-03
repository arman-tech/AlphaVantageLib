namespace AlphaVantage.Common.Models.TechnicalIndicators.VWAP
{
    public class AvVWAPBlock : AvBlockAbs<AvVWAPBlock>
    {
        [AvPropertyName(ExtractPropertyName = "VWAP")]
        public decimal VWAP { get; set; }

    }
}
