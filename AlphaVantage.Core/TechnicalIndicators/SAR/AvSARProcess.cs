using AlphaVantage.Common;
using AlphaVantage.Common.Models.TechnicalIndicators.SAR;
using AlphaVantage.Core.Abstracts;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace AlphaVantage.Core.TechnicalIndicators.SAR
{
    public class AvSARProcess : AvMapResourceAbs<AvSAR, AvSARMetaData, AvSARBlock>
    {
        protected override AvSARBlock MapToBlock(Dictionary<string, string> block, string dateTime)
        {
            var result = new AvSARBlock();

            var data = decimal.Parse(block[AvSARRes.BlockSARTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvSARBlock, decimal, AvPropertyNameAttribute, string>
                (AvSARRes.BlockSARTag, result, data, attr => attr.ExtractPropertyName);

            return result;
        }

        protected override AvSARMetaData MapToMetaData(Dictionary<string, string> metaData)
        {
            var result = new AvSARMetaData();

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvSARMetaData, string, AvPropertyNameAttribute, string>
                (AvSARRes.MetaDataSymbolTag, result, metaData[AvSARRes.MetaDataSymbolTag],
                attr => attr.ExtractPropertyName);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvSARMetaData, string, AvPropertyNameAttribute, string>
                (AvSARRes.MetaDataIndicatorTag, result, metaData[AvSARRes.MetaDataIndicatorTag],
                attr => attr.ExtractPropertyName);

            var lastRefreshed = DateTime.Parse(metaData[AvSARRes.MetaDataLastRefreshedTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvSARMetaData, DateTime, AvPropertyNameAttribute, string>
                (AvSARRes.MetaDataLastRefreshedTag, result,
                lastRefreshed,
                attr => attr.ExtractPropertyName);

            var interval = AvIntervalEnum.FromDisplayName<AvIntervalEnum>(
                metaData[AvSARRes.MetaDataIntervalTag],
                StringComparison.InvariantCultureIgnoreCase);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvSARMetaData, AvIntervalEnum, AvPropertyNameAttribute, string>
                (AvSARRes.MetaDataIntervalTag, result,
                interval,
                attr => attr.ExtractPropertyName);

            var timeZone = AvTimeZoneConvertor.AvTimeZone(metaData[AvSARRes.MetaDataTimeZoneTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvSARMetaData, TimeZoneInfo, AvPropertyNameAttribute, string>
                (AvSARRes.MetaDataTimeZoneTag, result,
                timeZone,
                attr => attr.ExtractPropertyName);

            var acceleration = decimal.Parse(metaData[AvSARRes.MetaDataAccelerationTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvSARMetaData, decimal, AvPropertyNameAttribute, string>
                (AvSARRes.MetaDataAccelerationTag, result,
                acceleration,
                attr => attr.ExtractPropertyName);

            var maximum = decimal.Parse(metaData[AvSARRes.MetaDataMaximumTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvSARMetaData, decimal, AvPropertyNameAttribute, string>
                (AvSARRes.MetaDataMaximumTag, result,
                maximum,
                attr => attr.ExtractPropertyName);

            return result;
        }

        protected override void ProcessDownloadResource(JObject remoteResource, string uri)
        {
            _metaData = remoteResource[AvSARProcessRes.MetaDataTag].ToObject<Dictionary<string, string>>();
            _content = remoteResource[AvSARProcessRes.TimeSeriesTag].ToObject<Dictionary<string, Dictionary<string, string>>>();
        }
    }
}
