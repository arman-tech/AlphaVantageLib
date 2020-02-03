using System;

namespace AlphaVantage.DataAccess.Exceptions
{
    public class AvTimeSeriesEmptyException : Exception
    {
        public AvTimeSeriesEmptyException() : base() { }
        public AvTimeSeriesEmptyException(string message) : base(message) { }
        public AvTimeSeriesEmptyException(string message, System.Exception inner) : base(message, inner) { }
    }
}
