using AlphaVantage.Common;
using AlphaVantage.Common.Models.TechnicalIndicators.VWAP;
using AlphaVantage.Core.Abstracts;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace AlphaVantage.Core.TechnicalIndicators.VWAP
{
    public class AvVWAPProcess : AvMapResourceAbs<AvVWAP, AvVWAPMetaData, AvVWAPBlock>
    {
        protected override AvVWAPBlock MapToBlock(Dictionary<string, string> block, string dateTime)
        {
            var result = new AvVWAPBlock();

            var data = decimal.Parse(block[AvVWAPRes.BlockVWAPTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvVWAPBlock, decimal, AvPropertyNameAttribute, string>
                (AvVWAPRes.BlockVWAPTag, result, data, attr => attr.ExtractPropertyName);

            return result;
        }

        protected override AvVWAPMetaData MapToMetaData(Dictionary<string, string> metaData)
        {
            var result = new AvVWAPMetaData();

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvVWAPMetaData, string, AvPropertyNameAttribute, string>
                (AvVWAPRes.MetaDataSymbolTag, result, metaData[AvVWAPRes.MetaDataSymbolTag],
                attr => attr.ExtractPropertyName);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvVWAPMetaData, string, AvPropertyNameAttribute, string>
                (AvVWAPRes.MetaDataIndicatorTag, result, metaData[AvVWAPRes.MetaDataIndicatorTag],
                attr => attr.ExtractPropertyName);

            var lastRefreshed = DateTime.Parse(metaData[AvVWAPRes.MetaDataLastRefreshedTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvVWAPMetaData, DateTime, AvPropertyNameAttribute, string>
                (AvVWAPRes.MetaDataLastRefreshedTag, result,
                lastRefreshed,
                attr => attr.ExtractPropertyName);

            var interval = AvIntervalEnum.FromDisplayName<AvIntervalEnum>(
                metaData[AvVWAPRes.MetaDataIntervalTag],
                StringComparison.InvariantCultureIgnoreCase);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvVWAPMetaData, AvIntervalEnum, AvPropertyNameAttribute, string>
                (AvVWAPRes.MetaDataIntervalTag, result,
                interval,
                attr => attr.ExtractPropertyName);

            var timeZone = AvTimeZoneConvertor.AvTimeZone(metaData[AvVWAPRes.MetaDataTimeZoneTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvVWAPMetaData, TimeZoneInfo, AvPropertyNameAttribute, string>
                (AvVWAPRes.MetaDataTimeZoneTag, result,
                timeZone,
                attr => attr.ExtractPropertyName);

            return result;
        }

        protected override void ProcessDownloadResource(JObject remoteResource, string uri)
        {
            _metaData = remoteResource[AvVWAPProcessRes.MetaDataTag].ToObject<Dictionary<string, string>>();
            _content = remoteResource[AvVWAPProcessRes.TimeSeriesTag].ToObject<Dictionary<string, Dictionary<string, string>>>();
        }
    }
}
