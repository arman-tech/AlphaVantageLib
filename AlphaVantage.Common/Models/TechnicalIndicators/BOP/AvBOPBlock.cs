namespace AlphaVantage.Common.Models.TechnicalIndicators.BOP
{
    public class AvBOPBlock : AvBlockAbs<AvBOPBlock>
    {
        [AvPropertyName(ExtractPropertyName = "BOP")]
        public decimal BOP { get; set; }

    }
}
