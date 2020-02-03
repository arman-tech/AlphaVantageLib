namespace AlphaVantage.Common.Models.TechnicalIndicators.AROONOSC
{
    public class AvAROONOSCBlock : AvBlockAbs<AvAROONOSCBlock>
    {
        [AvPropertyName(ExtractPropertyName = "AROONOSC")]
        public decimal AROONOSC { get; set; }

    }
}
