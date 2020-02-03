using AlphaVantage.Common;
using AlphaVantage.Common.Models.TechnicalIndicators.STOCHF;
using AlphaVantage.Core.Abstracts;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace AlphaVantage.Core.TechnicalIndicators.STOCHF
{
    public class AvSTOCHFProcess : AvMapResourceAbs<AvSTOCHF, AvSTOCHFMetaData, AvSTOCHFBlock>
    {
        protected override AvSTOCHFBlock MapToBlock(Dictionary<string, string> block, string dateTime)
        {
            var result = new AvSTOCHFBlock();

            var fastD = decimal.Parse(block[AvSTOCHFRes.BlockFastDTag]);
            var fastK = decimal.Parse(block[AvSTOCHFRes.BlockFastKTag]);


            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvSTOCHFBlock, decimal, AvPropertyNameAttribute, string>
                (AvSTOCHFRes.BlockFastDTag, result, fastD, attr => attr.ExtractPropertyName);
            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvSTOCHFBlock, decimal, AvPropertyNameAttribute, string>
                (AvSTOCHFRes.BlockFastKTag, result, fastK, attr => attr.ExtractPropertyName);

            return result;
        }

        protected override AvSTOCHFMetaData MapToMetaData(Dictionary<string, string> metaData)
        {
            var result = new AvSTOCHFMetaData();

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvSTOCHFMetaData, string, AvPropertyNameAttribute, string>
                (AvSTOCHFRes.MetaDataSymbolTag, result, metaData[AvSTOCHFRes.MetaDataSymbolTag],
                attr => attr.ExtractPropertyName);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvSTOCHFMetaData, string, AvPropertyNameAttribute, string>
                (AvSTOCHFRes.MetaDataIndicatorTag, result, metaData[AvSTOCHFRes.MetaDataIndicatorTag],
                attr => attr.ExtractPropertyName);

            var lastRefreshed = DateTime.Parse(metaData[AvSTOCHFRes.MetaDataLastRefreshedTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvSTOCHFMetaData, DateTime, AvPropertyNameAttribute, string>
                (AvSTOCHFRes.MetaDataLastRefreshedTag, result,
                lastRefreshed,
                attr => attr.ExtractPropertyName);

            var interval = AvIntervalEnum.FromDisplayName<AvIntervalEnum>(
                metaData[AvSTOCHFRes.MetaDataIntervalTag],
                StringComparison.InvariantCultureIgnoreCase);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvSTOCHFMetaData, AvIntervalEnum, AvPropertyNameAttribute, string>
                (AvSTOCHFRes.MetaDataIntervalTag, result,
                interval,
                attr => attr.ExtractPropertyName);

            var timeZone = AvTimeZoneConvertor.AvTimeZone(metaData[AvSTOCHFRes.MetaDataTimeZoneTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvSTOCHFMetaData, TimeZoneInfo, AvPropertyNameAttribute, string>
                (AvSTOCHFRes.MetaDataTimeZoneTag, result,
                timeZone,
                attr => attr.ExtractPropertyName);

            var fastKPeriod = int.Parse(metaData[AvSTOCHFRes.MetaDataFastKPeriodTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvSTOCHFMetaData, int, AvPropertyNameAttribute, string>
                (AvSTOCHFRes.MetaDataFastKPeriodTag, result,
                fastKPeriod,
                attr => attr.ExtractPropertyName);

            var fastDPeriod = int.Parse(metaData[AvSTOCHFRes.MetaDataFastDPeriodTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvSTOCHFMetaData, int, AvPropertyNameAttribute, string>
                (AvSTOCHFRes.MetaDataFastDPeriodTag, result,
                fastDPeriod,
                attr => attr.ExtractPropertyName);

            var fastDMAType = int.Parse(metaData[AvSTOCHFRes.MetaDataFastDMATypeTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvSTOCHFMetaData, int, AvPropertyNameAttribute, string>
                (AvSTOCHFRes.MetaDataFastDMATypeTag, result,
                fastDMAType,
                attr => attr.ExtractPropertyName);
            return result;
        }

        protected override void ProcessDownloadResource(JObject remoteResource, string uri)
        {
            _metaData = remoteResource[AvSTOCHFProcessRes.MetaDataTag].ToObject<Dictionary<string, string>>();
            _content = remoteResource[AvSTOCHFProcessRes.TimeSeriesTag].ToObject<Dictionary<string, Dictionary<string, string>>>();
        }
    }
}
