using AlphaVantage.Common;
using AlphaVantage.Common.Models.TechnicalIndicators.BOP;
using AlphaVantage.Core.Abstracts;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace AlphaVantage.Core.TechnicalIndicators.BOP
{
    public class AvBOPProcess : AvMapResourceAbs<AvBOP, AvBOPMetaData, AvBOPBlock>
    {
        protected override AvBOPBlock MapToBlock(Dictionary<string, string> block, string dateTime)
        {
            var result = new AvBOPBlock();

            var data = decimal.Parse(block[AvBOPRes.BlockBOPTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvBOPBlock, decimal, AvPropertyNameAttribute, string>
                (AvBOPRes.BlockBOPTag, result, data, attr => attr.ExtractPropertyName);

            return result;
        }

        protected override AvBOPMetaData MapToMetaData(Dictionary<string, string> metaData)
        {
            var result = new AvBOPMetaData();

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvBOPMetaData, string, AvPropertyNameAttribute, string>
                (AvBOPRes.MetaDataSymbolTag, result, metaData[AvBOPRes.MetaDataSymbolTag],
                attr => attr.ExtractPropertyName);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvBOPMetaData, string, AvPropertyNameAttribute, string>
                (AvBOPRes.MetaDataIndicatorTag, result, metaData[AvBOPRes.MetaDataIndicatorTag],
                attr => attr.ExtractPropertyName);

            var lastRefreshed = DateTime.Parse(metaData[AvBOPRes.MetaDataLastRefreshedTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvBOPMetaData, DateTime, AvPropertyNameAttribute, string>
                (AvBOPRes.MetaDataLastRefreshedTag, result,
                lastRefreshed,
                attr => attr.ExtractPropertyName);

            var interval = AvIntervalEnum.FromDisplayName<AvIntervalEnum>(
                metaData[AvBOPRes.MetaDataIntervalTag],
                StringComparison.InvariantCultureIgnoreCase);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvBOPMetaData, AvIntervalEnum, AvPropertyNameAttribute, string>
                (AvBOPRes.MetaDataIntervalTag, result,
                interval,
                attr => attr.ExtractPropertyName);

            var timeZone = AvTimeZoneConvertor.AvTimeZone(metaData[AvBOPRes.MetaDataTimeZoneTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvBOPMetaData, TimeZoneInfo, AvPropertyNameAttribute, string>
                (AvBOPRes.MetaDataTimeZoneTag, result,
                timeZone,
                attr => attr.ExtractPropertyName);

            return result;
        }

        protected override void ProcessDownloadResource(JObject remoteResource, string uri)
        {
            _metaData = remoteResource[AvBOPProcessRes.MetaDataTag].ToObject<Dictionary<string, string>>();
            _content = remoteResource[AvBOPProcessRes.TimeSeriesTag].ToObject<Dictionary<string, Dictionary<string, string>>>();

        }
    }
}
