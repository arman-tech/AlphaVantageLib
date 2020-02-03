namespace AlphaVantage.Common.Models.TechnicalIndicators.MIDPRICE
{
    public class AvMIDPRICEBlock : AvBlockAbs<AvMIDPRICEBlock>
    {
        [AvPropertyName(ExtractPropertyName = "MIDPRICE")]
        public decimal MIDPRICE { get; set; }

    }
}
