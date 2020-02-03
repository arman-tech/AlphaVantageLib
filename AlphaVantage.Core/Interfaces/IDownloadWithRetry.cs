using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaVantage.Core.Interfaces
{
    public interface IDownloadWithRetry
    {
        JObject DownloadWithRetries(string uri, int numOfRetries = 0, int retriesInMilliSec = 0);
    }
}
