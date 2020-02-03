using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaVantage.Common.Models.TechnicalIndicators.RSI
{
    public class AvRSIBlock : AvBlockAbs<AvRSIBlock>
    {
        [AvPropertyName(ExtractPropertyName = "RSI")]
        public decimal RSI { get; set; }
    }
}
