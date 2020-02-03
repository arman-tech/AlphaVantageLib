using AlphaVantage.Common;
using AlphaVantage.Common.Models.TechnicalIndicators.NATR;
using AlphaVantage.Core.Abstracts;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace AlphaVantage.Core.TechnicalIndicators.NATR
{
    public class AvNATRProcess : AvMapResourceAbs<AvNATR, AvNATRMetaData, AvNATRBlock>
    {
        protected override AvNATRBlock MapToBlock(Dictionary<string, string> block, string dateTime)
        {
            var result = new AvNATRBlock();

            var data = decimal.Parse(block[AvNATRRes.BlockNATRTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvNATRBlock, decimal, AvPropertyNameAttribute, string>
                (AvNATRRes.BlockNATRTag, result, data, attr => attr.ExtractPropertyName);

            return result;
        }

        protected override AvNATRMetaData MapToMetaData(Dictionary<string, string> metaData)
        {
            var result = new AvNATRMetaData();

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvNATRMetaData, string, AvPropertyNameAttribute, string>
                (AvNATRRes.MetaDataSymbolTag, result, metaData[AvNATRRes.MetaDataSymbolTag],
                attr => attr.ExtractPropertyName);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvNATRMetaData, string, AvPropertyNameAttribute, string>
                (AvNATRRes.MetaDataIndicatorTag, result, metaData[AvNATRRes.MetaDataIndicatorTag],
                attr => attr.ExtractPropertyName);

            var lastRefreshed = DateTime.Parse(metaData[AvNATRRes.MetaDataLastRefreshedTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvNATRMetaData, DateTime, AvPropertyNameAttribute, string>
                (AvNATRRes.MetaDataLastRefreshedTag, result,
                lastRefreshed,
                attr => attr.ExtractPropertyName);

            var interval = AvIntervalEnum.FromDisplayName<AvIntervalEnum>(
                metaData[AvNATRRes.MetaDataIntervalTag],
                StringComparison.InvariantCultureIgnoreCase);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvNATRMetaData, AvIntervalEnum, AvPropertyNameAttribute, string>
                (AvNATRRes.MetaDataIntervalTag, result,
                interval,
                attr => attr.ExtractPropertyName);

            var timeZone = AvTimeZoneConvertor.AvTimeZone(metaData[AvNATRRes.MetaDataTimeZoneTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvNATRMetaData, TimeZoneInfo, AvPropertyNameAttribute, string>
                (AvNATRRes.MetaDataTimeZoneTag, result,
                timeZone,
                attr => attr.ExtractPropertyName);

            var timePeriod = int.Parse(metaData[AvNATRRes.MetaDataTimePeriodTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvNATRMetaData, int, AvPropertyNameAttribute, string>
                (AvNATRRes.MetaDataTimePeriodTag, result,
                timePeriod,
                attr => attr.ExtractPropertyName);

            return result;
        }

        protected override void ProcessDownloadResource(JObject remoteResource, string uri)
        {
            _metaData = remoteResource[AvNATRProcessRes.MetaDataTag].ToObject<Dictionary<string, string>>();
            _content = remoteResource[AvNATRProcessRes.TimeSeriesTag].ToObject<Dictionary<string, Dictionary<string, string>>>();
        }
    }
}
