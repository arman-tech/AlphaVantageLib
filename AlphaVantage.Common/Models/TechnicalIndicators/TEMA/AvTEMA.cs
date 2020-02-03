using System.Collections.Generic;

namespace AlphaVantage.Common.Models.TechnicalIndicators.TEMA
{
    public class AvTEMA : AvSeriesObj<AvTEMA, AvTEMAMetaData, AvTEMABlock>
    {
        public override AvTEMAMetaData MetaData { get; set; }
        public override IList<AvTEMABlock> TimeSeries { get; set; }

        public AvTEMA() { }

    }
}
