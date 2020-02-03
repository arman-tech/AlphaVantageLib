using AlphaVantage.Common;
using AlphaVantage.Common.Models.TechnicalIndicators.STOCH;
using AlphaVantage.Core.Abstracts;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace AlphaVantage.Core.TechnicalIndicators.STOCH
{
    public class AvSTOCHProcess : AvMapResourceAbs<AvSTOCH, AvSTOCHMetaData, AvSTOCHBlock>
    {
        protected override AvSTOCHBlock MapToBlock(Dictionary<string, string> block, string dateTime)
        {
            var result = new AvSTOCHBlock();

            var slowD = decimal.Parse(block[AvSTOCHRes.BlockSlowDTag]);
            var slowK = decimal.Parse(block[AvSTOCHRes.BlockSlowKTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvSTOCHBlock, decimal, AvPropertyNameAttribute, string>
                (AvSTOCHRes.BlockSlowDTag, result, slowD, attr => attr.ExtractPropertyName);
            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvSTOCHBlock, decimal, AvPropertyNameAttribute, string>
                (AvSTOCHRes.BlockSlowKTag, result, slowK, attr => attr.ExtractPropertyName);

            return result;
        }

        protected override AvSTOCHMetaData MapToMetaData(Dictionary<string, string> metaData)
        {
            var result = new AvSTOCHMetaData();

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvSTOCHMetaData, string, AvPropertyNameAttribute, string>
                (AvSTOCHRes.MetaDataSymbolTag, result, metaData[AvSTOCHRes.MetaDataSymbolTag],
                attr => attr.ExtractPropertyName);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvSTOCHMetaData, string, AvPropertyNameAttribute, string>
                (AvSTOCHRes.MetaDataIndicatorTag, result, metaData[AvSTOCHRes.MetaDataIndicatorTag],
                attr => attr.ExtractPropertyName);

            var lastRefreshed = DateTime.Parse(metaData[AvSTOCHRes.MetaDataLastRefreshedTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvSTOCHMetaData, DateTime, AvPropertyNameAttribute, string>
                (AvSTOCHRes.MetaDataLastRefreshedTag, result,
                lastRefreshed,
                attr => attr.ExtractPropertyName);

            var interval = AvIntervalEnum.FromDisplayName<AvIntervalEnum>(
                metaData[AvSTOCHRes.MetaDataIntervalTag],
                StringComparison.InvariantCultureIgnoreCase);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvSTOCHMetaData, AvIntervalEnum, AvPropertyNameAttribute, string>
                (AvSTOCHRes.MetaDataIntervalTag, result,
                interval,
                attr => attr.ExtractPropertyName);

            var timeZone = AvTimeZoneConvertor.AvTimeZone(metaData[AvSTOCHRes.MetaDataTimeZoneTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvSTOCHMetaData, TimeZoneInfo, AvPropertyNameAttribute, string>
                (AvSTOCHRes.MetaDataTimeZoneTag, result,
                timeZone,
                attr => attr.ExtractPropertyName);

            var fastKPeriod = int.Parse(metaData[AvSTOCHRes.MetaDataFastKPeriodTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvSTOCHMetaData, int, AvPropertyNameAttribute, string>
                (AvSTOCHRes.MetaDataFastKPeriodTag, result,
                fastKPeriod,
                attr => attr.ExtractPropertyName);

            var slowKPeriod = int.Parse(metaData[AvSTOCHRes.MetaDataSlowKPeriodTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvSTOCHMetaData, int, AvPropertyNameAttribute, string>
                (AvSTOCHRes.MetaDataSlowKPeriodTag, result,
                slowKPeriod,
                attr => attr.ExtractPropertyName);

            var slowKMAType = int.Parse(metaData[AvSTOCHRes.MetaDataSlowKMATypeTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvSTOCHMetaData, int, AvPropertyNameAttribute, string>
                (AvSTOCHRes.MetaDataSlowKMATypeTag, result,
                slowKMAType,
                attr => attr.ExtractPropertyName);

            var slowDPeriod = int.Parse(metaData[AvSTOCHRes.MetaDataSlowDPeriodTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvSTOCHMetaData, int, AvPropertyNameAttribute, string>
                (AvSTOCHRes.MetaDataSlowDPeriodTag, result,
                slowDPeriod,
                attr => attr.ExtractPropertyName);

            var slowDMAType = int.Parse(metaData[AvSTOCHRes.MetaDataSlowDMATypeTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvSTOCHMetaData, int, AvPropertyNameAttribute, string>
                (AvSTOCHRes.MetaDataSlowDMATypeTag, result,
                slowDMAType,
                attr => attr.ExtractPropertyName);

            return result;
        }

        protected override void ProcessDownloadResource(JObject remoteResource, string uri)
        {
            _metaData = remoteResource[AvSTOCHProcessRes.MetaDataTag].ToObject<Dictionary<string, string>>();
            _content = remoteResource[AvSTOCHProcessRes.TimeSeriesTag].ToObject<Dictionary<string, Dictionary<string, string>>>();
        }
    }
}
