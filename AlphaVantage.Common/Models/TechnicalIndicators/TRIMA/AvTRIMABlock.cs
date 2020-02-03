namespace AlphaVantage.Common.Models.TechnicalIndicators.TRIMA
{
    public class AvTRIMABlock : AvBlockAbs<AvTRIMABlock>
    {
        [AvPropertyName(ExtractPropertyName = "TRIX")]
        public decimal TRIX { get; set; }

    }
}
