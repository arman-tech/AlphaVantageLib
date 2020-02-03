namespace AlphaVantage.Common.Models.TechnicalIndicators.AROON
{
    public class AvAROONBlock : AvBlockAbs<AvAROONBlock>
    {
        [AvPropertyName(ExtractPropertyName = "Aroon Up")]
        public decimal AroonUp { get; set; }

        [AvPropertyName(ExtractPropertyName = "Aroon Down")]
        public decimal AroonDown { get; set; }
    }
}
