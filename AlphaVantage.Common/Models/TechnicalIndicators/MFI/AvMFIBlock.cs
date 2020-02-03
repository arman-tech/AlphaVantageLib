namespace AlphaVantage.Common.Models.TechnicalIndicators.MFI
{
    public class AvMFIBlock : AvBlockAbs<AvMFIBlock>
    {
        [AvPropertyName(ExtractPropertyName = "MFI")]
        public decimal MFI { get; set; }

    }
}
