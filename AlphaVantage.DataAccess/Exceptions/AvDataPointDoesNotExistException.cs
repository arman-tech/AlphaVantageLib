using System;

namespace AlphaVantage.DataAccess.Exceptions
{
    public class AvDataPointDoesNotExistException : Exception
    {
        public AvDataPointDoesNotExistException() : base() { }
        public AvDataPointDoesNotExistException(string message) : base(message) { }
        public AvDataPointDoesNotExistException(string message, System.Exception inner) : base(message, inner) { }
    }
}
