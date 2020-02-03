namespace AlphaVantage.Common.Models.TechnicalIndicators.TRIX
{
    public class AvTRIXBlock : AvBlockAbs<AvTRIXBlock>
    {
        [AvPropertyName(ExtractPropertyName = "TRIX")]
        public decimal TRIX { get; set; }

    }
}
