using AlphaVantage.Common;
using AlphaVantage.Core.Interfaces;
using AlphaVantage.Core.TimeSeries.Daily;
using AlphaVantage.Core.TimeSeries.DailyAdjusted;
using AlphaVantage.Core.TimeSeries.IntraDay;
using AlphaVantage.Core.TimeSeries.Monthly;
using AlphaVantage.Core.TimeSeries.MonthlyAdjusted;
using AlphaVantage.Core.TimeSeries.Weekly;
using AlphaVantage.Core.TimeSeries.WeeklyAdjusted;
using AlphaVantage.Utilities.Common;
using Autofac;
using System;
using System.Linq;
using Xunit;

namespace AlphaVantage.Core.Test
{
    public class TimeSeriesMapResourceIntegrationTest
    {
        [Fact]
        public void ShouldMapTimeSeriesDailyAdjResources()
        {
            var url = "https://www.alphavantage.co/query?function=TIME_SERIES_DAILY_ADJUSTED&symbol=MSFT&outputsize=full&apikey=demo";
            var container = new AutoFacBootStrapper()
                                .AlphaVantageCoreSetup()
                                .Build();
            var downloader = container.Resolve<IDownloadWithRetry>();

            var jObj = downloader.DownloadWithRetries(url);

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
            var url = "https://www.alphavantage.co/query?function=TIME_SERIES_DAILY&symbol=MSFT&apikey=demo";
            var container = new AutoFacBootStrapper()
                                .AlphaVantageCoreSetup()
                                .Build();
            var downloader = container.Resolve<IDownloadWithRetry>();

            var jObj = downloader.DownloadWithRetries(url);

            var process = new AvDailyTimeSeriesProcess();
            process.Map(jObj, url);

            Assert.NotNull(process);
            Assert.NotNull(process.Data);
            Assert.NotNull(process.Data.TimeSeries);
            Assert.True(process.Data.TimeSeries.Count() > 0);

            Assert.True(process.Data.MetaData.LastRefreshed > DateTime.MinValue);
            Assert.True(process.Data.MetaData.Symbol == "MSFT");
            Assert.Equal(AvOutputSizeEnum.Compact, process.Data.MetaData.OutputSize);
            Assert.True(process.Data.MetaData.TimeZone.StandardName == AvTimeZoneConvertor.AvTimeZone("US/Eastern Time").StandardName);
        }

        [Fact]
        public void ShouldMapTimeSeriesIntraDayResources()
        {
            var url = "https://www.alphavantage.co/query?function=TIME_SERIES_INTRADAY&symbol=MSFT&interval=5min&apikey=demo";
            var container = new AutoFacBootStrapper()
                                .AlphaVantageCoreSetup()
                                .Build();
            var downloader = container.Resolve<IDownloadWithRetry>();

            var jObj = downloader.DownloadWithRetries(url);

            var process = new AvIntraDayTimeSeriesProcess();
            process.Map(jObj, url);

            Assert.NotNull(process);
            Assert.NotNull(process.Data);
            Assert.NotNull(process.Data.TimeSeries);
            Assert.True(process.Data.TimeSeries.Count() > 0);

            Assert.True(process.Data.MetaData.LastRefreshed > DateTime.MinValue);
            Assert.True(process.Data.MetaData.Symbol == "MSFT");
            Assert.Equal(AvOutputSizeEnum.Compact, process.Data.MetaData.OutputSize);
            Assert.True(process.Data.MetaData.TimeZone.StandardName == AvTimeZoneConvertor.AvTimeZone("US/Eastern Time").StandardName);
            Assert.Equal(AvIntervalEnum.FiveMin, process.Data.MetaData.Interval);

        }

        [Fact]
        public void ShouldMapTimeSeriesMonthlyResources()
        {
            var url = "https://www.alphavantage.co/query?function=TIME_SERIES_MONTHLY&symbol=MSFT&apikey=demo";
            var container = new AutoFacBootStrapper()
                                .AlphaVantageCoreSetup()
                                .Build();
            var downloader = container.Resolve<IDownloadWithRetry>();

            var jObj = downloader.DownloadWithRetries(url);

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
            var container = new AutoFacBootStrapper()
                                .AlphaVantageCoreSetup()
                                .Build();
            var downloader = container.Resolve<IDownloadWithRetry>();

            var jObj = downloader.DownloadWithRetries(url);

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
            var container = new AutoFacBootStrapper()
                                .AlphaVantageCoreSetup()
                                .Build();
            var downloader = container.Resolve<IDownloadWithRetry>();

            var jObj = downloader.DownloadWithRetries(url);

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
            var container = new AutoFacBootStrapper()
                                .AlphaVantageCoreSetup()
                                .Build();
            var downloader = container.Resolve<IDownloadWithRetry>();

            var jObj = downloader.DownloadWithRetries(url);

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
