namespace AlphaVantage.Common.Models.TechnicalIndicators.ADXR
{
    public class AvADXRBlock : AvBlockAbs<AvADXRBlock>
    {
        [AvPropertyName(ExtractPropertyName = "ADXR")]
        public decimal ADXR { get; set; }

    }
}
