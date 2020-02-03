using AlphaVantage.Common;
using AlphaVantage.Core.Interfaces;
using AlphaVantage.Common.Models.TimeSeries.DailyAdjusted;
using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace AlphaVantage.Core.TimeSeries.DailyAdjusted
{
    public class AvDailyAdjTimeSeriesProcess : IMapResource<AvDailyAdjTimeSeries>
    {
        private Dictionary<string, string> _metaData;
        private Dictionary<string, Dictionary<string, string>> _content;

        public AvDailyAdjTimeSeriesProcess()
        {
        }

        public AvDailyAdjTimeSeries Map(JObject remoteResource, string uri)
        {
            // sanity check
            if (string.IsNullOrWhiteSpace(uri))
            {
                throw new ArgumentNullException(nameof(Map));
            }

            // download resource
            ProcessDownloadResource(remoteResource, uri);

            // map resource
            Data = MapToDailyAdjTimeSeries(_metaData, _content);

            //Save(_dailyAdjTimeSeriesObj);
            return Data;
        }

        public AvDailyAdjTimeSeries Data { get; private set; }

        #region Helpers
        private void ProcessDownloadResource(JObject remoteResource, string uri)
        {

            _metaData = remoteResource[AvDailyAdjTimeSeriesProcessRes.MetaDataTag].ToObject<Dictionary<string, string>>();
            _content = remoteResource[AvDailyAdjTimeSeriesProcessRes.TimeSeriesTag].ToObject<Dictionary<string, Dictionary<string, string>>>();

        }

        private AvDailyAdjTimeSeries MapToDailyAdjTimeSeries(Dictionary<string, string> metaData, 
            Dictionary<string, Dictionary<string, string>> timeSeries)
        {
            if (null == metaData || null == timeSeries)
            {
                throw new ArgumentNullException(nameof(MapToDailyAdjTimeSeries));
            }

            return new AvDailyAdjTimeSeries
            {
                MetaData = MapToMetaData(metaData),
                TimeSeries = MapToBlockHolder(timeSeries)
            };
        }

        private IList<AvDailyAdjTimeSeriesBlock> MapToBlockHolder(Dictionary<string, Dictionary<string, string>> content)
        {
            var localBlocks = new List<AvDailyAdjTimeSeriesBlock>();
            foreach(var row in content)
            {
                localBlocks.Add(MapToBlock(row.Value, row.Key));
            }

            return localBlocks;
        }

        private AvDailyAdjTimeSeriesBlock MapToBlock(Dictionary<string, string> block, string dateTime)
        {
            var result = new AvDailyAdjTimeSeriesBlock();

            var open = decimal.Parse(block[DailyAdjTimeSeriesRes.TimeSeriesOpenTag]);
            var high = decimal.Parse(block[DailyAdjTimeSeriesRes.TimeSeriesHighTag]);
            var low = decimal.Parse(block[DailyAdjTimeSeriesRes.TimeSeriesLowTag]);
            var close = decimal.Parse(block[DailyAdjTimeSeriesRes.TimeSeriesCloseTag]);
            var adjClose = decimal.Parse(block[DailyAdjTimeSeriesRes.TimeSeriesAdjustedCloseTag]);
            ulong volume = ulong.Parse(block[DailyAdjTimeSeriesRes.TimeSeriesVolumeTag]);
            var dividendAmt = decimal.Parse(block[DailyAdjTimeSeriesRes.TimeSeriesDividendAmountTag]);
            var splitCofficient = decimal.Parse(block[DailyAdjTimeSeriesRes.TimeSeriesSplitCofficientTag]);
            var dateTimeStamp = DateTime.Parse(dateTime);

            // open
            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvDailyAdjTimeSeriesBlock, decimal, AvPropertyNameAttribute, string>
                (DailyAdjTimeSeriesRes.TimeSeriesOpenTag, result,
                open,
                attr => attr.ExtractPropertyName);

            // high
            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvDailyAdjTimeSeriesBlock, decimal, AvPropertyNameAttribute, string>
                (DailyAdjTimeSeriesRes.TimeSeriesHighTag, result,
                high,
                attr => attr.ExtractPropertyName);

            // low
            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvDailyAdjTimeSeriesBlock, decimal, AvPropertyNameAttribute, string>
                (DailyAdjTimeSeriesRes.TimeSeriesLowTag, result,
                low,
                attr => attr.ExtractPropertyName);

            // close
            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvDailyAdjTimeSeriesBlock, decimal, AvPropertyNameAttribute, string>
                (DailyAdjTimeSeriesRes.TimeSeriesCloseTag, result,
                close,
                attr => attr.ExtractPropertyName);

            // adj close
            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvDailyAdjTimeSeriesBlock, decimal, AvPropertyNameAttribute, string>
                (DailyAdjTimeSeriesRes.TimeSeriesAdjustedCloseTag, result,
                adjClose,
                attr => attr.ExtractPropertyName);

            // volume
            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvDailyAdjTimeSeriesBlock, ulong, AvPropertyNameAttribute, string>
                (DailyAdjTimeSeriesRes.TimeSeriesVolumeTag, result,
                volume,
                attr => attr.ExtractPropertyName);

            // dividend amount
            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvDailyAdjTimeSeriesBlock, decimal, AvPropertyNameAttribute, string>
                (DailyAdjTimeSeriesRes.TimeSeriesDividendAmountTag, result,
                dividendAmt,
                attr => attr.ExtractPropertyName);

            // split cofficient
            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvDailyAdjTimeSeriesBlock, decimal, AvPropertyNameAttribute, string>
                (DailyAdjTimeSeriesRes.TimeSeriesSplitCofficientTag, result,
                splitCofficient,
                attr => attr.ExtractPropertyName);

            // date time
            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvDailyAdjTimeSeriesBlock, DateTime, AvPropertyNameAttribute, string>
                (DailyAdjTimeSeriesRes.TimeSeriesDayTag, result,
                dateTimeStamp,
                attr => attr.ExtractPropertyName);

            return result;
        }

        private AvDailyAdjTimeSeriesMetaData MapToMetaData(Dictionary<string, string> metaData)
        {
            var localMetaData = new AvDailyAdjTimeSeriesMetaData();

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvDailyAdjTimeSeriesMetaData, string, AvPropertyNameAttribute, string>
                (DailyAdjTimeSeriesRes.MetaDataInformationTag, localMetaData,
                metaData[DailyAdjTimeSeriesRes.MetaDataInformationTag],
                attr => attr.ExtractPropertyName);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvDailyAdjTimeSeriesMetaData, string, AvPropertyNameAttribute, string>
                (DailyAdjTimeSeriesRes.MetaDataSymbolTag, localMetaData,
                metaData[DailyAdjTimeSeriesRes.MetaDataSymbolTag],
                attr => attr.ExtractPropertyName);

            var lastRefreshed = DateTime.Parse(metaData[DailyAdjTimeSeriesRes.MetaDataLastRefreshedTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvDailyAdjTimeSeriesMetaData, DateTime, AvPropertyNameAttribute, string>
                (DailyAdjTimeSeriesRes.MetaDataLastRefreshedTag, localMetaData,
                lastRefreshed,
                attr => attr.ExtractPropertyName);

            var outputSize = AvOutputSizeEnum.FromDisplayName<AvOutputSizeEnum>(
                metaData[DailyAdjTimeSeriesRes.MetaDataOutputSizeTag],
                StringComparison.InvariantCultureIgnoreCase);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvDailyAdjTimeSeriesMetaData, AvOutputSizeEnum, AvPropertyNameAttribute, string>
                (DailyAdjTimeSeriesRes.MetaDataOutputSizeTag, localMetaData,
                outputSize,
                attr => attr.ExtractPropertyName);

            var localTimeZone = AvTimeZoneConvertor.AvTimeZone(
                metaData[DailyAdjTimeSeriesRes.MetaDataTimeZoneTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvDailyAdjTimeSeriesMetaData, TimeZoneInfo, AvPropertyNameAttribute, string>
                (DailyAdjTimeSeriesRes.MetaDataTimeZoneTag, localMetaData,
                localTimeZone,
                attr => attr.ExtractPropertyName);


            return localMetaData;
        }
        #endregion
    }
}
