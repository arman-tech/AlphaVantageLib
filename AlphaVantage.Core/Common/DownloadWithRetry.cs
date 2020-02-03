using AlphaVantage.Core.Exceptions;
using AlphaVantage.Core.Interfaces;
using Newtonsoft.Json.Linq;
using System;
using System.Threading;

namespace AlphaVantage.Core.Common
{
    public class DownloadWithRetry : IDownloadWithRetry
    {
        public JObject DownloadWithRetries(string uri, int numOfRetries, int retriesInMilliSec)
        {
            var retries = 0;

            do
            {
                try
                {
                    JObject jObj = CoreHelper.CaptureRemoteJson(uri);
                    if (CoreHelper.HasKey(jObj, CommonProcessRes.ErrorMessageTag))
                    {
                        throw new AvDownloadException(CoreHelper.GetFirstValue(jObj));
                    }
                    else if (CoreHelper.HasKey(jObj, CommonProcessRes.NoteTag))
                    {
                        throw new AvApiCallLimitReachedException(CoreHelper.GetFirstValue(jObj));
                    }

                    return jObj;
                }
                catch (Exception e) when (CoreHelper.AvDownloadApiCallLimitException(e))
                {
                    // block the current thread based on 'retriesInMilliSec'
                    Thread.Sleep(retriesInMilliSec);

                    retries++;

                    if(retries > numOfRetries)
                    {
                        throw new AvDownloadRetryLimitReachedException();
                    }
                    
                }

            } while (retries <= numOfRetries);

            return default;
        }
    }
}
