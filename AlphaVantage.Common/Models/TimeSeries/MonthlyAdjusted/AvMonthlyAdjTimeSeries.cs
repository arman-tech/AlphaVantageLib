using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaVantage.Common.Models.TimeSeries.MonthlyAdjusted
{
    public class AvMonthlyAdjTimeSeries : AvSeriesObj<AvMonthlyAdjTimeSeries, AvMonthlyAdjTimeSeriesMetaData, AvMonthlyAdjTimeSeriesBlock>
    {

        public override AvMonthlyAdjTimeSeriesMetaData MetaData { get; set; }

        public override IList<AvMonthlyAdjTimeSeriesBlock> TimeSeries { get; set; }

        public AvMonthlyAdjTimeSeries() { }
    }
}
