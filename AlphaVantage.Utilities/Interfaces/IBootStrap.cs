using Autofac;

namespace AlphaVantage.Utilities.Interfaces
{
    public interface IBootStrap
    {
        IContainer Build();
    }
}