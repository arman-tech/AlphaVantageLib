namespace AlphaVantage.Common.Models.TechnicalIndicators.DEMA
{
    public class AvDEMABlock : AvBlockAbs<AvDEMABlock>
    {
        [AvPropertyName(ExtractPropertyName = "DEMA")]
        public decimal DEMA { get; set; }

    }
}
