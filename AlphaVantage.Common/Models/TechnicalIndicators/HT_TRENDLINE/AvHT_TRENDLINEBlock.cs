namespace AlphaVantage.Common.Models.TechnicalIndicators.HT_TRENDLINE
{
    public class AvHT_TRENDLINEBlock : AvBlockAbs<AvHT_TRENDLINEBlock>
    {
        [AvPropertyName(ExtractPropertyName = "HT_TRENDLINE")]
        public decimal HT_TRENDLINE { get; set; }

    }
}
