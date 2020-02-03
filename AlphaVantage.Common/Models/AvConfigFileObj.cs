using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace AlphaVantage.Common.Models
{
    public class AvConfigFileObj
    {
        [JsonProperty(PropertyName = "version")]
        public string Version { get; set; }

        [JsonProperty(PropertyName = "max-concurrent-thread")]
        public int MaxConcurrentThread { get; set; }

        [JsonProperty(PropertyName = "max-download-retries")]
        public int MaxDownloadRetries { get; set; }

        [JsonProperty(PropertyName = "retry-delay-millisec")]
        public int RetryDelayInMilliSeconds { get; set; }

        [JsonProperty(PropertyName = "api-calls-per-minute-allowed")]
        public int ApiCallsPerMinuteAllowed { get; set; }

        [JsonProperty(PropertyName = "exchanges")]
        public List<string> Exchanges { get; set; }

        [JsonProperty(PropertyName = "uris")]
        public List<AvConfigFileUri> Uris { get; set; }
    }

    public class AvConfigFileUri
    {
        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; }

        [JsonProperty(PropertyName = "uri")]
        public string Uri { get; set; }

        [JsonProperty(PropertyName = "country")]
        public string Country { get; set; }
    }
}
