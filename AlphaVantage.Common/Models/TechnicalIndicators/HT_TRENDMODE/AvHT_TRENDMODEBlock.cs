namespace AlphaVantage.Common.Models.TechnicalIndicators.HT_TRENDMODE
{
    public class AvHT_TRENDMODEBlock : AvBlockAbs<AvHT_TRENDMODEBlock>
    {
        [AvPropertyName(ExtractPropertyName = "TRENDMODE")]
        public decimal TRENDMODE { get; set; }

    }
}
