namespace AlphaVantage.Common.Models.TechnicalIndicators.TEMA
{
    public class AvTEMABlock : AvBlockAbs<AvTEMABlock>
    {
        [AvPropertyName(ExtractPropertyName = "TEMA")]
        public decimal TEMA { get; set; }

    }
}
