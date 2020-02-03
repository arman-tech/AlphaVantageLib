using AlphaVantage.Common;
using AlphaVantage.Common.Models.TechnicalIndicators.EMA;
using AlphaVantage.Core.Abstracts;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace AlphaVantage.Core.TechnicalIndicators.EMA
{
    public class AvEMAProcess : AvMapResourceAbs<AvEMA, AvEMAMetaData, AvEMABlock>
    {
        protected override AvEMABlock MapToBlock(Dictionary<string, string> block, string dateTime)
        {
            var result = new AvEMABlock();

            var data = decimal.Parse(block[AvEMARes.BlockEMATag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvEMABlock, decimal, AvPropertyNameAttribute, string>
                (AvEMARes.BlockEMATag, result, data, attr => attr.ExtractPropertyName);

            return result;
        }

        protected override AvEMAMetaData MapToMetaData(Dictionary<string, string> metaData)
        {
            var result = new AvEMAMetaData();

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvEMAMetaData, string, AvPropertyNameAttribute, string>
                (AvEMARes.MetaDataSymbolTag, result, metaData[AvEMARes.MetaDataSymbolTag],
                attr => attr.ExtractPropertyName);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvEMAMetaData, string, AvPropertyNameAttribute, string>
                (AvEMARes.MetaDataIndicatorTag, result, metaData[AvEMARes.MetaDataIndicatorTag],
                attr => attr.ExtractPropertyName);

            var lastRefreshed = DateTime.Parse(metaData[AvEMARes.MetaDataLastRefreshedTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvEMAMetaData, DateTime, AvPropertyNameAttribute, string>
                (AvEMARes.MetaDataLastRefreshedTag, result,
                lastRefreshed,
                attr => attr.ExtractPropertyName);

            var interval = AvIntervalEnum.FromDisplayName<AvIntervalEnum>(
                metaData[AvEMARes.MetaDataIntervalTag],
                StringComparison.InvariantCultureIgnoreCase);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvEMAMetaData, AvIntervalEnum, AvPropertyNameAttribute, string>
                (AvEMARes.MetaDataIntervalTag, result,
                interval,
                attr => attr.ExtractPropertyName);

            var timeZone = AvTimeZoneConvertor.AvTimeZone(metaData[AvEMARes.MetaDataTimeZoneTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvEMAMetaData, TimeZoneInfo, AvPropertyNameAttribute, string>
                (AvEMARes.MetaDataTimeZoneTag, result,
                timeZone,
                attr => attr.ExtractPropertyName);

            var timePeriod = int.Parse(metaData[AvEMARes.MetaDataTimePeriodTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvEMAMetaData, int, AvPropertyNameAttribute, string>
                (AvEMARes.MetaDataTimePeriodTag, result,
                timePeriod,
                attr => attr.ExtractPropertyName);

            var seriesType = AvSeriesTypeEnum.FromDisplayName<AvSeriesTypeEnum>(
                metaData[AvEMARes.MetaDataSeriesTypeTag],
                StringComparison.InvariantCultureIgnoreCase);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvEMAMetaData, AvSeriesTypeEnum, AvPropertyNameAttribute, string>
                (AvEMARes.MetaDataSeriesTypeTag, result,
                seriesType,
                attr => attr.ExtractPropertyName);

            return result;
        }

        protected override void ProcessDownloadResource(JObject remoteResource, string uri)
        {
            _metaData = remoteResource[AvEMAProcessRes.MetaDataTag].ToObject<Dictionary<string, string>>();
            _content = remoteResource[AvEMAProcessRes.TimeSeriesTag].ToObject<Dictionary<string, Dictionary<string, string>>>();
        }
    }
}
