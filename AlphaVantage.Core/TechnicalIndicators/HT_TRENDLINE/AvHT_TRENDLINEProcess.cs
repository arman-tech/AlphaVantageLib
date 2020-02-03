using AlphaVantage.Common;
using AlphaVantage.Common.Models.TechnicalIndicators.HT_TRENDLINE;
using AlphaVantage.Core.Abstracts;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace AlphaVantage.Core.TechnicalIndicators.HT_TRENDLINE
{
    public class AvHT_TRENDLINEProcess : AvMapResourceAbs<AvHT_TRENDLINE, AvHT_TRENDLINEMetaData, AvHT_TRENDLINEBlock>
    {
        protected override AvHT_TRENDLINEBlock MapToBlock(Dictionary<string, string> block, string dateTime)
        {
            var result = new AvHT_TRENDLINEBlock();

            var data = decimal.Parse(block[AvHT_TRENDLINERes.BlockHT_TRENDLINETag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvHT_TRENDLINEBlock, decimal, AvPropertyNameAttribute, string>
                (AvHT_TRENDLINERes.BlockHT_TRENDLINETag, result, data, attr => attr.ExtractPropertyName);

            return result;
        }

        protected override AvHT_TRENDLINEMetaData MapToMetaData(Dictionary<string, string> metaData)
        {
            var result = new AvHT_TRENDLINEMetaData();

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvHT_TRENDLINEMetaData, string, AvPropertyNameAttribute, string>
                (AvHT_TRENDLINERes.MetaDataSymbolTag, result, metaData[AvHT_TRENDLINERes.MetaDataSymbolTag],
                attr => attr.ExtractPropertyName);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvHT_TRENDLINEMetaData, string, AvPropertyNameAttribute, string>
                (AvHT_TRENDLINERes.MetaDataIndicatorTag, result, metaData[AvHT_TRENDLINERes.MetaDataIndicatorTag],
                attr => attr.ExtractPropertyName);

            var lastRefreshed = DateTime.Parse(metaData[AvHT_TRENDLINERes.MetaDataLastRefreshedTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvHT_TRENDLINEMetaData, DateTime, AvPropertyNameAttribute, string>
                (AvHT_TRENDLINERes.MetaDataLastRefreshedTag, result,
                lastRefreshed,
                attr => attr.ExtractPropertyName);

            var interval = AvIntervalEnum.FromDisplayName<AvIntervalEnum>(
                metaData[AvHT_TRENDLINERes.MetaDataIntervalTag],
                StringComparison.InvariantCultureIgnoreCase);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvHT_TRENDLINEMetaData, AvIntervalEnum, AvPropertyNameAttribute, string>
                (AvHT_TRENDLINERes.MetaDataIntervalTag, result,
                interval,
                attr => attr.ExtractPropertyName);

            var timeZone = AvTimeZoneConvertor.AvTimeZone(metaData[AvHT_TRENDLINERes.MetaDataTimeZoneTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvHT_TRENDLINEMetaData, TimeZoneInfo, AvPropertyNameAttribute, string>
                (AvHT_TRENDLINERes.MetaDataTimeZoneTag, result,
                timeZone,
                attr => attr.ExtractPropertyName);

            var seriesType = AvSeriesTypeEnum.FromDisplayName<AvSeriesTypeEnum>(
                metaData[AvHT_TRENDLINERes.MetaDataSeriesTypeTag],
                StringComparison.InvariantCultureIgnoreCase);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvHT_TRENDLINEMetaData, AvSeriesTypeEnum, AvPropertyNameAttribute, string>
                (AvHT_TRENDLINERes.MetaDataSeriesTypeTag, result,
                seriesType,
                attr => attr.ExtractPropertyName);

            return result;
        }

        protected override void ProcessDownloadResource(JObject remoteResource, string uri)
        {
            _metaData = remoteResource[AvHT_TRENDLINEProcessRes.MetaDataTag].ToObject<Dictionary<string, string>>();
            _content = remoteResource[AvHT_TRENDLINEProcessRes.TimeSeriesTag].ToObject<Dictionary<string, Dictionary<string, string>>>();
        }
    }
}
