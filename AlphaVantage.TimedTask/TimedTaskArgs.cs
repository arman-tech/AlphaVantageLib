using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaVantage.TimedTask
{
    public class TimedTaskArgs : EventArgs
    {
        public enum TimeTaskType { Info = 0, Error };

        /// <summary>
        /// Gets the execution identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Id { get; private set; }

        /// <summary>
        /// Gets the URI identifier. 
        /// </summary>
        /// <value>
        /// The URI identifier.
        /// </value>
        public Guid UriId { get; private set; }

        public string Uri { get; private set; }
        public string Country { get; private set; }
        public TimeTaskType Type { get; private set; }
        public string Message { get; set; }


        public TimedTaskArgs(Guid id, Guid uriId, string uri, string stockCountry, TimeTaskType type, string message)
        {
            this.Id = id;
            this.Uri = uri;
            this.UriId = uriId;
            this.Country = stockCountry;
            this.Type = type;
            this.Message = message; 
        }

        public TimedTaskArgs(Guid id, TimeTaskType type, string message)
        {
            this.Id = id;
            this.UriId = Guid.Empty;
            this.Uri = string.Empty;
            this.Country = string.Empty;
            this.Type = type;
            this.Message = message;

        }
    }
}
