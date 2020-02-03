using AlphaVantage.Common;
using AlphaVantage.Common.Models.TimeSeries.Monthly;
using AlphaVantage.Core.Interfaces;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace AlphaVantage.Core.TimeSeries.Monthly
{
    public class AvMonthlyTimeSeriesProcess : IMapResource<AvMonthlyTimeSeries>
    {
        private Dictionary<string, string> _metaData;
        private Dictionary<string, Dictionary<string, string>> _content;

        public AvMonthlyTimeSeriesProcess()
        {
        }

        public AvMonthlyTimeSeries Map(JObject remoteResource, string uri)
        {
            if (string.IsNullOrWhiteSpace(uri))
            {
                throw new ArgumentNullException(nameof(Map));
            }

            // download resource
            ProcessDownloadResource(remoteResource, uri);

            // map resource
            Data = MapToMonthlyTimeSeries(_metaData, _content);

            return Data;
        }

        public AvMonthlyTimeSeries Data { get; private set; }

        #region Helpers
        private void ProcessDownloadResource(JObject remoteResource, string uri)
        {
            _metaData = remoteResource[AvMonthlyTimeSeriesProcessRes.MetaDataTag].ToObject<Dictionary<string, string>>();
            _content = remoteResource[AvMonthlyTimeSeriesProcessRes.TimeSeriesTag].ToObject<Dictionary<string, Dictionary<string, string>>>();
        }

        private AvMonthlyTimeSeries MapToMonthlyTimeSeries(Dictionary<string, string> metaData,
            Dictionary<string, Dictionary<string, string>> timeSeries)
        {
            if (null == metaData || null == timeSeries)
            {
                throw new ArgumentNullException(nameof(MapToMonthlyTimeSeries));
            }

            return new AvMonthlyTimeSeries
            {
                MetaData = MapToMetaData(metaData),
                TimeSeries = MapToBlockHolder(timeSeries)
            };
        }

        private IList<AvMonthlyTimeSeriesBlock> MapToBlockHolder(Dictionary<string, Dictionary<string, string>> content)
        {
            var localBlocks = new List<AvMonthlyTimeSeriesBlock>();
            foreach (var row in content)
            {
                localBlocks.Add(MapToBlock(row.Value, row.Key));
            }

            return localBlocks;
        }

        private AvMonthlyTimeSeriesBlock MapToBlock(Dictionary<string, string> block, string dateTime)
        {
            var result = new AvMonthlyTimeSeriesBlock();

            var open = decimal.Parse(block[AvMonthlyTimeSeriesRes.TimeSeriesOpenTag]);
            var high = decimal.Parse(block[AvMonthlyTimeSeriesRes.TimeSeriesHighTag]);
            var low = decimal.Parse(block[AvMonthlyTimeSeriesRes.TimeSeriesLowTag]);
            var close = decimal.Parse(block[AvMonthlyTimeSeriesRes.TimeSeriesCloseTag]);
            ulong volume = ulong.Parse(block[AvMonthlyTimeSeriesRes.TimeSeriesVolumeTag]);

            var dateTimeStamp = DateTime.Parse(dateTime);

            // open
            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvMonthlyTimeSeriesBlock, decimal, AvPropertyNameAttribute, string>
                (AvMonthlyTimeSeriesRes.TimeSeriesOpenTag, result,
                open,
                attr => attr.ExtractPropertyName);

            // high
            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvMonthlyTimeSeriesBlock, decimal, AvPropertyNameAttribute, string>
                (AvMonthlyTimeSeriesRes.TimeSeriesHighTag, result,
                high,
                attr => attr.ExtractPropertyName);

            // low
            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvMonthlyTimeSeriesBlock, decimal, AvPropertyNameAttribute, string>
                (AvMonthlyTimeSeriesRes.TimeSeriesLowTag, result,
                low,
                attr => attr.ExtractPropertyName);

            // close
            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvMonthlyTimeSeriesBlock, decimal, AvPropertyNameAttribute, string>
                (AvMonthlyTimeSeriesRes.TimeSeriesCloseTag, result,
                close,
                attr => attr.ExtractPropertyName);

            // volume
            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvMonthlyTimeSeriesBlock, ulong, AvPropertyNameAttribute, string>
                (AvMonthlyTimeSeriesRes.TimeSeriesVolumeTag, result,
                volume,
                attr => attr.ExtractPropertyName);

            // date time
            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvMonthlyTimeSeriesBlock, DateTime, AvPropertyNameAttribute, string>
                (AvMonthlyTimeSeriesRes.TimeSeriesMonthlyTag, result,
                dateTimeStamp,
                attr => attr.ExtractPropertyName);

            return result;
        }

        private AvMonthlyTimeSeriesMetaData MapToMetaData(Dictionary<string, string> metaData)
        {
            var localMetaData = new AvMonthlyTimeSeriesMetaData();

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvMonthlyTimeSeriesMetaData, string, AvPropertyNameAttribute, string>
                (AvMonthlyTimeSeriesRes.MetaDataInformationTag, localMetaData,
                metaData[AvMonthlyTimeSeriesRes.MetaDataInformationTag],
                attr => attr.ExtractPropertyName);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvMonthlyTimeSeriesMetaData, string, AvPropertyNameAttribute, string>
                (AvMonthlyTimeSeriesRes.MetaDataSymbolTag, localMetaData,
                metaData[AvMonthlyTimeSeriesRes.MetaDataSymbolTag],
                attr => attr.ExtractPropertyName);

            var lastRefreshed = DateTime.Parse(metaData[AvMonthlyTimeSeriesRes.MetaDataLastRefreshedTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvMonthlyTimeSeriesMetaData, DateTime, AvPropertyNameAttribute, string>
                (AvMonthlyTimeSeriesRes.MetaDataLastRefreshedTag, localMetaData,
                lastRefreshed,
                attr => attr.ExtractPropertyName);

            var localTimeZone = AvTimeZoneConvertor.AvTimeZone(
                metaData[AvMonthlyTimeSeriesRes.MetaDataTimeZoneTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvMonthlyTimeSeriesMetaData, TimeZoneInfo, AvPropertyNameAttribute, string>
                (AvMonthlyTimeSeriesRes.MetaDataTimeZoneTag, localMetaData,
                localTimeZone,
                attr => attr.ExtractPropertyName);


            return localMetaData;
        }
        #endregion

    }
}
