using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaVantage.Core.Exceptions
{
    public class AvDownloadException : Exception
    {
        public AvDownloadException() : base() { }
        public AvDownloadException(string message) : base(message) { }
        public AvDownloadException(string message, System.Exception inner) : base(message, inner) { }
    }
}
