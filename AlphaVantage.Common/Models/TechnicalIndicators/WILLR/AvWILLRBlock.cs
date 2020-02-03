namespace AlphaVantage.Common.Models.TechnicalIndicators.WILLR
{
    public class AvWILLRBlock : AvBlockAbs<AvWILLRBlock>
    {
        [AvPropertyName(ExtractPropertyName = "WILLR")]
        public decimal WILLR { get; set; }

    }
}
