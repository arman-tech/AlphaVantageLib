using AlphaVantage.Common;
using AlphaVantage.Common.Models.TechnicalIndicators.MIDPOINT;
using AlphaVantage.Core.Abstracts;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace AlphaVantage.Core.TechnicalIndicators.MIDPOINT
{
    public class AvMIDPOINTProcess : AvMapResourceAbs<AvMIDPOINT, AvMIDPOINTMetaData, AvMIDPOINTBlock>
    {
        protected override AvMIDPOINTBlock MapToBlock(Dictionary<string, string> block, string dateTime)
        {
            var result = new AvMIDPOINTBlock();

            var data = decimal.Parse(block[AvMIDPOINTRes.BlockMIDPOINTTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvMIDPOINTBlock, decimal, AvPropertyNameAttribute, string>
                (AvMIDPOINTRes.BlockMIDPOINTTag, result, data, attr => attr.ExtractPropertyName);

            return result;
        }

        protected override AvMIDPOINTMetaData MapToMetaData(Dictionary<string, string> metaData)
        {
            var result = new AvMIDPOINTMetaData();

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvMIDPOINTMetaData, string, AvPropertyNameAttribute, string>
                (AvMIDPOINTRes.MetaDataSymbolTag, result, metaData[AvMIDPOINTRes.MetaDataSymbolTag],
                attr => attr.ExtractPropertyName);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvMIDPOINTMetaData, string, AvPropertyNameAttribute, string>
                (AvMIDPOINTRes.MetaDataIndicatorTag, result, metaData[AvMIDPOINTRes.MetaDataIndicatorTag],
                attr => attr.ExtractPropertyName);

            var lastRefreshed = DateTime.Parse(metaData[AvMIDPOINTRes.MetaDataLastRefreshedTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvMIDPOINTMetaData, DateTime, AvPropertyNameAttribute, string>
                (AvMIDPOINTRes.MetaDataLastRefreshedTag, result,
                lastRefreshed,
                attr => attr.ExtractPropertyName);

            var interval = AvIntervalEnum.FromDisplayName<AvIntervalEnum>(
                metaData[AvMIDPOINTRes.MetaDataIntervalTag],
                StringComparison.InvariantCultureIgnoreCase);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvMIDPOINTMetaData, AvIntervalEnum, AvPropertyNameAttribute, string>
                (AvMIDPOINTRes.MetaDataIntervalTag, result,
                interval,
                attr => attr.ExtractPropertyName);

            var timeZone = AvTimeZoneConvertor.AvTimeZone(metaData[AvMIDPOINTRes.MetaDataTimeZoneTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvMIDPOINTMetaData, TimeZoneInfo, AvPropertyNameAttribute, string>
                (AvMIDPOINTRes.MetaDataTimeZoneTag, result,
                timeZone,
                attr => attr.ExtractPropertyName);

            var timePeriod = int.Parse(metaData[AvMIDPOINTRes.MetaDataTimePeriodTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvMIDPOINTMetaData, int, AvPropertyNameAttribute, string>
                (AvMIDPOINTRes.MetaDataTimePeriodTag, result,
                timePeriod,
                attr => attr.ExtractPropertyName);

            var seriesType = AvSeriesTypeEnum.FromDisplayName<AvSeriesTypeEnum>(
                metaData[AvMIDPOINTRes.MetaDataSeriesTypeTag],
                StringComparison.InvariantCultureIgnoreCase);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvMIDPOINTMetaData, AvSeriesTypeEnum, AvPropertyNameAttribute, string>
                (AvMIDPOINTRes.MetaDataSeriesTypeTag, result,
                seriesType,
                attr => attr.ExtractPropertyName);

            return result;
        }

        protected override void ProcessDownloadResource(JObject remoteResource, string uri)
        {
            _metaData = remoteResource[AvMIDPOINTProcessRes.MetaDataTag].ToObject<Dictionary<string, string>>();
            _content = remoteResource[AvMIDPOINTProcessRes.TimeSeriesTag].ToObject<Dictionary<string, Dictionary<string, string>>>();
        }
    }
}
