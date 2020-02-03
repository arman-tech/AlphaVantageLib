using AlphaVantage.Common;
using AlphaVantage.Common.Models.TechnicalIndicators.TRIMA;
using AlphaVantage.Core.Abstracts;
using AlphaVantage.Core.Interfaces;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace AlphaVantage.Core.TechnicalIndicators.TRIMA
{
    public class AvTRIMAProcess : AvMapResourceAbs<AvTRIMA, AvTRIMAMetaData, AvTRIMABlock>
    {
        protected override AvTRIMABlock MapToBlock(Dictionary<string, string> block, string dateTime)
        {
            var result = new AvTRIMABlock();

            var data = decimal.Parse(block[AvTRIMARes.BlockTRIMATag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvTRIMABlock, decimal, AvPropertyNameAttribute, string>
                (AvTRIMARes.BlockTRIMATag, result, data, attr => attr.ExtractPropertyName);

            return result;
        }

        protected override AvTRIMAMetaData MapToMetaData(Dictionary<string, string> metaData)
        {
            var result = new AvTRIMAMetaData();

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvTRIMAMetaData, string, AvPropertyNameAttribute, string>
                (AvTRIMARes.MetaDataSymbolTag, result, metaData[AvTRIMARes.MetaDataSymbolTag],
                attr => attr.ExtractPropertyName);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvTRIMAMetaData, string, AvPropertyNameAttribute, string>
                (AvTRIMARes.MetaDataIndicatorTag, result, metaData[AvTRIMARes.MetaDataIndicatorTag],
                attr => attr.ExtractPropertyName);

            var lastRefreshed = DateTime.Parse(metaData[AvTRIMARes.MetaDataLastRefreshedTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvTRIMAMetaData, DateTime, AvPropertyNameAttribute, string>
                (AvTRIMARes.MetaDataLastRefreshedTag, result,
                lastRefreshed,
                attr => attr.ExtractPropertyName);

            var interval = AvIntervalEnum.FromDisplayName<AvIntervalEnum>(
                metaData[AvTRIMARes.MetaDataIntervalTag],
                StringComparison.InvariantCultureIgnoreCase);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvTRIMAMetaData, AvIntervalEnum, AvPropertyNameAttribute, string>
                (AvTRIMARes.MetaDataIntervalTag, result,
                interval,
                attr => attr.ExtractPropertyName);

            var timeZone = AvTimeZoneConvertor.AvTimeZone(metaData[AvTRIMARes.MetaDataTimeZoneTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvTRIMAMetaData, TimeZoneInfo, AvPropertyNameAttribute, string>
                (AvTRIMARes.MetaDataTimeZoneTag, result,
                timeZone,
                attr => attr.ExtractPropertyName);

            var timePeriod = int.Parse(metaData[AvTRIMARes.MetaDataTimePeriodTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvTRIMAMetaData, int, AvPropertyNameAttribute, string>
                (AvTRIMARes.MetaDataTimePeriodTag, result,
                timePeriod,
                attr => attr.ExtractPropertyName);

            var seriesType = AvSeriesTypeEnum.FromDisplayName<AvSeriesTypeEnum>(
                metaData[AvTRIMARes.MetaDataSeriesTypeTag],
                StringComparison.InvariantCultureIgnoreCase);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvTRIMAMetaData, AvSeriesTypeEnum, AvPropertyNameAttribute, string>
                (AvTRIMARes.MetaDataSeriesTypeTag, result,
                seriesType,
                attr => attr.ExtractPropertyName);

            return result;
        }

        protected override void ProcessDownloadResource(JObject remoteResource, string uri)
        {
            _metaData = remoteResource[AvTRIMAProcessRes.MetaDataTag].ToObject<Dictionary<string, string>>();
            _content = remoteResource[AvTRIMAProcessRes.TimeSeriesTag].ToObject<Dictionary<string, Dictionary<string, string>>>();
        }
    }
}
