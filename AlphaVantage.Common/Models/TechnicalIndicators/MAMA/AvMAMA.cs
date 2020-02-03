using System.Collections.Generic;

namespace AlphaVantage.Common.Models.TechnicalIndicators.MAMA
{
    public class AvMAMA : AvSeriesObj<AvMAMA, AvMAMAMetaData, AvMAMABlock>
    {
        public override AvMAMAMetaData MetaData { get; set; }
        public override IList<AvMAMABlock> TimeSeries { get; set; }

        public AvMAMA() { }

    }
}
