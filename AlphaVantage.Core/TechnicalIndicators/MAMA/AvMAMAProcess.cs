using AlphaVantage.Common;
using AlphaVantage.Common.Models.TechnicalIndicators.MAMA;
using AlphaVantage.Core.Abstracts;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace AlphaVantage.Core.TechnicalIndicators.MAMA
{
    public class AvMAMAProcess : AvMapResourceAbs<AvMAMA, AvMAMAMetaData, AvMAMABlock>
    {
        protected override AvMAMABlock MapToBlock(Dictionary<string, string> block, string dateTime)
        {
            var result = new AvMAMABlock();

            var data = decimal.Parse(block[AvMAMARes.BlockMAMATag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvMAMABlock, decimal, AvPropertyNameAttribute, string>
                (AvMAMARes.BlockMAMATag, result, data, attr => attr.ExtractPropertyName);

            return result;
        }

        protected override AvMAMAMetaData MapToMetaData(Dictionary<string, string> metaData)
        {
            var result = new AvMAMAMetaData();

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvMAMAMetaData, string, AvPropertyNameAttribute, string>
                (AvMAMARes.MetaDataSymbolTag, result, metaData[AvMAMARes.MetaDataSymbolTag],
                attr => attr.ExtractPropertyName);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvMAMAMetaData, string, AvPropertyNameAttribute, string>
                (AvMAMARes.MetaDataIndicatorTag, result, metaData[AvMAMARes.MetaDataIndicatorTag],
                attr => attr.ExtractPropertyName);

            var lastRefreshed = DateTime.Parse(metaData[AvMAMARes.MetaDataLastRefreshedTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvMAMAMetaData, DateTime, AvPropertyNameAttribute, string>
                (AvMAMARes.MetaDataLastRefreshedTag, result,
                lastRefreshed,
                attr => attr.ExtractPropertyName);

            var interval = AvIntervalEnum.FromDisplayName<AvIntervalEnum>(
                metaData[AvMAMARes.MetaDataIntervalTag],
                StringComparison.InvariantCultureIgnoreCase);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvMAMAMetaData, AvIntervalEnum, AvPropertyNameAttribute, string>
                (AvMAMARes.MetaDataIntervalTag, result,
                interval,
                attr => attr.ExtractPropertyName);

            var timeZone = AvTimeZoneConvertor.AvTimeZone(metaData[AvMAMARes.MetaDataTimeZoneTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvMAMAMetaData, TimeZoneInfo, AvPropertyNameAttribute, string>
                (AvMAMARes.MetaDataTimeZoneTag, result,
                timeZone,
                attr => attr.ExtractPropertyName);

            var fastLimit = decimal.Parse(metaData[AvMAMARes.MetaDataFastLimitTag]);
            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvMAMAMetaData, decimal, AvPropertyNameAttribute, string>
                (AvMAMARes.MetaDataFastLimitTag, result,
                fastLimit,
                attr => attr.ExtractPropertyName);

            var slowLimit = decimal.Parse(metaData[AvMAMARes.MetaDataSlowLimitTag]);
            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvMAMAMetaData, decimal, AvPropertyNameAttribute, string>
                (AvMAMARes.MetaDataSlowLimitTag, result,
                slowLimit,
                attr => attr.ExtractPropertyName);

            var seriesType = AvSeriesTypeEnum.FromDisplayName<AvSeriesTypeEnum>(
                metaData[AvMAMARes.MetaDataSeriesTypeTag],
                StringComparison.InvariantCultureIgnoreCase);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvMAMAMetaData, AvSeriesTypeEnum, AvPropertyNameAttribute, string>
                (AvMAMARes.MetaDataSeriesTypeTag, result,
                seriesType,
                attr => attr.ExtractPropertyName);

            return result;
        }

        protected override void ProcessDownloadResource(JObject remoteResource, string uri)
        {
            _metaData = remoteResource[AvMAMAProcessRes.MetaDataTag].ToObject<Dictionary<string, string>>();
            _content = remoteResource[AvMAMAProcessRes.TimeSeriesTag].ToObject<Dictionary<string, Dictionary<string, string>>>();
        }
    }
}
