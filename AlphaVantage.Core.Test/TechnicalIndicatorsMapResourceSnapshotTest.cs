using AlphaVantage.Common;
using AlphaVantage.Core.TechnicalIndicators.AD;
using AlphaVantage.Core.TechnicalIndicators.ADOSC;
using AlphaVantage.Core.TechnicalIndicators.ADX;
using AlphaVantage.Core.TechnicalIndicators.ADXR;
using AlphaVantage.Core.TechnicalIndicators.APO;
using AlphaVantage.Core.TechnicalIndicators.AROON;
using AlphaVantage.Core.TechnicalIndicators.AROONOSC;
using AlphaVantage.Core.TechnicalIndicators.ATR;
using AlphaVantage.Core.TechnicalIndicators.BBANDS;
using AlphaVantage.Core.TechnicalIndicators.BOP;
using AlphaVantage.Core.TechnicalIndicators.CCI;
using AlphaVantage.Core.TechnicalIndicators.CMO;
using AlphaVantage.Core.TechnicalIndicators.DEMA;
using AlphaVantage.Core.TechnicalIndicators.DX;
using AlphaVantage.Core.TechnicalIndicators.EMA;
using AlphaVantage.Core.TechnicalIndicators.HT_DCPERIOD;
using AlphaVantage.Core.TechnicalIndicators.HT_DCPHASE;
using AlphaVantage.Core.TechnicalIndicators.HT_PHASOR;
using AlphaVantage.Core.TechnicalIndicators.HT_SINE;
using AlphaVantage.Core.TechnicalIndicators.HT_TRENDLINE;
using AlphaVantage.Core.TechnicalIndicators.HT_TRENDMODE;
using AlphaVantage.Core.TechnicalIndicators.KAMA;
using AlphaVantage.Core.TechnicalIndicators.MACD;
using AlphaVantage.Core.TechnicalIndicators.MACDEXT;
using AlphaVantage.Core.TechnicalIndicators.MAMA;
using AlphaVantage.Core.TechnicalIndicators.MFI;
using AlphaVantage.Core.TechnicalIndicators.MIDPOINT;
using AlphaVantage.Core.TechnicalIndicators.MIDPRICE;
using AlphaVantage.Core.TechnicalIndicators.MINUS_DI;
using AlphaVantage.Core.TechnicalIndicators.MINUS_DM;
using AlphaVantage.Core.TechnicalIndicators.MOM;
using AlphaVantage.Core.TechnicalIndicators.NATR;
using AlphaVantage.Core.TechnicalIndicators.OBV;
using AlphaVantage.Core.TechnicalIndicators.PLUS_DI;
using AlphaVantage.Core.TechnicalIndicators.PLUS_DM;
using AlphaVantage.Core.TechnicalIndicators.PPO;
using AlphaVantage.Core.TechnicalIndicators.ROC;
using AlphaVantage.Core.TechnicalIndicators.ROCR;
using AlphaVantage.Core.TechnicalIndicators.RSI;
using AlphaVantage.Core.TechnicalIndicators.SAR;
using AlphaVantage.Core.TechnicalIndicators.SMA;
using AlphaVantage.Core.TechnicalIndicators.STOCH;
using AlphaVantage.Core.TechnicalIndicators.STOCHF;
using AlphaVantage.Core.TechnicalIndicators.STOCHRSI;
using AlphaVantage.Core.TechnicalIndicators.T3;
using AlphaVantage.Core.TechnicalIndicators.TEMA;
using AlphaVantage.Core.TechnicalIndicators.TRANGE;
using AlphaVantage.Core.TechnicalIndicators.TRIMA;
using AlphaVantage.Core.TechnicalIndicators.TRIX;
using AlphaVantage.Core.TechnicalIndicators.ULTOSC;
using AlphaVantage.Core.TechnicalIndicators.VWAP;
using AlphaVantage.Core.TechnicalIndicators.WILLR;
using AlphaVantage.Core.TechnicalIndicators.WMA;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Linq;
using System.Text;
using Xunit;

namespace AlphaVantage.Core.Test
{
    public class TechnicalIndicatorsMapResourceSnapshotTest
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
        public void ShouldMapRSIResources()
        {
            var url = "https://www.alphavantage.co/query?function=RSI&symbol=MSFT&interval=weekly&time_period=10&series_type=open&apikey=demo";
            var jObj = GetSnapshot(url);

            var process = new AvRSIProcess();
            process.Map(jObj, url);

            Assert.NotNull(process);
            Assert.NotNull(process.Data);
            Assert.NotNull(process.Data.MetaData);
            Assert.True(process.Data.MetaData.Symbol == "MSFT");
            Assert.True(process.Data.MetaData.Interval == AvIntervalEnum.Weekly);
            Assert.True(process.Data.MetaData.Indicator == "Relative Strength Index (RSI)");
            Assert.True(process.Data.MetaData.TimeZone.StandardName == AvTimeZoneConvertor.AvTimeZone("US/Eastern Time").StandardName);
            Assert.True(process.Data.MetaData.TimePeriod == 10);

            Assert.NotNull(process.Data.TimeSeries);
            Assert.True(process.Data.TimeSeries.Count() > 0);
        }

        [Fact]
        public void ShouldMapBBANDResources()
        {
            var url = "https://www.alphavantage.co/query?function=BBANDS&symbol=MSFT&interval=weekly&time_period=5&series_type=close&nbdevup=3&nbdevdn=3&apikey=demo";
            var jObj = GetSnapshot(url);

            var process = new AvBBANDSProcess();
            process.Map(jObj, url);

            Assert.NotNull(process);
            Assert.NotNull(process.Data);
            Assert.NotNull(process.Data.MetaData);
            Assert.True(process.Data.MetaData.Symbol == "MSFT");
            Assert.True(process.Data.MetaData.Interval == AvIntervalEnum.Weekly);
            Assert.True(process.Data.MetaData.Indicator == "Bollinger Bands (BBANDS)");
            Assert.True(process.Data.MetaData.TimeZone.StandardName == AvTimeZoneConvertor.AvTimeZone("US/Eastern Time").StandardName);
            Assert.True(process.Data.MetaData.TimePeriod == 5);
            Assert.True(process.Data.MetaData.DeviationMultiplierLowerBand == 3);
            Assert.True(process.Data.MetaData.DeviationMultiplierUpperBand == 3);
            Assert.True(process.Data.MetaData.MovingAvgType == AvMovingAverageTypeEnum.SMA);

            Assert.NotNull(process.Data.TimeSeries);
            Assert.True(process.Data.TimeSeries.Count() > 0);
        }

        [Fact]
        public void ShouldMapADResources()
        {
            var url = "https://www.alphavantage.co/query?function=AD&symbol=MSFT&interval=daily&apikey=demo";
            var jObj = GetSnapshot(url);

            var process = new AvADProcess();
            process.Map(jObj, url);

            Assert.NotNull(process);
            Assert.NotNull(process.Data);
            Assert.NotNull(process.Data.MetaData);
            Assert.True(process.Data.MetaData.LastRefreshed > DateTime.MinValue);
            Assert.True(process.Data.MetaData.Symbol == "MSFT");
            Assert.True(process.Data.MetaData.Interval == AvIntervalEnum.Daily);
            Assert.True(process.Data.MetaData.Indicator == "Chaikin A/D Line");
            Assert.True(process.Data.MetaData.TimeZone.StandardName == AvTimeZoneConvertor.AvTimeZone("US/Eastern Time").StandardName);

            Assert.NotNull(process.Data.TimeSeries);
            Assert.True(process.Data.TimeSeries.Count() > 0);
        }

        [Fact]
        public void ShouldMapADOSCResources()
        {
            var url = "https://www.alphavantage.co/query?function=ADOSC&symbol=MSFT&interval=daily&fastperiod=5&apikey=demo";
            var jObj = GetSnapshot(url);

            var process = new AvADOSCProcess();
            process.Map(jObj, url);

            Assert.NotNull(process);
            Assert.NotNull(process.Data);
            Assert.NotNull(process.Data.MetaData);
            Assert.True(process.Data.MetaData.LastRefreshed > DateTime.MinValue);
            Assert.True(process.Data.MetaData.Symbol == "MSFT");
            Assert.True(process.Data.MetaData.Interval == AvIntervalEnum.Daily);
            Assert.True(process.Data.MetaData.FastKPeriod == 5);
            Assert.True(process.Data.MetaData.SlowKPeriod == 10);
            Assert.True(process.Data.MetaData.Indicator == "Chaikin A/D Oscillator (ADOSC)");
            Assert.True(process.Data.MetaData.TimeZone.StandardName == AvTimeZoneConvertor.AvTimeZone("US/Eastern Time").StandardName);

            Assert.NotNull(process.Data.TimeSeries);
            Assert.True(process.Data.TimeSeries.Count() > 0);

        }

        [Fact]
        public void ShouldMapADXResources()
        {
            var url = "https://www.alphavantage.co/query?function=ADX&symbol=MSFT&interval=daily&time_period=10&apikey=demo";
            var jObj = GetSnapshot(url);

            var process = new AvADXProcess();
            process.Map(jObj, url);

            Assert.NotNull(process);
            Assert.NotNull(process.Data);
            Assert.NotNull(process.Data.MetaData);
            Assert.True(process.Data.MetaData.LastRefreshed > DateTime.MinValue);
            Assert.True(process.Data.MetaData.Symbol == "MSFT");
            Assert.True(process.Data.MetaData.Interval == AvIntervalEnum.Daily);
            Assert.True(process.Data.MetaData.Indicator == "Average Directional Movement Index (ADX)");
            Assert.True(process.Data.MetaData.TimeZone.StandardName == AvTimeZoneConvertor.AvTimeZone("US/Eastern Time").StandardName);
            Assert.True(process.Data.MetaData.TimePeriod == 10);

            Assert.NotNull(process.Data.TimeSeries);
            Assert.True(process.Data.TimeSeries.Count() > 0);

        }

        [Fact]
        public void ShouldMapADXRResources()
        {
            var url = "https://www.alphavantage.co/query?function=ADXR&symbol=MSFT&interval=daily&time_period=10&apikey=demo";
            var jObj = GetSnapshot(url);

            var process = new AvADXRProcess();
            process.Map(jObj, url);

            Assert.NotNull(process);
            Assert.NotNull(process.Data);
            Assert.NotNull(process.Data.MetaData);
            Assert.True(process.Data.MetaData.LastRefreshed > DateTime.MinValue);
            Assert.True(process.Data.MetaData.Symbol == "MSFT");
            Assert.True(process.Data.MetaData.Interval == AvIntervalEnum.Daily);
            Assert.True(process.Data.MetaData.Indicator == "Average Directional Movement Index Rating (ADXR)");
            Assert.True(process.Data.MetaData.TimeZone.StandardName == AvTimeZoneConvertor.AvTimeZone("US/Eastern Time").StandardName);
            Assert.True(process.Data.MetaData.TimePeriod == 10);

            Assert.NotNull(process.Data.TimeSeries);
            Assert.True(process.Data.TimeSeries.Count() > 0);
        }

        [Fact]
        public void ShouldMapAPOResources()
        {
            var url = "https://www.alphavantage.co/query?function=APO&symbol=MSFT&interval=daily&series_type=close&fastperiod=10&matype=1&apikey=demo";
            var jObj = GetSnapshot(url);

            var process = new AvAPOProcess();
            process.Map(jObj, url);

            Assert.NotNull(process);
            Assert.NotNull(process.Data);
            Assert.NotNull(process.Data.MetaData);
            Assert.True(process.Data.MetaData.LastRefreshed > DateTime.MinValue);
            Assert.True(process.Data.MetaData.Symbol == "MSFT");
            Assert.True(process.Data.MetaData.Interval == AvIntervalEnum.Daily);
            Assert.True(process.Data.MetaData.Indicator == "Absolute Price Oscillator (APO)");
            Assert.True(process.Data.MetaData.TimeZone.StandardName == AvTimeZoneConvertor.AvTimeZone("US/Eastern Time").StandardName);
            Assert.True(process.Data.MetaData.FastPeriod == 10);
            Assert.True(process.Data.MetaData.SlowPeriod == 26);
            Assert.True(process.Data.MetaData.MAType == AvMovingAverageTypeEnum.EMA);
            Assert.True(process.Data.MetaData.SeriesType == AvSeriesTypeEnum.Close);


            Assert.NotNull(process.Data.TimeSeries);
            Assert.True(process.Data.TimeSeries.Count() > 0);
        }

        [Fact]
        public void ShouldMapAROONResources()
        {
            var url = "https://www.alphavantage.co/query?function=AROON&symbol=MSFT&interval=daily&time_period=14&apikey=demo";
            var jObj = GetSnapshot(url);

            var process = new AvAROONProcess();
            process.Map(jObj, url);

            Assert.NotNull(process);
            Assert.NotNull(process.Data);
            Assert.NotNull(process.Data.MetaData);
            Assert.True(process.Data.MetaData.LastRefreshed > DateTime.MinValue);
            Assert.True(process.Data.MetaData.Symbol == "MSFT");
            Assert.True(process.Data.MetaData.Interval == AvIntervalEnum.Daily);
            Assert.True(process.Data.MetaData.Indicator == "Aroon (AROON)");
            Assert.True(process.Data.MetaData.TimeZone.StandardName == AvTimeZoneConvertor.AvTimeZone("US/Eastern Time").StandardName);
            Assert.True(process.Data.MetaData.TimePeriod == 14);


            Assert.NotNull(process.Data.TimeSeries);
            Assert.True(process.Data.TimeSeries.Count() > 0);
        }

        [Fact]
        public void ShouldMapAROONOSCResources()
        {
            var url = "https://www.alphavantage.co/query?function=AROONOSC&symbol=MSFT&interval=daily&time_period=10&apikey=demo";
            var jObj = GetSnapshot(url);

            var process = new AvAROONOSCProcess();
            process.Map(jObj, url);

            Assert.NotNull(process);
            Assert.NotNull(process.Data);
            Assert.NotNull(process.Data.MetaData);
            Assert.True(process.Data.MetaData.LastRefreshed > DateTime.MinValue);
            Assert.True(process.Data.MetaData.Symbol == "MSFT");
            Assert.True(process.Data.MetaData.Interval == AvIntervalEnum.Daily);
            Assert.True(process.Data.MetaData.Indicator == "Aroon Oscillator (AROONOSC)");
            Assert.True(process.Data.MetaData.TimeZone.StandardName == AvTimeZoneConvertor.AvTimeZone("US/Eastern Time").StandardName);
            Assert.True(process.Data.MetaData.TimePeriod == 10);


            Assert.NotNull(process.Data.TimeSeries);
            Assert.True(process.Data.TimeSeries.Count() > 0);
        }

        [Fact]
        public void ShouldMapATRResources()
        {
            var url = "https://www.alphavantage.co/query?function=ATR&symbol=MSFT&interval=daily&time_period=14&apikey=demo";
            var jObj = GetSnapshot(url);

            var process = new AvATRProcess();
            process.Map(jObj, url);

            Assert.NotNull(process);
            Assert.NotNull(process.Data);
            Assert.NotNull(process.Data.MetaData);
            Assert.True(process.Data.MetaData.LastRefreshed > DateTime.MinValue);
            Assert.True(process.Data.MetaData.Symbol == "MSFT");
            Assert.True(process.Data.MetaData.Interval == AvIntervalEnum.Daily);
            Assert.True(process.Data.MetaData.Indicator == "Average True Range (ATR)");
            Assert.True(process.Data.MetaData.TimeZone.StandardName == AvTimeZoneConvertor.AvTimeZone("US/Eastern Time").StandardName);
            Assert.True(process.Data.MetaData.TimePeriod == 14);


            Assert.NotNull(process.Data.TimeSeries);
            Assert.True(process.Data.TimeSeries.Count() > 0);
        }

        [Fact]
        public void ShouldMapBOPResources()
        {
            var url = "https://www.alphavantage.co/query?function=BOP&symbol=MSFT&interval=daily&apikey=demo";
            var jObj = GetSnapshot(url);

            var process = new AvBOPProcess();
            process.Map(jObj, url);

            Assert.NotNull(process);
            Assert.NotNull(process.Data);
            Assert.NotNull(process.Data.MetaData);
            Assert.True(process.Data.MetaData.LastRefreshed > DateTime.MinValue);
            Assert.True(process.Data.MetaData.Symbol == "MSFT");
            Assert.True(process.Data.MetaData.Interval == AvIntervalEnum.Daily);
            Assert.True(process.Data.MetaData.Indicator == "Balance Of Power (BOP)");
            Assert.True(process.Data.MetaData.TimeZone.StandardName == AvTimeZoneConvertor.AvTimeZone("US/Eastern Time").StandardName);


            Assert.NotNull(process.Data.TimeSeries);
            Assert.True(process.Data.TimeSeries.Count() > 0);
        }

        [Fact]
        public void ShouldMapCCIResources()
        {
            var url = "https://www.alphavantage.co/query?function=CCI&symbol=MSFT&interval=daily&time_period=10&apikey=demo";
            var jObj = GetSnapshot(url);

            var process = new AvCCIProcess();
            process.Map(jObj, url);

            Assert.NotNull(process);
            Assert.NotNull(process.Data);
            Assert.NotNull(process.Data.MetaData);
            Assert.True(process.Data.MetaData.LastRefreshed > DateTime.MinValue);
            Assert.True(process.Data.MetaData.Symbol == "MSFT");
            Assert.True(process.Data.MetaData.Interval == AvIntervalEnum.Daily);
            Assert.True(process.Data.MetaData.Indicator == "Commodity Channel Index (CCI)");
            Assert.True(process.Data.MetaData.TimeZone.StandardName == AvTimeZoneConvertor.AvTimeZone("US/Eastern Time").StandardName);
            Assert.True(process.Data.MetaData.TimePeriod == 10);

            Assert.NotNull(process.Data.TimeSeries);
            Assert.True(process.Data.TimeSeries.Count() > 0);
        }

        [Fact]
        public void ShouldMapCMOResources()
        {
            var url = "https://www.alphavantage.co/query?function=CMO&symbol=MSFT&interval=weekly&time_period=10&series_type=close&apikey=demo";
            var jObj = GetSnapshot(url);

            var process = new AvCMOProcess();
            process.Map(jObj, url);

            Assert.NotNull(process);
            Assert.NotNull(process.Data);
            Assert.NotNull(process.Data.MetaData);
            Assert.True(process.Data.MetaData.LastRefreshed > DateTime.MinValue);
            Assert.True(process.Data.MetaData.Symbol == "MSFT");
            Assert.True(process.Data.MetaData.Interval == AvIntervalEnum.Weekly);
            Assert.True(process.Data.MetaData.Indicator == "Chande Momentum Oscillator (CMO)");
            Assert.True(process.Data.MetaData.TimeZone.StandardName == AvTimeZoneConvertor.AvTimeZone("US/Eastern Time").StandardName);
            Assert.True(process.Data.MetaData.TimePeriod == 10);
            Assert.True(process.Data.MetaData.SeriesType == AvSeriesTypeEnum.Close);

            Assert.NotNull(process.Data.TimeSeries);
            Assert.True(process.Data.TimeSeries.Count() > 0);
        }

        [Fact]
        public void ShouldMapDEMAResources()
        {
            var url = "https://www.alphavantage.co/query?function=DEMA&symbol=MSFT&interval=weekly&time_period=10&series_type=open&apikey=demo";
            var jObj = GetSnapshot(url);

            var process = new AvDEMAProcess();
            process.Map(jObj, url);

            Assert.NotNull(process);
            Assert.NotNull(process.Data);
            Assert.NotNull(process.Data.MetaData);
            Assert.True(process.Data.MetaData.LastRefreshed > DateTime.MinValue);
            Assert.True(process.Data.MetaData.Symbol == "MSFT");
            Assert.True(process.Data.MetaData.Interval == AvIntervalEnum.Weekly);
            Assert.True(process.Data.MetaData.Indicator == "Double Exponential Moving Average (DEMA)");
            Assert.True(process.Data.MetaData.TimeZone.StandardName == AvTimeZoneConvertor.AvTimeZone("US/Eastern Time").StandardName);
            Assert.True(process.Data.MetaData.TimePeriod == 10);
            Assert.True(process.Data.MetaData.SeriesType == AvSeriesTypeEnum.Open);

            Assert.NotNull(process.Data.TimeSeries);
            Assert.True(process.Data.TimeSeries.Count() > 0);
        }

        [Fact]
        public void ShouldMapDXResources()
        {
            var url = "https://www.alphavantage.co/query?function=DX&symbol=MSFT&interval=daily&time_period=10&apikey=demo";
            var jObj = GetSnapshot(url);

            var process = new AvDXProcess();
            process.Map(jObj, url);

            Assert.NotNull(process);
            Assert.NotNull(process.Data);
            Assert.NotNull(process.Data.MetaData);
            Assert.True(process.Data.MetaData.LastRefreshed > DateTime.MinValue);
            Assert.True(process.Data.MetaData.Symbol == "MSFT");
            Assert.True(process.Data.MetaData.Interval == AvIntervalEnum.Daily);
            Assert.True(process.Data.MetaData.Indicator == "Directional Movement Index (DX)");
            Assert.True(process.Data.MetaData.TimeZone.StandardName == AvTimeZoneConvertor.AvTimeZone("US/Eastern Time").StandardName);
            Assert.True(process.Data.MetaData.TimePeriod == 10);

            Assert.NotNull(process.Data.TimeSeries);
            Assert.True(process.Data.TimeSeries.Count() > 0);
        }

        [Fact]
        public void ShouldMapEMAResources()
        {
            var url = "https://www.alphavantage.co/query?function=EMA&symbol=MSFT&interval=weekly&time_period=10&series_type=open&apikey=demo";
            var jObj = GetSnapshot(url);

            var process = new AvEMAProcess();
            process.Map(jObj, url);

            Assert.NotNull(process);
            Assert.NotNull(process.Data);
            Assert.NotNull(process.Data.MetaData);
            Assert.True(process.Data.MetaData.LastRefreshed > DateTime.MinValue);
            Assert.True(process.Data.MetaData.Symbol == "MSFT");
            Assert.True(process.Data.MetaData.Interval == AvIntervalEnum.Weekly);
            Assert.True(process.Data.MetaData.Indicator == "Exponential Moving Average (EMA)");
            Assert.True(process.Data.MetaData.TimeZone.StandardName == AvTimeZoneConvertor.AvTimeZone("US/Eastern Time").StandardName);
            Assert.True(process.Data.MetaData.TimePeriod == 10);
            Assert.True(process.Data.MetaData.SeriesType == AvSeriesTypeEnum.Open);

            Assert.NotNull(process.Data.TimeSeries);
            Assert.True(process.Data.TimeSeries.Count() > 0);
        }

        [Fact]
        public void ShouldMapHT_DCPERIODResources()
        {
            var url = "https://www.alphavantage.co/query?function=HT_DCPERIOD&symbol=MSFT&interval=daily&series_type=close&apikey=demo";
            var jObj = GetSnapshot(url);

            var process = new AvHT_DCPERIODProcess();
            process.Map(jObj, url);

            Assert.NotNull(process);
            Assert.NotNull(process.Data);
            Assert.NotNull(process.Data.MetaData);
            Assert.True(process.Data.MetaData.LastRefreshed > DateTime.MinValue);
            Assert.True(process.Data.MetaData.Symbol == "MSFT");
            Assert.True(process.Data.MetaData.Interval == AvIntervalEnum.Daily);
            Assert.True(process.Data.MetaData.Indicator == "Hilbert Transform - Dominant Cycle Period (HT_DCPERIOD)");
            Assert.True(process.Data.MetaData.TimeZone.StandardName == AvTimeZoneConvertor.AvTimeZone("US/Eastern Time").StandardName);
            Assert.True(process.Data.MetaData.SeriesType == AvSeriesTypeEnum.Close);

            Assert.NotNull(process.Data.TimeSeries);
            Assert.True(process.Data.TimeSeries.Count() > 0);
        }

        [Fact]
        public void ShouldMapHT_DCPHASEResources()
        {
            var url = "https://www.alphavantage.co/query?function=HT_DCPHASE&symbol=MSFT&interval=daily&series_type=close&apikey=demo";
            var jObj = GetSnapshot(url);

            var process = new AvHT_DCPHASEProcess();
            process.Map(jObj, url);

            Assert.NotNull(process);
            Assert.NotNull(process.Data);
            Assert.NotNull(process.Data.MetaData);
            Assert.True(process.Data.MetaData.LastRefreshed > DateTime.MinValue);
            Assert.True(process.Data.MetaData.Symbol == "MSFT");
            Assert.True(process.Data.MetaData.Interval == AvIntervalEnum.Daily);
            Assert.True(process.Data.MetaData.Indicator == "Hilbert Transform - Dominant Cycle Phase (HT_DCPHASE)");
            Assert.True(process.Data.MetaData.TimeZone.StandardName == AvTimeZoneConvertor.AvTimeZone("US/Eastern Time").StandardName);
            Assert.True(process.Data.MetaData.SeriesType == AvSeriesTypeEnum.Close);

            Assert.NotNull(process.Data.TimeSeries);
            Assert.True(process.Data.TimeSeries.Count() > 0);
        }

        [Fact]
        public void ShouldMapHT_PHASORResources()
        {
            var url = "https://www.alphavantage.co/query?function=HT_PHASOR&symbol=MSFT&interval=weekly&series_type=close&apikey=demo";
            var jObj = GetSnapshot(url);

            var process = new AvHT_PHASORProcess();
            process.Map(jObj, url);

            Assert.NotNull(process);
            Assert.NotNull(process.Data);
            Assert.NotNull(process.Data.MetaData);
            Assert.True(process.Data.MetaData.LastRefreshed > DateTime.MinValue);
            Assert.True(process.Data.MetaData.Symbol == "MSFT");
            Assert.True(process.Data.MetaData.Interval == AvIntervalEnum.Weekly);
            Assert.True(process.Data.MetaData.Indicator == "Hilbert Transform - Phasor Components (HT_PHASOR)");
            Assert.True(process.Data.MetaData.TimeZone.StandardName == AvTimeZoneConvertor.AvTimeZone("US/Eastern Time").StandardName);
            Assert.True(process.Data.MetaData.SeriesType == AvSeriesTypeEnum.Close);

            Assert.NotNull(process.Data.TimeSeries);
            Assert.True(process.Data.TimeSeries.Count() > 0);
        }

        [Fact]
        public void ShouldMapHT_SINEResources()
        {
            var url = "https://www.alphavantage.co/query?function=HT_SINE&symbol=MSFT&interval=daily&series_type=close&apikey=demo";
            var jObj = GetSnapshot(url);

            var process = new AvHT_SINEProcess();
            process.Map(jObj, url);

            Assert.NotNull(process);
            Assert.NotNull(process.Data);
            Assert.NotNull(process.Data.MetaData);
            Assert.True(process.Data.MetaData.LastRefreshed > DateTime.MinValue);
            Assert.True(process.Data.MetaData.Symbol == "MSFT");
            Assert.True(process.Data.MetaData.Interval == AvIntervalEnum.Daily);
            Assert.True(process.Data.MetaData.Indicator == "Hilbert Transform - SineWave (HT_SINE)");
            Assert.True(process.Data.MetaData.TimeZone.StandardName == AvTimeZoneConvertor.AvTimeZone("US/Eastern Time").StandardName);
            Assert.True(process.Data.MetaData.SeriesType == AvSeriesTypeEnum.Close);

            Assert.NotNull(process.Data.TimeSeries);
            Assert.True(process.Data.TimeSeries.Count() > 0);
        }

        [Fact]
        public void ShouldMapHT_TRENDLINEResources()
        {
            var url = "https://www.alphavantage.co/query?function=HT_TRENDLINE&symbol=MSFT&interval=daily&series_type=close&apikey=demo";
            var jObj = GetSnapshot(url);

            var process = new AvHT_TRENDLINEProcess();
            process.Map(jObj, url);

            Assert.NotNull(process);
            Assert.NotNull(process.Data);
            Assert.NotNull(process.Data.MetaData);
            Assert.True(process.Data.MetaData.LastRefreshed > DateTime.MinValue);
            Assert.True(process.Data.MetaData.Symbol == "MSFT");
            Assert.True(process.Data.MetaData.Interval == AvIntervalEnum.Daily);
            Assert.True(process.Data.MetaData.Indicator == "Hilbert Transform - Instantaneous Trendline (HT_TRENDLINE)");
            Assert.True(process.Data.MetaData.TimeZone.StandardName == AvTimeZoneConvertor.AvTimeZone("US/Eastern Time").StandardName);
            Assert.True(process.Data.MetaData.SeriesType == AvSeriesTypeEnum.Close);

            Assert.NotNull(process.Data.TimeSeries);
            Assert.True(process.Data.TimeSeries.Count() > 0);
        }

        [Fact]
        public void ShouldMapHT_TRENDMODEResources()
        {
            var url = "https://www.alphavantage.co/query?function=HT_TRENDMODE&symbol=MSFT&interval=weekly&series_type=close&apikey=demo";
            var jObj = GetSnapshot(url);

            var process = new AvHT_TRENDMODEProcess();
            process.Map(jObj, url);

            Assert.NotNull(process);
            Assert.NotNull(process.Data);
            Assert.NotNull(process.Data.MetaData);
            Assert.True(process.Data.MetaData.LastRefreshed > DateTime.MinValue);
            Assert.True(process.Data.MetaData.Symbol == "MSFT");
            Assert.True(process.Data.MetaData.Interval == AvIntervalEnum.Weekly);
            Assert.True(process.Data.MetaData.Indicator == "Hilbert Transform - Trend vs Cycle Mode (HT_TRENDMODE)");
            Assert.True(process.Data.MetaData.TimeZone.StandardName == AvTimeZoneConvertor.AvTimeZone("US/Eastern Time").StandardName);
            Assert.True(process.Data.MetaData.SeriesType == AvSeriesTypeEnum.Close);

            Assert.NotNull(process.Data.TimeSeries);
            Assert.True(process.Data.TimeSeries.Count() > 0);
        }

        [Fact]
        public void ShouldMapKAMAResources()
        {
            var url = "https://www.alphavantage.co/query?function=KAMA&symbol=MSFT&interval=weekly&time_period=10&series_type=open&apikey=demo";
            var jObj = GetSnapshot(url);

            var process = new AvKAMAProcess();
            process.Map(jObj, url);

            Assert.NotNull(process);
            Assert.NotNull(process.Data);
            Assert.NotNull(process.Data.MetaData);
            Assert.True(process.Data.MetaData.LastRefreshed > DateTime.MinValue);
            Assert.True(process.Data.MetaData.Symbol == "MSFT");
            Assert.True(process.Data.MetaData.Interval == AvIntervalEnum.Weekly);
            Assert.True(process.Data.MetaData.Indicator == "Kaufman Adaptive Moving Average (KAMA)");
            Assert.True(process.Data.MetaData.TimeZone.StandardName == AvTimeZoneConvertor.AvTimeZone("US/Eastern Time").StandardName);
            Assert.True(process.Data.MetaData.SeriesType == AvSeriesTypeEnum.Open);
            Assert.True(process.Data.MetaData.TimePeriod == 10);

            Assert.NotNull(process.Data.TimeSeries);
            Assert.True(process.Data.TimeSeries.Count() > 0);
        }

        [Fact]
        public void ShouldMapMACDResources()
        {
            var url = "https://www.alphavantage.co/query?function=MACD&symbol=MSFT&interval=daily&series_type=open&apikey=demo";
            var jObj = GetSnapshot(url);

            var process = new AvMACDProcess();
            process.Map(jObj, url);

            Assert.NotNull(process);
            Assert.NotNull(process.Data);
            Assert.NotNull(process.Data.MetaData);
            Assert.True(process.Data.MetaData.LastRefreshed > DateTime.MinValue);
            Assert.True(process.Data.MetaData.Symbol == "MSFT");
            Assert.True(process.Data.MetaData.Interval == AvIntervalEnum.Daily);
            Assert.True(process.Data.MetaData.Indicator == "Moving Average Convergence/Divergence (MACD)");
            Assert.True(process.Data.MetaData.TimeZone.StandardName == AvTimeZoneConvertor.AvTimeZone("US/Eastern Time").StandardName);
            Assert.True(process.Data.MetaData.SeriesType == AvSeriesTypeEnum.Open);
            Assert.True(process.Data.MetaData.FastPeriod == 12);
            Assert.True(process.Data.MetaData.SlowPeriod == 26);
            Assert.True(process.Data.MetaData.SignalPeriod == 9);

            Assert.NotNull(process.Data.TimeSeries);
            Assert.True(process.Data.TimeSeries.Count() > 0);
        }

        [Fact]
        public void ShouldMapMACDEXTResources()
        {
            var url = "https://www.alphavantage.co/query?function=MACDEXT&symbol=MSFT&interval=daily&series_type=open&apikey=demo";
            var jObj = GetSnapshot(url);

            var process = new AvMACDEXTProcess();
            process.Map(jObj, url);

            Assert.NotNull(process);
            Assert.NotNull(process.Data);
            Assert.NotNull(process.Data.MetaData);
            Assert.True(process.Data.MetaData.LastRefreshed > DateTime.MinValue);
            Assert.True(process.Data.MetaData.Symbol == "MSFT");
            Assert.True(process.Data.MetaData.Interval == AvIntervalEnum.Daily);
            Assert.True(process.Data.MetaData.Indicator == "MACD with Controllable MA Type (MACDEXT)");
            Assert.True(process.Data.MetaData.TimeZone.StandardName == AvTimeZoneConvertor.AvTimeZone("US/Eastern Time").StandardName);
            Assert.True(process.Data.MetaData.SeriesType == AvSeriesTypeEnum.Open);
            Assert.True(process.Data.MetaData.FastPeriod == 12);
            Assert.True(process.Data.MetaData.SlowPeriod == 26);
            Assert.True(process.Data.MetaData.SignalPeriod == 9);
            Assert.True(process.Data.MetaData.FastMAType == 0);
            Assert.True(process.Data.MetaData.SlowMAType == 0);
            Assert.True(process.Data.MetaData.SignalMAType == 0);

            Assert.NotNull(process.Data.TimeSeries);
            Assert.True(process.Data.TimeSeries.Count() > 0);
        }

        [Fact]
        public void ShouldMapMFIResources()
        {
            var url = "https://www.alphavantage.co/query?function=MFI&symbol=MSFT&interval=weekly&time_period=10&apikey=demo";
            var jObj = GetSnapshot(url);

            var process = new AvMFIProcess();
            process.Map(jObj, url);

            Assert.NotNull(process);
            Assert.NotNull(process.Data);
            Assert.NotNull(process.Data.MetaData);
            Assert.True(process.Data.MetaData.LastRefreshed > DateTime.MinValue);
            Assert.True(process.Data.MetaData.Symbol == "MSFT");
            Assert.True(process.Data.MetaData.Interval == AvIntervalEnum.Weekly);
            Assert.True(process.Data.MetaData.Indicator == "Money Flow Index (MFI)");
            Assert.True(process.Data.MetaData.TimeZone.StandardName == AvTimeZoneConvertor.AvTimeZone("US/Eastern Time").StandardName);
            Assert.True(process.Data.MetaData.TimePeriod == 10);

            Assert.NotNull(process.Data.TimeSeries);
            Assert.True(process.Data.TimeSeries.Count() > 0);
        }

        [Fact]
        public void ShouldMapMIDPOINTResources()
        {
            var url = "https://www.alphavantage.co/query?function=MIDPOINT&symbol=MSFT&interval=daily&time_period=10&series_type=close&apikey=demo";
            var jObj = GetSnapshot(url);

            var process = new AvMIDPOINTProcess();
            process.Map(jObj, url);

            Assert.NotNull(process);
            Assert.NotNull(process.Data);
            Assert.NotNull(process.Data.MetaData);
            Assert.True(process.Data.MetaData.LastRefreshed > DateTime.MinValue);
            Assert.True(process.Data.MetaData.Symbol == "MSFT");
            Assert.True(process.Data.MetaData.Interval == AvIntervalEnum.Daily);
            Assert.True(process.Data.MetaData.Indicator == "MidPoint over period (MIDPOINT)");
            Assert.True(process.Data.MetaData.TimeZone.StandardName == AvTimeZoneConvertor.AvTimeZone("US/Eastern Time").StandardName);
            Assert.True(process.Data.MetaData.TimePeriod == 10);
            Assert.True(process.Data.MetaData.SeriesType == AvSeriesTypeEnum.Close);

            Assert.NotNull(process.Data.TimeSeries);
            Assert.True(process.Data.TimeSeries.Count() > 0);
        }

        [Fact]
        public void ShouldMapMIDPRICEResources()
        {
            var url = "https://www.alphavantage.co/query?function=MIDPRICE&symbol=MSFT&interval=daily&time_period=10&apikey=demo";
            var jObj = GetSnapshot(url);

            var process = new AvMIDPRICEProcess();
            process.Map(jObj, url);

            Assert.NotNull(process);
            Assert.NotNull(process.Data);
            Assert.NotNull(process.Data.MetaData);
            Assert.True(process.Data.MetaData.LastRefreshed > DateTime.MinValue);
            Assert.True(process.Data.MetaData.Symbol == "MSFT");
            Assert.True(process.Data.MetaData.Interval == AvIntervalEnum.Daily);
            Assert.True(process.Data.MetaData.Indicator == "Midpoint Price over period (MIDPRICE)");
            Assert.True(process.Data.MetaData.TimeZone.StandardName == AvTimeZoneConvertor.AvTimeZone("US/Eastern Time").StandardName);
            Assert.True(process.Data.MetaData.TimePeriod == 10);


            Assert.NotNull(process.Data.TimeSeries);
            Assert.True(process.Data.TimeSeries.Count() > 0);
        }

        [Fact]
        public void ShouldMapMINUS_DIResources()
        {
            var url = "https://www.alphavantage.co/query?function=MINUS_DI&symbol=MSFT&interval=weekly&time_period=10&apikey=demo";
            var jObj = GetSnapshot(url);

            var process = new AvMINUS_DIProcess();
            process.Map(jObj, url);

            Assert.NotNull(process);
            Assert.NotNull(process.Data);
            Assert.NotNull(process.Data.MetaData);
            Assert.True(process.Data.MetaData.LastRefreshed > DateTime.MinValue);
            Assert.True(process.Data.MetaData.Symbol == "MSFT");
            Assert.True(process.Data.MetaData.Interval == AvIntervalEnum.Weekly);
            Assert.True(process.Data.MetaData.Indicator == "Minus Directional Indicator (MINUS_DI)");
            Assert.True(process.Data.MetaData.TimeZone.StandardName == AvTimeZoneConvertor.AvTimeZone("US/Eastern Time").StandardName);
            Assert.True(process.Data.MetaData.TimePeriod == 10);


            Assert.NotNull(process.Data.TimeSeries);
            Assert.True(process.Data.TimeSeries.Count() > 0);
        }

        [Fact]
        public void ShouldMapMINUS_DMResources()
        {
            var url = "https://www.alphavantage.co/query?function=MINUS_DM&symbol=MSFT&interval=daily&time_period=10&apikey=demo";
            var jObj = GetSnapshot(url);

            var process = new AvMINUS_DMProcess();
            process.Map(jObj, url);

            Assert.NotNull(process);
            Assert.NotNull(process.Data);
            Assert.NotNull(process.Data.MetaData);
            Assert.True(process.Data.MetaData.LastRefreshed > DateTime.MinValue);
            Assert.True(process.Data.MetaData.Symbol == "MSFT");
            Assert.True(process.Data.MetaData.Interval == AvIntervalEnum.Daily);
            Assert.True(process.Data.MetaData.Indicator == "Minus Directional Movement (MINUS_DM)");
            Assert.True(process.Data.MetaData.TimeZone.StandardName == AvTimeZoneConvertor.AvTimeZone("US/Eastern Time").StandardName);
            Assert.True(process.Data.MetaData.TimePeriod == 10);


            Assert.NotNull(process.Data.TimeSeries);
            Assert.True(process.Data.TimeSeries.Count() > 0);
        }

        [Fact]
        public void ShouldMapMOMResources()
        {
            var url = "https://www.alphavantage.co/query?function=MOM&symbol=MSFT&interval=daily&time_period=10&series_type=close&apikey=demo";
            var jObj = GetSnapshot(url);

            var process = new AvMOMProcess();
            process.Map(jObj, url);

            Assert.NotNull(process);
            Assert.NotNull(process.Data);
            Assert.NotNull(process.Data.MetaData);
            Assert.True(process.Data.MetaData.LastRefreshed > DateTime.MinValue);
            Assert.True(process.Data.MetaData.Symbol == "MSFT");
            Assert.True(process.Data.MetaData.Interval == AvIntervalEnum.Daily);
            Assert.True(process.Data.MetaData.Indicator == "Momentum (MOM)");
            Assert.True(process.Data.MetaData.TimeZone.StandardName == AvTimeZoneConvertor.AvTimeZone("US/Eastern Time").StandardName);
            Assert.True(process.Data.MetaData.TimePeriod == 10);
            Assert.True(process.Data.MetaData.SeriesType == AvSeriesTypeEnum.Close);

            Assert.NotNull(process.Data.TimeSeries);
            Assert.True(process.Data.TimeSeries.Count() > 0);
        }

        [Fact]
        public void ShouldMapNATRResources()
        {
            var url = "https://www.alphavantage.co/query?function=NATR&symbol=MSFT&interval=weekly&time_period=14&apikey=demo";
            var jObj = GetSnapshot(url);

            var process = new AvNATRProcess();
            process.Map(jObj, url);

            Assert.NotNull(process);
            Assert.NotNull(process.Data);
            Assert.NotNull(process.Data.MetaData);
            Assert.True(process.Data.MetaData.LastRefreshed > DateTime.MinValue);
            Assert.True(process.Data.MetaData.Symbol == "MSFT");
            Assert.True(process.Data.MetaData.Interval == AvIntervalEnum.Weekly);
            Assert.True(process.Data.MetaData.Indicator == "Normalized Average True Range (NATR)");
            Assert.True(process.Data.MetaData.TimeZone.StandardName == AvTimeZoneConvertor.AvTimeZone("US/Eastern Time").StandardName);
            Assert.True(process.Data.MetaData.TimePeriod == 14);


            Assert.NotNull(process.Data.TimeSeries);
            Assert.True(process.Data.TimeSeries.Count() > 0);
        }

        [Fact]
        public void ShouldMapOBVResources()
        {
            var url = "https://www.alphavantage.co/query?function=OBV&symbol=MSFT&interval=weekly&apikey=demo";
            var jObj = GetSnapshot(url);

            var process = new AvOBVProcess();
            process.Map(jObj, url);

            Assert.NotNull(process);
            Assert.NotNull(process.Data);
            Assert.NotNull(process.Data.MetaData);
            Assert.True(process.Data.MetaData.LastRefreshed > DateTime.MinValue);
            Assert.True(process.Data.MetaData.Symbol == "MSFT");
            Assert.True(process.Data.MetaData.Interval == AvIntervalEnum.Weekly);
            Assert.True(process.Data.MetaData.Indicator == "On Balance Volume (OBV)");
            Assert.True(process.Data.MetaData.TimeZone.StandardName == AvTimeZoneConvertor.AvTimeZone("US/Eastern Time").StandardName);



            Assert.NotNull(process.Data.TimeSeries);
            Assert.True(process.Data.TimeSeries.Count() > 0);
        }

        [Fact]
        public void ShouldMapPLUS_DIResources()
        {
            var url = "https://www.alphavantage.co/query?function=PLUS_DI&symbol=MSFT&interval=daily&time_period=10&apikey=demo";
            var jObj = GetSnapshot(url);

            var process = new AvPLUS_DIProcess();
            process.Map(jObj, url);

            Assert.NotNull(process);
            Assert.NotNull(process.Data);
            Assert.NotNull(process.Data.MetaData);
            Assert.True(process.Data.MetaData.LastRefreshed > DateTime.MinValue);
            Assert.True(process.Data.MetaData.Symbol == "MSFT");
            Assert.True(process.Data.MetaData.Interval == AvIntervalEnum.Daily);
            Assert.True(process.Data.MetaData.Indicator == "Plus Directional Indicator (PLUS_DI)");
            Assert.True(process.Data.MetaData.TimeZone.StandardName == AvTimeZoneConvertor.AvTimeZone("US/Eastern Time").StandardName);
            Assert.True(process.Data.MetaData.TimePeriod == 10);


            Assert.NotNull(process.Data.TimeSeries);
            Assert.True(process.Data.TimeSeries.Count() > 0);
        }

        [Fact]
        public void ShouldMapPLUS_DMResources()
        {
            var url = "https://www.alphavantage.co/query?function=PLUS_DM&symbol=MSFT&interval=daily&time_period=10&apikey=demo";
            var jObj = GetSnapshot(url);

            var process = new AvPLUS_DMProcess();
            process.Map(jObj, url);

            Assert.NotNull(process);
            Assert.NotNull(process.Data);
            Assert.NotNull(process.Data.MetaData);
            Assert.True(process.Data.MetaData.LastRefreshed > DateTime.MinValue);
            Assert.True(process.Data.MetaData.Symbol == "MSFT");
            Assert.True(process.Data.MetaData.Interval == AvIntervalEnum.Daily);
            Assert.True(process.Data.MetaData.Indicator == "Plus Directional Movement (PLUS_DM)");
            Assert.True(process.Data.MetaData.TimeZone.StandardName == AvTimeZoneConvertor.AvTimeZone("US/Eastern Time").StandardName);
            Assert.True(process.Data.MetaData.TimePeriod == 10);


            Assert.NotNull(process.Data.TimeSeries);
            Assert.True(process.Data.TimeSeries.Count() > 0);
        }

        [Fact]
        public void ShouldMapPPOResources()
        {
            var url = "https://www.alphavantage.co/query?function=PPO&symbol=MSFT&interval=daily&series_type=close&fastperiod=10&matype=1&apikey=demo";
            var jObj = GetSnapshot(url);

            var process = new AvPPOProcess();
            process.Map(jObj, url);

            Assert.NotNull(process);
            Assert.NotNull(process.Data);
            Assert.NotNull(process.Data.MetaData);
            Assert.True(process.Data.MetaData.LastRefreshed > DateTime.MinValue);
            Assert.True(process.Data.MetaData.Symbol == "MSFT");
            Assert.True(process.Data.MetaData.Interval == AvIntervalEnum.Daily);
            Assert.True(process.Data.MetaData.Indicator == "Percentage Price Oscillator (PPO)");
            Assert.True(process.Data.MetaData.TimeZone.StandardName == AvTimeZoneConvertor.AvTimeZone("US/Eastern Time").StandardName);
            Assert.True(process.Data.MetaData.FastPeriod == 10);
            Assert.True(process.Data.MetaData.SlowPeriod == 26);
            Assert.True(process.Data.MetaData.MAType == AvMovingAverageTypeEnum.EMA);
            Assert.True(process.Data.MetaData.SeriesType == AvSeriesTypeEnum.Close);

            Assert.NotNull(process.Data.TimeSeries);
            Assert.True(process.Data.TimeSeries.Count() > 0);
        }

        [Fact]
        public void ShouldMapROCResources()
        {
            var url = "https://www.alphavantage.co/query?function=ROC&symbol=MSFT&interval=weekly&time_period=10&series_type=close&apikey=demo";
            var jObj = GetSnapshot(url);

            var process = new AvROCProcess();
            process.Map(jObj, url);

            Assert.NotNull(process);
            Assert.NotNull(process.Data);
            Assert.NotNull(process.Data.MetaData);
            Assert.True(process.Data.MetaData.LastRefreshed > DateTime.MinValue);
            Assert.True(process.Data.MetaData.Symbol == "MSFT");
            Assert.True(process.Data.MetaData.Interval == AvIntervalEnum.Weekly);
            Assert.True(process.Data.MetaData.Indicator == "Rate of change : ((price/prevPrice)-1)*100");
            Assert.True(process.Data.MetaData.TimeZone.StandardName == AvTimeZoneConvertor.AvTimeZone("US/Eastern Time").StandardName);
            Assert.True(process.Data.MetaData.SeriesType == AvSeriesTypeEnum.Close);

            Assert.NotNull(process.Data.TimeSeries);
            Assert.True(process.Data.TimeSeries.Count() > 0);
        }

        [Fact]
        public void ShouldMapROCRResources()
        {
            var url = "https://www.alphavantage.co/query?function=ROCR&symbol=MSFT&interval=daily&time_period=10&series_type=close&apikey=demo";
            var jObj = GetSnapshot(url);

            var process = new AvROCRProcess();
            process.Map(jObj, url);

            Assert.NotNull(process);
            Assert.NotNull(process.Data);
            Assert.NotNull(process.Data.MetaData);
            Assert.True(process.Data.MetaData.LastRefreshed > DateTime.MinValue);
            Assert.True(process.Data.MetaData.Symbol == "MSFT");
            Assert.True(process.Data.MetaData.Interval == AvIntervalEnum.Daily);
            Assert.True(process.Data.MetaData.Indicator == "Rate of change ratio: (price/prevPrice)");
            Assert.True(process.Data.MetaData.TimeZone.StandardName == AvTimeZoneConvertor.AvTimeZone("US/Eastern Time").StandardName);
            Assert.True(process.Data.MetaData.SeriesType == AvSeriesTypeEnum.Close);
            Assert.True(process.Data.MetaData.TimePeriod == 10);

            Assert.NotNull(process.Data.TimeSeries);
            Assert.True(process.Data.TimeSeries.Count() > 0);
        }

        [Fact]
        public void ShouldMapSARResources()
        {
            var url = "https://www.alphavantage.co/query?function=SAR&symbol=MSFT&interval=weekly&acceleration=0.05&maximum=0.25&apikey=demo";
            var jObj = GetSnapshot(url);

            var process = new AvSARProcess();
            process.Map(jObj, url);

            Assert.NotNull(process);
            Assert.NotNull(process.Data);
            Assert.NotNull(process.Data.MetaData);
            Assert.True(process.Data.MetaData.LastRefreshed > DateTime.MinValue);
            Assert.True(process.Data.MetaData.Symbol == "MSFT");
            Assert.True(process.Data.MetaData.Interval == AvIntervalEnum.Weekly);
            Assert.True(process.Data.MetaData.Indicator == "Parabolic SAR (SAR)");
            Assert.True(process.Data.MetaData.TimeZone.StandardName == AvTimeZoneConvertor.AvTimeZone("US/Eastern Time").StandardName);
            Assert.True(process.Data.MetaData.Acceleration == 0.05m);
            Assert.True(process.Data.MetaData.Maximum == 0.25m);


            Assert.NotNull(process.Data.TimeSeries);
            Assert.True(process.Data.TimeSeries.Count() > 0);
        }

        [Fact]
        public void ShouldMapSMAResources()
        {
            var url = "https://www.alphavantage.co/query?function=SMA&symbol=MSFT&interval=weekly&time_period=10&series_type=open&apikey=demo";
            var jObj = GetSnapshot(url);

            var process = new AvSMAProcess();
            process.Map(jObj, url);

            Assert.NotNull(process);
            Assert.NotNull(process.Data);
            Assert.NotNull(process.Data.MetaData);
            Assert.True(process.Data.MetaData.LastRefreshed > DateTime.MinValue);
            Assert.True(process.Data.MetaData.Symbol == "MSFT");
            Assert.True(process.Data.MetaData.Interval == AvIntervalEnum.Weekly);
            Assert.True(process.Data.MetaData.Indicator == "Simple Moving Average (SMA)");
            Assert.True(process.Data.MetaData.TimeZone.StandardName == AvTimeZoneConvertor.AvTimeZone("US/Eastern Time").StandardName);


            Assert.NotNull(process.Data.TimeSeries);
            Assert.True(process.Data.TimeSeries.Count() > 0);
        }

        [Fact]
        public void ShouldMapSTOCHResources()
        {
            var url = "https://www.alphavantage.co/query?function=STOCH&symbol=MSFT&interval=daily&apikey=demo";
            var jObj = GetSnapshot(url);

            var process = new AvSTOCHProcess();
            process.Map(jObj, url);

            Assert.NotNull(process);
            Assert.NotNull(process.Data);
            Assert.NotNull(process.Data.MetaData);
            Assert.True(process.Data.MetaData.LastRefreshed > DateTime.MinValue);
            Assert.True(process.Data.MetaData.Symbol == "MSFT");
            Assert.True(process.Data.MetaData.Interval == AvIntervalEnum.Daily);
            Assert.True(process.Data.MetaData.Indicator == "Stochastic (STOCH)");
            Assert.True(process.Data.MetaData.TimeZone.StandardName == AvTimeZoneConvertor.AvTimeZone("US/Eastern Time").StandardName);
            Assert.Equal(5, process.Data.MetaData.FastKPeriod);
            Assert.Equal(3, process.Data.MetaData.SlowKPeriod);
            Assert.Equal(0, process.Data.MetaData.SlowKMAType);
            Assert.Equal(3, process.Data.MetaData.SlowDPeriod);
            Assert.Equal(0, process.Data.MetaData.SlowDMAType);

            Assert.NotNull(process.Data.TimeSeries);
            Assert.True(process.Data.TimeSeries.Count() > 0);
        }

        [Fact]
        public void ShouldMapSTOCHFResources()
        {
            var url = "https://www.alphavantage.co/query?function=STOCHF&symbol=MSFT&interval=daily&apikey=demo";
            var jObj = GetSnapshot(url);

            var process = new AvSTOCHFProcess();
            process.Map(jObj, url);

            Assert.NotNull(process);
            Assert.NotNull(process.Data);
            Assert.NotNull(process.Data.MetaData);
            Assert.True(process.Data.MetaData.LastRefreshed > DateTime.MinValue);
            Assert.True(process.Data.MetaData.Symbol == "MSFT");
            Assert.True(process.Data.MetaData.Interval == AvIntervalEnum.Daily);
            Assert.True(process.Data.MetaData.Indicator == "Stochastic Fast (STOCHF)");
            Assert.True(process.Data.MetaData.TimeZone.StandardName == AvTimeZoneConvertor.AvTimeZone("US/Eastern Time").StandardName);
            Assert.Equal(5, process.Data.MetaData.FastKPeriod);
            Assert.Equal(3, process.Data.MetaData.FastDPeriod);
            Assert.Equal(0, process.Data.MetaData.FastDMAType);

            Assert.NotNull(process.Data.TimeSeries);
            Assert.True(process.Data.TimeSeries.Count() > 0);
        }

        [Fact]
        public void ShouldMapSTOCHRSIResources()
        {
            var url = "https://www.alphavantage.co/query?function=STOCHRSI&symbol=MSFT&interval=daily&time_period=10&series_type=close&fastkperiod=6&fastdmatype=1&apikey=demo";
            var jObj = GetSnapshot(url);

            var process = new AvSTOCHRSIProcess();
            process.Map(jObj, url);

            Assert.NotNull(process);
            Assert.NotNull(process.Data);
            Assert.NotNull(process.Data.MetaData);
            Assert.True(process.Data.MetaData.LastRefreshed > DateTime.MinValue);
            Assert.True(process.Data.MetaData.Symbol == "MSFT");
            Assert.True(process.Data.MetaData.Interval == AvIntervalEnum.Daily);
            Assert.True(process.Data.MetaData.Indicator == "Stochastic Relative Strength Index (STOCHRSI)");
            Assert.True(process.Data.MetaData.TimeZone.StandardName == AvTimeZoneConvertor.AvTimeZone("US/Eastern Time").StandardName);
            Assert.Equal(6, process.Data.MetaData.FastKPeriod);
            Assert.Equal(3, process.Data.MetaData.FastDPeriod);
            Assert.Equal(1, process.Data.MetaData.FastDMAType);
            Assert.Equal(AvSeriesTypeEnum.Close, process.Data.MetaData.SeriesType);

            Assert.NotNull(process.Data.TimeSeries);
            Assert.True(process.Data.TimeSeries.Count() > 0);
        }

        [Fact]
        public void ShouldMapT3Resources()
        {
            var url = "https://www.alphavantage.co/query?function=T3&symbol=MSFT&interval=weekly&time_period=10&series_type=open&apikey=demo";
            var jObj = GetSnapshot(url);

            var process = new AvT3Process();
            process.Map(jObj, url);

            Assert.NotNull(process);
            Assert.NotNull(process.Data);
            Assert.NotNull(process.Data.MetaData);
            Assert.True(process.Data.MetaData.LastRefreshed > DateTime.MinValue);
            Assert.True(process.Data.MetaData.Symbol == "MSFT");
            Assert.True(process.Data.MetaData.Interval == AvIntervalEnum.Weekly);
            Assert.True(process.Data.MetaData.Indicator == "Triple Exponential Moving Average (T3)");
            Assert.True(process.Data.MetaData.TimeZone.StandardName == AvTimeZoneConvertor.AvTimeZone("US/Eastern Time").StandardName);
            Assert.Equal(0.7m, process.Data.MetaData.VolumeFactor);
            Assert.Equal(AvSeriesTypeEnum.Open, process.Data.MetaData.SeriesType);

            Assert.NotNull(process.Data.TimeSeries);
            Assert.True(process.Data.TimeSeries.Count() > 0);
        }

        [Fact]
        public void ShouldMapTRANGEResources()
        {
            var url = "https://www.alphavantage.co/query?function=TRANGE&symbol=MSFT&interval=daily&apikey=demo";
            var jObj = GetSnapshot(url);

            var process = new AvTRANGEProcess();
            process.Map(jObj, url);

            Assert.NotNull(process);
            Assert.NotNull(process.Data);
            Assert.NotNull(process.Data.MetaData);
            Assert.True(process.Data.MetaData.LastRefreshed > DateTime.MinValue);
            Assert.True(process.Data.MetaData.Symbol == "MSFT");
            Assert.True(process.Data.MetaData.Interval == AvIntervalEnum.Daily);
            Assert.True(process.Data.MetaData.Indicator == "True Range (TRANGE)");
            Assert.True(process.Data.MetaData.TimeZone.StandardName == AvTimeZoneConvertor.AvTimeZone("US/Eastern Time").StandardName);


            Assert.NotNull(process.Data.TimeSeries);
            Assert.True(process.Data.TimeSeries.Count() > 0);
        }

        [Fact]
        public void ShouldMapTRIMAResources()
        {
            var url = "https://www.alphavantage.co/query?function=TRIMA&symbol=MSFT&interval=weekly&time_period=10&series_type=open&apikey=demo";
            var jObj = GetSnapshot(url);

            var process = new AvTRIMAProcess();
            process.Map(jObj, url);

            Assert.NotNull(process);
            Assert.NotNull(process.Data);
            Assert.NotNull(process.Data.MetaData);
            Assert.True(process.Data.MetaData.LastRefreshed > DateTime.MinValue);
            Assert.True(process.Data.MetaData.Symbol == "MSFT");
            Assert.True(process.Data.MetaData.Interval == AvIntervalEnum.Weekly);
            Assert.True(process.Data.MetaData.Indicator == "Triangular Exponential Moving Average (TRIMA)");
            Assert.True(process.Data.MetaData.TimeZone.StandardName == AvTimeZoneConvertor.AvTimeZone("US/Eastern Time").StandardName);
            Assert.Equal(10, process.Data.MetaData.TimePeriod);
            Assert.Equal(AvSeriesTypeEnum.Open, process.Data.MetaData.SeriesType);

            Assert.NotNull(process.Data.TimeSeries);
            Assert.True(process.Data.TimeSeries.Count() > 0);
        }

        [Fact]
        public void ShouldMapTRIXResources()
        {
            var url = "https://www.alphavantage.co/query?function=TRIX&symbol=MSFT&interval=daily&time_period=10&series_type=close&apikey=demo";
            var jObj = GetSnapshot(url);

            var process = new AvTRIXProcess();
            process.Map(jObj, url);

            Assert.NotNull(process);
            Assert.NotNull(process.Data);
            Assert.NotNull(process.Data.MetaData);
            Assert.True(process.Data.MetaData.LastRefreshed > DateTime.MinValue);
            Assert.True(process.Data.MetaData.Symbol == "MSFT");
            Assert.True(process.Data.MetaData.Interval == AvIntervalEnum.Daily);
            Assert.True(process.Data.MetaData.Indicator == "1-day Rate-Of-Change (ROC) of a Triple Smooth EMA (TRIX)");
            Assert.True(process.Data.MetaData.TimeZone.StandardName == AvTimeZoneConvertor.AvTimeZone("US/Eastern Time").StandardName);
            Assert.Equal(10, process.Data.MetaData.TimePeriod);
            Assert.Equal(AvSeriesTypeEnum.Close, process.Data.MetaData.SeriesType);

            Assert.NotNull(process.Data.TimeSeries);
            Assert.True(process.Data.TimeSeries.Count() > 0);
        }

        [Fact]
        public void ShouldMapULTOSCResources()
        {
            var url = "https://www.alphavantage.co/query?function=ULTOSC&symbol=MSFT&interval=daily&timeperiod1=8&apikey=demo";
            var jObj = GetSnapshot(url);

            var process = new AvULTOSCProcess();
            process.Map(jObj, url);

            Assert.NotNull(process);
            Assert.NotNull(process.Data);
            Assert.NotNull(process.Data.MetaData);
            Assert.True(process.Data.MetaData.LastRefreshed > DateTime.MinValue);
            Assert.True(process.Data.MetaData.Symbol == "MSFT");
            Assert.True(process.Data.MetaData.Interval == AvIntervalEnum.Daily);
            Assert.True(process.Data.MetaData.Indicator == "Ultimate Oscillator (ULTOSC)");
            Assert.True(process.Data.MetaData.TimeZone.StandardName == AvTimeZoneConvertor.AvTimeZone("US/Eastern Time").StandardName);
            Assert.Equal(8, process.Data.MetaData.TimePeriodOne);
            Assert.Equal(14, process.Data.MetaData.TimePeriodTwo);
            Assert.Equal(28, process.Data.MetaData.TimePeriodThree);

            Assert.NotNull(process.Data.TimeSeries);
            Assert.True(process.Data.TimeSeries.Count() > 0);
        }

        [Fact]
        public void ShouldMapVWAPResources()
        {
            var url = "https://www.alphavantage.co/query?function=VWAP&symbol=MSFT&interval=15min&apikey=demo";
            var jObj = GetSnapshot(url);

            var process = new AvVWAPProcess();
            process.Map(jObj, url);

            Assert.NotNull(process);
            Assert.NotNull(process.Data);
            Assert.NotNull(process.Data.MetaData);
            Assert.True(process.Data.MetaData.LastRefreshed > DateTime.MinValue);
            Assert.True(process.Data.MetaData.Symbol == "MSFT");
            Assert.Equal(AvIntervalEnum.FifteenMin, process.Data.MetaData.Interval);
            Assert.True(process.Data.MetaData.Indicator == "Volume Weighted Average Price (VWAP)");
            Assert.True(process.Data.MetaData.TimeZone.StandardName == AvTimeZoneConvertor.AvTimeZone("US/Eastern Time").StandardName);


            Assert.NotNull(process.Data.TimeSeries);
            Assert.True(process.Data.TimeSeries.Count() > 0);
        }

        [Fact]
        public void ShouldMapWILLRResources()
        {
            var url = "https://www.alphavantage.co/query?function=WILLR&symbol=MSFT&interval=daily&time_period=10&apikey=demo";
            var jObj = GetSnapshot(url);

            var process = new AvWILLRProcess();
            process.Map(jObj, url);

            Assert.NotNull(process);
            Assert.NotNull(process.Data);
            Assert.NotNull(process.Data.MetaData);
            Assert.True(process.Data.MetaData.LastRefreshed > DateTime.MinValue);
            Assert.True(process.Data.MetaData.Symbol == "MSFT");
            Assert.Equal(AvIntervalEnum.Daily, process.Data.MetaData.Interval);
            Assert.True(process.Data.MetaData.Indicator == "Williams' %R (WILLR)");
            Assert.True(process.Data.MetaData.TimeZone.StandardName == AvTimeZoneConvertor.AvTimeZone("US/Eastern Time").StandardName);
            Assert.Equal(10, process.Data.MetaData.TimePeriod);


            Assert.NotNull(process.Data.TimeSeries);
            Assert.True(process.Data.TimeSeries.Count() > 0);
        }

        [Fact]
        public void ShouldMapWMAResources()
        {
            var url = "https://www.alphavantage.co/query?function=WMA&symbol=MSFT&interval=weekly&time_period=10&series_type=open&apikey=demo";
            var jObj = GetSnapshot(url);

            var process = new AvWMAProcess();
            process.Map(jObj, url);

            Assert.NotNull(process);
            Assert.NotNull(process.Data);
            Assert.NotNull(process.Data.MetaData);
            Assert.True(process.Data.MetaData.LastRefreshed > DateTime.MinValue);
            Assert.True(process.Data.MetaData.Symbol == "MSFT");
            Assert.Equal(AvIntervalEnum.Weekly, process.Data.MetaData.Interval);
            Assert.True(process.Data.MetaData.Indicator == "Weighted Moving Average (WMA)");
            Assert.True(process.Data.MetaData.TimeZone.StandardName == AvTimeZoneConvertor.AvTimeZone("US/Eastern Time").StandardName);
            Assert.Equal(10, process.Data.MetaData.TimePeriod);
            Assert.Equal(AvSeriesTypeEnum.Open, process.Data.MetaData.SeriesType);


            Assert.NotNull(process.Data.TimeSeries);
            Assert.True(process.Data.TimeSeries.Count() > 0);
        }

        [Fact]
        public void ShouldMapMAMAResources()
        {
            var url = "https://www.alphavantage.co/query?function=MAMA&symbol=MSFT&interval=daily&series_type=close&fastlimit=0.02&apikey=demo";
            var jObj = GetSnapshot(url);

            var process = new AvMAMAProcess();
            process.Map(jObj, url);

            Assert.NotNull(process);
            Assert.NotNull(process.Data);
            Assert.NotNull(process.Data.MetaData);
            Assert.True(process.Data.MetaData.LastRefreshed > DateTime.MinValue);
            Assert.True(process.Data.MetaData.Symbol == "MSFT");
            Assert.Equal(AvIntervalEnum.Daily, process.Data.MetaData.Interval);
            Assert.True(process.Data.MetaData.Indicator == "MESA Adaptive Moving Average (MAMA)");
            Assert.True(process.Data.MetaData.TimeZone.StandardName == AvTimeZoneConvertor.AvTimeZone("US/Eastern Time").StandardName);
            Assert.Equal(AvSeriesTypeEnum.Close, process.Data.MetaData.SeriesType);
            Assert.Equal(0.02m, process.Data.MetaData.FastLimit);
            Assert.Equal(0.01m, process.Data.MetaData.SlowLimit);

            Assert.NotNull(process.Data.TimeSeries);
            Assert.True(process.Data.TimeSeries.Count() > 0);
        }

        [Fact]
        public void ShouldMapTEMAResources()
        {
            var url = "https://www.alphavantage.co/query?function=TEMA&symbol=MSFT&interval=weekly&time_period=10&series_type=open&apikey=demo";
            var jObj = GetSnapshot(url);

            var process = new AvTEMAProcess();
            process.Map(jObj, url);

            Assert.NotNull(process);
            Assert.NotNull(process.Data);
            Assert.NotNull(process.Data.MetaData);
            Assert.True(process.Data.MetaData.LastRefreshed > DateTime.MinValue);
            Assert.True(process.Data.MetaData.Symbol == "MSFT");
            Assert.Equal(AvIntervalEnum.Weekly, process.Data.MetaData.Interval);
            Assert.True(process.Data.MetaData.Indicator == "Triple Exponential Moving Average (TEMA)");
            Assert.True(process.Data.MetaData.TimeZone.StandardName == AvTimeZoneConvertor.AvTimeZone("US/Eastern Time").StandardName);
            Assert.Equal(AvSeriesTypeEnum.Open, process.Data.MetaData.SeriesType);
            Assert.Equal(10, process.Data.MetaData.TimePeriod);


            Assert.NotNull(process.Data.TimeSeries);
            Assert.True(process.Data.TimeSeries.Count() > 0);
        }

    }
}
