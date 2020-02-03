using AlphaVantage.Common.Models.TechnicalIndicators.AD;
using AlphaVantage.Common;
using AlphaVantage.Core.Abstracts;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace AlphaVantage.Core.TechnicalIndicators.AD
{
    public class AvADProcess : AvMapResourceAbs<AvAD, AvADMetaData, AvADBlock>
    {
        protected override AvADBlock MapToBlock(Dictionary<string, string> block, string dateTime)
        {
            var result = new AvADBlock();

            var chaikain = decimal.Parse(block[AvADRes.BlockChaikinTag]);

            // chaikain
            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvADBlock, decimal, AvPropertyNameAttribute, string>
                (AvADRes.BlockChaikinTag, result, chaikain, attr => attr.ExtractPropertyName);

            return result;
        }

        protected override AvADMetaData MapToMetaData(Dictionary<string, string> metaData)
        {
            var result = new AvADMetaData();

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvADMetaData, string, AvPropertyNameAttribute, string>
                (AvADRes.MetaDataSymbolTag, result, metaData[AvADRes.MetaDataSymbolTag],
                attr => attr.ExtractPropertyName);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvADMetaData, string, AvPropertyNameAttribute, string>
                (AvADRes.MetaDataIndicatorTag, result, metaData[AvADRes.MetaDataIndicatorTag],
                attr => attr.ExtractPropertyName);

            var lastRefreshed = DateTime.Parse(metaData[AvADRes.MetaDataLastRefreshedTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvADMetaData, DateTime, AvPropertyNameAttribute, string>
                (AvADRes.MetaDataLastRefreshedTag, result,
                lastRefreshed,
                attr => attr.ExtractPropertyName);

            var interval = AvIntervalEnum.FromDisplayName<AvIntervalEnum>(
                metaData[AvADRes.MetaDataIntervalTag],
                StringComparison.InvariantCultureIgnoreCase);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvADMetaData, AvIntervalEnum, AvPropertyNameAttribute, string>
                (AvADRes.MetaDataIntervalTag, result,
                interval,
                attr => attr.ExtractPropertyName);

            var timeZone = AvTimeZoneConvertor.AvTimeZone(metaData[AvADRes.MetaDataTimeZoneTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvADMetaData, TimeZoneInfo, AvPropertyNameAttribute, string>
                (AvADRes.MetaDataTimeZoneTag, result,
                timeZone,
                attr => attr.ExtractPropertyName);

            return result;
        }

        protected override void ProcessDownloadResource(JObject remoteResource, string uri)
        {
            _metaData = remoteResource[AvADProcessRes.MetaDataTag].ToObject<Dictionary<string, string>>();
            _content = remoteResource[AvADProcessRes.TimeSeriesTag].ToObject<Dictionary<string, Dictionary<string, string>>>();
        }
    }
}
