namespace AlphaVantage.Common.Models.TechnicalIndicators.DX
{
    public class AvDXBlock : AvBlockAbs<AvDXBlock>
    {
        [AvPropertyName(ExtractPropertyName = "DX")]
        public decimal DX { get; set; }

    }
}
