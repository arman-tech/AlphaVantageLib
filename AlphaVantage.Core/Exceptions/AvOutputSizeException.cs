using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaVantage.Core.Exceptions
{
    [Serializable]
    public class AvOutputSizeException : Exception
    {
        public AvOutputSizeException() : base() { }
        public AvOutputSizeException(string message) : base(message) { }
        public AvOutputSizeException(string message, System.Exception inner) : base(message, inner) { }

    }
}
