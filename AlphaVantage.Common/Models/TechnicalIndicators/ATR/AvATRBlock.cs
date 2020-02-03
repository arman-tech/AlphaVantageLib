namespace AlphaVantage.Common.Models.TechnicalIndicators.ATR
{
    public class AvATRBlock : AvBlockAbs<AvATRBlock>
    {
        [AvPropertyName(ExtractPropertyName = "ATR")]
        public decimal ATR { get; set; }

    }
}
