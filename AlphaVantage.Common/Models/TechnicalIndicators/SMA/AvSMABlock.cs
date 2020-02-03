namespace AlphaVantage.Common.Models.TechnicalIndicators.SMA
{
    public class AvSMABlock : AvBlockAbs<AvSMABlock>
    {
        [AvPropertyName(ExtractPropertyName = "SMA")]
        public decimal SMA { get; set; }
    }
}
