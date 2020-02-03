namespace AlphaVantage.Common.Models.TechnicalIndicators.AD
{
    public class AvADBlock : AvBlockAbs<AvADBlock>
    {
        [AvPropertyName(ExtractPropertyName = "Chaikin A/D")]
        public decimal ChaikinAD { get; set; }

    }
}
