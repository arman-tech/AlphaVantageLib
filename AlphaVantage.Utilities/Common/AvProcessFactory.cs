
using AlphaVantage.Common;
using AlphaVantage.Core.Interfaces;
using Autofac.Features.Indexed;

namespace AlphaVantage.Utilities.Common
{
    public class AvMapFactory : IAvMapFactory
    {
        private readonly IIndex<AvFunctionEnum, IMapResourceAnchor> _services;

        public AvMapFactory(IIndex<AvFunctionEnum, IMapResourceAnchor> services)
        {
            _services = services;
        }

        public IMapResourceAnchor GetInstance(AvFunctionEnum functionType)
        {
            return _services[functionType];
        }

    }
}
