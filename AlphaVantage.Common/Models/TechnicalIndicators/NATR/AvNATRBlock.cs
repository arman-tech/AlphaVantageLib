namespace AlphaVantage.Common.Models.TechnicalIndicators.NATR
{ 
    public class AvNATRBlock : AvBlockAbs<AvNATRBlock>
    {
        [AvPropertyName(ExtractPropertyName = "T3")]
        public decimal T3 { get; set; }

    }
}
