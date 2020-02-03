namespace AlphaVantage.Common.Models.TechnicalIndicators.HT_SINE
{
    public class AvHT_SINEBlock : AvBlockAbs<AvHT_SINEBlock>
    {
        [AvPropertyName(ExtractPropertyName = "LEAD SINE")]
        public decimal LeadSine { get; set; }

        [AvPropertyName(ExtractPropertyName = "SINE")]
        public decimal Sine { get; set; }

    }
}
