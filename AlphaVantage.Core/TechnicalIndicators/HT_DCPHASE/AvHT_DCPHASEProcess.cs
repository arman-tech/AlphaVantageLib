using AlphaVantage.Common;
using AlphaVantage.Common.Models.TechnicalIndicators.HT_DCPHASE;
using AlphaVantage.Core.Abstracts;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace AlphaVantage.Core.TechnicalIndicators.HT_DCPHASE
{
    public class AvHT_DCPHASEProcess : AvMapResourceAbs<AvHT_DCPHASE, AvHT_DCPHASEMetaData, AvHT_DCPHASEBlock>
    {
        protected override AvHT_DCPHASEBlock MapToBlock(Dictionary<string, string> block, string dateTime)
        {
            var result = new AvHT_DCPHASEBlock();

            var data = decimal.Parse(block[AvHT_DCPHASERes.BlockHT_DCPHASETag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvHT_DCPHASEBlock, decimal, AvPropertyNameAttribute, string>
                (AvHT_DCPHASERes.BlockHT_DCPHASETag, result, data, attr => attr.ExtractPropertyName);

            return result;
        }

        protected override AvHT_DCPHASEMetaData MapToMetaData(Dictionary<string, string> metaData)
        {
            var result = new AvHT_DCPHASEMetaData();

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvHT_DCPHASEMetaData, string, AvPropertyNameAttribute, string>
                (AvHT_DCPHASERes.MetaDataSymbolTag, result, metaData[AvHT_DCPHASERes.MetaDataSymbolTag],
                attr => attr.ExtractPropertyName);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvHT_DCPHASEMetaData, string, AvPropertyNameAttribute, string>
                (AvHT_DCPHASERes.MetaDataIndicatorTag, result, metaData[AvHT_DCPHASERes.MetaDataIndicatorTag],
                attr => attr.ExtractPropertyName);

            var lastRefreshed = DateTime.Parse(metaData[AvHT_DCPHASERes.MetaDataLastRefreshedTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvHT_DCPHASEMetaData, DateTime, AvPropertyNameAttribute, string>
                (AvHT_DCPHASERes.MetaDataLastRefreshedTag, result,
                lastRefreshed,
                attr => attr.ExtractPropertyName);

            var interval = AvIntervalEnum.FromDisplayName<AvIntervalEnum>(
                metaData[AvHT_DCPHASERes.MetaDataIntervalTag],
                StringComparison.InvariantCultureIgnoreCase);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvHT_DCPHASEMetaData, AvIntervalEnum, AvPropertyNameAttribute, string>
                (AvHT_DCPHASERes.MetaDataIntervalTag, result,
                interval,
                attr => attr.ExtractPropertyName);

            var timeZone = AvTimeZoneConvertor.AvTimeZone(metaData[AvHT_DCPHASERes.MetaDataTimeZoneTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvHT_DCPHASEMetaData, TimeZoneInfo, AvPropertyNameAttribute, string>
                (AvHT_DCPHASERes.MetaDataTimeZoneTag, result,
                timeZone,
                attr => attr.ExtractPropertyName);

            var seriesType = AvSeriesTypeEnum.FromDisplayName<AvSeriesTypeEnum>(
                metaData[AvHT_DCPHASERes.MetaDataSeriesTypeTag],
                StringComparison.InvariantCultureIgnoreCase);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvHT_DCPHASEMetaData, AvSeriesTypeEnum, AvPropertyNameAttribute, string>
                (AvHT_DCPHASERes.MetaDataSeriesTypeTag, result,
                seriesType,
                attr => attr.ExtractPropertyName);

            return result;
        }

        protected override void ProcessDownloadResource(JObject remoteResource, string uri)
        {
            _metaData = remoteResource[AvHT_DCPHASEProcessRes.MetaDataTag].ToObject<Dictionary<string, string>>();
            _content = remoteResource[AvHT_DCPHASEProcessRes.TimeSeriesTag].ToObject<Dictionary<string, Dictionary<string, string>>>();
        }
    }
}
