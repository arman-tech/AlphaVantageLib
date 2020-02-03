namespace AlphaVantage.Common.Models.TechnicalIndicators.PLUS_DI
{
    public class AvPLUS_DIBlock : AvBlockAbs<AvPLUS_DIBlock>
    {
        [AvPropertyName(ExtractPropertyName = "PLUS_DI")]
        public decimal PLUS_DI { get; set; }

    }
}
