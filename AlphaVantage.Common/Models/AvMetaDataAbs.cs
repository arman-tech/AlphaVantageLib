using System;

namespace AlphaVantage.Common.Models
{
    public abstract class AvMetaDataAbs<T> : IAvMetaData<T> where T: class
    {
        public AvFunctionEnum Function { get; protected set; }
        public AvMetaDataTypeEnum Type { get; protected set; }
        public virtual string Symbol { get; set; }
        public virtual DateTime LastRefreshed { get; set; }
        public virtual TimeZoneInfo TimeZone { get; set; }

        public virtual T MemberClone()
        {
            return (T)this.MemberwiseClone();
        }
    }

    public interface IAvMetaData<T> 
    {
        AvFunctionEnum Function { get; }
        AvMetaDataTypeEnum Type { get; }
        string Symbol { get; set; }
        DateTime LastRefreshed { get; set; }
        TimeZoneInfo TimeZone { get; set; }

        T MemberClone();

    }
}
