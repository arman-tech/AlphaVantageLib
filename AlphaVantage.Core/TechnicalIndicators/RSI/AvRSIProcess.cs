using System;
using System.Collections.Generic;
using AlphaVantage.Common;
using AlphaVantage.Common.Models.TechnicalIndicators.RSI;
using AlphaVantage.Core.Abstracts;
using Newtonsoft.Json.Linq;

namespace AlphaVantage.Core.TechnicalIndicators.RSI
{
    public class AvRSIProcess : AvMapResourceAbs<AvRSI, AvRSIMetaData, AvRSIBlock>
    {

        protected override AvRSIBlock MapToBlock(Dictionary<string, string> block, string dateTime)
        {
            var result = new AvRSIBlock();

            var rsi = decimal.Parse(block[AvRSIRes.BlockRSITag]);
            var dateTimeStamp = DateTime.Parse(dateTime);

            // RSI
            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvRSIBlock, decimal, AvPropertyNameAttribute, string>
                (AvRSIRes.BlockRSITag, result, rsi, attr => attr.ExtractPropertyName);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvRSIBlock, DateTime, AvPropertyNameAttribute, string>
                (AvRSIRes.BlockDayTag, result,
                dateTimeStamp, attr => attr.ExtractPropertyName);

            return result;
        }

        protected override AvRSIMetaData MapToMetaData(Dictionary<string, string> metaData)
        {
            var result = new AvRSIMetaData();

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvRSIMetaData, string, AvPropertyNameAttribute, string>
                (AvRSIRes.MetaDataSymbolTag, result, metaData[AvRSIRes.MetaDataSymbolTag],
                attr => attr.ExtractPropertyName);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvRSIMetaData, string, AvPropertyNameAttribute, string>
                (AvRSIRes.MetaDataIndicatorTag, result, metaData[AvRSIRes.MetaDataIndicatorTag],
                attr => attr.ExtractPropertyName);

            var lastRefreshed = DateTime.Parse(metaData[AvRSIRes.MetaDataLastRefreshedTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvRSIMetaData, DateTime, AvPropertyNameAttribute, string>
                (AvRSIRes.MetaDataLastRefreshedTag, result,
                lastRefreshed,
                attr => attr.ExtractPropertyName);

            var interval = AvIntervalEnum.FromDisplayName<AvIntervalEnum>(
                metaData[AvRSIRes.MetaDataIntervalTag],
                StringComparison.InvariantCultureIgnoreCase);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvRSIMetaData, AvIntervalEnum, AvPropertyNameAttribute, string>
                (AvRSIRes.MetaDataIntervalTag, result,
                interval,
                attr => attr.ExtractPropertyName);

            var timePeriod = int.Parse(metaData[AvRSIRes.MetaDataTimePeriodTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvRSIMetaData, int, AvPropertyNameAttribute, string>
                (AvRSIRes.MetaDataTimePeriodTag, result, timePeriod, attr => attr.ExtractPropertyName);

            var seriesType = AvSeriesTypeEnum.FromDisplayName<AvSeriesTypeEnum>
                (metaData[AvRSIRes.MetaDataSeriesTypeTag], StringComparison.InvariantCultureIgnoreCase);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvRSIMetaData, AvSeriesTypeEnum, AvPropertyNameAttribute, string>
                (AvRSIRes.MetaDataSeriesTypeTag, result, seriesType, attr => attr.ExtractPropertyName);

            var timeZone = AvTimeZoneConvertor.AvTimeZone(metaData[AvRSIRes.MetaDataTimeZoneTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvRSIMetaData, TimeZoneInfo, AvPropertyNameAttribute, string>
                (AvRSIRes.MetaDataTimeZoneTag, result,
                timeZone,
                attr => attr.ExtractPropertyName);

            return result;
        }

        protected override void ProcessDownloadResource(JObject remoteResource, string uri)
        {
            _metaData = remoteResource[AvRSIProcessRes.MetaDataTag].ToObject<Dictionary<string, string>>();
            _content = remoteResource[AvRSIProcessRes.TimeSeriesTag].ToObject<Dictionary<string, Dictionary<string, string>>>();
        }

    }
}
