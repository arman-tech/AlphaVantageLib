using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaVantage.Common.Models.TimeSeries.Weekly
{
    public class AvWeeklyTimeSeries : AvSeriesObj<AvWeeklyTimeSeries, AvWeeklyTimeSeriesMetaData, AvWeeklyTimeSeriesBlock>
    {

        public override AvWeeklyTimeSeriesMetaData MetaData { get; set; }

        public override IList<AvWeeklyTimeSeriesBlock> TimeSeries { get; set; }

        public AvWeeklyTimeSeries() { }
    }
}
