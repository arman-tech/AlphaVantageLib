using AlphaVantage.Common;
using AlphaVantage.Common.Models.TechnicalIndicators.HT_DCPERIOD;
using AlphaVantage.Core.Abstracts;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace AlphaVantage.Core.TechnicalIndicators.HT_DCPERIOD
{
    public class AvHT_DCPERIODProcess : AvMapResourceAbs<AvHT_DCPERIOD, AvHT_DCPERIODMetaData, AvHT_DCPERIODBlock>
    {
        protected override AvHT_DCPERIODBlock MapToBlock(Dictionary<string, string> block, string dateTime)
        {
            var result = new AvHT_DCPERIODBlock();

            var data = decimal.Parse(block[AvHT_DCPERIODRes.BlockHT_DCPERIODTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvHT_DCPERIODBlock, decimal, AvPropertyNameAttribute, string>
                (AvHT_DCPERIODRes.BlockHT_DCPERIODTag, result, data, attr => attr.ExtractPropertyName);

            return result;
        }

        protected override AvHT_DCPERIODMetaData MapToMetaData(Dictionary<string, string> metaData)
        {
            var result = new AvHT_DCPERIODMetaData();

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvHT_DCPERIODMetaData, string, AvPropertyNameAttribute, string>
                (AvHT_DCPERIODRes.MetaDataSymbolTag, result, metaData[AvHT_DCPERIODRes.MetaDataSymbolTag],
                attr => attr.ExtractPropertyName);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvHT_DCPERIODMetaData, string, AvPropertyNameAttribute, string>
                (AvHT_DCPERIODRes.MetaDataIndicatorTag, result, metaData[AvHT_DCPERIODRes.MetaDataIndicatorTag],
                attr => attr.ExtractPropertyName);

            var lastRefreshed = DateTime.Parse(metaData[AvHT_DCPERIODRes.MetaDataLastRefreshedTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvHT_DCPERIODMetaData, DateTime, AvPropertyNameAttribute, string>
                (AvHT_DCPERIODRes.MetaDataLastRefreshedTag, result,
                lastRefreshed,
                attr => attr.ExtractPropertyName);

            var interval = AvIntervalEnum.FromDisplayName<AvIntervalEnum>(
                metaData[AvHT_DCPERIODRes.MetaDataIntervalTag],
                StringComparison.InvariantCultureIgnoreCase);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvHT_DCPERIODMetaData, AvIntervalEnum, AvPropertyNameAttribute, string>
                (AvHT_DCPERIODRes.MetaDataIntervalTag, result,
                interval,
                attr => attr.ExtractPropertyName);

            var timeZone = AvTimeZoneConvertor.AvTimeZone(metaData[AvHT_DCPERIODRes.MetaDataTimeZoneTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvHT_DCPERIODMetaData, TimeZoneInfo, AvPropertyNameAttribute, string>
                (AvHT_DCPERIODRes.MetaDataTimeZoneTag, result,
                timeZone,
                attr => attr.ExtractPropertyName);

            var seriesType = AvSeriesTypeEnum.FromDisplayName<AvSeriesTypeEnum>(
                metaData[AvHT_DCPERIODRes.MetaDataSeriesTypeTag],
                StringComparison.InvariantCultureIgnoreCase);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvHT_DCPERIODMetaData, AvSeriesTypeEnum, AvPropertyNameAttribute, string>
                (AvHT_DCPERIODRes.MetaDataSeriesTypeTag, result,
                seriesType,
                attr => attr.ExtractPropertyName);

            return result;
        }

        protected override void ProcessDownloadResource(JObject remoteResource, string uri)
        {
            _metaData = remoteResource[AvHT_DCPERIODProcessRes.MetaDataTag].ToObject<Dictionary<string, string>>();
            _content = remoteResource[AvHT_DCPERIODProcessRes.TimeSeriesTag].ToObject<Dictionary<string, Dictionary<string, string>>>();
        }
    }
}
