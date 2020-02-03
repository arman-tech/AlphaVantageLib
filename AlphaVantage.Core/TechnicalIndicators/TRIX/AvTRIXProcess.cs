using AlphaVantage.Common;
using AlphaVantage.Common.Models.TechnicalIndicators.TRIX;
using AlphaVantage.Core.Abstracts;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace AlphaVantage.Core.TechnicalIndicators.TRIX
{
    public class AvTRIXProcess : AvMapResourceAbs<AvTRIX, AvTRIXMetaData, AvTRIXBlock>
    {
        protected override AvTRIXBlock MapToBlock(Dictionary<string, string> block, string dateTime)
        {
            var result = new AvTRIXBlock();

            var data = decimal.Parse(block[AvTRIXRes.BlockTRIXTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvTRIXBlock, decimal, AvPropertyNameAttribute, string>
                (AvTRIXRes.BlockTRIXTag, result, data, attr => attr.ExtractPropertyName);

            return result;
        }

        protected override AvTRIXMetaData MapToMetaData(Dictionary<string, string> metaData)
        {
            var result = new AvTRIXMetaData();

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvTRIXMetaData, string, AvPropertyNameAttribute, string>
                (AvTRIXRes.MetaDataSymbolTag, result, metaData[AvTRIXRes.MetaDataSymbolTag],
                attr => attr.ExtractPropertyName);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvTRIXMetaData, string, AvPropertyNameAttribute, string>
                (AvTRIXRes.MetaDataIndicatorTag, result, metaData[AvTRIXRes.MetaDataIndicatorTag],
                attr => attr.ExtractPropertyName);

            var lastRefreshed = DateTime.Parse(metaData[AvTRIXRes.MetaDataLastRefreshedTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvTRIXMetaData, DateTime, AvPropertyNameAttribute, string>
                (AvTRIXRes.MetaDataLastRefreshedTag, result,
                lastRefreshed,
                attr => attr.ExtractPropertyName);

            var interval = AvIntervalEnum.FromDisplayName<AvIntervalEnum>(
                metaData[AvTRIXRes.MetaDataIntervalTag],
                StringComparison.InvariantCultureIgnoreCase);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvTRIXMetaData, AvIntervalEnum, AvPropertyNameAttribute, string>
                (AvTRIXRes.MetaDataIntervalTag, result,
                interval,
                attr => attr.ExtractPropertyName);

            var timeZone = AvTimeZoneConvertor.AvTimeZone(metaData[AvTRIXRes.MetaDataTimeZoneTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvTRIXMetaData, TimeZoneInfo, AvPropertyNameAttribute, string>
                (AvTRIXRes.MetaDataTimeZoneTag, result,
                timeZone,
                attr => attr.ExtractPropertyName);

            var timePeriod = int.Parse(metaData[AvTRIXRes.MetaDataTimePeriodTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvTRIXMetaData, int, AvPropertyNameAttribute, string>
                (AvTRIXRes.MetaDataTimePeriodTag, result,
                timePeriod,
                attr => attr.ExtractPropertyName);

            var seriesType = AvSeriesTypeEnum.FromDisplayName<AvSeriesTypeEnum>(
                metaData[AvTRIXRes.MetaDataSeriesTypeTag],
                StringComparison.InvariantCultureIgnoreCase);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvTRIXMetaData, AvSeriesTypeEnum, AvPropertyNameAttribute, string>
                (AvTRIXRes.MetaDataSeriesTypeTag, result,
                seriesType,
                attr => attr.ExtractPropertyName);

            return result;
        }

        protected override void ProcessDownloadResource(JObject remoteResource, string uri)
        {
            _metaData = remoteResource[AvTRIXProcessRes.MetaDataTag].ToObject<Dictionary<string, string>>();
            _content = remoteResource[AvTRIXProcessRes.TimeSeriesTag].ToObject<Dictionary<string, Dictionary<string, string>>>();
        }
    }
}
