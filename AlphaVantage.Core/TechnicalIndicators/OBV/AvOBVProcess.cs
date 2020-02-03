using AlphaVantage.Common;
using AlphaVantage.Common.Models.TechnicalIndicators.OBV;
using AlphaVantage.Core.Abstracts;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace AlphaVantage.Core.TechnicalIndicators.OBV
{
    public class AvOBVProcess : AvMapResourceAbs<AvOBV, AvOBVMetaData, AvOBVBlock>
    {
        protected override AvOBVBlock MapToBlock(Dictionary<string, string> block, string dateTime)
        {
            var result = new AvOBVBlock();

            var data = decimal.Parse(block[AvOBVRes.BlockOBVTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvOBVBlock, decimal, AvPropertyNameAttribute, string>
                (AvOBVRes.BlockOBVTag, result, data, attr => attr.ExtractPropertyName);

            return result;
        }

        protected override AvOBVMetaData MapToMetaData(Dictionary<string, string> metaData)
        {
            var result = new AvOBVMetaData();

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvOBVMetaData, string, AvPropertyNameAttribute, string>
                (AvOBVRes.MetaDataSymbolTag, result, metaData[AvOBVRes.MetaDataSymbolTag],
                attr => attr.ExtractPropertyName);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvOBVMetaData, string, AvPropertyNameAttribute, string>
                (AvOBVRes.MetaDataIndicatorTag, result, metaData[AvOBVRes.MetaDataIndicatorTag],
                attr => attr.ExtractPropertyName);

            var lastRefreshed = DateTime.Parse(metaData[AvOBVRes.MetaDataLastRefreshedTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvOBVMetaData, DateTime, AvPropertyNameAttribute, string>
                (AvOBVRes.MetaDataLastRefreshedTag, result,
                lastRefreshed,
                attr => attr.ExtractPropertyName);

            var interval = AvIntervalEnum.FromDisplayName<AvIntervalEnum>(
                metaData[AvOBVRes.MetaDataIntervalTag],
                StringComparison.InvariantCultureIgnoreCase);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvOBVMetaData, AvIntervalEnum, AvPropertyNameAttribute, string>
                (AvOBVRes.MetaDataIntervalTag, result,
                interval,
                attr => attr.ExtractPropertyName);

            var timeZone = AvTimeZoneConvertor.AvTimeZone(metaData[AvOBVRes.MetaDataTimeZoneTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvOBVMetaData, TimeZoneInfo, AvPropertyNameAttribute, string>
                (AvOBVRes.MetaDataTimeZoneTag, result,
                timeZone,
                attr => attr.ExtractPropertyName);


            return result;
        }

        protected override void ProcessDownloadResource(JObject remoteResource, string uri)
        {
            _metaData = remoteResource[AvOBVProcessRes.MetaDataTag].ToObject<Dictionary<string, string>>();
            _content = remoteResource[AvOBVProcessRes.TimeSeriesTag].ToObject<Dictionary<string, Dictionary<string, string>>>();
        }
    }
}
