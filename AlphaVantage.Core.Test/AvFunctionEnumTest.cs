using Xunit;
using Autofac;
using AlphaVantage.Core.Interfaces;
using AlphaVantage.Common;
using AlphaVantage.Core.Common;
using AlphaVantage.Utilities.Common;

namespace AlphaVantage.Core.Test
{
    public class AvFunctionEnumTest
    {

        public AvFunctionEnumTest()
        {
        }


        [Theory]
        [ClassData(typeof(AvFunctionEnumCheckTestData))]
        public void ShouldReturnValidAvFunctionEnum(AvFunctionEnum expectedAvFuncEnum, string url)
        {
            var funcEnum = GetFuncEnum(url);

            Assert.Equal<AvFunctionEnum>(expectedAvFuncEnum, funcEnum);
        }

        protected AvFunctionEnum GetFuncEnum(string uri)
        {
            var function = CommonHelper.UriQuery(uri)?[CommonRes.UriFunctionTagName];
            return AvFunctionEnum.FromName(function);
        }

        protected class AvFunctionEnumCheckTestData: TheoryData<AvFunctionEnum, string>
        {
            public AvFunctionEnumCheckTestData()
            {
                Add(AvFunctionEnum.RSI, 
                    "https://www.alphavantage.co/query?function=RSI&symbol=MSFT&interval=weekly&time_period=10&series_type=open&apikey=demo");
                Add(AvFunctionEnum.BBANDS,
                    "https://www.alphavantage.co/query?function=BBANDS&symbol=MSFT&interval=weekly&time_period=5&series_type=close&nbdevup=3&nbdevdn=3&apikey=demo");
                Add(AvFunctionEnum.Daily,
                    "https://www.alphavantage.co/query?function=TIME_SERIES_DAILY&symbol=MSFT&apikey=demo");
                Add(AvFunctionEnum.DailyAdjusted,
                    "https://www.alphavantage.co/query?function=TIME_SERIES_DAILY_ADJUSTED&symbol=MSFT&apikey=demo");
                Add(AvFunctionEnum.InteraDay,
                    "https://www.alphavantage.co/query?function=TIME_SERIES_INTRADAY&symbol=MSFT&interval=5min&apikey=demo");
                Add(AvFunctionEnum.Monthly,
                    "https://www.alphavantage.co/query?function=TIME_SERIES_MONTHLY&symbol=MSFT&apikey=demo");
                Add(AvFunctionEnum.MonthlyAdjusted, 
                    "https://www.alphavantage.co/query?function=TIME_SERIES_MONTHLY_ADJUSTED&symbol=MSFT&apikey=demo");
                Add(AvFunctionEnum.Weekly, 
                    "https://www.alphavantage.co/query?function=TIME_SERIES_WEEKLY&symbol=MSFT&apikey=demo");
                Add(AvFunctionEnum.WeeklyAdjusted, 
                    "https://www.alphavantage.co/query?function=TIME_SERIES_WEEKLY_ADJUSTED&symbol=MSFT&apikey=demo");
            }
        }

    }
}
