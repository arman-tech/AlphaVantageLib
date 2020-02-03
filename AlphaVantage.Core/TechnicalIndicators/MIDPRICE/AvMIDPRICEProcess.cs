using AlphaVantage.Common;
using AlphaVantage.Common.Models.TechnicalIndicators.MIDPRICE;
using AlphaVantage.Core.Abstracts;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace AlphaVantage.Core.TechnicalIndicators.MIDPRICE
{
    public class AvMIDPRICEProcess : AvMapResourceAbs<AvMIDPRICE, AvMIDPRICEMetaData, AvMIDPRICEBlock>
    {
        protected override AvMIDPRICEBlock MapToBlock(Dictionary<string, string> block, string dateTime)
        {
            var result = new AvMIDPRICEBlock();

            var data = decimal.Parse(block[AvMIDPRICERes.BlockMIDPRICETag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvMIDPRICEBlock, decimal, AvPropertyNameAttribute, string>
                (AvMIDPRICERes.BlockMIDPRICETag, result, data, attr => attr.ExtractPropertyName);

            return result;
        }

        protected override AvMIDPRICEMetaData MapToMetaData(Dictionary<string, string> metaData)
        {
            var result = new AvMIDPRICEMetaData();

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvMIDPRICEMetaData, string, AvPropertyNameAttribute, string>
                (AvMIDPRICERes.MetaDataSymbolTag, result, metaData[AvMIDPRICERes.MetaDataSymbolTag],
                attr => attr.ExtractPropertyName);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvMIDPRICEMetaData, string, AvPropertyNameAttribute, string>
                (AvMIDPRICERes.MetaDataIndicatorTag, result, metaData[AvMIDPRICERes.MetaDataIndicatorTag],
                attr => attr.ExtractPropertyName);

            var lastRefreshed = DateTime.Parse(metaData[AvMIDPRICERes.MetaDataLastRefreshedTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvMIDPRICEMetaData, DateTime, AvPropertyNameAttribute, string>
                (AvMIDPRICERes.MetaDataLastRefreshedTag, result,
                lastRefreshed,
                attr => attr.ExtractPropertyName);

            var interval = AvIntervalEnum.FromDisplayName<AvIntervalEnum>(
                metaData[AvMIDPRICERes.MetaDataIntervalTag],
                StringComparison.InvariantCultureIgnoreCase);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvMIDPRICEMetaData, AvIntervalEnum, AvPropertyNameAttribute, string>
                (AvMIDPRICERes.MetaDataIntervalTag, result,
                interval,
                attr => attr.ExtractPropertyName);

            var timeZone = AvTimeZoneConvertor.AvTimeZone(metaData[AvMIDPRICERes.MetaDataTimeZoneTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvMIDPRICEMetaData, TimeZoneInfo, AvPropertyNameAttribute, string>
                (AvMIDPRICERes.MetaDataTimeZoneTag, result,
                timeZone,
                attr => attr.ExtractPropertyName);

            var timePeriod = int.Parse(metaData[AvMIDPRICERes.MetaDataTimePeriodTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvMIDPRICEMetaData, int, AvPropertyNameAttribute, string>
                (AvMIDPRICERes.MetaDataTimePeriodTag, result,
                timePeriod,
                attr => attr.ExtractPropertyName);


            return result;
        }

        protected override void ProcessDownloadResource(JObject remoteResource, string uri)
        {
            _metaData = remoteResource[AvMIDPRICEProcessRes.MetaDataTag].ToObject<Dictionary<string, string>>();
            _content = remoteResource[AvMIDPRICEProcessRes.TimeSeriesTag].ToObject<Dictionary<string, Dictionary<string, string>>>();
        }
    }
}
