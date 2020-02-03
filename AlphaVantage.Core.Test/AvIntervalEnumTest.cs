using AlphaVantage.Common;
using AlphaVantage.Core.Common;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace AlphaVantage.Core.Test
{
    public class AvIntervalEnumTest
    {

        [Theory]
        [ClassData(typeof(AvInteralEnumTestData))]
        public void ValidIntervalCheck(string uri, AvIntervalEnum expected)
        {
            var conversion = CommonHelper.ConvertToAvIntervalEnum(uri);
            Assert.Equal(expected, conversion);
        }

        [Fact]
        public void ShouldThrowArgumentException()
        {
            var uri = "https://www.alphavantage.co/query?function=TIME_SERIES_DAILY&symbol=MSFT&apikey=demo";

            Assert.Throws<InvalidOperationException>(() =>
                CommonHelper.ConvertToAvIntervalEnum(uri));
        }

        [Fact]
        public void EmptyUriShouldThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() =>
                CommonHelper.ConvertToAvIntervalEnum(string.Empty));
        }

        [Fact]
        public void NullUriShouldThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() =>
                CommonHelper.ConvertToAvIntervalEnum(null));
        }

        protected class AvInteralEnumTestData: TheoryData<string, AvIntervalEnum>
        {
            public AvInteralEnumTestData()
            {
                Add("https://www.alphavantage.co/query?function=SMA&symbol=MSFT&interval=1min&time_period=10&series_type=open&apikey=demo",
                    AvIntervalEnum.OneMin);
                Add("https://www.alphavantage.co/query?function=SMA&symbol=MSFT&interval=5min&time_period=10&series_type=open&apikey=demo",
                    AvIntervalEnum.FiveMin);
                Add("https://www.alphavantage.co/query?function=SMA&symbol=MSFT&interval=15min&time_period=10&series_type=open&apikey=demo",
                    AvIntervalEnum.FifteenMin);
                Add("https://www.alphavantage.co/query?function=SMA&symbol=MSFT&interval=30min&time_period=10&series_type=open&apikey=demo",
                    AvIntervalEnum.ThirtyMin);
                Add("https://www.alphavantage.co/query?function=SMA&symbol=MSFT&interval=60min&time_period=10&series_type=open&apikey=demo",
                    AvIntervalEnum.SixtyMin);
                Add("https://www.alphavantage.co/query?function=SMA&symbol=MSFT&interval=daily&time_period=10&series_type=open&apikey=demo",
                    AvIntervalEnum.Daily);
                Add("https://www.alphavantage.co/query?function=SMA&symbol=MSFT&interval=weekly&time_period=10&series_type=open&apikey=demo",
                    AvIntervalEnum.Weekly);
                Add("https://www.alphavantage.co/query?function=SMA&symbol=MSFT&interval=monthly&time_period=10&series_type=open&apikey=demo",
                    AvIntervalEnum.Monthly);
            }
        }
    }
}
