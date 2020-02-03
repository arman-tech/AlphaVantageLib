using AlphaVantage.Common;
using AlphaVantage.Common.Models.TechnicalIndicators.BBANDS;
using AlphaVantage.Core.Interfaces;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace AlphaVantage.Core.TechnicalIndicators.BBANDS
{
    public class AvBBANDSProcess : IMapResource<AvBBANDS>
    {
        private Dictionary<string, string> _metaData;
        private Dictionary<string, Dictionary<string, string>> _content;

        public AvBBANDSProcess()
        {
        }

        public AvBBANDS Map(JObject remoteResource, string uri)
        {
            // sanity check
            if (string.IsNullOrWhiteSpace(uri))
            {
                throw new ArgumentNullException(nameof(Map));
            }

            // download resource
            ProcessDownloadResource(remoteResource, uri);

            // map resource
            Data = MapToBollingerBand(_metaData, _content);

            return Data;
        }

        public AvBBANDS Data { get; private set; }

        #region Helpers
        private void ProcessDownloadResource(JObject remoteResource, string uri)
        { 
            _metaData = remoteResource[AvBBANDSProcessRes.MetaDataTag].ToObject<Dictionary<string, string>>();
            _content = remoteResource[AvBBANDSProcessRes.TimeSeriesTag].ToObject<Dictionary<string, Dictionary<string, string>>>();
        }



        private AvBBANDS MapToBollingerBand(Dictionary<string, string> metaData, 
            Dictionary<string, Dictionary<string, string>> timeSeries)
        {
            if (null == metaData || null == timeSeries)
            {
                throw new ArgumentNullException(nameof(MapToBollingerBand));
            }

            return new AvBBANDS
            {
                MetaData = MapToMetaData(metaData),
                TimeSeries = MapToBlockHolder(timeSeries)
            };
        }

        private IList<AvBBANDSBlock> MapToBlockHolder(Dictionary<string, Dictionary<string, string>> content)
        {
            var blocks = new List<AvBBANDSBlock>();
            foreach(var row in content)
            {
                blocks.Add(MapToBlock(row.Value, row.Key));
            }

            return blocks;
        }

        private AvBBANDSBlock MapToBlock(Dictionary<string, string> block, string dateTime)
        {
            var result = new AvBBANDSBlock();

            var realUpperBand = decimal.Parse(block[AvBBANDSRes.BlockRealUpperBandTag]);
            var realMiddleBand = decimal.Parse(block[AvBBANDSRes.BlockRealMiddleBandTag]);
            var realLowerBand = decimal.Parse(block[AvBBANDSRes.BlockRealLowerBandTag]);
            var dateTimeStamp = DateTime.Parse(dateTime);
            // upper
            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvBBANDSBlock, decimal, AvPropertyNameAttribute, string>
                (AvBBANDSRes.BlockRealUpperBandTag, result, realUpperBand, attr => attr.ExtractPropertyName);

            // middle
            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvBBANDSBlock, decimal, AvPropertyNameAttribute, string>
                (AvBBANDSRes.BlockRealMiddleBandTag, result, realMiddleBand, attr => attr.ExtractPropertyName);

            // lower
            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvBBANDSBlock, decimal, AvPropertyNameAttribute, string>
                (AvBBANDSRes.BlockRealLowerBandTag, result, realLowerBand, attr => attr.ExtractPropertyName);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvBBANDSBlock, DateTime, AvPropertyNameAttribute, string>
                (AvBBANDSRes.BlockDayTag, result,
                dateTimeStamp, attr => attr.ExtractPropertyName);

            return result;
        }

        private AvBBANDSMetaData MapToMetaData(Dictionary<string, string> metaData)
        {
            var result = new AvBBANDSMetaData();

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvBBANDSMetaData, string, AvPropertyNameAttribute, string>
                (AvBBANDSRes.MetaDataSymbolTag, result, metaData[AvBBANDSRes.MetaDataSymbolTag],
                attr => attr.ExtractPropertyName);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvBBANDSMetaData, string, AvPropertyNameAttribute, string>
                (AvBBANDSRes.MetaDataIndicatorTag, result, metaData[AvBBANDSRes.MetaDataIndicatorTag],
                attr => attr.ExtractPropertyName);

            var lastRefreshed = DateTime.Parse(metaData[AvBBANDSRes.MetaDataLastRefreshedTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvBBANDSMetaData, DateTime, AvPropertyNameAttribute, string>
                (AvBBANDSRes.MetaDataLastRefreshedTag, result,
                lastRefreshed,
                attr => attr.ExtractPropertyName);

            var interval = AvIntervalEnum.FromDisplayName<AvIntervalEnum>(
                metaData[AvBBANDSRes.MetaDataIntervalTag],
                StringComparison.InvariantCultureIgnoreCase);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvBBANDSMetaData, AvIntervalEnum, AvPropertyNameAttribute, string>
                (AvBBANDSRes.MetaDataIntervalTag, result,
                interval,
                attr => attr.ExtractPropertyName);

            var timePeriod = int.Parse(metaData[AvBBANDSRes.MetaDataTimePeriodTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvBBANDSMetaData, int, AvPropertyNameAttribute, string>
                (AvBBANDSRes.MetaDataTimePeriodTag, result, timePeriod, attr => attr.ExtractPropertyName);

            var deviationMultiplierUpperband = int.Parse(metaData[AvBBANDSRes.MetaDataDeviationMultiUpperBandTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvBBANDSMetaData, int, AvPropertyNameAttribute, string>
                (AvBBANDSRes.MetaDataDeviationMultiUpperBandTag, result, deviationMultiplierUpperband,
                attr => attr.ExtractPropertyName);

            var deviationMultiplierLowerband = int.Parse(metaData[AvBBANDSRes.MetaDataDeviationMultiLowerBandTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvBBANDSMetaData, int, AvPropertyNameAttribute, string>
                (AvBBANDSRes.MetaDataDeviationMultiLowerBandTag, result, deviationMultiplierLowerband,
                attr => attr.ExtractPropertyName);

            var movingAvgValue = int.Parse(metaData[AvBBANDSRes.MetaDataMovingAvgTag]);
            var movingAvgEnum = AvMovingAverageTypeEnum.FromValue<AvMovingAverageTypeEnum>(movingAvgValue);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvBBANDSMetaData, AvMovingAverageTypeEnum, AvPropertyNameAttribute, string>
                (AvBBANDSRes.MetaDataMovingAvgTag, result, movingAvgEnum,
                attr => attr.ExtractPropertyName);

            var seriesType = AvSeriesTypeEnum.FromDisplayName<AvSeriesTypeEnum>
                (metaData[AvBBANDSRes.MetaDataSeriesTypeTag], StringComparison.InvariantCultureIgnoreCase);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvBBANDSMetaData, AvSeriesTypeEnum, AvPropertyNameAttribute, string>
                (AvBBANDSRes.MetaDataSeriesTypeTag, result, seriesType, attr => attr.ExtractPropertyName);

            var timeZone = AvTimeZoneConvertor.AvTimeZone(metaData[AvBBANDSRes.MetaDataTimeZoneTag]);

            AttributeHelper.SetPropertyBasedOnAvPropertyName<
                AvBBANDSMetaData, TimeZoneInfo, AvPropertyNameAttribute, string>
                (AvBBANDSRes.MetaDataTimeZoneTag, result,
                timeZone,
                attr => attr.ExtractPropertyName);



            return result;
        }

        #endregion
    }
}
