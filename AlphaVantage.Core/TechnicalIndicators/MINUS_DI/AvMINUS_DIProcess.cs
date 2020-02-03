using AlphaVantage.Common;
using AlphaVantage.Common.Models.TechnicalIndicators.MINUS_DI;
using AlphaVantage.Core.Abstracts;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace AlphaVantage.Core.TechnicalIndicators.MINUS_DI
{
    public class AvMINUS_DIProcess : AvMapResourceAbs<AvMINUS_DI, AvMINUS_DIMetaData, AvMINUS_DIBlock>
    {
        protected override AvMINUS_DIBlock MapToBlock(Dictionary<string, string> block, string dateTime)
        {
            var result = new AvMINUS_DIBlock();

            var data = decimal.Parse(block[AvMINUS_DIRes.BlockMINUS_DITag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvMINUS_DIBlock, decimal, AvPropertyNameAttribute, string>
                (AvMINUS_DIRes.BlockMINUS_DITag, result, data, attr => attr.ExtractPropertyName);

            return result;
        }

        protected override AvMINUS_DIMetaData MapToMetaData(Dictionary<string, string> metaData)
        {
            var result = new AvMINUS_DIMetaData();

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvMINUS_DIMetaData, string, AvPropertyNameAttribute, string>
                (AvMINUS_DIRes.MetaDataSymbolTag, result, metaData[AvMINUS_DIRes.MetaDataSymbolTag],
                attr => attr.ExtractPropertyName);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvMINUS_DIMetaData, string, AvPropertyNameAttribute, string>
                (AvMINUS_DIRes.MetaDataIndicatorTag, result, metaData[AvMINUS_DIRes.MetaDataIndicatorTag],
                attr => attr.ExtractPropertyName);

            var lastRefreshed = DateTime.Parse(metaData[AvMINUS_DIRes.MetaDataLastRefreshedTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvMINUS_DIMetaData, DateTime, AvPropertyNameAttribute, string>
                (AvMINUS_DIRes.MetaDataLastRefreshedTag, result,
                lastRefreshed,
                attr => attr.ExtractPropertyName);

            var interval = AvIntervalEnum.FromDisplayName<AvIntervalEnum>(
                metaData[AvMINUS_DIRes.MetaDataIntervalTag],
                StringComparison.InvariantCultureIgnoreCase);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvMINUS_DIMetaData, AvIntervalEnum, AvPropertyNameAttribute, string>
                (AvMINUS_DIRes.MetaDataIntervalTag, result,
                interval,
                attr => attr.ExtractPropertyName);

            var timeZone = AvTimeZoneConvertor.AvTimeZone(metaData[AvMINUS_DIRes.MetaDataTimeZoneTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvMINUS_DIMetaData, TimeZoneInfo, AvPropertyNameAttribute, string>
                (AvMINUS_DIRes.MetaDataTimeZoneTag, result,
                timeZone,
                attr => attr.ExtractPropertyName);

            var timePeriod = int.Parse(metaData[AvMINUS_DIRes.MetaDataTimePeriodTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvMINUS_DIMetaData, int, AvPropertyNameAttribute, string>
                (AvMINUS_DIRes.MetaDataTimePeriodTag, result,
                timePeriod,
                attr => attr.ExtractPropertyName);

            return result;
        }

        protected override void ProcessDownloadResource(JObject remoteResource, string uri)
        {
            _metaData = remoteResource[AvMINUS_DIProcessRes.MetaDataTag].ToObject<Dictionary<string, string>>();
            _content = remoteResource[AvMINUS_DIProcessRes.TimeSeriesTag].ToObject<Dictionary<string, Dictionary<string, string>>>();
        }
    }
}
