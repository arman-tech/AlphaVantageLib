using AlphaVantage.Common;
using AlphaVantage.Common.Models.TechnicalIndicators.MOM;
using AlphaVantage.Core.Abstracts;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace AlphaVantage.Core.TechnicalIndicators.MOM
{
    public class AvMOMProcess : AvMapResourceAbs<AvMOM, AvMOMMetaData, AvMOMBlock>
    {
        protected override AvMOMBlock MapToBlock(Dictionary<string, string> block, string dateTime)
        {
            var result = new AvMOMBlock();

            var data = decimal.Parse(block[AvMOMRes.BlockMOMTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvMOMBlock, decimal, AvPropertyNameAttribute, string>
                (AvMOMRes.BlockMOMTag, result, data, attr => attr.ExtractPropertyName);

            return result;
        }

        protected override AvMOMMetaData MapToMetaData(Dictionary<string, string> metaData)
        {
            var result = new AvMOMMetaData();

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvMOMMetaData, string, AvPropertyNameAttribute, string>
                (AvMOMRes.MetaDataSymbolTag, result, metaData[AvMOMRes.MetaDataSymbolTag],
                attr => attr.ExtractPropertyName);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvMOMMetaData, string, AvPropertyNameAttribute, string>
                (AvMOMRes.MetaDataIndicatorTag, result, metaData[AvMOMRes.MetaDataIndicatorTag],
                attr => attr.ExtractPropertyName);

            var lastRefreshed = DateTime.Parse(metaData[AvMOMRes.MetaDataLastRefreshedTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvMOMMetaData, DateTime, AvPropertyNameAttribute, string>
                (AvMOMRes.MetaDataLastRefreshedTag, result,
                lastRefreshed,
                attr => attr.ExtractPropertyName);

            var interval = AvIntervalEnum.FromDisplayName<AvIntervalEnum>(
                metaData[AvMOMRes.MetaDataIntervalTag],
                StringComparison.InvariantCultureIgnoreCase);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvMOMMetaData, AvIntervalEnum, AvPropertyNameAttribute, string>
                (AvMOMRes.MetaDataIntervalTag, result,
                interval,
                attr => attr.ExtractPropertyName);

            var timeZone = AvTimeZoneConvertor.AvTimeZone(metaData[AvMOMRes.MetaDataTimeZoneTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvMOMMetaData, TimeZoneInfo, AvPropertyNameAttribute, string>
                (AvMOMRes.MetaDataTimeZoneTag, result,
                timeZone,
                attr => attr.ExtractPropertyName);

            var timePeriod = int.Parse(metaData[AvMOMRes.MetaDataTimePeriodTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvMOMMetaData, int, AvPropertyNameAttribute, string>
                (AvMOMRes.MetaDataTimePeriodTag, result,
                timePeriod,
                attr => attr.ExtractPropertyName);

            var seriesType = AvSeriesTypeEnum.FromDisplayName<AvSeriesTypeEnum>(
                metaData[AvMOMRes.MetaDataSeriesTypeTag],
                StringComparison.InvariantCultureIgnoreCase);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvMOMMetaData, AvSeriesTypeEnum, AvPropertyNameAttribute, string>
                (AvMOMRes.MetaDataSeriesTypeTag, result,
                seriesType,
                attr => attr.ExtractPropertyName);

            return result;
        }

        protected override void ProcessDownloadResource(JObject remoteResource, string uri)
        {
            _metaData = remoteResource[AvMOMProcessRes.MetaDataTag].ToObject<Dictionary<string, string>>();
            _content = remoteResource[AvMOMProcessRes.TimeSeriesTag].ToObject<Dictionary<string, Dictionary<string, string>>>();
        }
    }
}
