namespace AlphaVantage.Common.Models.TechnicalIndicators.MINUS_DI
{
    public class AvMINUS_DIBlock : AvBlockAbs<AvMINUS_DIBlock>
    {
        [AvPropertyName(ExtractPropertyName = "MINUS_DI")]
        public decimal MINUS_DI { get; set; }

    }
}
