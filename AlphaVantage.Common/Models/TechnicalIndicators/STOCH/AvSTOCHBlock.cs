namespace AlphaVantage.Common.Models.TechnicalIndicators.STOCH
{
    public class AvSTOCHBlock : AvBlockAbs<AvSTOCHBlock>
    {
        [AvPropertyName(ExtractPropertyName = "SlowK")]
        public decimal SlowK { get; set; }

        [AvPropertyName(ExtractPropertyName = "SlowD")]
        public decimal SlowD { get; set; }

    }
}
