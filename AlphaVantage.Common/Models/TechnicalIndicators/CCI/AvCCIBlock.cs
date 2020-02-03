namespace AlphaVantage.Common.Models.TechnicalIndicators.CCI
{
    public class AvCCIBlock : AvBlockAbs<AvCCIBlock>
    {
        [AvPropertyName(ExtractPropertyName = "CCI")]
        public decimal CCI { get; set; }

    }
}
