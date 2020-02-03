using AlphaVantage.Common;
using AlphaVantage.Common.Models.TechnicalIndicators.HT_SINE;
using AlphaVantage.Core.Abstracts;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace AlphaVantage.Core.TechnicalIndicators.HT_SINE
{
    public class AvHT_SINEProcess : AvMapResourceAbs<AvHT_SINE, AvHT_SINEMetaData, AvHT_SINEBlock>
    {
        protected override AvHT_SINEBlock MapToBlock(Dictionary<string, string> block, string dateTime)
        {
            var result = new AvHT_SINEBlock();

            var leadSine = decimal.Parse(block[AvHT_SINERes.BlockLeadSineTag]);
            var sine = decimal.Parse(block[AvHT_SINERes.BlockSineTag]);


            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvHT_SINEBlock, decimal, AvPropertyNameAttribute, string>
                (AvHT_SINERes.BlockLeadSineTag, result, leadSine, attr => attr.ExtractPropertyName);
            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvHT_SINEBlock, decimal, AvPropertyNameAttribute, string>
                (AvHT_SINERes.BlockSineTag, result, leadSine, attr => attr.ExtractPropertyName);

            return result;
        }

        protected override AvHT_SINEMetaData MapToMetaData(Dictionary<string, string> metaData)
        {
            var result = new AvHT_SINEMetaData();

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvHT_SINEMetaData, string, AvPropertyNameAttribute, string>
                (AvHT_SINERes.MetaDataSymbolTag, result, metaData[AvHT_SINERes.MetaDataSymbolTag],
                attr => attr.ExtractPropertyName);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvHT_SINEMetaData, string, AvPropertyNameAttribute, string>
                (AvHT_SINERes.MetaDataIndicatorTag, result, metaData[AvHT_SINERes.MetaDataIndicatorTag],
                attr => attr.ExtractPropertyName);

            var lastRefreshed = DateTime.Parse(metaData[AvHT_SINERes.MetaDataLastRefreshedTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvHT_SINEMetaData, DateTime, AvPropertyNameAttribute, string>
                (AvHT_SINERes.MetaDataLastRefreshedTag, result,
                lastRefreshed,
                attr => attr.ExtractPropertyName);

            var interval = AvIntervalEnum.FromDisplayName<AvIntervalEnum>(
                metaData[AvHT_SINERes.MetaDataIntervalTag],
                StringComparison.InvariantCultureIgnoreCase);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvHT_SINEMetaData, AvIntervalEnum, AvPropertyNameAttribute, string>
                (AvHT_SINERes.MetaDataIntervalTag, result,
                interval,
                attr => attr.ExtractPropertyName);

            var timeZone = AvTimeZoneConvertor.AvTimeZone(metaData[AvHT_SINERes.MetaDataTimeZoneTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvHT_SINEMetaData, TimeZoneInfo, AvPropertyNameAttribute, string>
                (AvHT_SINERes.MetaDataTimeZoneTag, result,
                timeZone,
                attr => attr.ExtractPropertyName);

            var seriesType = AvSeriesTypeEnum.FromDisplayName<AvSeriesTypeEnum>(
                metaData[AvHT_SINERes.MetaDataSeriesTypeTag],
                StringComparison.InvariantCultureIgnoreCase);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvHT_SINEMetaData, AvSeriesTypeEnum, AvPropertyNameAttribute, string>
                (AvHT_SINERes.MetaDataSeriesTypeTag, result,
                seriesType,
                attr => attr.ExtractPropertyName);

            return result;
        }

        protected override void ProcessDownloadResource(JObject remoteResource, string uri)
        {
            _metaData = remoteResource[AvHT_SINEProcessRes.MetaDataTag].ToObject<Dictionary<string, string>>();
            _content = remoteResource[AvHT_SINEProcessRes.TimeSeriesTag].ToObject<Dictionary<string, Dictionary<string, string>>>();
        }
    }
}
