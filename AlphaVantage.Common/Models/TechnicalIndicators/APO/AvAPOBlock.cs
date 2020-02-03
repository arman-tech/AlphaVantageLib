namespace AlphaVantage.Common.Models.TechnicalIndicators.APO
{
    public class AvAPOBlock : AvBlockAbs<AvAPOBlock>
    {
        [AvPropertyName(ExtractPropertyName = "APO")]
        public decimal APO { get; set; }

    }
}
