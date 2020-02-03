using System.Collections.Generic;

namespace AlphaVantage.Common.Models.TimeSeries.DailyAdjusted
{

    public class AvDailyAdjTimeSeries : AvSeriesObj<AvDailyAdjTimeSeries, AvDailyAdjTimeSeriesMetaData, AvDailyAdjTimeSeriesBlock>
    {

        public override AvDailyAdjTimeSeriesMetaData MetaData { get; set; }

        public override IList<AvDailyAdjTimeSeriesBlock> TimeSeries { get; set; }

        public AvDailyAdjTimeSeries()
        {

        }

    }
}
