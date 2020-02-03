using AlphaVantage.Common;
using AlphaVantage.Common.Models.TimeSeries.IntraDay;
using AlphaVantage.Core.Interfaces;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace AlphaVantage.Core.TimeSeries.IntraDay
{
    public class AvIntraDayTimeSeriesProcess : IMapResource<AvIntraDayTimeSeries>
    {
        private Dictionary<string, string> _metaData;
        private Dictionary<string, Dictionary<string, string>> _content;


        public AvIntraDayTimeSeriesProcess()
        {
        }

        public AvIntraDayTimeSeries Map(JObject remoteResource, string uri)
        {
            // sanity check
            if (string.IsNullOrWhiteSpace(uri))
            {
                throw new ArgumentNullException(nameof(Map));
            }

            // download resource
            ProcessDownloadResource(remoteResource, uri);

            // map resource
            Data = MapToIntraDayTimeSeries(_metaData, _content);

            return Data;

        }

        public AvIntraDayTimeSeries Data { get; private set; }

        #region Helpers

        private void ProcessDownloadResource(JObject remoteResource, string uri)
        {
            var intervalEnum = CommonHelper.ConvertToAvIntervalEnum(uri);
            var tagName = string.Format(AvIntraDayTimeSeriesProcessRes.TimeSeriesTagFormat, intervalEnum.Name);

            _metaData = remoteResource[AvIntraDayTimeSeriesProcessRes.MetaDataTag].ToObject<Dictionary<string, string>>();

            // NOTE: when it comes to intraDay time series, AlphaVantage uses different content parent name.  
            // These can be: Time Series (1min) ... Time Series (60min)
            // https://www.alphavantage.co/documentation/#intraday
            _content = remoteResource[tagName].ToObject<Dictionary<string, Dictionary<string, string>>>();
        }

        private AvIntraDayTimeSeries MapToIntraDayTimeSeries(Dictionary<string, string> metaData,
            Dictionary<string, Dictionary<string, string>> timeSeries)
        {
            if (null == metaData || null == timeSeries)
            {
                throw new ArgumentNullException(nameof(MapToIntraDayTimeSeries));
            }


            return new AvIntraDayTimeSeries
            {
                MetaData = MapToMetaData(metaData),
                TimeSeries = MapToBlockHolder(timeSeries)
            };
        }

        private AvIntraDayTimeSeriesMetaData MapToMetaData(Dictionary<string, string> metaData)
        {
            var localMetaData = new AvIntraDayTimeSeriesMetaData();

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvIntraDayTimeSeriesMetaData, string, AvPropertyNameAttribute, string>
                (AvIntraDayTimeSeriesRes.MetaDataInformationTag, localMetaData,
                metaData[AvIntraDayTimeSeriesRes.MetaDataInformationTag],
                attr => attr.ExtractPropertyName);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvIntraDayTimeSeriesMetaData, string, AvPropertyNameAttribute, string>
                (AvIntraDayTimeSeriesRes.MetaDataSymbolTag, localMetaData,
                metaData[AvIntraDayTimeSeriesRes.MetaDataSymbolTag],
                attr => attr.ExtractPropertyName);

            var lastRefreshed = DateTime.Parse(metaData[AvIntraDayTimeSeriesRes.MetaDataLastRefreshedTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvIntraDayTimeSeriesMetaData, DateTime, AvPropertyNameAttribute, string>
                (AvIntraDayTimeSeriesRes.MetaDataLastRefreshedTag, localMetaData,
                lastRefreshed,
                attr => attr.ExtractPropertyName);


            var interval = AvIntervalEnum.FromDisplayName<AvIntervalEnum>(
                metaData[AvIntraDayTimeSeriesRes.MetaDataIntervalTag],
                StringComparison.InvariantCultureIgnoreCase);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvIntraDayTimeSeriesMetaData, AvIntervalEnum, AvPropertyNameAttribute, string>
                (AvIntraDayTimeSeriesRes.MetaDataIntervalTag, localMetaData,
                interval,
                attr => attr.ExtractPropertyName);

            var outputSize = AvOutputSizeEnum.FromDisplayName<AvOutputSizeEnum>(
                metaData[AvIntraDayTimeSeriesRes.MetaDataOutputSizeTag],
                StringComparison.InvariantCultureIgnoreCase);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvIntraDayTimeSeriesMetaData, AvOutputSizeEnum, AvPropertyNameAttribute, string>
                (AvIntraDayTimeSeriesRes.MetaDataOutputSizeTag, localMetaData,
                outputSize,
                attr => attr.ExtractPropertyName);

            var localTimeZone = AvTimeZoneConvertor.AvTimeZone(
                metaData[AvIntraDayTimeSeriesRes.MetaDataTimeZoneTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvIntraDayTimeSeriesMetaData, TimeZoneInfo, AvPropertyNameAttribute, string>
                (AvIntraDayTimeSeriesRes.MetaDataTimeZoneTag, localMetaData,
                localTimeZone,
                attr => attr.ExtractPropertyName);


            return localMetaData;
        }

        public IList<AvIntraDayTimeSeriesBlock> MapToBlockHolder(Dictionary<string, Dictionary<string, string>> content)
        {
            var localBlocks = new List<AvIntraDayTimeSeriesBlock>();
            foreach (var row in content)
            {
                localBlocks.Add(MapToBlock(row.Value, row.Key));
            }

            return localBlocks;
        }

        public AvIntraDayTimeSeriesBlock MapToBlock(Dictionary<string, string> block, string dateTime)
        {
            var result = new AvIntraDayTimeSeriesBlock();

            var open = decimal.Parse(block[AvIntraDayTimeSeriesRes.TimeSeriesOpenTag]);
            var high = decimal.Parse(block[AvIntraDayTimeSeriesRes.TimeSeriesHighTag]);
            var low = decimal.Parse(block[AvIntraDayTimeSeriesRes.TimeSeriesLowTag]);
            var close = decimal.Parse(block[AvIntraDayTimeSeriesRes.TimeSeriesCloseTag]);
            ulong volume = ulong.Parse(block[AvIntraDayTimeSeriesRes.TimeSeriesVolumeTag]);

            var dateTimeStamp = DateTime.Parse(dateTime);


            // open
            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvIntraDayTimeSeriesBlock, decimal, AvPropertyNameAttribute, string>
                (AvIntraDayTimeSeriesRes.TimeSeriesOpenTag, result,
                open,
                attr => attr.ExtractPropertyName);

            // high
            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvIntraDayTimeSeriesBlock, decimal, AvPropertyNameAttribute, string>
                (AvIntraDayTimeSeriesRes.TimeSeriesHighTag, result,
                high,
                attr => attr.ExtractPropertyName);

            // low
            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvIntraDayTimeSeriesBlock, decimal, AvPropertyNameAttribute, string>
                (AvIntraDayTimeSeriesRes.TimeSeriesLowTag, result,
                low,
                attr => attr.ExtractPropertyName);

            // close
            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvIntraDayTimeSeriesBlock, decimal, AvPropertyNameAttribute, string>
                (AvIntraDayTimeSeriesRes.TimeSeriesCloseTag, result,
                close,
                attr => attr.ExtractPropertyName);

            // volume
            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvIntraDayTimeSeriesBlock, ulong, AvPropertyNameAttribute, string>
                (AvIntraDayTimeSeriesRes.TimeSeriesVolumeTag, result,
                volume,
                attr => attr.ExtractPropertyName);

            // date time
            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvIntraDayTimeSeriesBlock, DateTime, AvPropertyNameAttribute, string>
                (AvIntraDayTimeSeriesRes.TimeSeriesDayTag, result,
                dateTimeStamp,
                attr => attr.ExtractPropertyName);

            return result;
        }
        #endregion
    }
}
