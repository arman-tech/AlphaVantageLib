using AlphaVantage.Common;
using AlphaVantage.Common.Models.TimeSeries.MonthlyAdjusted;
using AlphaVantage.Core.Interfaces;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace AlphaVantage.Core.TimeSeries.MonthlyAdjusted
{
    public class AvMonthlyAdjTimeSeriesProcess : IMapResource<AvMonthlyAdjTimeSeries>
    {
        private Dictionary<string, string> _metaData;
        private Dictionary<string, Dictionary<string, string>> _content;

        public AvMonthlyAdjTimeSeriesProcess()
        {
        }


        public AvMonthlyAdjTimeSeries Map(JObject remoteResource, string uri)
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

        public AvMonthlyAdjTimeSeries Data { get; private set; }

        #region Helpers
        private void ProcessDownloadResource(JObject remoteResource, string uri)
        {
            _metaData = remoteResource[AvMonthlyAdjTimeSeriesProcessRes.MetaDataTag].ToObject<Dictionary<string, string>>();
            _content = remoteResource[AvMonthlyAdjTimeSeriesProcessRes.TimeSeriesTag].ToObject<Dictionary<string, Dictionary<string, string>>>();
        }

        private AvMonthlyAdjTimeSeries MapToMonthlyAdjTimeSeries(Dictionary<string, string> metaData,
            Dictionary<string, Dictionary<string, string>> timeSeries)
        {
            if (null == metaData || null == timeSeries)
            {
                throw new ArgumentNullException(nameof(MapToMonthlyAdjTimeSeries));
            }

            return new AvMonthlyAdjTimeSeries
            {
                MetaData = MapToMetaData(metaData),
                TimeSeries = MapToBlockHolder(timeSeries)
            };
        }

        private IList<AvMonthlyAdjTimeSeriesBlock> MapToBlockHolder(Dictionary<string, Dictionary<string, string>> content)
        {
            var localBlocks = new List<AvMonthlyAdjTimeSeriesBlock>();
            foreach (var row in content)
            {
                localBlocks.Add(MapToBlock(row.Value, row.Key));
            }

            return localBlocks;
        }

        private AvMonthlyAdjTimeSeriesBlock MapToBlock(Dictionary<string, string> block, string dateTime)
        {
            var result = new AvMonthlyAdjTimeSeriesBlock();

            var open = decimal.Parse(block[AvMonthlyAdjTimeSeriesRes.TimeSeriesOpenTag]);
            var high = decimal.Parse(block[AvMonthlyAdjTimeSeriesRes.TimeSeriesHighTag]);
            var low = decimal.Parse(block[AvMonthlyAdjTimeSeriesRes.TimeSeriesLowTag]);
            var close = decimal.Parse(block[AvMonthlyAdjTimeSeriesRes.TimeSeriesCloseTag]);
            var adjClose = decimal.Parse(block[AvMonthlyAdjTimeSeriesRes.TimeSeriesAdjustedCloseTag]);
            ulong volume = ulong.Parse(block[AvMonthlyAdjTimeSeriesRes.TimeSeriesVolumeTag]);
            var divdendAmt = decimal.Parse(block[AvMonthlyAdjTimeSeriesRes.TimeSeriesDividendAmountTag]);

            var dateTimeStamp = DateTime.Parse(dateTime);

            // open
            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvMonthlyAdjTimeSeriesBlock, decimal, AvPropertyNameAttribute, string>
                (AvMonthlyAdjTimeSeriesRes.TimeSeriesOpenTag, result,
                open,
                attr => attr.ExtractPropertyName);

            // high
            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvMonthlyAdjTimeSeriesBlock, decimal, AvPropertyNameAttribute, string>
                (AvMonthlyAdjTimeSeriesRes.TimeSeriesHighTag, result,
                high,
                attr => attr.ExtractPropertyName);

            // low
            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvMonthlyAdjTimeSeriesBlock, decimal, AvPropertyNameAttribute, string>
                (AvMonthlyAdjTimeSeriesRes.TimeSeriesLowTag, result,
                low,
                attr => attr.ExtractPropertyName);

            // close
            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvMonthlyAdjTimeSeriesBlock, decimal, AvPropertyNameAttribute, string>
                (AvMonthlyAdjTimeSeriesRes.TimeSeriesCloseTag, result,
                close,
                attr => attr.ExtractPropertyName);

            // adjusted close
            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvMonthlyAdjTimeSeriesBlock, decimal, AvPropertyNameAttribute, string>
                (AvMonthlyAdjTimeSeriesRes.TimeSeriesAdjustedCloseTag, result,
                adjClose,
                attr => attr.ExtractPropertyName);

            // volume
            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvMonthlyAdjTimeSeriesBlock, ulong, AvPropertyNameAttribute, string>
                (AvMonthlyAdjTimeSeriesRes.TimeSeriesVolumeTag, result,
                volume,
                attr => attr.ExtractPropertyName);

            // dividend amount
            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvMonthlyAdjTimeSeriesBlock, decimal, AvPropertyNameAttribute, string>
                (AvMonthlyAdjTimeSeriesRes.TimeSeriesDividendAmountTag, result,
                divdendAmt,
                attr => attr.ExtractPropertyName);

            // date time
            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvMonthlyAdjTimeSeriesBlock, DateTime, AvPropertyNameAttribute, string>
                (AvMonthlyAdjTimeSeriesRes.TimeSeriesMonthlyTag, result,
                dateTimeStamp,
                attr => attr.ExtractPropertyName);

            return result;
        }

        private AvMonthlyAdjTimeSeriesMetaData MapToMetaData(Dictionary<string, string> metaData)
        {
            var localMetaData = new AvMonthlyAdjTimeSeriesMetaData();

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvMonthlyAdjTimeSeriesMetaData, string, AvPropertyNameAttribute, string>
                (AvMonthlyAdjTimeSeriesRes.MetaDataInformationTag, localMetaData,
                metaData[AvMonthlyAdjTimeSeriesRes.MetaDataInformationTag],
                attr => attr.ExtractPropertyName);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvMonthlyAdjTimeSeriesMetaData, string, AvPropertyNameAttribute, string>
                (AvMonthlyAdjTimeSeriesRes.MetaDataSymbolTag, localMetaData,
                metaData[AvMonthlyAdjTimeSeriesRes.MetaDataSymbolTag],
                attr => attr.ExtractPropertyName);

            var lastRefreshed = DateTime.Parse(metaData[AvMonthlyAdjTimeSeriesRes.MetaDataLastRefreshedTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvMonthlyAdjTimeSeriesMetaData, DateTime, AvPropertyNameAttribute, string>
                (AvMonthlyAdjTimeSeriesRes.MetaDataLastRefreshedTag, localMetaData,
                lastRefreshed,
                attr => attr.ExtractPropertyName);

            var localTimeZone = AvTimeZoneConvertor.AvTimeZone(
                metaData[AvMonthlyAdjTimeSeriesRes.MetaDataTimeZoneTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvMonthlyAdjTimeSeriesMetaData, TimeZoneInfo, AvPropertyNameAttribute, string>
                (AvMonthlyAdjTimeSeriesRes.MetaDataTimeZoneTag, localMetaData,
                localTimeZone,
                attr => attr.ExtractPropertyName);


            return localMetaData;
        }
        #endregion
    }
}
