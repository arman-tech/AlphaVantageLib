using AlphaVantage.Common;
using AlphaVantage.Common.Models.TechnicalIndicators.TEMA;
using AlphaVantage.Core.Abstracts;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace AlphaVantage.Core.TechnicalIndicators.TEMA
{
    public class AvTEMAProcess : AvMapResourceAbs<AvTEMA, AvTEMAMetaData, AvTEMABlock>
    {
        protected override AvTEMABlock MapToBlock(Dictionary<string, string> block, string dateTime)
        {
            var result = new AvTEMABlock();

            var data = decimal.Parse(block[AvTEMARes.BlockTEMATag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvTEMABlock, decimal, AvPropertyNameAttribute, string>
                (AvTEMARes.BlockTEMATag, result, data, attr => attr.ExtractPropertyName);

            return result;
        }

        protected override AvTEMAMetaData MapToMetaData(Dictionary<string, string> metaData)
        {
            var result = new AvTEMAMetaData();

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvTEMAMetaData, string, AvPropertyNameAttribute, string>
                (AvTEMARes.MetaDataSymbolTag, result, metaData[AvTEMARes.MetaDataSymbolTag],
                attr => attr.ExtractPropertyName);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvTEMAMetaData, string, AvPropertyNameAttribute, string>
                (AvTEMARes.MetaDataIndicatorTag, result, metaData[AvTEMARes.MetaDataIndicatorTag],
                attr => attr.ExtractPropertyName);

            var lastRefreshed = DateTime.Parse(metaData[AvTEMARes.MetaDataLastRefreshedTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvTEMAMetaData, DateTime, AvPropertyNameAttribute, string>
                (AvTEMARes.MetaDataLastRefreshedTag, result,
                lastRefreshed,
                attr => attr.ExtractPropertyName);

            var interval = AvIntervalEnum.FromDisplayName<AvIntervalEnum>(
                metaData[AvTEMARes.MetaDataIntervalTag],
                StringComparison.InvariantCultureIgnoreCase);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvTEMAMetaData, AvIntervalEnum, AvPropertyNameAttribute, string>
                (AvTEMARes.MetaDataIntervalTag, result,
                interval,
                attr => attr.ExtractPropertyName);

            var timeZone = AvTimeZoneConvertor.AvTimeZone(metaData[AvTEMARes.MetaDataTimeZoneTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvTEMAMetaData, TimeZoneInfo, AvPropertyNameAttribute, string>
                (AvTEMARes.MetaDataTimeZoneTag, result,
                timeZone,
                attr => attr.ExtractPropertyName);

            var timePeriod = int.Parse(metaData[AvTEMARes.MetaDataTimePeriodTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvTEMAMetaData, int, AvPropertyNameAttribute, string>
                (AvTEMARes.MetaDataTimePeriodTag, result,
                timePeriod,
                attr => attr.ExtractPropertyName);

            var seriesType = AvSeriesTypeEnum.FromDisplayName<AvSeriesTypeEnum>(
                metaData[AvTEMARes.MetaDataSeriesTypeTag],
                StringComparison.InvariantCultureIgnoreCase);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvTEMAMetaData, AvSeriesTypeEnum, AvPropertyNameAttribute, string>
                (AvTEMARes.MetaDataSeriesTypeTag, result,
                seriesType,
                attr => attr.ExtractPropertyName);

            return result;
        }

        protected override void ProcessDownloadResource(JObject remoteResource, string uri)
        {
            _metaData = remoteResource[AvTEMAProcessRes.MetaDataTag].ToObject<Dictionary<string, string>>();
            _content = remoteResource[AvTEMAProcessRes.TimeSeriesTag].ToObject<Dictionary<string, Dictionary<string, string>>>();
        }
    }
}
