using AlphaVantage.Common;
using AlphaVantage.Common.Models.TechnicalIndicators.ADX;
using AlphaVantage.Core.Abstracts;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace AlphaVantage.Core.TechnicalIndicators.ADX
{
    public class AvADXProcess : AvMapResourceAbs<AvADX, AvADXMetaData, AvADXBlock>
    {
        protected override AvADXBlock MapToBlock(Dictionary<string, string> block, string dateTime)
        {
            var result = new AvADXBlock();

            var chaikain = decimal.Parse(block[AvADXRes.BlockADXTag]);

            // chaikain
            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvADXBlock, decimal, AvPropertyNameAttribute, string>
                (AvADXRes.BlockADXTag, result, chaikain, attr => attr.ExtractPropertyName);

            return result;
        }

        protected override AvADXMetaData MapToMetaData(Dictionary<string, string> metaData)
        {
            var result = new AvADXMetaData();

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvADXMetaData, string, AvPropertyNameAttribute, string>
                (AvADXRes.MetaDataSymbolTag, result, metaData[AvADXRes.MetaDataSymbolTag],
                attr => attr.ExtractPropertyName);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvADXMetaData, string, AvPropertyNameAttribute, string>
                (AvADXRes.MetaDataIndicatorTag, result, metaData[AvADXRes.MetaDataIndicatorTag],
                attr => attr.ExtractPropertyName);

            var lastRefreshed = DateTime.Parse(metaData[AvADXRes.MetaDataLastRefreshedTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvADXMetaData, DateTime, AvPropertyNameAttribute, string>
                (AvADXRes.MetaDataLastRefreshedTag, result,
                lastRefreshed,
                attr => attr.ExtractPropertyName);

            var interval = AvIntervalEnum.FromDisplayName<AvIntervalEnum>(
                metaData[AvADXRes.MetaDataIntervalTag],
                StringComparison.InvariantCultureIgnoreCase);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvADXMetaData, AvIntervalEnum, AvPropertyNameAttribute, string>
                (AvADXRes.MetaDataIntervalTag, result,
                interval,
                attr => attr.ExtractPropertyName);

            var timeZone = AvTimeZoneConvertor.AvTimeZone(metaData[AvADXRes.MetaDataTimeZoneTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvADXMetaData, TimeZoneInfo, AvPropertyNameAttribute, string>
                (AvADXRes.MetaDataTimeZoneTag, result,
                timeZone,
                attr => attr.ExtractPropertyName);

            var timePeriod = int.Parse(metaData[AvADXRes.MetaDataTimePeriodTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvADXMetaData, int, AvPropertyNameAttribute, string>
                (AvADXRes.MetaDataTimePeriodTag, result,
                timePeriod,
                attr => attr.ExtractPropertyName);

            return result;
        }

        protected override void ProcessDownloadResource(JObject remoteResource, string uri)
        {
            _metaData = remoteResource[AvADXProcessRes.MetaDataTag].ToObject<Dictionary<string, string>>();
            _content = remoteResource[AvADXProcessRes.TimeSeriesTag].ToObject<Dictionary<string, Dictionary<string, string>>>();
        }
    }
}
