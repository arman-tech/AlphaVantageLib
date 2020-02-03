using AlphaVantage.Common;
using AlphaVantage.Common.Models.TechnicalIndicators.CCI;
using AlphaVantage.Core.Abstracts;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace AlphaVantage.Core.TechnicalIndicators.CCI
{
    public class AvCCIProcess : AvMapResourceAbs<AvCCI, AvCCIMetaData, AvCCIBlock>
    {
        protected override AvCCIBlock MapToBlock(Dictionary<string, string> block, string dateTime)
        {
            var result = new AvCCIBlock();

            var data = decimal.Parse(block[AvCCIRes.BlockCCITag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvCCIBlock, decimal, AvPropertyNameAttribute, string>
                (AvCCIRes.BlockCCITag, result, data, attr => attr.ExtractPropertyName);

            return result;
        }

        protected override AvCCIMetaData MapToMetaData(Dictionary<string, string> metaData)
        {
            var result = new AvCCIMetaData();

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvCCIMetaData, string, AvPropertyNameAttribute, string>
                (AvCCIRes.MetaDataSymbolTag, result, metaData[AvCCIRes.MetaDataSymbolTag],
                attr => attr.ExtractPropertyName);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvCCIMetaData, string, AvPropertyNameAttribute, string>
                (AvCCIRes.MetaDataIndicatorTag, result, metaData[AvCCIRes.MetaDataIndicatorTag],
                attr => attr.ExtractPropertyName);

            var lastRefreshed = DateTime.Parse(metaData[AvCCIRes.MetaDataLastRefreshedTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvCCIMetaData, DateTime, AvPropertyNameAttribute, string>
                (AvCCIRes.MetaDataLastRefreshedTag, result,
                lastRefreshed,
                attr => attr.ExtractPropertyName);

            var interval = AvIntervalEnum.FromDisplayName<AvIntervalEnum>(
                metaData[AvCCIRes.MetaDataIntervalTag],
                StringComparison.InvariantCultureIgnoreCase);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvCCIMetaData, AvIntervalEnum, AvPropertyNameAttribute, string>
                (AvCCIRes.MetaDataIntervalTag, result,
                interval,
                attr => attr.ExtractPropertyName);

            var timeZone = AvTimeZoneConvertor.AvTimeZone(metaData[AvCCIRes.MetaDataTimeZoneTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvCCIMetaData, TimeZoneInfo, AvPropertyNameAttribute, string>
                (AvCCIRes.MetaDataTimeZoneTag, result,
                timeZone,
                attr => attr.ExtractPropertyName);

            var timePeriod = int.Parse(metaData[AvCCIRes.MetaDataTimePeriodTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvCCIMetaData, int, AvPropertyNameAttribute, string>
                (AvCCIRes.MetaDataTimePeriodTag, result,
                timePeriod,
                attr => attr.ExtractPropertyName);

            return result;
        }

        protected override void ProcessDownloadResource(JObject remoteResource, string uri)
        {
            _metaData = remoteResource[AvCCIProcessRes.MetaDataTag].ToObject<Dictionary<string, string>>();
            _content = remoteResource[AvCCIProcessRes.TimeSeriesTag].ToObject<Dictionary<string, Dictionary<string, string>>>();

        }
    }
}
