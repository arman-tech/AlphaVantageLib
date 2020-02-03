using AlphaVantage.Common;
using AlphaVantage.Common.Models.TechnicalIndicators.WILLR;
using AlphaVantage.Core.Abstracts;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace AlphaVantage.Core.TechnicalIndicators.WILLR
{
    public class AvWILLRProcess : AvMapResourceAbs<AvWILLR, AvWILLRMetaData, AvWILLRBlock>
    {
        protected override AvWILLRBlock MapToBlock(Dictionary<string, string> block, string dateTime)
        {
            var result = new AvWILLRBlock();

            var data = decimal.Parse(block[AvWILLRRes.BlockWILLRTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvWILLRBlock, decimal, AvPropertyNameAttribute, string>
                (AvWILLRRes.BlockWILLRTag, result, data, attr => attr.ExtractPropertyName);

            return result;
        }

        protected override AvWILLRMetaData MapToMetaData(Dictionary<string, string> metaData)
        {
            var result = new AvWILLRMetaData();

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvWILLRMetaData, string, AvPropertyNameAttribute, string>
                (AvWILLRRes.MetaDataSymbolTag, result, metaData[AvWILLRRes.MetaDataSymbolTag],
                attr => attr.ExtractPropertyName);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvWILLRMetaData, string, AvPropertyNameAttribute, string>
                (AvWILLRRes.MetaDataIndicatorTag, result, metaData[AvWILLRRes.MetaDataIndicatorTag],
                attr => attr.ExtractPropertyName);

            var lastRefreshed = DateTime.Parse(metaData[AvWILLRRes.MetaDataLastRefreshedTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvWILLRMetaData, DateTime, AvPropertyNameAttribute, string>
                (AvWILLRRes.MetaDataLastRefreshedTag, result,
                lastRefreshed,
                attr => attr.ExtractPropertyName);

            var interval = AvIntervalEnum.FromDisplayName<AvIntervalEnum>(
                metaData[AvWILLRRes.MetaDataIntervalTag],
                StringComparison.InvariantCultureIgnoreCase);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvWILLRMetaData, AvIntervalEnum, AvPropertyNameAttribute, string>
                (AvWILLRRes.MetaDataIntervalTag, result,
                interval,
                attr => attr.ExtractPropertyName);

            var timeZone = AvTimeZoneConvertor.AvTimeZone(metaData[AvWILLRRes.MetaDataTimeZoneTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvWILLRMetaData, TimeZoneInfo, AvPropertyNameAttribute, string>
                (AvWILLRRes.MetaDataTimeZoneTag, result,
                timeZone,
                attr => attr.ExtractPropertyName);

            var timePeriod = int.Parse(metaData[AvWILLRRes.MetaDataTimePeriodTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvWILLRMetaData, int, AvPropertyNameAttribute, string>
                (AvWILLRRes.MetaDataTimePeriodTag, result,
                timePeriod,
                attr => attr.ExtractPropertyName);

            return result;
        }

        protected override void ProcessDownloadResource(JObject remoteResource, string uri)
        {
            _metaData = remoteResource[AvWILLRProcessRes.MetaDataTag].ToObject<Dictionary<string, string>>();
            _content = remoteResource[AvWILLRProcessRes.TimeSeriesTag].ToObject<Dictionary<string, Dictionary<string, string>>>();
        }
    }
}
