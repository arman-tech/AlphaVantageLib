using AlphaVantage.Common;
using AlphaVantage.Common.Models.TechnicalIndicators.DEMA;
using AlphaVantage.Core.Abstracts;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace AlphaVantage.Core.TechnicalIndicators.DEMA
{
    public class AvDEMAProcess : AvMapResourceAbs<AvDEMA, AvDEMAMetaData, AvDEMABlock>
    {
        protected override AvDEMABlock MapToBlock(Dictionary<string, string> block, string dateTime)
        {
            var result = new AvDEMABlock();

            var data = decimal.Parse(block[AvDEMARes.BlockDEMATag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvDEMABlock, decimal, AvPropertyNameAttribute, string>
                (AvDEMARes.BlockDEMATag, result, data, attr => attr.ExtractPropertyName);

            return result;
        }

        protected override AvDEMAMetaData MapToMetaData(Dictionary<string, string> metaData)
        {
            var result = new AvDEMAMetaData();

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvDEMAMetaData, string, AvPropertyNameAttribute, string>
                (AvDEMARes.MetaDataSymbolTag, result, metaData[AvDEMARes.MetaDataSymbolTag],
                attr => attr.ExtractPropertyName);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvDEMAMetaData, string, AvPropertyNameAttribute, string>
                (AvDEMARes.MetaDataIndicatorTag, result, metaData[AvDEMARes.MetaDataIndicatorTag],
                attr => attr.ExtractPropertyName);

            var lastRefreshed = DateTime.Parse(metaData[AvDEMARes.MetaDataLastRefreshedTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvDEMAMetaData, DateTime, AvPropertyNameAttribute, string>
                (AvDEMARes.MetaDataLastRefreshedTag, result,
                lastRefreshed,
                attr => attr.ExtractPropertyName);

            var interval = AvIntervalEnum.FromDisplayName<AvIntervalEnum>(
                metaData[AvDEMARes.MetaDataIntervalTag],
                StringComparison.InvariantCultureIgnoreCase);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvDEMAMetaData, AvIntervalEnum, AvPropertyNameAttribute, string>
                (AvDEMARes.MetaDataIntervalTag, result,
                interval,
                attr => attr.ExtractPropertyName);

            var timeZone = AvTimeZoneConvertor.AvTimeZone(metaData[AvDEMARes.MetaDataTimeZoneTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvDEMAMetaData, TimeZoneInfo, AvPropertyNameAttribute, string>
                (AvDEMARes.MetaDataTimeZoneTag, result,
                timeZone,
                attr => attr.ExtractPropertyName);

            var timePeriod = int.Parse(metaData[AvDEMARes.MetaDataTimePeriodTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvDEMAMetaData, int, AvPropertyNameAttribute, string>
                (AvDEMARes.MetaDataTimePeriodTag, result,
                timePeriod,
                attr => attr.ExtractPropertyName);

            var seriesType = AvSeriesTypeEnum.FromDisplayName<AvSeriesTypeEnum>(
                metaData[AvDEMARes.MetaDataSeriesTypeTag],
                StringComparison.InvariantCultureIgnoreCase);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvDEMAMetaData, AvSeriesTypeEnum, AvPropertyNameAttribute, string>
                (AvDEMARes.MetaDataSeriesTypeTag, result,
                seriesType,
                attr => attr.ExtractPropertyName);

            return result;
        }

        protected override void ProcessDownloadResource(JObject remoteResource, string uri)
        {
            _metaData = remoteResource[AvDEMAProcessRes.MetaDataTag].ToObject<Dictionary<string, string>>();
            _content = remoteResource[AvDEMAProcessRes.TimeSeriesTag].ToObject<Dictionary<string, Dictionary<string, string>>>();
        }
    }
}
