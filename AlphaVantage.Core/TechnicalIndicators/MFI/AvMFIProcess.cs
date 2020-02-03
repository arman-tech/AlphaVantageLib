using AlphaVantage.Common;
using AlphaVantage.Common.Models.TechnicalIndicators.MFI;
using AlphaVantage.Core.Abstracts;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace AlphaVantage.Core.TechnicalIndicators.MFI
{
    public class AvMFIProcess : AvMapResourceAbs<AvMFI, AvMFIMetaData, AvMFIBlock>
    {
        protected override AvMFIBlock MapToBlock(Dictionary<string, string> block, string dateTime)
        {
            var result = new AvMFIBlock();

            var data = decimal.Parse(block[AvMFIRes.BlockMFITag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvMFIBlock, decimal, AvPropertyNameAttribute, string>
                (AvMFIRes.BlockMFITag, result, data, attr => attr.ExtractPropertyName);

            return result;
        }

        protected override AvMFIMetaData MapToMetaData(Dictionary<string, string> metaData)
        {
            var result = new AvMFIMetaData();

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvMFIMetaData, string, AvPropertyNameAttribute, string>
                (AvMFIRes.MetaDataSymbolTag, result, metaData[AvMFIRes.MetaDataSymbolTag],
                attr => attr.ExtractPropertyName);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvMFIMetaData, string, AvPropertyNameAttribute, string>
                (AvMFIRes.MetaDataIndicatorTag, result, metaData[AvMFIRes.MetaDataIndicatorTag],
                attr => attr.ExtractPropertyName);

            var lastRefreshed = DateTime.Parse(metaData[AvMFIRes.MetaDataLastRefreshedTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvMFIMetaData, DateTime, AvPropertyNameAttribute, string>
                (AvMFIRes.MetaDataLastRefreshedTag, result,
                lastRefreshed,
                attr => attr.ExtractPropertyName);

            var interval = AvIntervalEnum.FromDisplayName<AvIntervalEnum>(
                metaData[AvMFIRes.MetaDataIntervalTag],
                StringComparison.InvariantCultureIgnoreCase);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvMFIMetaData, AvIntervalEnum, AvPropertyNameAttribute, string>
                (AvMFIRes.MetaDataIntervalTag, result,
                interval,
                attr => attr.ExtractPropertyName);

            var timeZone = AvTimeZoneConvertor.AvTimeZone(metaData[AvMFIRes.MetaDataTimeZoneTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvMFIMetaData, TimeZoneInfo, AvPropertyNameAttribute, string>
                (AvMFIRes.MetaDataTimeZoneTag, result,
                timeZone,
                attr => attr.ExtractPropertyName);

            var timePeriod = int.Parse(metaData[AvMFIRes.MetaDataTimePeriodTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvMFIMetaData, int, AvPropertyNameAttribute, string>
                (AvMFIRes.MetaDataTimePeriodTag, result,
                timePeriod,
                attr => attr.ExtractPropertyName);

            return result;
        }

        protected override void ProcessDownloadResource(JObject remoteResource, string uri)
        {
            _metaData = remoteResource[AvMFIProcessRes.MetaDataTag].ToObject<Dictionary<string, string>>();
            _content = remoteResource[AvMFIProcessRes.TimeSeriesTag].ToObject<Dictionary<string, Dictionary<string, string>>>();
        }
    }
}
