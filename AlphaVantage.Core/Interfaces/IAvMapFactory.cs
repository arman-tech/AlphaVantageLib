using AlphaVantage.Common;

namespace AlphaVantage.Core.Interfaces
{
    public interface IAvMapFactory
    {
        IMapResourceAnchor GetInstance(AvFunctionEnum functionType);
    }
}
