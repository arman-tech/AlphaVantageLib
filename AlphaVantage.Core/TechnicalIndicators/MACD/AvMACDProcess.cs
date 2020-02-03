using AlphaVantage.Common;
using AlphaVantage.Common.Models.TechnicalIndicators.MACD;
using AlphaVantage.Core.Abstracts;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace AlphaVantage.Core.TechnicalIndicators.MACD
{
    public class AvMACDProcess : AvMapResourceAbs<AvMACD, AvMACDMetaData, AvMACDBlock>
    {
        protected override AvMACDBlock MapToBlock(Dictionary<string, string> block, string dateTime)
        {
            var result = new AvMACDBlock();

            var hist = decimal.Parse(block[AvMACDRes.BlockMACDHistTag]);
            var mcad = decimal.Parse(block[AvMACDRes.BlockMACDTag]);
            var signal = decimal.Parse(block[AvMACDRes.BlockMACDSignalTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvMACDBlock, decimal, AvPropertyNameAttribute, string>
                (AvMACDRes.BlockMACDHistTag, result, hist, attr => attr.ExtractPropertyName);
            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvMACDBlock, decimal, AvPropertyNameAttribute, string>
                (AvMACDRes.BlockMACDTag, result, hist, attr => attr.ExtractPropertyName);
            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvMACDBlock, decimal, AvPropertyNameAttribute, string>
                (AvMACDRes.BlockMACDSignalTag, result, hist, attr => attr.ExtractPropertyName);

            return result;
        }

        protected override AvMACDMetaData MapToMetaData(Dictionary<string, string> metaData)
        {
            var result = new AvMACDMetaData();

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvMACDMetaData, string, AvPropertyNameAttribute, string>
                (AvMACDRes.MetaDataSymbolTag, result, metaData[AvMACDRes.MetaDataSymbolTag],
                attr => attr.ExtractPropertyName);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvMACDMetaData, string, AvPropertyNameAttribute, string>
                (AvMACDRes.MetaDataIndicatorTag, result, metaData[AvMACDRes.MetaDataIndicatorTag],
                attr => attr.ExtractPropertyName);

            var lastRefreshed = DateTime.Parse(metaData[AvMACDRes.MetaDataLastRefreshedTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvMACDMetaData, DateTime, AvPropertyNameAttribute, string>
                (AvMACDRes.MetaDataLastRefreshedTag, result,
                lastRefreshed,
                attr => attr.ExtractPropertyName);

            var interval = AvIntervalEnum.FromDisplayName<AvIntervalEnum>(
                metaData[AvMACDRes.MetaDataIntervalTag],
                StringComparison.InvariantCultureIgnoreCase);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvMACDMetaData, AvIntervalEnum, AvPropertyNameAttribute, string>
                (AvMACDRes.MetaDataIntervalTag, result,
                interval,
                attr => attr.ExtractPropertyName);

            var timeZone = AvTimeZoneConvertor.AvTimeZone(metaData[AvMACDRes.MetaDataTimeZoneTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvMACDMetaData, TimeZoneInfo, AvPropertyNameAttribute, string>
                (AvMACDRes.MetaDataTimeZoneTag, result,
                timeZone,
                attr => attr.ExtractPropertyName);

            var seriesType = AvSeriesTypeEnum.FromDisplayName<AvSeriesTypeEnum>(
                metaData[AvMACDRes.MetaDataSeriesTypeTag],
                StringComparison.InvariantCultureIgnoreCase);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvMACDMetaData, AvSeriesTypeEnum, AvPropertyNameAttribute, string>
                (AvMACDRes.MetaDataSeriesTypeTag, result,
                seriesType,
                attr => attr.ExtractPropertyName);

            var fastPeriod = int.Parse(metaData[AvMACDRes.MetaDataFastPeriodTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvMACDMetaData, int, AvPropertyNameAttribute, string>
                (AvMACDRes.MetaDataFastPeriodTag, result,
                fastPeriod,
                attr => attr.ExtractPropertyName);

            var slowPeriod = int.Parse(metaData[AvMACDRes.MetaDataSlowPeriodTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvMACDMetaData, int, AvPropertyNameAttribute, string>
                (AvMACDRes.MetaDataSlowPeriodTag, result,
                slowPeriod,
                attr => attr.ExtractPropertyName);

            var signalPeriod = int.Parse(metaData[AvMACDRes.MetaDataSignalPeriodTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvMACDMetaData, int, AvPropertyNameAttribute, string>
                (AvMACDRes.MetaDataSignalPeriodTag, result,
                signalPeriod,
                attr => attr.ExtractPropertyName);


            return result;
        }

        protected override void ProcessDownloadResource(JObject remoteResource, string uri)
        {
            _metaData = remoteResource[AvMACDProcessRes.MetaDataTag].ToObject<Dictionary<string, string>>();
            _content = remoteResource[AvMACDProcessRes.TimeSeriesTag].ToObject<Dictionary<string, Dictionary<string, string>>>();
        }
    }
}
