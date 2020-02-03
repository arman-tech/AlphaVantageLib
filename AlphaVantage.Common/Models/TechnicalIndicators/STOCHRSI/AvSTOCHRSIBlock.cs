namespace AlphaVantage.Common.Models.TechnicalIndicators.STOCHRSI
{
    public class AvSTOCHRSIBlock : AvBlockAbs<AvSTOCHRSIBlock>
    {
        [AvPropertyName(ExtractPropertyName = "FastK")]
        public decimal FastK { get; set; }

        [AvPropertyName(ExtractPropertyName = "FastD")]
        public decimal FastD { get; set; }

    }
}
