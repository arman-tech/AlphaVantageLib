namespace AlphaVantage.Common.Models.TechnicalIndicators.MINUS_DM
{
    public class AvMINUS_DMBlock : AvBlockAbs<AvMINUS_DMBlock>
    {
        [AvPropertyName(ExtractPropertyName = "MINUS_DM")]
        public decimal MINUS_DM { get; set; }

    }
}
