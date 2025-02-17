﻿using Xunit;
using Autofac;
using AlphaVantage.Core.Interfaces;
using AlphaVantage.Utilities.Common;

namespace AlphaVantage.Core.Test
{
    public class RetrieveRemoteResourceIntegrationTest
    {
        const string FailureResponse = @"""Information"": ""The **demo** API key is for demo purposes only. Please claim your free API key at (https://www.alphavantage.co/support/#api-key) to explore our full API offerings. It takes fewer than 20 seconds, and we are committed to making it free forever.""";


        [Theory]
        [InlineData("https://www.alphavantage.co/query?function=TIME_SERIES_DAILY_ADJUSTED&symbol=MSFT&outputsize=full&apikey=demo")]
        [InlineData("https://www.alphavantage.co/query?function=TIME_SERIES_DAILY&symbol=MSFT&apikey=demo")]
        [InlineData("https://www.alphavantage.co/query?function=TIME_SERIES_DAILY_ADJUSTED&symbol=MSFT&apikey=demo")]
        [InlineData("https://www.alphavantage.co/query?function=TIME_SERIES_INTRADAY&symbol=MSFT&interval=5min&apikey=demo")]
        [InlineData("https://www.alphavantage.co/query?function=TIME_SERIES_MONTHLY&symbol=MSFT&apikey=demo")]
        [InlineData("https://www.alphavantage.co/query?function=TIME_SERIES_MONTHLY_ADJUSTED&symbol=MSFT&apikey=demo")]
        [InlineData("https://www.alphavantage.co/query?function=TIME_SERIES_WEEKLY&symbol=MSFT&apikey=demo")]
        [InlineData("https://www.alphavantage.co/query?function=TIME_SERIES_WEEKLY_ADJUSTED&symbol=MSFT&apikey=demo")]

        [InlineData("https://www.alphavantage.co/query?function=SMA&symbol=MSFT&interval=weekly&time_period=10&series_type=open&apikey=demo")]
        [InlineData("https://www.alphavantage.co/query?function=EMA&symbol=MSFT&interval=weekly&time_period=10&series_type=open&apikey=demo")]
        [InlineData("https://www.alphavantage.co/query?function=WMA&symbol=MSFT&interval=weekly&time_period=10&series_type=open&apikey=demo")]
        [InlineData("https://www.alphavantage.co/query?function=DEMA&symbol=MSFT&interval=weekly&time_period=10&series_type=open&apikey=demo")]
        [InlineData("https://www.alphavantage.co/query?function=TEMA&symbol=MSFT&interval=weekly&time_period=10&series_type=open&apikey=demo")]
        [InlineData("https://www.alphavantage.co/query?function=TRIMA&symbol=MSFT&interval=weekly&time_period=10&series_type=open&apikey=demo")]
        [InlineData("https://www.alphavantage.co/query?function=KAMA&symbol=MSFT&interval=weekly&time_period=10&series_type=open&apikey=demo")]
        [InlineData("https://www.alphavantage.co/query?function=MAMA&symbol=MSFT&interval=daily&series_type=close&fastlimit=0.02&apikey=demo")]
        [InlineData("https://www.alphavantage.co/query?function=VWAP&symbol=MSFT&interval=15min&apikey=demo")]
        [InlineData("https://www.alphavantage.co/query?function=T3&symbol=MSFT&interval=weekly&time_period=10&series_type=open&apikey=demo")]
        [InlineData("https://www.alphavantage.co/query?function=MACD&symbol=MSFT&interval=daily&series_type=open&apikey=demo")]
        [InlineData("https://www.alphavantage.co/query?function=MACDEXT&symbol=MSFT&interval=daily&series_type=open&apikey=demo")]
        [InlineData("https://www.alphavantage.co/query?function=STOCH&symbol=MSFT&interval=daily&apikey=demo")]
        [InlineData("https://www.alphavantage.co/query?function=STOCHF&symbol=MSFT&interval=daily&apikey=demo")]
        [InlineData("https://www.alphavantage.co/query?function=RSI&symbol=MSFT&interval=weekly&time_period=10&series_type=open&apikey=demo")]
        [InlineData("https://www.alphavantage.co/query?function=STOCHRSI&symbol=MSFT&interval=daily&time_period=10&series_type=close&fastkperiod=6&fastdmatype=1&apikey=demo")]
        [InlineData("https://www.alphavantage.co/query?function=WILLR&symbol=MSFT&interval=daily&time_period=10&apikey=demo")]
        [InlineData("https://www.alphavantage.co/query?function=ADX&symbol=MSFT&interval=daily&time_period=10&apikey=demo")]
        [InlineData("https://www.alphavantage.co/query?function=ADXR&symbol=MSFT&interval=daily&time_period=10&apikey=demo")]
        [InlineData("https://www.alphavantage.co/query?function=APO&symbol=MSFT&interval=daily&series_type=close&fastperiod=10&matype=1&apikey=demo")]
        [InlineData("https://www.alphavantage.co/query?function=PPO&symbol=MSFT&interval=daily&series_type=close&fastperiod=10&matype=1&apikey=demo")]
        [InlineData("https://www.alphavantage.co/query?function=MOM&symbol=MSFT&interval=daily&time_period=10&series_type=close&apikey=demo")]
        [InlineData("https://www.alphavantage.co/query?function=BOP&symbol=MSFT&interval=daily&apikey=demo")]
        [InlineData("https://www.alphavantage.co/query?function=CCI&symbol=MSFT&interval=daily&time_period=10&apikey=demo")]
        [InlineData("https://www.alphavantage.co/query?function=CMO&symbol=MSFT&interval=weekly&time_period=10&series_type=close&apikey=demo")]
        [InlineData("https://www.alphavantage.co/query?function=ROC&symbol=MSFT&interval=weekly&time_period=10&series_type=close&apikey=demo")]
        [InlineData("https://www.alphavantage.co/query?function=ROCR&symbol=MSFT&interval=daily&time_period=10&series_type=close&apikey=demo")]
        [InlineData("https://www.alphavantage.co/query?function=AROON&symbol=MSFT&interval=daily&time_period=14&apikey=demo")]
        [InlineData("https://www.alphavantage.co/query?function=AROONOSC&symbol=MSFT&interval=daily&time_period=10&apikey=demo")]
        [InlineData("https://www.alphavantage.co/query?function=MFI&symbol=MSFT&interval=weekly&time_period=10&apikey=demo")]
        [InlineData("https://www.alphavantage.co/query?function=TRIX&symbol=MSFT&interval=daily&time_period=10&series_type=close&apikey=demo")]
        [InlineData("https://www.alphavantage.co/query?function=ULTOSC&symbol=MSFT&interval=daily&timeperiod1=8&apikey=demo")]
        [InlineData("https://www.alphavantage.co/query?function=DX&symbol=MSFT&interval=daily&time_period=10&apikey=demo")]
        [InlineData("https://www.alphavantage.co/query?function=MINUS_DI&symbol=MSFT&interval=weekly&time_period=10&apikey=demo")]
        [InlineData("https://www.alphavantage.co/query?function=PLUS_DI&symbol=MSFT&interval=daily&time_period=10&apikey=demo")]
        [InlineData("https://www.alphavantage.co/query?function=MINUS_DM&symbol=MSFT&interval=daily&time_period=10&apikey=demo")]
        [InlineData("https://www.alphavantage.co/query?function=PLUS_DM&symbol=MSFT&interval=daily&time_period=10&apikey=demo")]
        [InlineData("https://www.alphavantage.co/query?function=BBANDS&symbol=MSFT&interval=weekly&time_period=5&series_type=close&nbdevup=3&nbdevdn=3&apikey=demo")]
        [InlineData("https://www.alphavantage.co/query?function=MIDPOINT&symbol=MSFT&interval=daily&time_period=10&series_type=close&apikey=demo")]
        [InlineData("https://www.alphavantage.co/query?function=MIDPRICE&symbol=MSFT&interval=daily&time_period=10&apikey=demo")]
        [InlineData("https://www.alphavantage.co/query?function=SAR&symbol=MSFT&interval=weekly&acceleration=0.05&maximum=0.25&apikey=demo")]
        [InlineData("https://www.alphavantage.co/query?function=TRANGE&symbol=MSFT&interval=daily&apikey=demo")]
        [InlineData("https://www.alphavantage.co/query?function=ATR&symbol=MSFT&interval=daily&time_period=14&apikey=demo")]
        [InlineData("https://www.alphavantage.co/query?function=NATR&symbol=MSFT&interval=weekly&time_period=14&apikey=demo")]
        [InlineData("https://www.alphavantage.co/query?function=AD&symbol=MSFT&interval=daily&apikey=demo")]
        [InlineData("https://www.alphavantage.co/query?function=ADOSC&symbol=MSFT&interval=daily&fastperiod=5&apikey=demo")]
        [InlineData("https://www.alphavantage.co/query?function=OBV&symbol=MSFT&interval=weekly&apikey=demo")]
        [InlineData("https://www.alphavantage.co/query?function=HT_TRENDLINE&symbol=MSFT&interval=daily&series_type=close&apikey=demo")]
        [InlineData("https://www.alphavantage.co/query?function=HT_SINE&symbol=MSFT&interval=daily&series_type=close&apikey=demo")]
        [InlineData("https://www.alphavantage.co/query?function=HT_TRENDMODE&symbol=MSFT&interval=weekly&series_type=close&apikey=demo")]
        [InlineData("https://www.alphavantage.co/query?function=HT_DCPERIOD&symbol=MSFT&interval=daily&series_type=close&apikey=demo")]
        [InlineData("https://www.alphavantage.co/query?function=HT_DCPHASE&symbol=MSFT&interval=daily&series_type=close&apikey=demo")]
        [InlineData("https://www.alphavantage.co/query?function=HT_PHASOR&symbol=MSFT&interval=weekly&series_type=close&apikey=demo")]
        public void ShouldDownloadAlphaVantageJsonObj(string url)
        {
            var container = new AutoFacBootStrapper()
                                .AlphaVantageCoreSetup()
                                .Build();
            var downloader = container.Resolve<IDownloadWithRetry>();

            var jObj = downloader.DownloadWithRetries(url);

            Assert.NotNull(jObj);
            Assert.NotEqual(FailureResponse, jObj.First.ToString());
            Assert.Equal(2, jObj.Count);
        }
    }
}
