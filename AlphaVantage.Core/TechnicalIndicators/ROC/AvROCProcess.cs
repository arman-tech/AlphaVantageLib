using AlphaVantage.Common;
using AlphaVantage.Common.Models.TechnicalIndicators.ROC;
using AlphaVantage.Core.Abstracts;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace AlphaVantage.Core.TechnicalIndicators.ROC
{
    public class AvROCProcess : AvMapResourceAbs<AvROC, AvROCMetaData, AvROCBlock>
    {
        protected override AvROCBlock MapToBlock(Dictionary<string, string> block, string dateTime)
        {
            var result = new AvROCBlock();

            var data = decimal.Parse(block[AvROCRes.BlockROCTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvROCBlock, decimal, AvPropertyNameAttribute, string>
                (AvROCRes.BlockROCTag, result, data, attr => attr.ExtractPropertyName);

            return result;
        }

        protected override AvROCMetaData MapToMetaData(Dictionary<string, string> metaData)
        {
            var result = new AvROCMetaData();

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvROCMetaData, string, AvPropertyNameAttribute, string>
                (AvROCRes.MetaDataSymbolTag, result, metaData[AvROCRes.MetaDataSymbolTag],
                attr => attr.ExtractPropertyName);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvROCMetaData, string, AvPropertyNameAttribute, string>
                (AvROCRes.MetaDataIndicatorTag, result, metaData[AvROCRes.MetaDataIndicatorTag],
                attr => attr.ExtractPropertyName);

            var lastRefreshed = DateTime.Parse(metaData[AvROCRes.MetaDataLastRefreshedTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvROCMetaData, DateTime, AvPropertyNameAttribute, string>
                (AvROCRes.MetaDataLastRefreshedTag, result,
                lastRefreshed,
                attr => attr.ExtractPropertyName);

            var interval = AvIntervalEnum.FromDisplayName<AvIntervalEnum>(
                metaData[AvROCRes.MetaDataIntervalTag],
                StringComparison.InvariantCultureIgnoreCase);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvROCMetaData, AvIntervalEnum, AvPropertyNameAttribute, string>
                (AvROCRes.MetaDataIntervalTag, result,
                interval,
                attr => attr.ExtractPropertyName);

            var timeZone = AvTimeZoneConvertor.AvTimeZone(metaData[AvROCRes.MetaDataTimeZoneTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvROCMetaData, TimeZoneInfo, AvPropertyNameAttribute, string>
                (AvROCRes.MetaDataTimeZoneTag, result,
                timeZone,
                attr => attr.ExtractPropertyName);

            var timePeriod = int.Parse(metaData[AvROCRes.MetaDataTimePeriodTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvROCMetaData, int, AvPropertyNameAttribute, string>
                (AvROCRes.MetaDataTimePeriodTag, result,
                timePeriod,
                attr => attr.ExtractPropertyName);

            var seriesType = AvSeriesTypeEnum.FromDisplayName<AvSeriesTypeEnum>(
                metaData[AvROCRes.MetaDataSeriesTypeTag],
                StringComparison.InvariantCultureIgnoreCase);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvROCMetaData, AvSeriesTypeEnum, AvPropertyNameAttribute, string>
                (AvROCRes.MetaDataSeriesTypeTag, result,
                seriesType,
                attr => attr.ExtractPropertyName);

            return result;
        }

        protected override void ProcessDownloadResource(JObject remoteResource, string uri)
        {
            _metaData = remoteResource[AvROCProcessRes.MetaDataTag].ToObject<Dictionary<string, string>>();
            _content = remoteResource[AvROCProcessRes.TimeSeriesTag].ToObject<Dictionary<string, Dictionary<string, string>>>();
        }
    }
}
