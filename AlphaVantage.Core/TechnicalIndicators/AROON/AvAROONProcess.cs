using AlphaVantage.Common;
using AlphaVantage.Common.Models.TechnicalIndicators.AROON;
using AlphaVantage.Core.Abstracts;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace AlphaVantage.Core.TechnicalIndicators.AROON
{
    public class AvAROONProcess : AvMapResourceAbs<AvAROON, AvAROONMetaData, AvAROONBlock>
    {
        protected override AvAROONBlock MapToBlock(Dictionary<string, string> block, string dateTime)
        {
            var result = new AvAROONBlock();

            var down = decimal.Parse(block[AvAROONRes.BlockAroonDownTag]);
            var up = decimal.Parse(block[AvAROONRes.BlockAroonDownTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvAROONBlock, decimal, AvPropertyNameAttribute, string>
                (AvAROONRes.BlockAroonDownTag, result, down, attr => attr.ExtractPropertyName);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvAROONBlock, decimal, AvPropertyNameAttribute, string>
                (AvAROONRes.BlockAroonUpTag, result, up, attr => attr.ExtractPropertyName);

            return result;
        }

        protected override AvAROONMetaData MapToMetaData(Dictionary<string, string> metaData)
        {
            var result = new AvAROONMetaData();

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvAROONMetaData, string, AvPropertyNameAttribute, string>
                (AvAROONRes.MetaDataSymbolTag, result, metaData[AvAROONRes.MetaDataSymbolTag],
                attr => attr.ExtractPropertyName);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvAROONMetaData, string, AvPropertyNameAttribute, string>
                (AvAROONRes.MetaDataIndicatorTag, result, metaData[AvAROONRes.MetaDataIndicatorTag],
                attr => attr.ExtractPropertyName);

            var lastRefreshed = DateTime.Parse(metaData[AvAROONRes.MetaDataLastRefreshedTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvAROONMetaData, DateTime, AvPropertyNameAttribute, string>
                (AvAROONRes.MetaDataLastRefreshedTag, result,
                lastRefreshed,
                attr => attr.ExtractPropertyName);

            var interval = AvIntervalEnum.FromDisplayName<AvIntervalEnum>(
                metaData[AvAROONRes.MetaDataIntervalTag],
                StringComparison.InvariantCultureIgnoreCase);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvAROONMetaData, AvIntervalEnum, AvPropertyNameAttribute, string>
                (AvAROONRes.MetaDataIntervalTag, result,
                interval,
                attr => attr.ExtractPropertyName);

            var timeZone = AvTimeZoneConvertor.AvTimeZone(metaData[AvAROONRes.MetaDataTimeZoneTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvAROONMetaData, TimeZoneInfo, AvPropertyNameAttribute, string>
                (AvAROONRes.MetaDataTimeZoneTag, result,
                timeZone,
                attr => attr.ExtractPropertyName);

            var timePeriod = int.Parse(metaData[AvAROONRes.MetaDataTimePeriodTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvAROONMetaData, int, AvPropertyNameAttribute, string>
                (AvAROONRes.MetaDataTimePeriodTag, result,
                timePeriod,
                attr => attr.ExtractPropertyName);


            return result;
        }

        protected override void ProcessDownloadResource(JObject remoteResource, string uri)
        {
            _metaData = remoteResource[AvAROONProcessRes.MetaDataTag].ToObject<Dictionary<string, string>>();
            _content = remoteResource[AvAROONProcessRes.TimeSeriesTag].ToObject<Dictionary<string, Dictionary<string, string>>>();
        }
    }
}
