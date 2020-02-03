using System.Collections.Generic;

namespace AlphaVantage.Common.Models.TechnicalIndicators.SMA
{
    public class AvSMA : AvSeriesObj<AvSMA, AvSMAMetaData, AvSMABlock>
    {
        public override AvSMAMetaData MetaData { get; set; }
        public override IList<AvSMABlock> TimeSeries { get; set; }

        public AvSMA() { }
    }
}
