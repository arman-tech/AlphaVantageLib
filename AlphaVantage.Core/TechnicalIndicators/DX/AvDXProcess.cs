using AlphaVantage.Common;
using AlphaVantage.Common.Models.TechnicalIndicators.DX;
using AlphaVantage.Core.Abstracts;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace AlphaVantage.Core.TechnicalIndicators.DX
{
    public class AvDXProcess : AvMapResourceAbs<AvDX, AvDXMetaData, AvDXBlock>
    {
        protected override AvDXBlock MapToBlock(Dictionary<string, string> block, string dateTime)
        {
            var result = new AvDXBlock();

            var data = decimal.Parse(block[AvDXRes.BlockDXTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvDXBlock, decimal, AvPropertyNameAttribute, string>
                (AvDXRes.BlockDXTag, result, data, attr => attr.ExtractPropertyName);

            return result;
        }

        protected override AvDXMetaData MapToMetaData(Dictionary<string, string> metaData)
        {
            var result = new AvDXMetaData();

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvDXMetaData, string, AvPropertyNameAttribute, string>
                (AvDXRes.MetaDataSymbolTag, result, metaData[AvDXRes.MetaDataSymbolTag],
                attr => attr.ExtractPropertyName);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvDXMetaData, string, AvPropertyNameAttribute, string>
                (AvDXRes.MetaDataIndicatorTag, result, metaData[AvDXRes.MetaDataIndicatorTag],
                attr => attr.ExtractPropertyName);

            var lastRefreshed = DateTime.Parse(metaData[AvDXRes.MetaDataLastRefreshedTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvDXMetaData, DateTime, AvPropertyNameAttribute, string>
                (AvDXRes.MetaDataLastRefreshedTag, result,
                lastRefreshed,
                attr => attr.ExtractPropertyName);

            var interval = AvIntervalEnum.FromDisplayName<AvIntervalEnum>(
                metaData[AvDXRes.MetaDataIntervalTag],
                StringComparison.InvariantCultureIgnoreCase);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvDXMetaData, AvIntervalEnum, AvPropertyNameAttribute, string>
                (AvDXRes.MetaDataIntervalTag, result,
                interval,
                attr => attr.ExtractPropertyName);

            var timeZone = AvTimeZoneConvertor.AvTimeZone(metaData[AvDXRes.MetaDataTimeZoneTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvDXMetaData, TimeZoneInfo, AvPropertyNameAttribute, string>
                (AvDXRes.MetaDataTimeZoneTag, result,
                timeZone,
                attr => attr.ExtractPropertyName);

            var timePeriod = int.Parse(metaData[AvDXRes.MetaDataTimePeriodTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvDXMetaData, int, AvPropertyNameAttribute, string>
                (AvDXRes.MetaDataTimePeriodTag, result,
                timePeriod,
                attr => attr.ExtractPropertyName);

            return result;
        }

        protected override void ProcessDownloadResource(JObject remoteResource, string uri)
        {
            _metaData = remoteResource[AvDXProcessRes.MetaDataTag].ToObject<Dictionary<string, string>>();
            _content = remoteResource[AvDXProcessRes.TimeSeriesTag].ToObject<Dictionary<string, Dictionary<string, string>>>();
        }
    }
}
