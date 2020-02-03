namespace AlphaVantage.Common.Models.TechnicalIndicators.STOCHF
{
    public class AvSTOCHFBlock : AvBlockAbs<AvSTOCHFBlock>
    {
        [AvPropertyName(ExtractPropertyName = "FastK")]
        public decimal FastK { get; set; }

        [AvPropertyName(ExtractPropertyName = "FastD")]
        public decimal FastD { get; set; }

    }
}
