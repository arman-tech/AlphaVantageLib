namespace AlphaVantage.Common.Models.TechnicalIndicators.MAMA
{
    public class AvMAMABlock : AvBlockAbs<AvMAMABlock>
    {
        [AvPropertyName(ExtractPropertyName = "MAMA")]
        public decimal MAMA { get; set; }

        [AvPropertyName(ExtractPropertyName = "FAMA")]
        public decimal FAMA { get; set; }
    }
}
