using System.Collections.Generic;

namespace AlphaVantage.Common.Models.TechnicalIndicators.RSI
{

    public class AvRSI : AvSeriesObj<AvRSI, AvRSIMetaData, AvRSIBlock>
    {
        public override AvRSIMetaData MetaData { get; set; }
        public override IList<AvRSIBlock> TimeSeries { get; set; }

        public AvRSI() { }

    }
}
