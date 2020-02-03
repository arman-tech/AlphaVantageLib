namespace AlphaVantage.Common.Models.TechnicalIndicators.T3
{
    public class AvT3Block : AvBlockAbs<AvT3Block>
    {
        [AvPropertyName(ExtractPropertyName = "T3")]
        public decimal T3 { get; set; }

    }
}
