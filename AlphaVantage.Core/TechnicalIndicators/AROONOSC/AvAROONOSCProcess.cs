using AlphaVantage.Common;
using AlphaVantage.Common.Models.TechnicalIndicators.AROONOSC;
using AlphaVantage.Core.Abstracts;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace AlphaVantage.Core.TechnicalIndicators.AROONOSC
{
    public class AvAROONOSCProcess : AvMapResourceAbs<AvAROONOSC, AvAROONOSCMetaData, AvAROONOSCBlock>
    {
        protected override AvAROONOSCBlock MapToBlock(Dictionary<string, string> block, string dateTime)
        {
            var result = new AvAROONOSCBlock();

            var data = decimal.Parse(block[AvAROONOSCRes.BlockAROONOSCTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvAROONOSCBlock, decimal, AvPropertyNameAttribute, string>
                (AvAROONOSCRes.BlockAROONOSCTag, result, data, attr => attr.ExtractPropertyName);

            return result;
        }

        protected override AvAROONOSCMetaData MapToMetaData(Dictionary<string, string> metaData)
        {
            var result = new AvAROONOSCMetaData();

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvAROONOSCMetaData, string, AvPropertyNameAttribute, string>
                (AvAROONOSCRes.MetaDataSymbolTag, result, metaData[AvAROONOSCRes.MetaDataSymbolTag],
                attr => attr.ExtractPropertyName);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvAROONOSCMetaData, string, AvPropertyNameAttribute, string>
                (AvAROONOSCRes.MetaDataIndicatorTag, result, metaData[AvAROONOSCRes.MetaDataIndicatorTag],
                attr => attr.ExtractPropertyName);

            var lastRefreshed = DateTime.Parse(metaData[AvAROONOSCRes.MetaDataLastRefreshedTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvAROONOSCMetaData, DateTime, AvPropertyNameAttribute, string>
                (AvAROONOSCRes.MetaDataLastRefreshedTag, result,
                lastRefreshed,
                attr => attr.ExtractPropertyName);

            var interval = AvIntervalEnum.FromDisplayName<AvIntervalEnum>(
                metaData[AvAROONOSCRes.MetaDataIntervalTag],
                StringComparison.InvariantCultureIgnoreCase);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvAROONOSCMetaData, AvIntervalEnum, AvPropertyNameAttribute, string>
                (AvAROONOSCRes.MetaDataIntervalTag, result,
                interval,
                attr => attr.ExtractPropertyName);

            var timeZone = AvTimeZoneConvertor.AvTimeZone(metaData[AvAROONOSCRes.MetaDataTimeZoneTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvAROONOSCMetaData, TimeZoneInfo, AvPropertyNameAttribute, string>
                (AvAROONOSCRes.MetaDataTimeZoneTag, result,
                timeZone,
                attr => attr.ExtractPropertyName);

            var timePeriod = int.Parse(metaData[AvAROONOSCRes.MetaDataTimePeriodTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvAROONOSCMetaData, int, AvPropertyNameAttribute, string>
                (AvAROONOSCRes.MetaDataTimePeriodTag, result,
                timePeriod,
                attr => attr.ExtractPropertyName);


            return result;
        }

        protected override void ProcessDownloadResource(JObject remoteResource, string uri)
        {
            _metaData = remoteResource[AvAROONOSCProcessRes.MetaDataTag].ToObject<Dictionary<string, string>>();
            _content = remoteResource[AvAROONOSCProcessRes.TimeSeriesTag].ToObject<Dictionary<string, Dictionary<string, string>>>();
        }
    }
}
