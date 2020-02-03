using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaVantage.Core.Exceptions
{
    class AvApiCallLimitReachedException : Exception
    {
        public AvApiCallLimitReachedException() : base() { }
        public AvApiCallLimitReachedException(string message) : base(message) { }
        public AvApiCallLimitReachedException(string message, System.Exception inner) : base(message, inner) { }
    }
}
