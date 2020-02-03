
using AlphaVantage.Common;
using AlphaVantage.Common.Models.TimeSeries.Daily;
using AlphaVantage.Core.Interfaces;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace AlphaVantage.Core.TimeSeries.Daily
{
    public class AvDailyTimeSeriesProcess : IMapResource<AvDailyTimeSeries>
    {
        private Dictionary<string, string> _metaData;
        private Dictionary<string, Dictionary<string, string>> _content;


        public AvDailyTimeSeriesProcess()
        {
        }

        public AvDailyTimeSeries Map(JObject remoteResource, string uri)
        {
            if(string.IsNullOrWhiteSpace(uri))
            {
                throw new ArgumentNullException(nameof(Map));
            }

            // download resource
            ProcessDownloadResource(remoteResource, uri);

            // map resource
            Data = MapToDailyTimeSeries(_metaData, _content);

            //Save(_dailyTimeSeriesObj);
            return Data;
        }

        public AvDailyTimeSeries Data { get; private set; }

        #region Helpers

        private void ProcessDownloadResource(JObject remoteResource, string uri)
        {

            _metaData = remoteResource[AvDailyTimeSeriesProcessRes.MetaDataTag].ToObject<Dictionary<string, string>>();
            _content = remoteResource[AvDailyTimeSeriesProcessRes.TimeSeriesTag].ToObject<Dictionary<string, Dictionary<string, string>>>();

        }

        private AvDailyTimeSeries MapToDailyTimeSeries(Dictionary<string, string> metaData,
            Dictionary<string, Dictionary<string, string>> timeSeries)
        {
            if (null == metaData || null == timeSeries)
            {
                throw new ArgumentNullException(nameof(MapToDailyTimeSeries));
            }

            return new AvDailyTimeSeries
            {
                MetaData = MapToMetaData(metaData),
                TimeSeries = MapToBlockHolder(timeSeries)
            };
        }

        private IList<AvDailyTimeSeriesBlock> MapToBlockHolder(Dictionary<string, Dictionary<string, string>> content)
        {
            var localBlocks = new List<AvDailyTimeSeriesBlock>();
            foreach (var row in content)
            {
                localBlocks.Add(MapToBlock(row.Value, row.Key));
            }

            return localBlocks;
        }

        private AvDailyTimeSeriesBlock MapToBlock(Dictionary<string, string> block, string dateTime)
        {
            var result = new AvDailyTimeSeriesBlock();

            var open = decimal.Parse(block[AvDailyTimeSeriesRes.TimeSeriesOpenTag]);
            var high = decimal.Parse(block[AvDailyTimeSeriesRes.TimeSeriesHighTag]);
            var low = decimal.Parse(block[AvDailyTimeSeriesRes.TimeSeriesLowTag]);
            var close = decimal.Parse(block[AvDailyTimeSeriesRes.TimeSeriesCloseTag]);
            ulong volume = ulong.Parse(block[AvDailyTimeSeriesRes.TimeSeriesVolumeTag]);

            var dateTimeStamp = DateTime.Parse(dateTime);

            // open
            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvDailyTimeSeriesBlock, decimal, AvPropertyNameAttribute, string>
                (AvDailyTimeSeriesRes.TimeSeriesOpenTag, result,
                open,
                attr => attr.ExtractPropertyName);

            // high
            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvDailyTimeSeriesBlock, decimal, AvPropertyNameAttribute, string>
                (AvDailyTimeSeriesRes.TimeSeriesHighTag, result,
                high,
                attr => attr.ExtractPropertyName);

            // low
            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvDailyTimeSeriesBlock, decimal, AvPropertyNameAttribute, string>
                (AvDailyTimeSeriesRes.TimeSeriesLowTag, result,
                low,
                attr => attr.ExtractPropertyName);

            // close
            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvDailyTimeSeriesBlock, decimal, AvPropertyNameAttribute, string>
                (AvDailyTimeSeriesRes.TimeSeriesCloseTag, result,
                close,
                attr => attr.ExtractPropertyName);

            // volume
            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvDailyTimeSeriesBlock, ulong, AvPropertyNameAttribute, string>
                (AvDailyTimeSeriesRes.TimeSeriesVolumeTag, result,
                volume,
                attr => attr.ExtractPropertyName);

            // date time
            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvDailyTimeSeriesBlock, DateTime, AvPropertyNameAttribute, string>
                (AvDailyTimeSeriesRes.TimeSeriesDayTag, result,
                dateTimeStamp,
                attr => attr.ExtractPropertyName);

            return result;
        }

        private AvDailyTimeSeriesMetaData MapToMetaData(Dictionary<string, string> metaData)
        {
            var localMetaData = new AvDailyTimeSeriesMetaData();

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvDailyTimeSeriesMetaData, string, AvPropertyNameAttribute, string>
                (AvDailyTimeSeriesRes.MetaDataInformationTag, localMetaData,
                metaData[AvDailyTimeSeriesRes.MetaDataInformationTag],
                attr => attr.ExtractPropertyName);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvDailyTimeSeriesMetaData, string, AvPropertyNameAttribute, string>
                (AvDailyTimeSeriesRes.MetaDataSymbolTag, localMetaData,
                metaData[AvDailyTimeSeriesRes.MetaDataSymbolTag],
                attr => attr.ExtractPropertyName);

            var lastRefreshed = DateTime.Parse(metaData[AvDailyTimeSeriesRes.MetaDataLastRefreshedTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvDailyTimeSeriesMetaData, DateTime, AvPropertyNameAttribute, string>
                (AvDailyTimeSeriesRes.MetaDataLastRefreshedTag, localMetaData,
                lastRefreshed,
                attr => attr.ExtractPropertyName);

            var outputSize = AvOutputSizeEnum.FromDisplayName<AvOutputSizeEnum>(
                metaData[AvDailyTimeSeriesRes.MetaDataOutputSizeTag],
                StringComparison.InvariantCultureIgnoreCase);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvDailyTimeSeriesMetaData, AvOutputSizeEnum, AvPropertyNameAttribute, string>
                (AvDailyTimeSeriesRes.MetaDataOutputSizeTag, localMetaData,
                outputSize,
                attr => attr.ExtractPropertyName);

            var localTimeZone = AvTimeZoneConvertor.AvTimeZone(
                metaData[AvDailyTimeSeriesRes.MetaDataTimeZoneTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvDailyTimeSeriesMetaData, TimeZoneInfo, AvPropertyNameAttribute, string>
                (AvDailyTimeSeriesRes.MetaDataTimeZoneTag, localMetaData,
                localTimeZone,
                attr => attr.ExtractPropertyName);


            return localMetaData;
        }
        #endregion
    }
}
