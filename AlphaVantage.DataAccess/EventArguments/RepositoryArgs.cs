using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaVantage.DataAccess.EventArguments
{
    public class RepositoryArgs : EventArgs
    {
        public string Message { get; set; }
        public Guid Guid { get; private set; }
        public DateTime DateTime { get; private set; }

        public RepositoryArgs(string message)
        {
            Guid = Guid.NewGuid();
            Message = message;
            DateTime = DateTime.UtcNow;
        }

        public RepositoryArgs(Guid guid, string message)
        {
            Guid = guid;
            Message = message;
            DateTime = DateTime.UtcNow;
        }
    }
}
