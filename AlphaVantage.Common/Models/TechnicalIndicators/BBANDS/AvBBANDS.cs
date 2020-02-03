using System.Collections.Generic;

namespace AlphaVantage.Common.Models.TechnicalIndicators.BBANDS
{
    public class AvBBANDS : AvSeriesObj<AvBBANDS, AvBBANDSMetaData, AvBBANDSBlock>
    {
        public override AvBBANDSMetaData MetaData { get; set; }
        public override IList<AvBBANDSBlock> TimeSeries { get; set; }

        public AvBBANDS() { }

    }
}
