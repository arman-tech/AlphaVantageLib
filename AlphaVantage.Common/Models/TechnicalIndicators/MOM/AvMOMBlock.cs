namespace AlphaVantage.Common.Models.TechnicalIndicators.MOM
{
    public class AvMOMBlock : AvBlockAbs<AvMOMBlock>
    {
        [AvPropertyName(ExtractPropertyName = "MOM")]
        public decimal MOM { get; set; }

    }
}
