using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaVantage.Common.Models.TimeSeries.Monthly
{
    public class AvMonthlyTimeSeries : AvSeriesObj<AvMonthlyTimeSeries, AvMonthlyTimeSeriesMetaData, AvMonthlyTimeSeriesBlock>
    {

        public override AvMonthlyTimeSeriesMetaData MetaData { get; set; }

        public override IList<AvMonthlyTimeSeriesBlock> TimeSeries { get; set; }

        public AvMonthlyTimeSeries(){}
    }
}
