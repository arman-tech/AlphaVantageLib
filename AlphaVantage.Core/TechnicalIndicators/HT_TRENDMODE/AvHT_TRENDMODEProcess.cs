using AlphaVantage.Common;
using AlphaVantage.Common.Models.TechnicalIndicators.HT_TRENDMODE;
using AlphaVantage.Core.Abstracts;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace AlphaVantage.Core.TechnicalIndicators.HT_TRENDMODE
{
    public class AvHT_TRENDMODEProcess : AvMapResourceAbs<AvHT_TRENDMODE, AvHT_TRENDMODEMetaData, AvHT_TRENDMODEBlock>
    {
        protected override AvHT_TRENDMODEBlock MapToBlock(Dictionary<string, string> block, string dateTime)
        {
            var result = new AvHT_TRENDMODEBlock();

            var data = decimal.Parse(block[AvHT_TRENDMODERes.BlockHT_TRENDMODETag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvHT_TRENDMODEBlock, decimal, AvPropertyNameAttribute, string>
                (AvHT_TRENDMODERes.BlockHT_TRENDMODETag, result, data, attr => attr.ExtractPropertyName);

            return result;
        }

        protected override AvHT_TRENDMODEMetaData MapToMetaData(Dictionary<string, string> metaData)
        {
            var result = new AvHT_TRENDMODEMetaData();

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvHT_TRENDMODEMetaData, string, AvPropertyNameAttribute, string>
                (AvHT_TRENDMODERes.MetaDataSymbolTag, result, metaData[AvHT_TRENDMODERes.MetaDataSymbolTag],
                attr => attr.ExtractPropertyName);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvHT_TRENDMODEMetaData, string, AvPropertyNameAttribute, string>
                (AvHT_TRENDMODERes.MetaDataIndicatorTag, result, metaData[AvHT_TRENDMODERes.MetaDataIndicatorTag],
                attr => attr.ExtractPropertyName);

            var lastRefreshed = DateTime.Parse(metaData[AvHT_TRENDMODERes.MetaDataLastRefreshedTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvHT_TRENDMODEMetaData, DateTime, AvPropertyNameAttribute, string>
                (AvHT_TRENDMODERes.MetaDataLastRefreshedTag, result,
                lastRefreshed,
                attr => attr.ExtractPropertyName);

            var interval = AvIntervalEnum.FromDisplayName<AvIntervalEnum>(
                metaData[AvHT_TRENDMODERes.MetaDataIntervalTag],
                StringComparison.InvariantCultureIgnoreCase);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvHT_TRENDMODEMetaData, AvIntervalEnum, AvPropertyNameAttribute, string>
                (AvHT_TRENDMODERes.MetaDataIntervalTag, result,
                interval,
                attr => attr.ExtractPropertyName);

            var timeZone = AvTimeZoneConvertor.AvTimeZone(metaData[AvHT_TRENDMODERes.MetaDataTimeZoneTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvHT_TRENDMODEMetaData, TimeZoneInfo, AvPropertyNameAttribute, string>
                (AvHT_TRENDMODERes.MetaDataTimeZoneTag, result,
                timeZone,
                attr => attr.ExtractPropertyName);

            var seriesType = AvSeriesTypeEnum.FromDisplayName<AvSeriesTypeEnum>(
                metaData[AvHT_TRENDMODERes.MetaDataSeriesTypeTag],
                StringComparison.InvariantCultureIgnoreCase);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvHT_TRENDMODEMetaData, AvSeriesTypeEnum, AvPropertyNameAttribute, string>
                (AvHT_TRENDMODERes.MetaDataSeriesTypeTag, result,
                seriesType,
                attr => attr.ExtractPropertyName);

            return result;
        }

        protected override void ProcessDownloadResource(JObject remoteResource, string uri)
        {
            _metaData = remoteResource[AvHT_TRENDMODEProcessRes.MetaDataTag].ToObject<Dictionary<string, string>>();
            _content = remoteResource[AvHT_TRENDMODEProcessRes.TimeSeriesTag].ToObject<Dictionary<string, Dictionary<string, string>>>();
        }
    }
}
