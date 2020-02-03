using AlphaVantage.Common;
using AlphaVantage.Common.Models.TechnicalIndicators.ATR;
using AlphaVantage.Core.Abstracts;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace AlphaVantage.Core.TechnicalIndicators.ATR
{
    public class AvATRProcess : AvMapResourceAbs<AvATR, AvATRMetaData, AvATRBlock>
    {
        protected override AvATRBlock MapToBlock(Dictionary<string, string> block, string dateTime)
        {
            var result = new AvATRBlock();

            var data = decimal.Parse(block[AvATRRes.BlockATRTag]);

            // chaikain
            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvATRBlock, decimal, AvPropertyNameAttribute, string>
                (AvATRRes.BlockATRTag, result, data, attr => attr.ExtractPropertyName);

            return result;
        }

        protected override AvATRMetaData MapToMetaData(Dictionary<string, string> metaData)
        {
            var result = new AvATRMetaData();

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvATRMetaData, string, AvPropertyNameAttribute, string>
                (AvATRRes.MetaDataSymbolTag, result, metaData[AvATRRes.MetaDataSymbolTag],
                attr => attr.ExtractPropertyName);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvATRMetaData, string, AvPropertyNameAttribute, string>
                (AvATRRes.MetaDataIndicatorTag, result, metaData[AvATRRes.MetaDataIndicatorTag],
                attr => attr.ExtractPropertyName);

            var lastRefreshed = DateTime.Parse(metaData[AvATRRes.MetaDataLastRefreshedTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvATRMetaData, DateTime, AvPropertyNameAttribute, string>
                (AvATRRes.MetaDataLastRefreshedTag, result,
                lastRefreshed,
                attr => attr.ExtractPropertyName);

            var interval = AvIntervalEnum.FromDisplayName<AvIntervalEnum>(
                metaData[AvATRRes.MetaDataIntervalTag],
                StringComparison.InvariantCultureIgnoreCase);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvATRMetaData, AvIntervalEnum, AvPropertyNameAttribute, string>
                (AvATRRes.MetaDataIntervalTag, result,
                interval,
                attr => attr.ExtractPropertyName);

            var timeZone = AvTimeZoneConvertor.AvTimeZone(metaData[AvATRRes.MetaDataTimeZoneTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvATRMetaData, TimeZoneInfo, AvPropertyNameAttribute, string>
                (AvATRRes.MetaDataTimeZoneTag, result,
                timeZone,
                attr => attr.ExtractPropertyName);

            var timePeriod = int.Parse(metaData[AvATRRes.MetaDataTimePeriodTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvATRMetaData, int, AvPropertyNameAttribute, string>
                (AvATRRes.MetaDataTimePeriodTag, result,
                timePeriod,
                attr => attr.ExtractPropertyName);


            return result;
        }

        protected override void ProcessDownloadResource(JObject remoteResource, string uri)
        {
            _metaData = remoteResource[AvATRProcessRes.MetaDataTag].ToObject<Dictionary<string, string>>();
            _content = remoteResource[AvATRProcessRes.TimeSeriesTag].ToObject<Dictionary<string, Dictionary<string, string>>>();
        }
    }
}
