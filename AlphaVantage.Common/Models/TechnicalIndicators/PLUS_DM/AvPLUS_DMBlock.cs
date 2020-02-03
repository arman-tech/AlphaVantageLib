namespace AlphaVantage.Common.Models.TechnicalIndicators.PLUS_DM
{
    public class AvPLUS_DMBlock : AvBlockAbs<AvPLUS_DMBlock>
    {
        [AvPropertyName(ExtractPropertyName = "PLUS_DM")]
        public decimal PLUS_DM { get; set; }

    }
}
