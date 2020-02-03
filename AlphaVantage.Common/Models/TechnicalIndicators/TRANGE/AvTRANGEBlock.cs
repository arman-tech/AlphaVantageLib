namespace AlphaVantage.Common.Models.TechnicalIndicators.TRANGE
{
    public class AvTRANGEBlock : AvBlockAbs<AvTRANGEBlock>
    {
        [AvPropertyName(ExtractPropertyName = "TRANGE")]
        public decimal TRANGE { get; set; }

    }
}
