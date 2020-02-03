using AlphaVantage.Common;
using AlphaVantage.Common.Models.TechnicalIndicators.TRANGE;
using AlphaVantage.Core.Abstracts;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace AlphaVantage.Core.TechnicalIndicators.TRANGE
{
    public class AvTRANGEProcess : AvMapResourceAbs<AvTRANGE, AvTRANGEMetaData, AvTRANGEBlock>
    {
        protected override AvTRANGEBlock MapToBlock(Dictionary<string, string> block, string dateTime)
        {
            var result = new AvTRANGEBlock();

            var data = decimal.Parse(block[AvTRANGERes.BlockTRANGETag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvTRANGEBlock, decimal, AvPropertyNameAttribute, string>
                (AvTRANGERes.BlockTRANGETag, result, data, attr => attr.ExtractPropertyName);

            return result;
        }

        protected override AvTRANGEMetaData MapToMetaData(Dictionary<string, string> metaData)
        {
            var result = new AvTRANGEMetaData();

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvTRANGEMetaData, string, AvPropertyNameAttribute, string>
                (AvTRANGERes.MetaDataSymbolTag, result, metaData[AvTRANGERes.MetaDataSymbolTag],
                attr => attr.ExtractPropertyName);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvTRANGEMetaData, string, AvPropertyNameAttribute, string>
                (AvTRANGERes.MetaDataIndicatorTag, result, metaData[AvTRANGERes.MetaDataIndicatorTag],
                attr => attr.ExtractPropertyName);

            var lastRefreshed = DateTime.Parse(metaData[AvTRANGERes.MetaDataLastRefreshedTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvTRANGEMetaData, DateTime, AvPropertyNameAttribute, string>
                (AvTRANGERes.MetaDataLastRefreshedTag, result,
                lastRefreshed,
                attr => attr.ExtractPropertyName);

            var interval = AvIntervalEnum.FromDisplayName<AvIntervalEnum>(
                metaData[AvTRANGERes.MetaDataIntervalTag],
                StringComparison.InvariantCultureIgnoreCase);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvTRANGEMetaData, AvIntervalEnum, AvPropertyNameAttribute, string>
                (AvTRANGERes.MetaDataIntervalTag, result,
                interval,
                attr => attr.ExtractPropertyName);

            var timeZone = AvTimeZoneConvertor.AvTimeZone(metaData[AvTRANGERes.MetaDataTimeZoneTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvTRANGEMetaData, TimeZoneInfo, AvPropertyNameAttribute, string>
                (AvTRANGERes.MetaDataTimeZoneTag, result,
                timeZone,
                attr => attr.ExtractPropertyName);


            return result;
        }

        protected override void ProcessDownloadResource(JObject remoteResource, string uri)
        {
            _metaData = remoteResource[AvTRANGEProcessRes.MetaDataTag].ToObject<Dictionary<string, string>>();
            _content = remoteResource[AvTRANGEProcessRes.TimeSeriesTag].ToObject<Dictionary<string, Dictionary<string, string>>>();
        }
    }
}
