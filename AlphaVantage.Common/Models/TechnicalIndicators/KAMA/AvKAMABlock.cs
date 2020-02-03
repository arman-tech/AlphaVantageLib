namespace AlphaVantage.Common.Models.TechnicalIndicators.KAMA
{
    public class AvKAMABlock : AvBlockAbs<AvKAMABlock>
    {
        [AvPropertyName(ExtractPropertyName = "KAMA")]
        public decimal KAMA { get; set; }

    }
}
