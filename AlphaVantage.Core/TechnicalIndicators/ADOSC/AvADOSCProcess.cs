using AlphaVantage.Common.Models.TechnicalIndicators.ADOSC;
using AlphaVantage.Common;
using AlphaVantage.Core.Abstracts;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace AlphaVantage.Core.TechnicalIndicators.ADOSC
{
    public class AvADOSCProcess : AvMapResourceAbs<AvADOSC, AvADOSCMetaData, AvADOSCBlock>
    {
        protected override AvADOSCBlock MapToBlock(Dictionary<string, string> block, string dateTime)
        {
            var result = new AvADOSCBlock();

            var chaikain = decimal.Parse(block[AvADOSCRes.BlockADOSCTag]);

            // ADOSC
            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvADOSCBlock, decimal, AvPropertyNameAttribute, string>
                (AvADOSCRes.BlockADOSCTag, result, chaikain, attr => attr.ExtractPropertyName);

            return result;
        }

        protected override AvADOSCMetaData MapToMetaData(Dictionary<string, string> metaData)
        {
            var result = new AvADOSCMetaData();

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvADOSCMetaData, string, AvPropertyNameAttribute, string>
                (AvADOSCRes.MetaDataSymbolTag, result, metaData[AvADOSCRes.MetaDataSymbolTag],
                attr => attr.ExtractPropertyName);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvADOSCMetaData, string, AvPropertyNameAttribute, string>
                (AvADOSCRes.MetaDataIndicatorTag, result, metaData[AvADOSCRes.MetaDataIndicatorTag],
                attr => attr.ExtractPropertyName);

            var lastRefreshed = DateTime.Parse(metaData[AvADOSCRes.MetaDataLastRefreshedTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvADOSCMetaData, DateTime, AvPropertyNameAttribute, string>
                (AvADOSCRes.MetaDataLastRefreshedTag, result,
                lastRefreshed,
                attr => attr.ExtractPropertyName);

            var interval = AvIntervalEnum.FromDisplayName<AvIntervalEnum>(
                metaData[AvADOSCRes.MetaDataIntervalTag],
                StringComparison.InvariantCultureIgnoreCase);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvADOSCMetaData, AvIntervalEnum, AvPropertyNameAttribute, string>
                (AvADOSCRes.MetaDataIntervalTag, result,
                interval,
                attr => attr.ExtractPropertyName);

            var timeZone = AvTimeZoneConvertor.AvTimeZone(metaData[AvADOSCRes.MetaDataTimeZoneTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvADOSCMetaData, TimeZoneInfo, AvPropertyNameAttribute, string>
                (AvADOSCRes.MetaDataTimeZoneTag, result,
                timeZone,
                attr => attr.ExtractPropertyName);

            var fastKPeriod = int.Parse(metaData[AvADOSCRes.MetaDataFastKPeriodTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvADOSCMetaData, int, AvPropertyNameAttribute, string>
                (AvADOSCRes.MetaDataFastKPeriodTag, result,
                fastKPeriod,
                attr => attr.ExtractPropertyName);

            var slowKPeriod = int.Parse(metaData[AvADOSCRes.MetaDataSlowKPeriodTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvADOSCMetaData, int, AvPropertyNameAttribute, string>
                (AvADOSCRes.MetaDataSlowKPeriodTag, result,
                slowKPeriod,
                attr => attr.ExtractPropertyName);

            return result;
        }

        protected override void ProcessDownloadResource(JObject remoteResource, string uri)
        {
            _metaData = remoteResource[AvADOSCProcessRes.MetaDataTag].ToObject<Dictionary<string, string>>();
            _content = remoteResource[AvADOSCProcessRes.TimeSeriesTag].ToObject<Dictionary<string, Dictionary<string, string>>>();
        }
    }
}
