namespace AlphaVantage.Common.Models.TechnicalIndicators.ADOSC
{
    public class AvADOSCBlock : AvBlockAbs<AvADOSCBlock>
    {
        [AvPropertyName(ExtractPropertyName = "ADOSC")]
        public decimal ADOSC { get; set; }

    }
}
