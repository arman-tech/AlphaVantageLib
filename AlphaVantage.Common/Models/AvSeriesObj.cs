using System.Collections.Generic;

namespace AlphaVantage.Common.Models
{
    public abstract class AvSeriesObj<T, K, X> : IAvSeriesObj<T,K,X>    where T : class 
                                                                        where K : IAvMetaData<K>
                                                                        where X: IAvBlock<X>
    {
        public virtual K MetaData { get; set; }
        public virtual IList<X> TimeSeries { get; set; }

        public virtual T MemberClone()
        {
            return (T)this.MemberwiseClone();
        }
    }

    public interface IAvSeriesObj<T,K,X> where T : class
                                                where K : IAvMetaData<K>
                                                where X : IAvBlock<X>
    {
        K MetaData { get; set; }
        IList<X> TimeSeries { get; set; }

        T MemberClone();
    }
}
