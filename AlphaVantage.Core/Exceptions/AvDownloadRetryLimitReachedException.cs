using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaVantage.Core.Exceptions
{
    public class AvDownloadRetryLimitReachedException : Exception
    {
        public AvDownloadRetryLimitReachedException() : base() { }
        public AvDownloadRetryLimitReachedException(string message) : base(message) { }
        public AvDownloadRetryLimitReachedException(string message, System.Exception inner) : base(message, inner) { }
    }
}
