namespace AlphaVantage.Common.Models.TechnicalIndicators.SAR
{
    public class AvSARBlock : AvBlockAbs<AvSARBlock>
    {
        [AvPropertyName(ExtractPropertyName = "SAR")]
        public decimal SAR { get; set; }

    }
}
