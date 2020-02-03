using AlphaVantage.Common;
using AlphaVantage.Common.Models.TechnicalIndicators.KAMA;
using AlphaVantage.Core.Abstracts;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace AlphaVantage.Core.TechnicalIndicators.KAMA
{
    public class AvKAMAProcess : AvMapResourceAbs<AvKAMA, AvKAMAMetaData, AvKAMABlock>
    {
        protected override AvKAMABlock MapToBlock(Dictionary<string, string> block, string dateTime)
        {
            var result = new AvKAMABlock();

            var data = decimal.Parse(block[AvKAMARes.BlockKAMATag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvKAMABlock, decimal, AvPropertyNameAttribute, string>
                (AvKAMARes.BlockKAMATag, result, data, attr => attr.ExtractPropertyName);

            return result;
        }

        protected override AvKAMAMetaData MapToMetaData(Dictionary<string, string> metaData)
        {
            var result = new AvKAMAMetaData();

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvKAMAMetaData, string, AvPropertyNameAttribute, string>
                (AvKAMARes.MetaDataSymbolTag, result, metaData[AvKAMARes.MetaDataSymbolTag],
                attr => attr.ExtractPropertyName);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvKAMAMetaData, string, AvPropertyNameAttribute, string>
                (AvKAMARes.MetaDataIndicatorTag, result, metaData[AvKAMARes.MetaDataIndicatorTag],
                attr => attr.ExtractPropertyName);

            var lastRefreshed = DateTime.Parse(metaData[AvKAMARes.MetaDataLastRefreshedTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvKAMAMetaData, DateTime, AvPropertyNameAttribute, string>
                (AvKAMARes.MetaDataLastRefreshedTag, result,
                lastRefreshed,
                attr => attr.ExtractPropertyName);

            var interval = AvIntervalEnum.FromDisplayName<AvIntervalEnum>(
                metaData[AvKAMARes.MetaDataIntervalTag],
                StringComparison.InvariantCultureIgnoreCase);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvKAMAMetaData, AvIntervalEnum, AvPropertyNameAttribute, string>
                (AvKAMARes.MetaDataIntervalTag, result,
                interval,
                attr => attr.ExtractPropertyName);

            var timeZone = AvTimeZoneConvertor.AvTimeZone(metaData[AvKAMARes.MetaDataTimeZoneTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvKAMAMetaData, TimeZoneInfo, AvPropertyNameAttribute, string>
                (AvKAMARes.MetaDataTimeZoneTag, result,
                timeZone,
                attr => attr.ExtractPropertyName);

            var timePeriod = int.Parse(metaData[AvKAMARes.MetaDataTimePeriodTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvKAMAMetaData, int, AvPropertyNameAttribute, string>
                (AvKAMARes.MetaDataTimePeriodTag, result,
                timePeriod,
                attr => attr.ExtractPropertyName);

            var seriesType = AvSeriesTypeEnum.FromDisplayName<AvSeriesTypeEnum>(
                metaData[AvKAMARes.MetaDataSeriesTypeTag],
                StringComparison.InvariantCultureIgnoreCase);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvKAMAMetaData, AvSeriesTypeEnum, AvPropertyNameAttribute, string>
                (AvKAMARes.MetaDataSeriesTypeTag, result,
                seriesType,
                attr => attr.ExtractPropertyName);

            return result;
        }

        protected override void ProcessDownloadResource(JObject remoteResource, string uri)
        {
            _metaData = remoteResource[AvKAMAProcessRes.MetaDataTag].ToObject<Dictionary<string, string>>();
            _content = remoteResource[AvKAMAProcessRes.TimeSeriesTag].ToObject<Dictionary<string, Dictionary<string, string>>>();
        }
    }
}
