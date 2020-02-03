using AlphaVantage.Common;
using AlphaVantage.Common.Models.TechnicalIndicators.PLUS_DI;
using AlphaVantage.Core.Abstracts;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace AlphaVantage.Core.TechnicalIndicators.PLUS_DI
{
    public class AvPLUS_DIProcess : AvMapResourceAbs<AvPLUS_DI, AvPLUS_DIMetaData, AvPLUS_DIBlock>
    {
        protected override AvPLUS_DIBlock MapToBlock(Dictionary<string, string> block, string dateTime)
        {
            var result = new AvPLUS_DIBlock();

            var data = decimal.Parse(block[AvPLUS_DIRes.BlockPLUS_DITag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvPLUS_DIBlock, decimal, AvPropertyNameAttribute, string>
                (AvPLUS_DIRes.BlockPLUS_DITag, result, data, attr => attr.ExtractPropertyName);

            return result;
        }

        protected override AvPLUS_DIMetaData MapToMetaData(Dictionary<string, string> metaData)
        {
            var result = new AvPLUS_DIMetaData();

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvPLUS_DIMetaData, string, AvPropertyNameAttribute, string>
                (AvPLUS_DIRes.MetaDataSymbolTag, result, metaData[AvPLUS_DIRes.MetaDataSymbolTag],
                attr => attr.ExtractPropertyName);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvPLUS_DIMetaData, string, AvPropertyNameAttribute, string>
                (AvPLUS_DIRes.MetaDataIndicatorTag, result, metaData[AvPLUS_DIRes.MetaDataIndicatorTag],
                attr => attr.ExtractPropertyName);

            var lastRefreshed = DateTime.Parse(metaData[AvPLUS_DIRes.MetaDataLastRefreshedTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvPLUS_DIMetaData, DateTime, AvPropertyNameAttribute, string>
                (AvPLUS_DIRes.MetaDataLastRefreshedTag, result,
                lastRefreshed,
                attr => attr.ExtractPropertyName);

            var interval = AvIntervalEnum.FromDisplayName<AvIntervalEnum>(
                metaData[AvPLUS_DIRes.MetaDataIntervalTag],
                StringComparison.InvariantCultureIgnoreCase);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvPLUS_DIMetaData, AvIntervalEnum, AvPropertyNameAttribute, string>
                (AvPLUS_DIRes.MetaDataIntervalTag, result,
                interval,
                attr => attr.ExtractPropertyName);

            var timeZone = AvTimeZoneConvertor.AvTimeZone(metaData[AvPLUS_DIRes.MetaDataTimeZoneTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvPLUS_DIMetaData, TimeZoneInfo, AvPropertyNameAttribute, string>
                (AvPLUS_DIRes.MetaDataTimeZoneTag, result,
                timeZone,
                attr => attr.ExtractPropertyName);

            var timePeriod = int.Parse(metaData[AvPLUS_DIRes.MetaDataTimePeriodTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvPLUS_DIMetaData, int, AvPropertyNameAttribute, string>
                (AvPLUS_DIRes.MetaDataTimePeriodTag, result,
                timePeriod,
                attr => attr.ExtractPropertyName);


            return result;
        }

        protected override void ProcessDownloadResource(JObject remoteResource, string uri)
        {
            _metaData = remoteResource[AvPLUS_DIProcessRes.MetaDataTag].ToObject<Dictionary<string, string>>();
            _content = remoteResource[AvPLUS_DIProcessRes.TimeSeriesTag].ToObject<Dictionary<string, Dictionary<string, string>>>();

        }
    }
}
