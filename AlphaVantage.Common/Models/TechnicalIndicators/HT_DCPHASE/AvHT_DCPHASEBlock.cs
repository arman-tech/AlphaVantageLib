namespace AlphaVantage.Common.Models.TechnicalIndicators.HT_DCPHASE
{
    public class AvHT_DCPHASEBlock : AvBlockAbs<AvHT_DCPHASEBlock>
    {
        [AvPropertyName(ExtractPropertyName = "HT_DCPHASE")]
        public decimal HT_DCPHASE { get; set; }

    }
}
