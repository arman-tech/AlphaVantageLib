using System;

namespace AlphaVantage.DataAccess.Interfaces
{
    public interface IContext<out TProvider, out TDatabase> : IDisposable
    {
        TProvider GetProvider();
        TDatabase GetDatabase();

        bool SetDatabase(string databaseName);
    }
}
