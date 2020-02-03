using AlphaVantage.Common;
using AlphaVantage.Common.Models.TechnicalIndicators.APO;
using AlphaVantage.Core.Abstracts;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace AlphaVantage.Core.TechnicalIndicators.APO
{
    public class AvAPOProcess : AvMapResourceAbs<AvAPO, AvAPOMetaData, AvAPOBlock>
    {
        protected override AvAPOBlock MapToBlock(Dictionary<string, string> block, string dateTime)
        {
            var result = new AvAPOBlock();

            var apo = decimal.Parse(block[AvAPORes.BlockAPOTag]);

            // chaikain
            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvAPOBlock, decimal, AvPropertyNameAttribute, string>
                (AvAPORes.BlockAPOTag, result, apo, attr => attr.ExtractPropertyName);

            return result;
        }

        protected override AvAPOMetaData MapToMetaData(Dictionary<string, string> metaData)
        {
            var result = new AvAPOMetaData();

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvAPOMetaData, string, AvPropertyNameAttribute, string>
                (AvAPORes.MetaDataSymbolTag, result, metaData[AvAPORes.MetaDataSymbolTag],
                attr => attr.ExtractPropertyName);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvAPOMetaData, string, AvPropertyNameAttribute, string>
                (AvAPORes.MetaDataIndicatorTag, result, metaData[AvAPORes.MetaDataIndicatorTag],
                attr => attr.ExtractPropertyName);

            var lastRefreshed = DateTime.Parse(metaData[AvAPORes.MetaDataLastRefreshedTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvAPOMetaData, DateTime, AvPropertyNameAttribute, string>
                (AvAPORes.MetaDataLastRefreshedTag, result,
                lastRefreshed,
                attr => attr.ExtractPropertyName);

            var interval = AvIntervalEnum.FromDisplayName<AvIntervalEnum>(
                metaData[AvAPORes.MetaDataIntervalTag],
                StringComparison.InvariantCultureIgnoreCase);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvAPOMetaData, AvIntervalEnum, AvPropertyNameAttribute, string>
                (AvAPORes.MetaDataIntervalTag, result,
                interval,
                attr => attr.ExtractPropertyName);

            var timeZone = AvTimeZoneConvertor.AvTimeZone(metaData[AvAPORes.MetaDataTimeZoneTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvAPOMetaData, TimeZoneInfo, AvPropertyNameAttribute, string>
                (AvAPORes.MetaDataTimeZoneTag, result,
                timeZone,
                attr => attr.ExtractPropertyName);

            var fastPeriod = int.Parse(metaData[AvAPORes.MetaDataFastPeriodTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvAPOMetaData, int, AvPropertyNameAttribute, string>
                (AvAPORes.MetaDataFastPeriodTag, result,
                fastPeriod,
                attr => attr.ExtractPropertyName);

            var slowPeriod = int.Parse(metaData[AvAPORes.MetaDataSlowPeriodTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvAPOMetaData, int, AvPropertyNameAttribute, string>
                (AvAPORes.MetaDataSlowPeriodTag, result,
                slowPeriod,
                attr => attr.ExtractPropertyName);

            var maType = AvMovingAverageTypeEnum.FromValue<AvMovingAverageTypeEnum>(
                int.Parse(metaData[AvAPORes.MetaDataMATypeTag]));

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvAPOMetaData, AvMovingAverageTypeEnum, AvPropertyNameAttribute, string>
                (AvAPORes.MetaDataMATypeTag, result,
                maType,
                attr => attr.ExtractPropertyName);

            var seriesType = AvSeriesTypeEnum.FromDisplayName<AvSeriesTypeEnum>(
                metaData[AvAPORes.MetaDataSeriesTypeTag],
                StringComparison.InvariantCultureIgnoreCase);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvAPOMetaData, AvSeriesTypeEnum, AvPropertyNameAttribute, string>
                (AvAPORes.MetaDataSeriesTypeTag, result,
                seriesType,
                attr => attr.ExtractPropertyName);

            return result;
        }

        protected override void ProcessDownloadResource(JObject remoteResource, string uri)
        {
            _metaData = remoteResource[AvAPOProcessRes.MetaDataTag].ToObject<Dictionary<string, string>>();
            _content = remoteResource[AvAPOProcessRes.TimeSeriesTag].ToObject<Dictionary<string, Dictionary<string, string>>>();
        }
    }
}
