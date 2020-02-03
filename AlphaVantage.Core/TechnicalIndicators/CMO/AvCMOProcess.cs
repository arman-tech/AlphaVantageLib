using AlphaVantage.Common;
using AlphaVantage.Common.Models.TechnicalIndicators.CMO;
using AlphaVantage.Core.Abstracts;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace AlphaVantage.Core.TechnicalIndicators.CMO
{
    public class AvCMOProcess : AvMapResourceAbs<AvCMO, AvCMOMetaData, AvCMOBlock>
    {
        protected override AvCMOBlock MapToBlock(Dictionary<string, string> block, string dateTime)
        {
            var result = new AvCMOBlock();

            var data = decimal.Parse(block[AvCMORes.BlockCMOTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvCMOBlock, decimal, AvPropertyNameAttribute, string>
                (AvCMORes.BlockCMOTag, result, data, attr => attr.ExtractPropertyName);

            return result;
        }

        protected override AvCMOMetaData MapToMetaData(Dictionary<string, string> metaData)
        {
            var result = new AvCMOMetaData();

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvCMOMetaData, string, AvPropertyNameAttribute, string>
                (AvCMORes.MetaDataSymbolTag, result, metaData[AvCMORes.MetaDataSymbolTag],
                attr => attr.ExtractPropertyName);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvCMOMetaData, string, AvPropertyNameAttribute, string>
                (AvCMORes.MetaDataIndicatorTag, result, metaData[AvCMORes.MetaDataIndicatorTag],
                attr => attr.ExtractPropertyName);

            var lastRefreshed = DateTime.Parse(metaData[AvCMORes.MetaDataLastRefreshedTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvCMOMetaData, DateTime, AvPropertyNameAttribute, string>
                (AvCMORes.MetaDataLastRefreshedTag, result,
                lastRefreshed,
                attr => attr.ExtractPropertyName);

            var interval = AvIntervalEnum.FromDisplayName<AvIntervalEnum>(
                metaData[AvCMORes.MetaDataIntervalTag],
                StringComparison.InvariantCultureIgnoreCase);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvCMOMetaData, AvIntervalEnum, AvPropertyNameAttribute, string>
                (AvCMORes.MetaDataIntervalTag, result,
                interval,
                attr => attr.ExtractPropertyName);

            var timeZone = AvTimeZoneConvertor.AvTimeZone(metaData[AvCMORes.MetaDataTimeZoneTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvCMOMetaData, TimeZoneInfo, AvPropertyNameAttribute, string>
                (AvCMORes.MetaDataTimeZoneTag, result,
                timeZone,
                attr => attr.ExtractPropertyName);

            var timePeriod = int.Parse(metaData[AvCMORes.MetaDataTimePeriodTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvCMOMetaData, int, AvPropertyNameAttribute, string>
                (AvCMORes.MetaDataTimePeriodTag, result,
                timePeriod,
                attr => attr.ExtractPropertyName);

            var seriesType = AvSeriesTypeEnum.FromDisplayName<AvSeriesTypeEnum>(
                metaData[AvCMORes.MetaDataSeriesTypeTag],
                StringComparison.InvariantCultureIgnoreCase);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvCMOMetaData, AvSeriesTypeEnum, AvPropertyNameAttribute, string>
                (AvCMORes.MetaDataSeriesTypeTag, result,
                seriesType,
                attr => attr.ExtractPropertyName);

            return result;
        }

        protected override void ProcessDownloadResource(JObject remoteResource, string uri)
        {
            _metaData = remoteResource[AvCMOProcessRes.MetaDataTag].ToObject<Dictionary<string, string>>();
            _content = remoteResource[AvCMOProcessRes.TimeSeriesTag].ToObject<Dictionary<string, Dictionary<string, string>>>();
        }
    }
}
