using AlphaVantage.Common;
using AlphaVantage.Common.Models.TechnicalIndicators.T3;
using AlphaVantage.Core.Abstracts;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace AlphaVantage.Core.TechnicalIndicators.T3
{
    public class AvT3Process : AvMapResourceAbs<AvT3, AvT3MetaData, AvT3Block>
    {
        protected override AvT3Block MapToBlock(Dictionary<string, string> block, string dateTime)
        {
            var result = new AvT3Block();

            var data = decimal.Parse(block[AvT3Res.BlockT3Tag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvT3Block, decimal, AvPropertyNameAttribute, string>
                (AvT3Res.BlockT3Tag, result, data, attr => attr.ExtractPropertyName);

            return result;
        }

        protected override AvT3MetaData MapToMetaData(Dictionary<string, string> metaData)
        {
            var result = new AvT3MetaData();

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvT3MetaData, string, AvPropertyNameAttribute, string>
                (AvT3Res.MetaDataSymbolTag, result, metaData[AvT3Res.MetaDataSymbolTag],
                attr => attr.ExtractPropertyName);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvT3MetaData, string, AvPropertyNameAttribute, string>
                (AvT3Res.MetaDataIndicatorTag, result, metaData[AvT3Res.MetaDataIndicatorTag],
                attr => attr.ExtractPropertyName);

            var lastRefreshed = DateTime.Parse(metaData[AvT3Res.MetaDataLastRefreshedTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvT3MetaData, DateTime, AvPropertyNameAttribute, string>
                (AvT3Res.MetaDataLastRefreshedTag, result,
                lastRefreshed,
                attr => attr.ExtractPropertyName);

            var interval = AvIntervalEnum.FromDisplayName<AvIntervalEnum>(
                metaData[AvT3Res.MetaDataIntervalTag],
                StringComparison.InvariantCultureIgnoreCase);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvT3MetaData, AvIntervalEnum, AvPropertyNameAttribute, string>
                (AvT3Res.MetaDataIntervalTag, result,
                interval,
                attr => attr.ExtractPropertyName);

            var timeZone = AvTimeZoneConvertor.AvTimeZone(metaData[AvT3Res.MetaDataTimeZoneTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvT3MetaData, TimeZoneInfo, AvPropertyNameAttribute, string>
                (AvT3Res.MetaDataTimeZoneTag, result,
                timeZone,
                attr => attr.ExtractPropertyName);

            var timePeriod = int.Parse(metaData[AvT3Res.MetaDataTimePeriodTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvT3MetaData, int, AvPropertyNameAttribute, string>
                (AvT3Res.MetaDataTimePeriodTag, result,
                timePeriod,
                attr => attr.ExtractPropertyName);

            var seriesType = AvSeriesTypeEnum.FromDisplayName<AvSeriesTypeEnum>(
                metaData[AvT3Res.MetaDataSeriesTypeTag],
                StringComparison.InvariantCultureIgnoreCase);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvT3MetaData, AvSeriesTypeEnum, AvPropertyNameAttribute, string>
                (AvT3Res.MetaDataSeriesTypeTag, result,
                seriesType,
                attr => attr.ExtractPropertyName);

            var volumeFactor = decimal.Parse(metaData[AvT3Res.MetaDataVolumeFactorTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvT3MetaData, decimal, AvPropertyNameAttribute, string>
                (AvT3Res.MetaDataVolumeFactorTag, result,
                volumeFactor,
                attr => attr.ExtractPropertyName);

            return result;
        }

        protected override void ProcessDownloadResource(JObject remoteResource, string uri)
        {
            _metaData = remoteResource[AvT3ProcessRes.MetaDataTag].ToObject<Dictionary<string, string>>();
            _content = remoteResource[AvT3ProcessRes.TimeSeriesTag].ToObject<Dictionary<string, Dictionary<string, string>>>();
        }
    }
}
