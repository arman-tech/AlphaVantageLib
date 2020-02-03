namespace AlphaVantage.Common.Models.TechnicalIndicators.ROCR
{
    public class AvROCRBlock : AvBlockAbs<AvROCRBlock>
    {
        [AvPropertyName(ExtractPropertyName = "ROCR")]
        public decimal ROCR { get; set; }

    }
}
