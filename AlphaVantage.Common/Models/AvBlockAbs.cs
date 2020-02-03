using System;

namespace AlphaVantage.Common.Models
{
    public abstract class AvBlockAbs<T> : IAvBlock<T> where T : class
    {
        // as JSON this is the name of the block
        [AvPropertyName(ExtractPropertyName = "block day")]
        public DateTime TimeStamp { get; set; }

        public virtual T MemberClone()
        {
            return (T)this.MemberwiseClone();
        }
    }

    public interface IAvBlock<T> 
    {
        DateTime TimeStamp { get; set; }

        T MemberClone();

    }
}
