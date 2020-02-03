using AlphaVantage.DataAccess.Interfaces;
using Autofac.Features.Indexed;

namespace AlphaVantage.Utilities.Common
{
    public class AvRepositoryFactory : IAvRepositoryFactory
    {
        private readonly IIndex<string, IRepositoryAnchor> _services;

        public AvRepositoryFactory(IIndex<string, IRepositoryAnchor> services)
        {
            _services = services;
        }

        public IRepositoryAnchor GetInstance(string type)
        {
            return _services[type];
        }
    }
}
