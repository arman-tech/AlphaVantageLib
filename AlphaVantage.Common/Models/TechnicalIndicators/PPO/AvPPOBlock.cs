namespace AlphaVantage.Common.Models.TechnicalIndicators.PPO
{
    public class AvPPOBlock : AvBlockAbs<AvPPOBlock>
    {
        [AvPropertyName(ExtractPropertyName = "PPO")]
        public decimal PPO { get; set; }

    }
}
