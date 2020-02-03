namespace AlphaVantage.Common.Models.TechnicalIndicators.ADX
{
    public class AvADXBlock : AvBlockAbs<AvADXBlock>
    {
        [AvPropertyName(ExtractPropertyName = "ADX")]
        public decimal ADX { get; set; }

    }
}
