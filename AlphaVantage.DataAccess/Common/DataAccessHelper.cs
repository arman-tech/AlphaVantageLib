using AlphaVantage.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using AlphaVantage.Common;

namespace AlphaVantage.DataAccess.Common
{
    public class DataAccessHelper
    {
        public static IRepositoryAnchor ConvertToRepositoryResource(string uri, IAvRepositoryFactory factoryMethod)
        {
            // sanity check
            if (string.IsNullOrWhiteSpace(uri) || factoryMethod == null)
            {
                throw new ArgumentNullException(nameof(ConvertToRepositoryResource));
            }

            var function = CommonHelper.UriQuery(uri)?[CommonRes.UriFunctionTagName];
            var funcEnum = AvFunctionEnum.FromName(function);

            var interval = CommonHelper.UriQuery(uri)?[CommonRes.IntervalFunctionTagName];
            var intervalEnum = AvIntervalEnum.Default;

            if (!string.IsNullOrWhiteSpace(interval))
            {
                // convert to AvIntervalEnum
                intervalEnum = AvIntervalEnum.FromName(interval);
            }

            return factoryMethod.GetInstance(CommonHelper.GetRepositoryKeyedName(funcEnum, intervalEnum));
        }

    }
}
