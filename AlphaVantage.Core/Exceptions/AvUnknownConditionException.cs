using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaVantage.Core.Exceptions
{
    [Serializable]
    public class AvUnknownConditionException : Exception
    {
        public AvUnknownConditionException(): base() { }
        public AvUnknownConditionException(string message) : base(message) { }
        public AvUnknownConditionException(string message, System.Exception inner) : base(message, inner) { }
    }
}
