using AlphaVantage.Common;
using AlphaVantage.Common.Models.TechnicalIndicators.ROCR;
using AlphaVantage.Core.Abstracts;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace AlphaVantage.Core.TechnicalIndicators.ROCR
{
    public class AvROCRProcess : AvMapResourceAbs<AvROCR, AvROCRMetaData, AvROCRBlock>
    {
        protected override AvROCRBlock MapToBlock(Dictionary<string, string> block, string dateTime)
        {
            var result = new AvROCRBlock();

            var data = decimal.Parse(block[AvROCRRes.BlockROCRTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvROCRBlock, decimal, AvPropertyNameAttribute, string>
                (AvROCRRes.BlockROCRTag, result, data, attr => attr.ExtractPropertyName);

            return result;
        }

        protected override AvROCRMetaData MapToMetaData(Dictionary<string, string> metaData)
        {
            var result = new AvROCRMetaData();

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvROCRMetaData, string, AvPropertyNameAttribute, string>
                (AvROCRRes.MetaDataSymbolTag, result, metaData[AvROCRRes.MetaDataSymbolTag],
                attr => attr.ExtractPropertyName);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvROCRMetaData, string, AvPropertyNameAttribute, string>
                (AvROCRRes.MetaDataIndicatorTag, result, metaData[AvROCRRes.MetaDataIndicatorTag],
                attr => attr.ExtractPropertyName);

            var lastRefreshed = DateTime.Parse(metaData[AvROCRRes.MetaDataLastRefreshedTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvROCRMetaData, DateTime, AvPropertyNameAttribute, string>
                (AvROCRRes.MetaDataLastRefreshedTag, result,
                lastRefreshed,
                attr => attr.ExtractPropertyName);

            var interval = AvIntervalEnum.FromDisplayName<AvIntervalEnum>(
                metaData[AvROCRRes.MetaDataIntervalTag],
                StringComparison.InvariantCultureIgnoreCase);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvROCRMetaData, AvIntervalEnum, AvPropertyNameAttribute, string>
                (AvROCRRes.MetaDataIntervalTag, result,
                interval,
                attr => attr.ExtractPropertyName);

            var timeZone = AvTimeZoneConvertor.AvTimeZone(metaData[AvROCRRes.MetaDataTimeZoneTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvROCRMetaData, TimeZoneInfo, AvPropertyNameAttribute, string>
                (AvROCRRes.MetaDataTimeZoneTag, result,
                timeZone,
                attr => attr.ExtractPropertyName);

            var timePeriod = int.Parse(metaData[AvROCRRes.MetaDataTimePeriodTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvROCRMetaData, int, AvPropertyNameAttribute, string>
                (AvROCRRes.MetaDataTimePeriodTag, result,
                timePeriod,
                attr => attr.ExtractPropertyName);

            var seriesType = AvSeriesTypeEnum.FromDisplayName<AvSeriesTypeEnum>(
                metaData[AvROCRRes.MetaDataSeriesTypeTag],
                StringComparison.InvariantCultureIgnoreCase);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvROCRMetaData, AvSeriesTypeEnum, AvPropertyNameAttribute, string>
                (AvROCRRes.MetaDataSeriesTypeTag, result,
                seriesType,
                attr => attr.ExtractPropertyName);

            return result;
        }

        protected override void ProcessDownloadResource(JObject remoteResource, string uri)
        {
            _metaData = remoteResource[AvROCRProcessRes.MetaDataTag].ToObject<Dictionary<string, string>>();
            _content = remoteResource[AvROCRProcessRes.TimeSeriesTag].ToObject<Dictionary<string, Dictionary<string, string>>>();
        }
    }
}
