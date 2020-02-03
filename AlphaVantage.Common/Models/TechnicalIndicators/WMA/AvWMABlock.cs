namespace AlphaVantage.Common.Models.TechnicalIndicators.WMA
{
    public class AvWMABlock : AvBlockAbs<AvWMABlock>
    {
        [AvPropertyName(ExtractPropertyName = "WMA")]
        public decimal WMA { get; set; }

    }
}
