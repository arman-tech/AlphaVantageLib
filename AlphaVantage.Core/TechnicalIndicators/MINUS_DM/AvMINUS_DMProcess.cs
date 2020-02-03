using AlphaVantage.Common;
using AlphaVantage.Common.Models.TechnicalIndicators.MINUS_DM;
using AlphaVantage.Core.Abstracts;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace AlphaVantage.Core.TechnicalIndicators.MINUS_DM
{
    public class AvMINUS_DMProcess : AvMapResourceAbs<AvMINUS_DM, AvMINUS_DMMetaData, AvMINUS_DMBlock>
    {
        protected override AvMINUS_DMBlock MapToBlock(Dictionary<string, string> block, string dateTime)
        {
            var result = new AvMINUS_DMBlock();

            var data = decimal.Parse(block[AvMINUS_DMRes.BlockMINUS_DMTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvMINUS_DMBlock, decimal, AvPropertyNameAttribute, string>
                (AvMINUS_DMRes.BlockMINUS_DMTag, result, data, attr => attr.ExtractPropertyName);

            return result;
        }

        protected override AvMINUS_DMMetaData MapToMetaData(Dictionary<string, string> metaData)
        {
            var result = new AvMINUS_DMMetaData();

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvMINUS_DMMetaData, string, AvPropertyNameAttribute, string>
                (AvMINUS_DMRes.MetaDataSymbolTag, result, metaData[AvMINUS_DMRes.MetaDataSymbolTag],
                attr => attr.ExtractPropertyName);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvMINUS_DMMetaData, string, AvPropertyNameAttribute, string>
                (AvMINUS_DMRes.MetaDataIndicatorTag, result, metaData[AvMINUS_DMRes.MetaDataIndicatorTag],
                attr => attr.ExtractPropertyName);

            var lastRefreshed = DateTime.Parse(metaData[AvMINUS_DMRes.MetaDataLastRefreshedTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvMINUS_DMMetaData, DateTime, AvPropertyNameAttribute, string>
                (AvMINUS_DMRes.MetaDataLastRefreshedTag, result,
                lastRefreshed,
                attr => attr.ExtractPropertyName);

            var interval = AvIntervalEnum.FromDisplayName<AvIntervalEnum>(
                metaData[AvMINUS_DMRes.MetaDataIntervalTag],
                StringComparison.InvariantCultureIgnoreCase);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvMINUS_DMMetaData, AvIntervalEnum, AvPropertyNameAttribute, string>
                (AvMINUS_DMRes.MetaDataIntervalTag, result,
                interval,
                attr => attr.ExtractPropertyName);

            var timeZone = AvTimeZoneConvertor.AvTimeZone(metaData[AvMINUS_DMRes.MetaDataTimeZoneTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvMINUS_DMMetaData, TimeZoneInfo, AvPropertyNameAttribute, string>
                (AvMINUS_DMRes.MetaDataTimeZoneTag, result,
                timeZone,
                attr => attr.ExtractPropertyName);

            var timePeriod = int.Parse(metaData[AvMINUS_DMRes.MetaDataTimePeriodTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvMINUS_DMMetaData, int, AvPropertyNameAttribute, string>
                (AvMINUS_DMRes.MetaDataTimePeriodTag, result,
                timePeriod,
                attr => attr.ExtractPropertyName);


            return result;
        }

        protected override void ProcessDownloadResource(JObject remoteResource, string uri)
        {
            _metaData = remoteResource[AvMINUS_DMProcessRes.MetaDataTag].ToObject<Dictionary<string, string>>();
            _content = remoteResource[AvMINUS_DMProcessRes.TimeSeriesTag].ToObject<Dictionary<string, Dictionary<string, string>>>();
        }
    }
}
