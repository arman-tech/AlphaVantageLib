namespace AlphaVantage.Common.Models.TechnicalIndicators.HT_PHASOR
{
    public class AvHT_PHASORBlock : AvBlockAbs<AvHT_PHASORBlock>
    {
        [AvPropertyName(ExtractPropertyName = "PHASE")]
        public decimal Phase { get; set; }

        [AvPropertyName(ExtractPropertyName = "QUADRATURE")]
        public decimal Quadrature { get; set; }

    }
}
