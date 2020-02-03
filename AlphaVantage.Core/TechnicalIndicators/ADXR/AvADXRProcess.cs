using AlphaVantage.Common;
using AlphaVantage.Common.Models.TechnicalIndicators.ADXR;
using AlphaVantage.Core.Abstracts;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace AlphaVantage.Core.TechnicalIndicators.ADXR
{
    public class AvADXRProcess : AvMapResourceAbs<AvADXR, AvADXRMetaData, AvADXRBlock>
    {
        protected override AvADXRBlock MapToBlock(Dictionary<string, string> block, string dateTime)
        {
            var result = new AvADXRBlock();

            var adxr = decimal.Parse(block[AvADXRRes.BlockADXRTag]);

            // ADXR
            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvADXRBlock, decimal, AvPropertyNameAttribute, string>
                (AvADXRRes.BlockADXRTag, result, adxr, attr => attr.ExtractPropertyName);

            return result;
        }

        protected override AvADXRMetaData MapToMetaData(Dictionary<string, string> metaData)
        {
            var result = new AvADXRMetaData();

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvADXRMetaData, string, AvPropertyNameAttribute, string>
                (AvADXRRes.MetaDataSymbolTag, result, metaData[AvADXRRes.MetaDataSymbolTag],
                attr => attr.ExtractPropertyName);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvADXRMetaData, string, AvPropertyNameAttribute, string>
                (AvADXRRes.MetaDataIndicatorTag, result, metaData[AvADXRRes.MetaDataIndicatorTag],
                attr => attr.ExtractPropertyName);

            var lastRefreshed = DateTime.Parse(metaData[AvADXRRes.MetaDataLastRefreshedTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvADXRMetaData, DateTime, AvPropertyNameAttribute, string>
                (AvADXRRes.MetaDataLastRefreshedTag, result,
                lastRefreshed,
                attr => attr.ExtractPropertyName);

            var interval = AvIntervalEnum.FromDisplayName<AvIntervalEnum>(
                metaData[AvADXRRes.MetaDataIntervalTag],
                StringComparison.InvariantCultureIgnoreCase);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvADXRMetaData, AvIntervalEnum, AvPropertyNameAttribute, string>
                (AvADXRRes.MetaDataIntervalTag, result,
                interval,
                attr => attr.ExtractPropertyName);

            var timeZone = AvTimeZoneConvertor.AvTimeZone(metaData[AvADXRRes.MetaDataTimeZoneTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvADXRMetaData, TimeZoneInfo, AvPropertyNameAttribute, string>
                (AvADXRRes.MetaDataTimeZoneTag, result,
                timeZone,
                attr => attr.ExtractPropertyName);

            var timePeriod = int.Parse(metaData[AvADXRRes.MetaDataTimePeriodTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvADXRMetaData, int, AvPropertyNameAttribute, string>
                (AvADXRRes.MetaDataTimePeriodTag, result,
                timePeriod,
                attr => attr.ExtractPropertyName);

            return result;
        }

        protected override void ProcessDownloadResource(JObject remoteResource, string uri)
        {
            _metaData = remoteResource[AvADXRProcessRes.MetaDataTag].ToObject<Dictionary<string, string>>();
            _content = remoteResource[AvADXRProcessRes.TimeSeriesTag].ToObject<Dictionary<string, Dictionary<string, string>>>();

        }
    }
}
