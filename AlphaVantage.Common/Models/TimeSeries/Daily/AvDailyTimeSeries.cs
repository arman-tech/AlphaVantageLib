using System.Collections.Generic;

namespace AlphaVantage.Common.Models.TimeSeries.Daily
{

    public class AvDailyTimeSeries : AvSeriesObj<AvDailyTimeSeries, AvDailyTimeSeriesMetaData, AvDailyTimeSeriesBlock>
    {
        public override AvDailyTimeSeriesMetaData MetaData { get; set; }
        public override IList<AvDailyTimeSeriesBlock> TimeSeries { get; set; }

        public AvDailyTimeSeries()
        {

        }
    }
}
