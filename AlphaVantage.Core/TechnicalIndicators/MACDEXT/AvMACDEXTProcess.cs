using AlphaVantage.Common;
using AlphaVantage.Common.Models.TechnicalIndicators.MACDEXT;
using AlphaVantage.Core.Abstracts;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace AlphaVantage.Core.TechnicalIndicators.MACDEXT
{
    public class AvMACDEXTProcess : AvMapResourceAbs<AvMACDEXT, AvMACDEXTMetaData, AvMACDEXTBlock>
    {
        protected override AvMACDEXTBlock MapToBlock(Dictionary<string, string> block, string dateTime)
        {
            var result = new AvMACDEXTBlock();

            var hist = decimal.Parse(block[AvMACDEXTRes.BlockMACDHistTag]);
            var mcad = decimal.Parse(block[AvMACDEXTRes.BlockMACDTag]);
            var signal = decimal.Parse(block[AvMACDEXTRes.BlockMACDSignalTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvMACDEXTBlock, decimal, AvPropertyNameAttribute, string>
                (AvMACDEXTRes.BlockMACDHistTag, result, hist, attr => attr.ExtractPropertyName);
            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvMACDEXTBlock, decimal, AvPropertyNameAttribute, string>
                (AvMACDEXTRes.BlockMACDTag, result, hist, attr => attr.ExtractPropertyName);
            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvMACDEXTBlock, decimal, AvPropertyNameAttribute, string>
                (AvMACDEXTRes.BlockMACDSignalTag, result, hist, attr => attr.ExtractPropertyName);

            return result;
        }

        protected override AvMACDEXTMetaData MapToMetaData(Dictionary<string, string> metaData)
        {
            var result = new AvMACDEXTMetaData();

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvMACDEXTMetaData, string, AvPropertyNameAttribute, string>
                (AvMACDEXTRes.MetaDataSymbolTag, result, metaData[AvMACDEXTRes.MetaDataSymbolTag],
                attr => attr.ExtractPropertyName);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvMACDEXTMetaData, string, AvPropertyNameAttribute, string>
                (AvMACDEXTRes.MetaDataIndicatorTag, result, metaData[AvMACDEXTRes.MetaDataIndicatorTag],
                attr => attr.ExtractPropertyName);

            var lastRefreshed = DateTime.Parse(metaData[AvMACDEXTRes.MetaDataLastRefreshedTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvMACDEXTMetaData, DateTime, AvPropertyNameAttribute, string>
                (AvMACDEXTRes.MetaDataLastRefreshedTag, result,
                lastRefreshed,
                attr => attr.ExtractPropertyName);

            var interval = AvIntervalEnum.FromDisplayName<AvIntervalEnum>(
                metaData[AvMACDEXTRes.MetaDataIntervalTag],
                StringComparison.InvariantCultureIgnoreCase);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvMACDEXTMetaData, AvIntervalEnum, AvPropertyNameAttribute, string>
                (AvMACDEXTRes.MetaDataIntervalTag, result,
                interval,
                attr => attr.ExtractPropertyName);

            var timeZone = AvTimeZoneConvertor.AvTimeZone(metaData[AvMACDEXTRes.MetaDataTimeZoneTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvMACDEXTMetaData, TimeZoneInfo, AvPropertyNameAttribute, string>
                (AvMACDEXTRes.MetaDataTimeZoneTag, result,
                timeZone,
                attr => attr.ExtractPropertyName);

            var seriesType = AvSeriesTypeEnum.FromDisplayName<AvSeriesTypeEnum>(
                metaData[AvMACDEXTRes.MetaDataSeriesTypeTag],
                StringComparison.InvariantCultureIgnoreCase);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvMACDEXTMetaData, AvSeriesTypeEnum, AvPropertyNameAttribute, string>
                (AvMACDEXTRes.MetaDataSeriesTypeTag, result,
                seriesType,
                attr => attr.ExtractPropertyName);

            var fastPeriod = int.Parse(metaData[AvMACDEXTRes.MetaDataFastPeriodTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvMACDEXTMetaData, int, AvPropertyNameAttribute, string>
                (AvMACDEXTRes.MetaDataFastPeriodTag, result,
                fastPeriod,
                attr => attr.ExtractPropertyName);

            var slowPeriod = int.Parse(metaData[AvMACDEXTRes.MetaDataSlowPeriodTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvMACDEXTMetaData, int, AvPropertyNameAttribute, string>
                (AvMACDEXTRes.MetaDataSlowPeriodTag, result,
                slowPeriod,
                attr => attr.ExtractPropertyName);

            var signalPeriod = int.Parse(metaData[AvMACDEXTRes.MetaDataSignalPeriodTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvMACDEXTMetaData, int, AvPropertyNameAttribute, string>
                (AvMACDEXTRes.MetaDataSignalPeriodTag, result,
                signalPeriod,
                attr => attr.ExtractPropertyName);

            var fastMAType = int.Parse(metaData[AvMACDEXTRes.MetaDataFastMATypeTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvMACDEXTMetaData, int, AvPropertyNameAttribute, string>
                (AvMACDEXTRes.MetaDataFastMATypeTag, result,
                fastMAType,
                attr => attr.ExtractPropertyName);

            var slowMAType = int.Parse(metaData[AvMACDEXTRes.MetaDataSlowMATypeTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvMACDEXTMetaData, int, AvPropertyNameAttribute, string>
                (AvMACDEXTRes.MetaDataSlowMATypeTag, result,
                slowMAType,
                attr => attr.ExtractPropertyName);

            var signalMAType = int.Parse(metaData[AvMACDEXTRes.MetaDataSignalMATypeTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvMACDEXTMetaData, int, AvPropertyNameAttribute, string>
                (AvMACDEXTRes.MetaDataSignalMATypeTag, result,
                signalMAType,
                attr => attr.ExtractPropertyName);


            return result;
        }

        protected override void ProcessDownloadResource(JObject remoteResource, string uri)
        {
            _metaData = remoteResource[AvMACDEXTProcessRes.MetaDataTag].ToObject<Dictionary<string, string>>();
            _content = remoteResource[AvMACDEXTProcessRes.TimeSeriesTag].ToObject<Dictionary<string, Dictionary<string, string>>>();
        }
    }
}
