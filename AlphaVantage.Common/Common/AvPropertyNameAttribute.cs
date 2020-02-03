using System;

namespace AlphaVantage.Common
{
    public class AvPropertyNameAttribute : Attribute
    {
        public string ExtractPropertyName { get; set; }
    }
}
