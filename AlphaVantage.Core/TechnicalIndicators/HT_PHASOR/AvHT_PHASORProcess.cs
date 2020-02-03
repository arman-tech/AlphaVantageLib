using AlphaVantage.Common;
using AlphaVantage.Common.Models.TechnicalIndicators.HT_PHASOR;
using AlphaVantage.Core.Abstracts;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace AlphaVantage.Core.TechnicalIndicators.HT_PHASOR
{
    public class AvHT_PHASORProcess : AvMapResourceAbs<AvHT_PHASOR, AvHT_PHASORMetaData, AvHT_PHASORBlock>
    {
        protected override AvHT_PHASORBlock MapToBlock(Dictionary<string, string> block, string dateTime)
        {
            var result = new AvHT_PHASORBlock();

            var phase = decimal.Parse(block[AvHT_PHASORRes.BlockPhaseTag]);
            var quadrature = decimal.Parse(block[AvHT_PHASORRes.BlockQuadratureTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvHT_PHASORBlock, decimal, AvPropertyNameAttribute, string>
                (AvHT_PHASORRes.BlockPhaseTag, result, phase, attr => attr.ExtractPropertyName);
            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvHT_PHASORBlock, decimal, AvPropertyNameAttribute, string>
                (AvHT_PHASORRes.BlockQuadratureTag, result, phase, attr => attr.ExtractPropertyName);

            return result;
        }

        protected override AvHT_PHASORMetaData MapToMetaData(Dictionary<string, string> metaData)
        {
            var result = new AvHT_PHASORMetaData();

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvHT_PHASORMetaData, string, AvPropertyNameAttribute, string>
                (AvHT_PHASORRes.MetaDataSymbolTag, result, metaData[AvHT_PHASORRes.MetaDataSymbolTag],
                attr => attr.ExtractPropertyName);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvHT_PHASORMetaData, string, AvPropertyNameAttribute, string>
                (AvHT_PHASORRes.MetaDataIndicatorTag, result, metaData[AvHT_PHASORRes.MetaDataIndicatorTag],
                attr => attr.ExtractPropertyName);

            var lastRefreshed = DateTime.Parse(metaData[AvHT_PHASORRes.MetaDataLastRefreshedTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvHT_PHASORMetaData, DateTime, AvPropertyNameAttribute, string>
                (AvHT_PHASORRes.MetaDataLastRefreshedTag, result,
                lastRefreshed,
                attr => attr.ExtractPropertyName);

            var interval = AvIntervalEnum.FromDisplayName<AvIntervalEnum>(
                metaData[AvHT_PHASORRes.MetaDataIntervalTag],
                StringComparison.InvariantCultureIgnoreCase);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvHT_PHASORMetaData, AvIntervalEnum, AvPropertyNameAttribute, string>
                (AvHT_PHASORRes.MetaDataIntervalTag, result,
                interval,
                attr => attr.ExtractPropertyName);

            var timeZone = AvTimeZoneConvertor.AvTimeZone(metaData[AvHT_PHASORRes.MetaDataTimeZoneTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvHT_PHASORMetaData, TimeZoneInfo, AvPropertyNameAttribute, string>
                (AvHT_PHASORRes.MetaDataTimeZoneTag, result,
                timeZone,
                attr => attr.ExtractPropertyName);

            var seriesType = AvSeriesTypeEnum.FromDisplayName<AvSeriesTypeEnum>(
                metaData[AvHT_PHASORRes.MetaDataSeriesTypeTag],
                StringComparison.InvariantCultureIgnoreCase);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvHT_PHASORMetaData, AvSeriesTypeEnum, AvPropertyNameAttribute, string>
                (AvHT_PHASORRes.MetaDataSeriesTypeTag, result,
                seriesType,
                attr => attr.ExtractPropertyName);

            return result;
        }

        protected override void ProcessDownloadResource(JObject remoteResource, string uri)
        {
            _metaData = remoteResource[AvHT_PHASORProcessRes.MetaDataTag].ToObject<Dictionary<string, string>>();
            _content = remoteResource[AvHT_PHASORProcessRes.TimeSeriesTag].ToObject<Dictionary<string, Dictionary<string, string>>>();
        }
    }
}
