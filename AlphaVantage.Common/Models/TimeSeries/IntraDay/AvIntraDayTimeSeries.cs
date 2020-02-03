using System.Collections.Generic;

namespace AlphaVantage.Common.Models.TimeSeries.IntraDay
{
    public class AvIntraDayTimeSeries : AvSeriesObj<AvIntraDayTimeSeries, AvIntraDayTimeSeriesMetaData, AvIntraDayTimeSeriesBlock>
    {

        public override AvIntraDayTimeSeriesMetaData MetaData { get; set; }
        public override IList<AvIntraDayTimeSeriesBlock> TimeSeries { get; set; }

        public AvIntraDayTimeSeries()
        {

        }
    }
}
