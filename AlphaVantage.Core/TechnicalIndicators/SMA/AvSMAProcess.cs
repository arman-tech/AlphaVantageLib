using AlphaVantage.Common;
using AlphaVantage.Common.Models.TechnicalIndicators.SMA;
using AlphaVantage.Core.Abstracts;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace AlphaVantage.Core.TechnicalIndicators.SMA
{
    public class AvSMAProcess : AvMapResourceAbs<AvSMA, AvSMAMetaData, AvSMABlock>
    {
        protected override AvSMABlock MapToBlock(Dictionary<string, string> block, string dateTime)
        {
            var result = new AvSMABlock();

            var sma = decimal.Parse(block[AvSMARes.BlockSMATag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvSMABlock, decimal, AvPropertyNameAttribute, string>
                (AvSMARes.BlockSMATag, result, sma, attr => attr.ExtractPropertyName);

            return result;
        }

        protected override AvSMAMetaData MapToMetaData(Dictionary<string, string> metaData)
        {
            var result = new AvSMAMetaData();

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvSMAMetaData, string, AvPropertyNameAttribute, string>
                (AvSMARes.MetaDataSymbolTag, result, metaData[AvSMARes.MetaDataSymbolTag],
                attr => attr.ExtractPropertyName);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvSMAMetaData, string, AvPropertyNameAttribute, string>
                (AvSMARes.MetaDataIndicatorTag, result, metaData[AvSMARes.MetaDataIndicatorTag],
                attr => attr.ExtractPropertyName);

            var lastRefreshed = DateTime.Parse(metaData[AvSMARes.MetaDataLastRefreshedTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvSMAMetaData, DateTime, AvPropertyNameAttribute, string>
                (AvSMARes.MetaDataLastRefreshedTag, result,
                lastRefreshed,
                attr => attr.ExtractPropertyName);

            var interval = AvIntervalEnum.FromDisplayName<AvIntervalEnum>(
                metaData[AvSMARes.MetaDataIntervalTag],
                StringComparison.InvariantCultureIgnoreCase);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvSMAMetaData, AvIntervalEnum, AvPropertyNameAttribute, string>
                (AvSMARes.MetaDataIntervalTag, result,
                interval,
                attr => attr.ExtractPropertyName);

            var timeZone = AvTimeZoneConvertor.AvTimeZone(metaData[AvSMARes.MetaDataTimeZoneTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvSMAMetaData, TimeZoneInfo, AvPropertyNameAttribute, string>
                (AvSMARes.MetaDataTimeZoneTag, result,
                timeZone,
                attr => attr.ExtractPropertyName);

            var timePeriod = int.Parse(metaData[AvSMARes.MetaDataTimePeriodTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvSMAMetaData, int, AvPropertyNameAttribute, string>
                (AvSMARes.MetaDataTimePeriodTag, result,
                timePeriod,
                attr => attr.ExtractPropertyName);

            var seriesType = AvSeriesTypeEnum.FromDisplayName<AvSeriesTypeEnum>(
                metaData[AvSMARes.MetaDataSeriesTypeTag],
                StringComparison.InvariantCultureIgnoreCase);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvSMAMetaData, AvSeriesTypeEnum, AvPropertyNameAttribute, string>
                (AvSMARes.MetaDataSeriesTypeTag, result,
                seriesType,
                attr => attr.ExtractPropertyName);

            return result;
        }

        protected override void ProcessDownloadResource(JObject remoteResource, string uri)
        {
            _metaData = remoteResource[AvSMAProcessRes.MetaDataTag].ToObject<Dictionary<string, string>>();
            _content = remoteResource[AvSMAProcessRes.TimeSeriesTag].ToObject<Dictionary<string, Dictionary<string, string>>>();
        }
    }
}
