namespace AlphaVantage.Common.Models.TechnicalIndicators.OBV
{
    public class AvOBVBlock : AvBlockAbs<AvOBVBlock>
    {
        [AvPropertyName(ExtractPropertyName = "OBV")]
        public decimal OBV { get; set; }

    }
}
