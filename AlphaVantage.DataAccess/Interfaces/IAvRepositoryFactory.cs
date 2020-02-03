
namespace AlphaVantage.DataAccess.Interfaces
{
    public interface IAvRepositoryFactory
    {
        IRepositoryAnchor GetInstance(string type);
    }
}
