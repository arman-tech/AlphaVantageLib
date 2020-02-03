using AlphaVantage.Common;
using AlphaVantage.Common.Models.TechnicalIndicators.WMA;
using AlphaVantage.Core.Abstracts;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace AlphaVantage.Core.TechnicalIndicators.WMA
{
    public class AvWMAProcess : AvMapResourceAbs<AvWMA, AvWMAMetaData, AvWMABlock>
    {
        protected override AvWMABlock MapToBlock(Dictionary<string, string> block, string dateTime)
        {
            var result = new AvWMABlock();

            var data = decimal.Parse(block[AvWMARes.BlockWMATag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvWMABlock, decimal, AvPropertyNameAttribute, string>
                (AvWMARes.BlockWMATag, result, data, attr => attr.ExtractPropertyName);

            return result;
        }

        protected override AvWMAMetaData MapToMetaData(Dictionary<string, string> metaData)
        {
            var result = new AvWMAMetaData();

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvWMAMetaData, string, AvPropertyNameAttribute, string>
                (AvWMARes.MetaDataSymbolTag, result, metaData[AvWMARes.MetaDataSymbolTag],
                attr => attr.ExtractPropertyName);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvWMAMetaData, string, AvPropertyNameAttribute, string>
                (AvWMARes.MetaDataIndicatorTag, result, metaData[AvWMARes.MetaDataIndicatorTag],
                attr => attr.ExtractPropertyName);

            var lastRefreshed = DateTime.Parse(metaData[AvWMARes.MetaDataLastRefreshedTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvWMAMetaData, DateTime, AvPropertyNameAttribute, string>
                (AvWMARes.MetaDataLastRefreshedTag, result,
                lastRefreshed,
                attr => attr.ExtractPropertyName);

            var interval = AvIntervalEnum.FromDisplayName<AvIntervalEnum>(
                metaData[AvWMARes.MetaDataIntervalTag],
                StringComparison.InvariantCultureIgnoreCase);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvWMAMetaData, AvIntervalEnum, AvPropertyNameAttribute, string>
                (AvWMARes.MetaDataIntervalTag, result,
                interval,
                attr => attr.ExtractPropertyName);

            var timeZone = AvTimeZoneConvertor.AvTimeZone(metaData[AvWMARes.MetaDataTimeZoneTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvWMAMetaData, TimeZoneInfo, AvPropertyNameAttribute, string>
                (AvWMARes.MetaDataTimeZoneTag, result,
                timeZone,
                attr => attr.ExtractPropertyName);

            var timePeriod = int.Parse(metaData[AvWMARes.MetaDataTimePeriodTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvWMAMetaData, int, AvPropertyNameAttribute, string>
                (AvWMARes.MetaDataTimePeriodTag, result,
                timePeriod,
                attr => attr.ExtractPropertyName);

            var seriesType = AvSeriesTypeEnum.FromDisplayName<AvSeriesTypeEnum>(
                metaData[AvWMARes.MetaDataSeriesTypeTag],
                StringComparison.InvariantCultureIgnoreCase);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvWMAMetaData, AvSeriesTypeEnum, AvPropertyNameAttribute, string>
                (AvWMARes.MetaDataSeriesTypeTag, result,
                seriesType,
                attr => attr.ExtractPropertyName);

            return result;
        }

        protected override void ProcessDownloadResource(JObject remoteResource, string uri)
        {
            _metaData = remoteResource[AvWMAProcessRes.MetaDataTag].ToObject<Dictionary<string, string>>();
            _content = remoteResource[AvWMAProcessRes.TimeSeriesTag].ToObject<Dictionary<string, Dictionary<string, string>>>();
        }
    }
}
