namespace AlphaVantage.Common.Models.TechnicalIndicators.BBANDS
{
    public class AvBBANDSBlock : AvBlockAbs<AvBBANDSBlock>
    {
        [AvPropertyName(ExtractPropertyName = "Real Lower Band")]
        public decimal RealLowerBand { get; set; }

        [AvPropertyName(ExtractPropertyName = "Real Middle Band")]
        public decimal RealMiddleBand { get; set; }

        [AvPropertyName(ExtractPropertyName = "Real Upper Band")]
        public decimal RealUpperBand { get; set; }
    }
}
