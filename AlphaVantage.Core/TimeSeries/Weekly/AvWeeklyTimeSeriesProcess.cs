using AlphaVantage.Common;
using AlphaVantage.Common.Models.TimeSeries.Weekly;
using AlphaVantage.Core.Interfaces;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace AlphaVantage.Core.TimeSeries.Weekly
{
    public class AvWeeklyTimeSeriesProcess : IMapResource<AvWeeklyTimeSeries>
    {
        private Dictionary<string, string> _metaData;
        private Dictionary<string, Dictionary<string, string>> _content;

        public AvWeeklyTimeSeriesProcess()
        {
        }

        public AvWeeklyTimeSeries Map(JObject remoteResource, string uri)
        {
            if (string.IsNullOrWhiteSpace(uri))
            {
                throw new ArgumentNullException(nameof(Map));
            }

            // download resource
            ProcessDownloadResource(remoteResource, uri);

            // map resource
            Data = MapToWeeklyTimeSeries(_metaData, _content);

            return Data;
        }

        public AvWeeklyTimeSeries Data { get; private set; }

        #region Helpers

        private void ProcessDownloadResource(JObject remoteResource, string uri)
        {
            _metaData = remoteResource[AvWeeklyTimeSeriesProcessRes.MetaDataTag].ToObject<Dictionary<string, string>>();
            _content = remoteResource[AvWeeklyTimeSeriesProcessRes.TimeSeriesTag].ToObject<Dictionary<string, Dictionary<string, string>>>();
        }

        private AvWeeklyTimeSeries MapToWeeklyTimeSeries(Dictionary<string, string> metaData,
            Dictionary<string, Dictionary<string, string>> timeSeries)
        {
            if (null == metaData || null == timeSeries)
            {
                throw new ArgumentNullException(nameof(MapToWeeklyTimeSeries));
            }

            return new AvWeeklyTimeSeries
            {
                MetaData = MapToMetaData(metaData),
                TimeSeries = MapToBlockHolder(timeSeries)
            };
        }

        private IList<AvWeeklyTimeSeriesBlock> MapToBlockHolder(Dictionary<string, Dictionary<string, string>> content)
        {
            var localBlocks = new List<AvWeeklyTimeSeriesBlock>();
            foreach (var row in content)
            {
                localBlocks.Add(MapToBlock(row.Value, row.Key));
            }

            return localBlocks;
        }

        private AvWeeklyTimeSeriesBlock MapToBlock(Dictionary<string, string> block, string dateTime)
        {
            var result = new AvWeeklyTimeSeriesBlock();

            var open = decimal.Parse(block[AvWeeklyTimeSeriesRes.TimeSeriesOpenTag]);
            var high = decimal.Parse(block[AvWeeklyTimeSeriesRes.TimeSeriesHighTag]);
            var low = decimal.Parse(block[AvWeeklyTimeSeriesRes.TimeSeriesLowTag]);
            var close = decimal.Parse(block[AvWeeklyTimeSeriesRes.TimeSeriesCloseTag]);
            ulong volume = ulong.Parse(block[AvWeeklyTimeSeriesRes.TimeSeriesVolumeTag]);

            var dateTimeStamp = DateTime.Parse(dateTime);

            // open
            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvWeeklyTimeSeriesBlock, decimal, AvPropertyNameAttribute, string>
                (AvWeeklyTimeSeriesRes.TimeSeriesOpenTag, result,
                open,
                attr => attr.ExtractPropertyName);

            // high
            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvWeeklyTimeSeriesBlock, decimal, AvPropertyNameAttribute, string>
                (AvWeeklyTimeSeriesRes.TimeSeriesHighTag, result,
                high,
                attr => attr.ExtractPropertyName);

            // low
            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvWeeklyTimeSeriesBlock, decimal, AvPropertyNameAttribute, string>
                (AvWeeklyTimeSeriesRes.TimeSeriesLowTag, result,
                low,
                attr => attr.ExtractPropertyName);

            // close
            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvWeeklyTimeSeriesBlock, decimal, AvPropertyNameAttribute, string>
                (AvWeeklyTimeSeriesRes.TimeSeriesCloseTag, result,
                close,
                attr => attr.ExtractPropertyName);

            // volume
            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvWeeklyTimeSeriesBlock, ulong, AvPropertyNameAttribute, string>
                (AvWeeklyTimeSeriesRes.TimeSeriesVolumeTag, result,
                volume,
                attr => attr.ExtractPropertyName);

            // date time
            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvWeeklyTimeSeriesBlock, DateTime, AvPropertyNameAttribute, string>
                (AvWeeklyTimeSeriesRes.TimeSeriesWeeklyTag, result,
                dateTimeStamp,
                attr => attr.ExtractPropertyName);

            return result;
        }

        private AvWeeklyTimeSeriesMetaData MapToMetaData(Dictionary<string, string> metaData)
        {
            var localMetaData = new AvWeeklyTimeSeriesMetaData();

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvWeeklyTimeSeriesMetaData, string, AvPropertyNameAttribute, string>
                (AvWeeklyTimeSeriesRes.MetaDataInformationTag, localMetaData,
                metaData[AvWeeklyTimeSeriesRes.MetaDataInformationTag],
                attr => attr.ExtractPropertyName);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvWeeklyTimeSeriesMetaData, string, AvPropertyNameAttribute, string>
                (AvWeeklyTimeSeriesRes.MetaDataSymbolTag, localMetaData,
                metaData[AvWeeklyTimeSeriesRes.MetaDataSymbolTag],
                attr => attr.ExtractPropertyName);

            var lastRefreshed = DateTime.Parse(metaData[AvWeeklyTimeSeriesRes.MetaDataLastRefreshedTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvWeeklyTimeSeriesMetaData, DateTime, AvPropertyNameAttribute, string>
                (AvWeeklyTimeSeriesRes.MetaDataLastRefreshedTag, localMetaData,
                lastRefreshed,
                attr => attr.ExtractPropertyName);

            var localTimeZone = AvTimeZoneConvertor.AvTimeZone(
                metaData[AvWeeklyTimeSeriesRes.MetaDataTimeZoneTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvWeeklyTimeSeriesMetaData, TimeZoneInfo, AvPropertyNameAttribute, string>
                (AvWeeklyTimeSeriesRes.MetaDataTimeZoneTag, localMetaData,
                localTimeZone,
                attr => attr.ExtractPropertyName);


            return localMetaData;
        }
        #endregion
    }
}
