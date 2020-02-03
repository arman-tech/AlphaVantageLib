using AlphaVantage.Core.Interfaces;
using AlphaVantage.DataAccess.Interfaces;
using Newtonsoft.Json.Linq;

namespace AlphaVantage.TimedTask.Runner.Interfaces
{
    public interface IHelm
    {
        T MapInvocation<T>(IMapResource<T> obj, JObject jobj, string ur) where T : class, new();

        void SaveInvocation<T>(IRepository<T> repository, T data) where T : class, new();

    }
}
