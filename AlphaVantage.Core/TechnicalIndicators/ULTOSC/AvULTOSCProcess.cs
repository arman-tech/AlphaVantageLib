using AlphaVantage.Common;
using AlphaVantage.Common.Models.TechnicalIndicators.ULTOSC;
using AlphaVantage.Core.Abstracts;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace AlphaVantage.Core.TechnicalIndicators.ULTOSC
{
    public class AvULTOSCProcess : AvMapResourceAbs<AvULTOSC, AvULTOSCMetaData, AvULTOSCBlock>
    {
        protected override AvULTOSCBlock MapToBlock(Dictionary<string, string> block, string dateTime)
        {
            var result = new AvULTOSCBlock();

            var data = decimal.Parse(block[AvULTOSCRes.BlockULTOSCTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvULTOSCBlock, decimal, AvPropertyNameAttribute, string>
                (AvULTOSCRes.BlockULTOSCTag, result, data, attr => attr.ExtractPropertyName);

            return result;
        }

        protected override AvULTOSCMetaData MapToMetaData(Dictionary<string, string> metaData)
        {
            var result = new AvULTOSCMetaData();

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvULTOSCMetaData, string, AvPropertyNameAttribute, string>
                (AvULTOSCRes.MetaDataSymbolTag, result, metaData[AvULTOSCRes.MetaDataSymbolTag],
                attr => attr.ExtractPropertyName);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvULTOSCMetaData, string, AvPropertyNameAttribute, string>
                (AvULTOSCRes.MetaDataIndicatorTag, result, metaData[AvULTOSCRes.MetaDataIndicatorTag],
                attr => attr.ExtractPropertyName);

            var lastRefreshed = DateTime.Parse(metaData[AvULTOSCRes.MetaDataLastRefreshedTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvULTOSCMetaData, DateTime, AvPropertyNameAttribute, string>
                (AvULTOSCRes.MetaDataLastRefreshedTag, result,
                lastRefreshed,
                attr => attr.ExtractPropertyName);

            var interval = AvIntervalEnum.FromDisplayName<AvIntervalEnum>(
                metaData[AvULTOSCRes.MetaDataIntervalTag],
                StringComparison.InvariantCultureIgnoreCase);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvULTOSCMetaData, AvIntervalEnum, AvPropertyNameAttribute, string>
                (AvULTOSCRes.MetaDataIntervalTag, result,
                interval,
                attr => attr.ExtractPropertyName);

            var timeZone = AvTimeZoneConvertor.AvTimeZone(metaData[AvULTOSCRes.MetaDataTimeZoneTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvULTOSCMetaData, TimeZoneInfo, AvPropertyNameAttribute, string>
                (AvULTOSCRes.MetaDataTimeZoneTag, result,
                timeZone,
                attr => attr.ExtractPropertyName);

            var timePeriodOne = int.Parse(metaData[AvULTOSCRes.MetaDataTimePeriodOneTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvULTOSCMetaData, int, AvPropertyNameAttribute, string>
                (AvULTOSCRes.MetaDataTimePeriodOneTag, result,
                timePeriodOne,
                attr => attr.ExtractPropertyName);

            var timePeriodTwo = int.Parse(metaData[AvULTOSCRes.MetaDataTimePeriodTwoTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvULTOSCMetaData, int, AvPropertyNameAttribute, string>
                (AvULTOSCRes.MetaDataTimePeriodTwoTag, result,
                timePeriodTwo,
                attr => attr.ExtractPropertyName);

            var timePeriodThree = int.Parse(metaData[AvULTOSCRes.MetaDataTimePeriodThreeTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvULTOSCMetaData, int, AvPropertyNameAttribute, string>
                (AvULTOSCRes.MetaDataTimePeriodThreeTag, result,
                timePeriodThree,
                attr => attr.ExtractPropertyName);


            return result;

        }

        protected override void ProcessDownloadResource(JObject remoteResource, string uri)
        {
            _metaData = remoteResource[AvULTOSCProcessRes.MetaDataTag].ToObject<Dictionary<string, string>>();
            _content = remoteResource[AvULTOSCProcessRes.TimeSeriesTag].ToObject<Dictionary<string, Dictionary<string, string>>>();
        }
    }
}
