namespace AlphaVantage.Common.Models.TechnicalIndicators.ULTOSC
{
    public class AvULTOSCBlock : AvBlockAbs<AvULTOSCBlock>
    {
        [AvPropertyName(ExtractPropertyName = "ULTOSC")]
        public decimal ULTOSC { get; set; }

    }
}
