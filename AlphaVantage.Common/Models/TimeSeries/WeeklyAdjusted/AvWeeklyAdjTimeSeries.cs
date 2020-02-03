using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaVantage.Common.Models.TimeSeries.WeeklyAdjusted
{
    public class AvWeeklyAdjTimeSeries: AvSeriesObj<AvWeeklyAdjTimeSeries, AvWeeklyAdjTimeSeriesMetaData, AvWeeklyAdjTimeSeriesBlock>
    {

        public override AvWeeklyAdjTimeSeriesMetaData MetaData { get; set; }

        public override IList<AvWeeklyAdjTimeSeriesBlock> TimeSeries { get; set; }

        public AvWeeklyAdjTimeSeries()
        {

        }
    }
}
