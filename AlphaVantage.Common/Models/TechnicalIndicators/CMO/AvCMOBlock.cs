namespace AlphaVantage.Common.Models.TechnicalIndicators.CMO
{
    public class AvCMOBlock : AvBlockAbs<AvCMOBlock>
    {
        [AvPropertyName(ExtractPropertyName = "CMO")]
        public decimal CMO { get; set; }

    }
}
