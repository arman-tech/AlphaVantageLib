namespace AlphaVantage.Common.Models.TechnicalIndicators.EMA
{
    public class AvEMABlock : AvBlockAbs<AvEMABlock>
    {
        [AvPropertyName(ExtractPropertyName = "EMA")]
        public decimal EMA { get; set; }

    }
}
