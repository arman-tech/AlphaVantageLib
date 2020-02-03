using AlphaVantage.Common;
using AlphaVantage.Common.Models.TechnicalIndicators.PLUS_DM;
using AlphaVantage.Core.Abstracts;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace AlphaVantage.Core.TechnicalIndicators.PLUS_DM
{
    public class AvPLUS_DMProcess : AvMapResourceAbs<AvPLUS_DM, AvPLUS_DMMetaData, AvPLUS_DMBlock>
    {
        protected override AvPLUS_DMBlock MapToBlock(Dictionary<string, string> block, string dateTime)
        {
            var result = new AvPLUS_DMBlock();

            var data = decimal.Parse(block[AvPLUS_DMRes.BlockPLUS_DMTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvPLUS_DMBlock, decimal, AvPropertyNameAttribute, string>
                (AvPLUS_DMRes.BlockPLUS_DMTag, result, data, attr => attr.ExtractPropertyName);

            return result;
        }

        protected override AvPLUS_DMMetaData MapToMetaData(Dictionary<string, string> metaData)
        {
            var result = new AvPLUS_DMMetaData();

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvPLUS_DMMetaData, string, AvPropertyNameAttribute, string>
                (AvPLUS_DMRes.MetaDataSymbolTag, result, metaData[AvPLUS_DMRes.MetaDataSymbolTag],
                attr => attr.ExtractPropertyName);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvPLUS_DMMetaData, string, AvPropertyNameAttribute, string>
                (AvPLUS_DMRes.MetaDataIndicatorTag, result, metaData[AvPLUS_DMRes.MetaDataIndicatorTag],
                attr => attr.ExtractPropertyName);

            var lastRefreshed = DateTime.Parse(metaData[AvPLUS_DMRes.MetaDataLastRefreshedTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvPLUS_DMMetaData, DateTime, AvPropertyNameAttribute, string>
                (AvPLUS_DMRes.MetaDataLastRefreshedTag, result,
                lastRefreshed,
                attr => attr.ExtractPropertyName);

            var interval = AvIntervalEnum.FromDisplayName<AvIntervalEnum>(
                metaData[AvPLUS_DMRes.MetaDataIntervalTag],
                StringComparison.InvariantCultureIgnoreCase);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvPLUS_DMMetaData, AvIntervalEnum, AvPropertyNameAttribute, string>
                (AvPLUS_DMRes.MetaDataIntervalTag, result,
                interval,
                attr => attr.ExtractPropertyName);

            var timeZone = AvTimeZoneConvertor.AvTimeZone(metaData[AvPLUS_DMRes.MetaDataTimeZoneTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvPLUS_DMMetaData, TimeZoneInfo, AvPropertyNameAttribute, string>
                (AvPLUS_DMRes.MetaDataTimeZoneTag, result,
                timeZone,
                attr => attr.ExtractPropertyName);

            var timePeriod = int.Parse(metaData[AvPLUS_DMRes.MetaDataTimePeriodTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvPLUS_DMMetaData, int, AvPropertyNameAttribute, string>
                (AvPLUS_DMRes.MetaDataTimePeriodTag, result,
                timePeriod,
                attr => attr.ExtractPropertyName);

            return result;
        }

        protected override void ProcessDownloadResource(JObject remoteResource, string uri)
        {
            _metaData = remoteResource[AvPLUS_DMProcessRes.MetaDataTag].ToObject<Dictionary<string, string>>();
            _content = remoteResource[AvPLUS_DMProcessRes.TimeSeriesTag].ToObject<Dictionary<string, Dictionary<string, string>>>();
        }
    }
}
