namespace AlphaVantage.Common.Models.TechnicalIndicators.ROC
{
    public class AvROCBlock : AvBlockAbs<AvROCBlock>
    {
        [AvPropertyName(ExtractPropertyName = "ROC")]
        public decimal ROC { get; set; }

    }
}
