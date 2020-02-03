using AlphaVantage.Common;
using AlphaVantage.Common.Models.TechnicalIndicators.PPO;
using AlphaVantage.Core.Abstracts;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace AlphaVantage.Core.TechnicalIndicators.PPO
{
    public class AvPPOProcess : AvMapResourceAbs<AvPPO, AvPPOMetaData, AvPPOBlock>
    {
        protected override AvPPOBlock MapToBlock(Dictionary<string, string> block, string dateTime)
        {
            var result = new AvPPOBlock();

            var data = decimal.Parse(block[AvPPORes.BlockPPOTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvPPOBlock, decimal, AvPropertyNameAttribute, string>
                (AvPPORes.BlockPPOTag, result, data, attr => attr.ExtractPropertyName);

            return result;
        }

        protected override AvPPOMetaData MapToMetaData(Dictionary<string, string> metaData)
        {
            var result = new AvPPOMetaData();

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvPPOMetaData, string, AvPropertyNameAttribute, string>
                (AvPPORes.MetaDataSymbolTag, result, metaData[AvPPORes.MetaDataSymbolTag],
                attr => attr.ExtractPropertyName);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvPPOMetaData, string, AvPropertyNameAttribute, string>
                (AvPPORes.MetaDataIndicatorTag, result, metaData[AvPPORes.MetaDataIndicatorTag],
                attr => attr.ExtractPropertyName);

            var lastRefreshed = DateTime.Parse(metaData[AvPPORes.MetaDataLastRefreshedTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvPPOMetaData, DateTime, AvPropertyNameAttribute, string>
                (AvPPORes.MetaDataLastRefreshedTag, result,
                lastRefreshed,
                attr => attr.ExtractPropertyName);

            var interval = AvIntervalEnum.FromDisplayName<AvIntervalEnum>(
                metaData[AvPPORes.MetaDataIntervalTag],
                StringComparison.InvariantCultureIgnoreCase);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvPPOMetaData, AvIntervalEnum, AvPropertyNameAttribute, string>
                (AvPPORes.MetaDataIntervalTag, result,
                interval,
                attr => attr.ExtractPropertyName);

            var timeZone = AvTimeZoneConvertor.AvTimeZone(metaData[AvPPORes.MetaDataTimeZoneTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvPPOMetaData, TimeZoneInfo, AvPropertyNameAttribute, string>
                (AvPPORes.MetaDataTimeZoneTag, result,
                timeZone,
                attr => attr.ExtractPropertyName);

            var seriesType = AvSeriesTypeEnum.FromDisplayName<AvSeriesTypeEnum>(
                metaData[AvPPORes.MetaDataSeriesTypeTag],
                StringComparison.InvariantCultureIgnoreCase);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvPPOMetaData, AvSeriesTypeEnum, AvPropertyNameAttribute, string>
                (AvPPORes.MetaDataSeriesTypeTag, result,
                seriesType,
                attr => attr.ExtractPropertyName);

            var fastPeriod = int.Parse(metaData[AvPPORes.MetaDataFastPeriodTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvPPOMetaData, int, AvPropertyNameAttribute, string>
                (AvPPORes.MetaDataFastPeriodTag, result,
                fastPeriod,
                attr => attr.ExtractPropertyName);

            var slowPeriod = int.Parse(metaData[AvPPORes.MetaDataSlowPeriodTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvPPOMetaData, int, AvPropertyNameAttribute, string>
                (AvPPORes.MetaDataSlowPeriodTag, result,
                slowPeriod,
                attr => attr.ExtractPropertyName);

            var maType = AvMovingAverageTypeEnum.FromValue<AvMovingAverageTypeEnum>(
                int.Parse(metaData[AvPPORes.MetaDataMATypeTag]));

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvPPOMetaData, AvMovingAverageTypeEnum, AvPropertyNameAttribute, string>
                (AvPPORes.MetaDataMATypeTag, result,
                maType,
                attr => attr.ExtractPropertyName);

            return result;
        }

        protected override void ProcessDownloadResource(JObject remoteResource, string uri)
        {
            _metaData = remoteResource[AvPPOProcessRes.MetaDataTag].ToObject<Dictionary<string, string>>();
            _content = remoteResource[AvPPOProcessRes.TimeSeriesTag].ToObject<Dictionary<string, Dictionary<string, string>>>();
        }
    }
}
