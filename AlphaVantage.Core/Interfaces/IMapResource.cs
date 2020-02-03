using Newtonsoft.Json.Linq;

namespace AlphaVantage.Core.Interfaces
{
    public interface IMapResourceAnchor { }

    public interface IMapResource<T> : IMapResourceAnchor where T : class, new()
    {
        //event EventHandler<Response> Begin;
        //event EventHandler<Response> Error;
        //event EventHandler<Response> Complete;

        T Map(JObject remoteResource, string uri);
        T Data { get; }
    }
}
