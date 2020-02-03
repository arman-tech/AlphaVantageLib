using System;
using System.Collections.Generic;

namespace AlphaVantage.Common
{
    public class Response
    {
        public Guid Id { get; private set; }
        public DateTime LastUpdated { get; private set; }
        public IDictionary<SeverityType, IEnumerable<string>> Reports { get; protected set; }
        public SeverityType Severity { get; private set; }

        public Response(IDictionary<SeverityType, IEnumerable<string>> reports, SeverityType severity)
        {
            this.Reports = reports;
            this.Severity = severity;
            this.Id = Guid.NewGuid();
            this.LastUpdated = DateTime.UtcNow;
        }

    }



    public enum SeverityType
    {
        Unknown = 0,
        Success = 1,
        Error = 2,
        Info = 3,
    }
}
