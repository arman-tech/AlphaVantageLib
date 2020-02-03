using AlphaVantage.Common;
using AlphaVantage.Core.TimeSeries.Daily;
using AlphaVantage.Core.TimeSeries.DailyAdjusted;
using AlphaVantage.Core.TimeSeries.IntraDay;
using AlphaVantage.Core.TimeSeries.Monthly;
using AlphaVantage.Core.TimeSeries.MonthlyAdjusted;
using AlphaVantage.Core.TimeSeries.Weekly;
using AlphaVantage.Core.TimeSeries.WeeklyAdjusted;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Linq;
using System.Text;
using Xunit;

namespace AlphaVantage.Core.Test
{
    public class TimeSeriesMapResourceSnapshotTest
    {
        const string directoryPath = @"..\..\..\snapshots\30-nov-2019";
        const string fileFormat = @"{0}-{1}.json";

        public string ReadFileToString(string path, string name)
        {
            return File.ReadAllText($"{path}\\{name}", Encoding.UTF8);
        }

        public JObject GetSnapshot(string url)
        {
            var functionName = CommonHelper.UriQuery(url)?["function"].ToLower();
            var symbolName = CommonHelper.UriQuery(url)?["symbol"].ToLower();

            var content = ReadFileToString(directoryPath,
                string.Format(fileFormat, functionName, symbolName));

            return JObject.Parse(content);
        }

        [Fact]
        public void ShouldMapTimeSeriesDailyAdjResources()
        {
            var url = "https://www.alphavantage.co/query?function=TIME_SERIES_DAILY_ADJUSTED&symbol=MSFT&outputsize=full&apikey=demo";

            var jObj = GetSnapshot(url);

            var process = new AvDailyAdjTimeSeriesProcess();
            process.Map(jObj, url);

            Assert.NotNull(process);
            Assert.NotNull(process.Data);
            Assert.NotNull(process.Data.TimeSeries);
            Assert.True(process.Data.TimeSeries.Count() > 0);

            Assert.True(process.Data.MetaData.LastRefreshed > DateTime.MinValue);
            Assert.True(process.Data.MetaData.Symbol == "MSFT");
            Assert.Equal(AvOutputSizeEnum.Full, process.Data.MetaData.OutputSize);
            Assert.True(process.Data.MetaData.TimeZone.StandardName == AvTimeZoneConvertor.AvTimeZone("US/Eastern Time").StandardName);

        }

        [Fact]
        public void ShouldMapTimeSeriesDailyResources()
        {
            var url = "https://www.alphavantage.co/query?function=TIME_SERIES_DAILY&symbol=MSFT&outputsize=full&apikey=demo";
            var jObj = GetSnapshot(url);

            var process = new AvDailyTimeSeriesProcess();
            process.Map(jObj, url);

            Assert.NotNull(process);
            Assert.NotNull(process.Data);
            Assert.NotNull(process.Data.TimeSeries);
            Assert.True(process.Data.TimeSeries.Count() > 0);

            Assert.True(process.Data.MetaData.LastRefreshed > DateTime.MinValue);
            Assert.True(process.Data.MetaData.Symbol == "MSFT");
            Assert.Equal(AvOutputSizeEnum.Full, process.Data.MetaData.OutputSize);
            Assert.True(process.Data.MetaData.TimeZone.StandardName == AvTimeZoneConvertor.AvTimeZone("US/Eastern Time").StandardName);
        }

        [Fact]
        public void ShouldMapTimeSeriesIntraDayResources()
        {
            var url = "https://www.alphavantage.co/query?function=TIME_SERIES_INTRADAY&symbol=MSFT&interval=5min&outputsize=full&apikey=demo";
            var jObj = GetSnapshot(url);

            var process = new AvIntraDayTimeSeriesProcess();
            process.Map(jObj, url);

            Assert.NotNull(process);
            Assert.NotNull(process.Data);
            Assert.NotNull(process.Data.TimeSeries);
            Assert.True(process.Data.TimeSeries.Count() > 0);

            Assert.True(process.Data.MetaData.LastRefreshed > DateTime.MinValue);
            Assert.True(process.Data.MetaData.Symbol == "MSFT");
            Assert.Equal(AvOutputSizeEnum.Full, process.Data.MetaData.OutputSize);
            Assert.True(process.Data.MetaData.TimeZone.StandardName == AvTimeZoneConvertor.AvTimeZone("US/Eastern Time").StandardName);
            Assert.Equal(AvIntervalEnum.FiveMin, process.Data.MetaData.Interval);

        }

        [Fact]
        public void ShouldMapTimeSeriesMonthlyResources()
        {
            var url = "https://www.alphavantage.co/query?function=TIME_SERIES_MONTHLY&symbol=MSFT&apikey=demo";
            var jObj = GetSnapshot(url);

            var process = new AvMonthlyTimeSeriesProcess();
            process.Map(jObj, url);

            Assert.NotNull(process);
            Assert.NotNull(process.Data);
            Assert.NotNull(process.Data.TimeSeries);
            Assert.True(process.Data.TimeSeries.Count() > 0);

            Assert.True(process.Data.MetaData.LastRefreshed > DateTime.MinValue);
            Assert.True(process.Data.MetaData.Symbol == "MSFT");
            Assert.True(process.Data.MetaData.TimeZone.StandardName == AvTimeZoneConvertor.AvTimeZone("US/Eastern Time").StandardName);

        }

        [Fact]
        public void ShouldMapTimeSeriesMonthlyAdjustedResources()
        {
            var url = "https://www.alphavantage.co/query?function=TIME_SERIES_MONTHLY_ADJUSTED&symbol=MSFT&apikey=demo";
            var jObj = GetSnapshot(url);

            var process = new AvMonthlyAdjTimeSeriesProcess();
            process.Map(jObj, url);

            Assert.NotNull(process);
            Assert.NotNull(process.Data);
            Assert.NotNull(process.Data.TimeSeries);
            Assert.True(process.Data.TimeSeries.Count() > 0);

            Assert.True(process.Data.MetaData.LastRefreshed > DateTime.MinValue);
            Assert.True(process.Data.MetaData.Symbol == "MSFT");
            Assert.True(process.Data.MetaData.TimeZone.StandardName == AvTimeZoneConvertor.AvTimeZone("US/Eastern Time").StandardName);

        }

        [Fact]
        public void ShouldMapTimeSeriesWeeklyResources()
        {
            var url = "https://www.alphavantage.co/query?function=TIME_SERIES_WEEKLY&symbol=MSFT&apikey=demo";
            var jObj = GetSnapshot(url);

            var process = new AvWeeklyTimeSeriesProcess();
            process.Map(jObj, url);

            Assert.NotNull(process);
            Assert.NotNull(process.Data);
            Assert.NotNull(process.Data.TimeSeries);
            Assert.True(process.Data.TimeSeries.Count() > 0);

            Assert.True(process.Data.MetaData.LastRefreshed > DateTime.MinValue);
            Assert.True(process.Data.MetaData.Symbol == "MSFT");
            Assert.True(process.Data.MetaData.TimeZone.StandardName == AvTimeZoneConvertor.AvTimeZone("US/Eastern Time").StandardName);

        }

        [Fact]
        public void ShouldMapTimeSeriesWeeklyAdjustedResources()
        {
            var url = "https://www.alphavantage.co/query?function=TIME_SERIES_WEEKLY_ADJUSTED&symbol=MSFT&apikey=demo";
            var jObj = GetSnapshot(url);

            var process = new AvWeeklyAdjTimeSeriesProcess();
            process.Map(jObj, url);

            Assert.NotNull(process);
            Assert.NotNull(process.Data);
            Assert.NotNull(process.Data.TimeSeries);
            Assert.True(process.Data.TimeSeries.Count() > 0);

            Assert.True(process.Data.MetaData.LastRefreshed > DateTime.MinValue);
            Assert.True(process.Data.MetaData.Symbol == "MSFT");
            Assert.True(process.Data.MetaData.TimeZone.StandardName == AvTimeZoneConvertor.AvTimeZone("US/Eastern Time").StandardName);

        }
    }
}
