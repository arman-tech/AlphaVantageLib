using System;
using System.Collections.Specialized;
using System.Net.Http;
using System.Net.Http.Headers;

namespace AlphaVantage.Common
{
    public class CommonHelper
    {
        public static string GetRepositoryKeyedName(AvFunctionEnum funcEnum, AvIntervalEnum intervalEnum)
        {
            return $"{funcEnum.Name}-{intervalEnum.Name}";
        }

        public static NameValueCollection UriQuery(string uri)
        {
            NameValueCollection queryDictionary;

            using (var client = new HttpClient())
            {
                Uri uriObj = new Uri(uri);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(CommonRes.MediaType));

                // get the query parameters
                string queryString = uriObj.Query;
                queryDictionary = System.Web.HttpUtility.ParseQueryString(queryString);
            }

            return queryDictionary;
        }

        public static AvIntervalEnum ConvertToAvIntervalEnum(string uri)
        {
            // sanity check
            if (string.IsNullOrWhiteSpace(uri))
            {
                throw new ArgumentNullException(nameof(ConvertToAvIntervalEnum));
            }

            var interval = UriQuery(uri)?[CommonRes.IntervalFunctionTagName];
            return AvIntervalEnum.FromName(interval);
        }

    }
}
