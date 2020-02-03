using AlphaVantage.Common;
using AlphaVantage.Common.Models.TechnicalIndicators.STOCHRSI;
using AlphaVantage.Core.Abstracts;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace AlphaVantage.Core.TechnicalIndicators.STOCHRSI
{
    public class AvSTOCHRSIProcess : AvMapResourceAbs<AvSTOCHRSI, AvSTOCHRSIMetaData, AvSTOCHRSIBlock>
    {
        protected override AvSTOCHRSIBlock MapToBlock(Dictionary<string, string> block, string dateTime)
        {
            var result = new AvSTOCHRSIBlock();

            var fastD = decimal.Parse(block[AvSTOCHRSIRes.BlockFastDTag]);
            var fastK = decimal.Parse(block[AvSTOCHRSIRes.BlockFastKTag]);


            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvSTOCHRSIBlock, decimal, AvPropertyNameAttribute, string>
                (AvSTOCHRSIRes.BlockFastDTag, result, fastD, attr => attr.ExtractPropertyName);
            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvSTOCHRSIBlock, decimal, AvPropertyNameAttribute, string>
                (AvSTOCHRSIRes.BlockFastKTag, result, fastK, attr => attr.ExtractPropertyName);

            return result;
        }

        protected override AvSTOCHRSIMetaData MapToMetaData(Dictionary<string, string> metaData)
        {
            var result = new AvSTOCHRSIMetaData();

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvSTOCHRSIMetaData, string, AvPropertyNameAttribute, string>
                (AvSTOCHRSIRes.MetaDataSymbolTag, result, metaData[AvSTOCHRSIRes.MetaDataSymbolTag],
                attr => attr.ExtractPropertyName);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvSTOCHRSIMetaData, string, AvPropertyNameAttribute, string>
                (AvSTOCHRSIRes.MetaDataIndicatorTag, result, metaData[AvSTOCHRSIRes.MetaDataIndicatorTag],
                attr => attr.ExtractPropertyName);

            var lastRefreshed = DateTime.Parse(metaData[AvSTOCHRSIRes.MetaDataLastRefreshedTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvSTOCHRSIMetaData, DateTime, AvPropertyNameAttribute, string>
                (AvSTOCHRSIRes.MetaDataLastRefreshedTag, result,
                lastRefreshed,
                attr => attr.ExtractPropertyName);

            var interval = AvIntervalEnum.FromDisplayName<AvIntervalEnum>(
                metaData[AvSTOCHRSIRes.MetaDataIntervalTag],
                StringComparison.InvariantCultureIgnoreCase);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvSTOCHRSIMetaData, AvIntervalEnum, AvPropertyNameAttribute, string>
                (AvSTOCHRSIRes.MetaDataIntervalTag, result,
                interval,
                attr => attr.ExtractPropertyName);

            var timeZone = AvTimeZoneConvertor.AvTimeZone(metaData[AvSTOCHRSIRes.MetaDataTimeZoneTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvSTOCHRSIMetaData, TimeZoneInfo, AvPropertyNameAttribute, string>
                (AvSTOCHRSIRes.MetaDataTimeZoneTag, result,
                timeZone,
                attr => attr.ExtractPropertyName);

            var timePeriod = int.Parse(metaData[AvSTOCHRSIRes.MetaDataTimePeriodTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvSTOCHRSIMetaData, int, AvPropertyNameAttribute, string>
                (AvSTOCHRSIRes.MetaDataTimePeriodTag, result,
                timePeriod,
                attr => attr.ExtractPropertyName);

            var seriesType = AvSeriesTypeEnum.FromDisplayName<AvSeriesTypeEnum>(
                metaData[AvSTOCHRSIRes.MetaDataSeriesTypeTag],
                StringComparison.InvariantCultureIgnoreCase);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvSTOCHRSIMetaData, AvSeriesTypeEnum, AvPropertyNameAttribute, string>
                (AvSTOCHRSIRes.MetaDataSeriesTypeTag, result,
                seriesType,
                attr => attr.ExtractPropertyName);

            var fastKPeriod = int.Parse(metaData[AvSTOCHRSIRes.MetaDataFastKPeriodTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvSTOCHRSIMetaData, int, AvPropertyNameAttribute, string>
                (AvSTOCHRSIRes.MetaDataFastKPeriodTag, result,
                fastKPeriod,
                attr => attr.ExtractPropertyName);

            var fastDPeriod = int.Parse(metaData[AvSTOCHRSIRes.MetaDataFastDPeriodTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvSTOCHRSIMetaData, int, AvPropertyNameAttribute, string>
                (AvSTOCHRSIRes.MetaDataFastDPeriodTag, result,
                fastDPeriod,
                attr => attr.ExtractPropertyName);

            var fastDMAType = int.Parse(metaData[AvSTOCHRSIRes.MetaDataFastDMATypeTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvSTOCHRSIMetaData, int, AvPropertyNameAttribute, string>
                (AvSTOCHRSIRes.MetaDataFastDMATypeTag, result,
                fastDMAType,
                attr => attr.ExtractPropertyName);

            return result;
        }

        protected override void ProcessDownloadResource(JObject remoteResource, string uri)
        {
            _metaData = remoteResource[AvSTOTCHRSIProcessRes.MetaDataTag].ToObject<Dictionary<string, string>>();
            _content = remoteResource[AvSTOTCHRSIProcessRes.TimeSeriesTag].ToObject<Dictionary<string, Dictionary<string, string>>>();
        }
    }
}
