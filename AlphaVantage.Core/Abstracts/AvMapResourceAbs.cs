using AlphaVantage.Common.Models;
using AlphaVantage.Core.Interfaces;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace AlphaVantage.Core.Abstracts
{
    public abstract class AvMapResourceAbs<T, K, X> : IMapResource<T>   where T : class, IAvSeriesObj<T, K, X>, new()
                                                                        where K : IAvMetaData<K>
                                                                        where X : IAvBlock<X>
    {
        protected Dictionary<string, string> _metaData;
        protected Dictionary<string, Dictionary<string, string>> _content;

        public T Data { get; protected set; }

        public T Map(JObject remoteResource, string uri)
        {
            // sanity check
            if (string.IsNullOrWhiteSpace(uri))
            {
                throw new ArgumentNullException(nameof(Map));
            }

            // download resource
            ProcessDownloadResource(remoteResource, uri);

            // map resource
            Data = MapToAvObject(_metaData, _content);

            return Data;
        }

        #region Helpers
        protected abstract void ProcessDownloadResource(JObject remoteResource, string uri);
        protected abstract X MapToBlock(Dictionary<string, string> block, string dateTime);

        protected abstract K MapToMetaData(Dictionary<string, string> metaData);

        protected T MapToAvObject(Dictionary<string, string> metaData,
            Dictionary<string, Dictionary<string, string>> timeSeries)
        {
            if (null == metaData || null == timeSeries)
            {
                throw new ArgumentNullException(nameof(MapToAvObject));
            }

            return new T
            {
                MetaData = MapToMetaData(metaData),
                TimeSeries = MapToBlockHolder(timeSeries)
            };
        }

        protected IList<X> MapToBlockHolder(Dictionary<string, Dictionary<string, string>> content)
        {
            var blocks = new List<X>();
            foreach (var row in content)
            {
                blocks.Add(MapToBlock(row.Value, row.Key));
            }

            return blocks;
        }


        #endregion Helpers
    }
}
