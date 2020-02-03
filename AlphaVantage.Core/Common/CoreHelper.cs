using AlphaVantage.Common;
using AlphaVantage.Core.Exceptions;
using AlphaVantage.Core.Interfaces;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Specialized;
using System.Net.Http;
using System.Net.Http.Headers;

namespace AlphaVantage.Core.Common
{
    public class CoreHelper
    {
        public static JObject CaptureRemoteJson(string uri)
        {
            using (var client = new HttpClient())
            {
                Uri uriObj = new Uri(uri);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(CommonProcessRes.MediaType));

                var response = client.GetAsync(uriObj).Result;
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;

                    return JObject.Parse(result);
                }
            }

            return default;
        }

        public static IMapResourceAnchor ConvertToMapResource(string uri, IAvMapFactory factoryMethod)
        {
            // sanity check
            if (string.IsNullOrWhiteSpace(uri) || factoryMethod == null)
            {
                throw new ArgumentNullException(nameof(ConvertToMapResource));
            }

            var function = CommonHelper.UriQuery(uri)?[CommonRes.UriFunctionTagName];
            var funcEnum = AvFunctionEnum.FromName(function);

            return factoryMethod.GetInstance(funcEnum);
        }                   

        public static bool HasKey(JObject obj, string key)
        {
            return ((Newtonsoft.Json.Linq.JProperty)obj.First).Name.Equals(key, StringComparison.InvariantCultureIgnoreCase);
        }

        public static string GetFirstValue(JObject obj)
        {
            return ((Newtonsoft.Json.Linq.JProperty)obj.First).Value.ToString();
        }

        public static bool AvDownloadApiCallLimitException(Exception e)
        {
            return e is AvDownloadException || e is AvApiCallLimitReachedException;
        }

    }
}
