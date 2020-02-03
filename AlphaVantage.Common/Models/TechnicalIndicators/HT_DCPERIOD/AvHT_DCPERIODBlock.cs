namespace AlphaVantage.Common.Models.TechnicalIndicators.HT_DCPERIOD
{
    public class AvHT_DCPERIODBlock : AvBlockAbs<AvHT_DCPERIODBlock>
    {
        [AvPropertyName(ExtractPropertyName = "DCPERIOD")]
        public decimal DCPERIOD { get; set; }

    }
}
