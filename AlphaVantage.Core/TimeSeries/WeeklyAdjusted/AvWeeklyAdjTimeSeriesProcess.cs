using AlphaVantage.Common;
using AlphaVantage.Common.Models.TimeSeries.WeeklyAdjusted;
using AlphaVantage.Core.Interfaces;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace AlphaVantage.Core.TimeSeries.WeeklyAdjusted
{
    public class AvWeeklyAdjTimeSeriesProcess : IMapResource<AvWeeklyAdjTimeSeries>
    {
        private Dictionary<string, string> _metaData;
        private Dictionary<string, Dictionary<string, string>> _content;

        public AvWeeklyAdjTimeSeriesProcess()
        {
        }

        public AvWeeklyAdjTimeSeries Map(JObject remoteResource, string uri)
        {
            if (string.IsNullOrWhiteSpace(uri))
            {
                throw new ArgumentNullException(nameof(Map));
            }

            // download resource
            ProcessDownloadResource(remoteResource, uri);

            // map resource
            Data = MapToMonthlyAdjTimeSeries(_metaData, _content);

            return Data;
        }
        
        public AvWeeklyAdjTimeSeries Data { get; private set; }


        #region Helpers
        private void ProcessDownloadResource(JObject remoteResource, string uri)
        {
            _metaData = remoteResource[AvWeeklyAdjTimeSeriesProcessRes.MetaDataTag].ToObject<Dictionary<string, string>>();
            _content = remoteResource[AvWeeklyAdjTimeSeriesProcessRes.TimeSeriesTag].ToObject<Dictionary<string, Dictionary<string, string>>>();
        }

        private AvWeeklyAdjTimeSeries MapToMonthlyAdjTimeSeries(Dictionary<string, string> metaData,
            Dictionary<string, Dictionary<string, string>> timeSeries)
        {
            if (null == metaData || null == timeSeries)
            {
                throw new ArgumentNullException(nameof(MapToMonthlyAdjTimeSeries));
            }

            return new AvWeeklyAdjTimeSeries
            {
                MetaData = MapToMetaData(metaData),
                TimeSeries = MapToBlockHolder(timeSeries)
            };
        }

        private IList<AvWeeklyAdjTimeSeriesBlock> MapToBlockHolder(Dictionary<string, Dictionary<string, string>> content)
        {
            var localBlocks = new List<AvWeeklyAdjTimeSeriesBlock>();
            foreach (var row in content)
            {
                localBlocks.Add(MapToBlock(row.Value, row.Key));
            }

            return localBlocks;
        }

        private AvWeeklyAdjTimeSeriesBlock MapToBlock(Dictionary<string, string> block, string dateTime)
        {
            var result = new AvWeeklyAdjTimeSeriesBlock();

            var open = decimal.Parse(block[AvWeeklyAdjTimeSeriesRes.TimeSeriesOpenTag]);
            var high = decimal.Parse(block[AvWeeklyAdjTimeSeriesRes.TimeSeriesHighTag]);
            var low = decimal.Parse(block[AvWeeklyAdjTimeSeriesRes.TimeSeriesLowTag]);
            var close = decimal.Parse(block[AvWeeklyAdjTimeSeriesRes.TimeSeriesCloseTag]);
            var adjClose = decimal.Parse(block[AvWeeklyAdjTimeSeriesRes.TimeSeriesAdjustedCloseTag]);
            ulong volume = ulong.Parse(block[AvWeeklyAdjTimeSeriesRes.TimeSeriesVolumeTag]);
            var divdendAmt = decimal.Parse(block[AvWeeklyAdjTimeSeriesRes.TimeSeriesDividendAmountTag]);

            var dateTimeStamp = DateTime.Parse(dateTime);

            // open
            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvWeeklyAdjTimeSeriesBlock, decimal, AvPropertyNameAttribute, string>
                (AvWeeklyAdjTimeSeriesRes.TimeSeriesOpenTag, result,
                open,
                attr => attr.ExtractPropertyName);

            // high
            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvWeeklyAdjTimeSeriesBlock, decimal, AvPropertyNameAttribute, string>
                (AvWeeklyAdjTimeSeriesRes.TimeSeriesHighTag, result,
                high,
                attr => attr.ExtractPropertyName);

            // low
            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvWeeklyAdjTimeSeriesBlock, decimal, AvPropertyNameAttribute, string>
                (AvWeeklyAdjTimeSeriesRes.TimeSeriesLowTag, result,
                low,
                attr => attr.ExtractPropertyName);

            // close
            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvWeeklyAdjTimeSeriesBlock, decimal, AvPropertyNameAttribute, string>
                (AvWeeklyAdjTimeSeriesRes.TimeSeriesCloseTag, result,
                close,
                attr => attr.ExtractPropertyName);

            // adjusted close
            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvWeeklyAdjTimeSeriesBlock, decimal, AvPropertyNameAttribute, string>
                (AvWeeklyAdjTimeSeriesRes.TimeSeriesAdjustedCloseTag, result,
                adjClose,
                attr => attr.ExtractPropertyName);

            // volume
            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvWeeklyAdjTimeSeriesBlock, ulong, AvPropertyNameAttribute, string>
                (AvWeeklyAdjTimeSeriesRes.TimeSeriesVolumeTag, result,
                volume,
                attr => attr.ExtractPropertyName);

            // dividend amount
            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvWeeklyAdjTimeSeriesBlock, decimal, AvPropertyNameAttribute, string>
                (AvWeeklyAdjTimeSeriesRes.TimeSeriesDividendAmountTag, result,
                divdendAmt,
                attr => attr.ExtractPropertyName);

            // date time
            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvWeeklyAdjTimeSeriesBlock, DateTime, AvPropertyNameAttribute, string>
                (AvWeeklyAdjTimeSeriesRes.TimeSeriesWeeklyAdjTag, result,
                dateTimeStamp,
                attr => attr.ExtractPropertyName);

            return result;
        }

        private AvWeeklyAdjTimeSeriesMetaData MapToMetaData(Dictionary<string, string> metaData)
        {
            var localMetaData = new AvWeeklyAdjTimeSeriesMetaData();

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvWeeklyAdjTimeSeriesMetaData, string, AvPropertyNameAttribute, string>
                (AvWeeklyAdjTimeSeriesRes.MetaDataInformationTag, localMetaData,
                metaData[AvWeeklyAdjTimeSeriesRes.MetaDataInformationTag],
                attr => attr.ExtractPropertyName);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvWeeklyAdjTimeSeriesMetaData, string, AvPropertyNameAttribute, string>
                (AvWeeklyAdjTimeSeriesRes.MetaDataSymbolTag, localMetaData,
                metaData[AvWeeklyAdjTimeSeriesRes.MetaDataSymbolTag],
                attr => attr.ExtractPropertyName);

            var lastRefreshed = DateTime.Parse(metaData[AvWeeklyAdjTimeSeriesRes.MetaDataLastRefreshedTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvWeeklyAdjTimeSeriesMetaData, DateTime, AvPropertyNameAttribute, string>
                (AvWeeklyAdjTimeSeriesRes.MetaDataLastRefreshedTag, localMetaData,
                lastRefreshed,
                attr => attr.ExtractPropertyName);

            var localTimeZone = AvTimeZoneConvertor.AvTimeZone(
                metaData[AvWeeklyAdjTimeSeriesRes.MetaDataTimeZoneTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvWeeklyAdjTimeSeriesMetaData, TimeZoneInfo, AvPropertyNameAttribute, string>
                (AvWeeklyAdjTimeSeriesRes.MetaDataTimeZoneTag, localMetaData,
                localTimeZone,
                attr => attr.ExtractPropertyName);


            return localMetaData;
        }
        #endregion
    }
}
