namespace AlphaVantage.Common.Models.TechnicalIndicators.MIDPOINT
{
    public class AvMIDPOINTBlock : AvBlockAbs<AvMIDPOINTBlock>
    {
        [AvPropertyName(ExtractPropertyName = "MIDPOINT")]
        public decimal MIDPOINT { get; set; }

    }
}
