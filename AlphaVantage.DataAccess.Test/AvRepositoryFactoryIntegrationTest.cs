using AlphaVantage.DataAccess.Common;
using AlphaVantage.DataAccess.Interfaces;

using AlphaVantage.DataAccess.MongoDb.TechnicalIndicators.SMA;
using AlphaVantage.DataAccess.MongoDb.TechnicalIndicators.EMA;
using AlphaVantage.DataAccess.MongoDb.TechnicalIndicators.WMA;
using AlphaVantage.DataAccess.MongoDb.TechnicalIndicators.DEMA;
using AlphaVantage.DataAccess.MongoDb.TechnicalIndicators.TEMA;
using AlphaVantage.DataAccess.MongoDb.TechnicalIndicators.TRIMA;
using AlphaVantage.DataAccess.MongoDb.TechnicalIndicators.KAMA;
using AlphaVantage.DataAccess.MongoDb.TechnicalIndicators.MAMA;
using AlphaVantage.DataAccess.MongoDb.TechnicalIndicators.VWAP;
using AlphaVantage.DataAccess.MongoDb.TechnicalIndicators.T3;
using AlphaVantage.DataAccess.MongoDb.TechnicalIndicators.MACD;
using AlphaVantage.DataAccess.MongoDb.TechnicalIndicators.MACDEXT;
using AlphaVantage.DataAccess.MongoDb.TechnicalIndicators.STOCH;
using AlphaVantage.DataAccess.MongoDb.TechnicalIndicators.STOCHF;
using AlphaVantage.DataAccess.MongoDb.TechnicalIndicators.RSI;
using AlphaVantage.DataAccess.MongoDb.TechnicalIndicators.STOCHRSI;
using AlphaVantage.DataAccess.MongoDb.TechnicalIndicators.WILLR;
using AlphaVantage.DataAccess.MongoDb.TechnicalIndicators.ADX;
using AlphaVantage.DataAccess.MongoDb.TechnicalIndicators.ADXR;
using AlphaVantage.DataAccess.MongoDb.TechnicalIndicators.APO;
using AlphaVantage.DataAccess.MongoDb.TechnicalIndicators.PPO;
using AlphaVantage.DataAccess.MongoDb.TechnicalIndicators.MOM;
using AlphaVantage.DataAccess.MongoDb.TechnicalIndicators.BOP;
using AlphaVantage.DataAccess.MongoDb.TechnicalIndicators.CCI;
using AlphaVantage.DataAccess.MongoDb.TechnicalIndicators.CMO;
using AlphaVantage.DataAccess.MongoDb.TechnicalIndicators.ROC;
using AlphaVantage.DataAccess.MongoDb.TechnicalIndicators.ROCR;
using AlphaVantage.DataAccess.MongoDb.TechnicalIndicators.AROON;
using AlphaVantage.DataAccess.MongoDb.TechnicalIndicators.AROONOSC;
using AlphaVantage.DataAccess.MongoDb.TechnicalIndicators.MFI;
using AlphaVantage.DataAccess.MongoDb.TechnicalIndicators.TRIX;
using AlphaVantage.DataAccess.MongoDb.TechnicalIndicators.ULTOSC;
using AlphaVantage.DataAccess.MongoDb.TechnicalIndicators.DX;
using AlphaVantage.DataAccess.MongoDb.TechnicalIndicators.MINUS_DI;
using AlphaVantage.DataAccess.MongoDb.TechnicalIndicators.PLUS_DI;
using AlphaVantage.DataAccess.MongoDb.TechnicalIndicators.MINUS_DM;
using AlphaVantage.DataAccess.MongoDb.TechnicalIndicators.PLUS_DM;
using AlphaVantage.DataAccess.MongoDb.TechnicalIndicators.BBANDS;
using AlphaVantage.DataAccess.MongoDb.TechnicalIndicators.MIDPOINT;
using AlphaVantage.DataAccess.MongoDb.TechnicalIndicators.MIDPRICE;
using AlphaVantage.DataAccess.MongoDb.TechnicalIndicators.SAR;
using AlphaVantage.DataAccess.MongoDb.TechnicalIndicators.TRANGE;
using AlphaVantage.DataAccess.MongoDb.TechnicalIndicators.ATR;
using AlphaVantage.DataAccess.MongoDb.TechnicalIndicators.NATR;
using AlphaVantage.DataAccess.MongoDb.TechnicalIndicators.AD;
using AlphaVantage.DataAccess.MongoDb.TechnicalIndicators.ADOSC;
using AlphaVantage.DataAccess.MongoDb.TechnicalIndicators.OBV;
using AlphaVantage.DataAccess.MongoDb.TechnicalIndicators.HT_TRENDLINE;
using AlphaVantage.DataAccess.MongoDb.TechnicalIndicators.HT_SINE;
using AlphaVantage.DataAccess.MongoDb.TechnicalIndicators.HT_TRENDMODE;
using AlphaVantage.DataAccess.MongoDb.TechnicalIndicators.HT_DCPERIOD;
using AlphaVantage.DataAccess.MongoDb.TechnicalIndicators.HT_DCPHASE;
using AlphaVantage.DataAccess.MongoDb.TechnicalIndicators.HT_PHASOR;

using AlphaVantage.DataAccess.MongoDb.TimeSeries.Daily;
using AlphaVantage.DataAccess.MongoDb.TimeSeries.DailyAdjusted;
using AlphaVantage.DataAccess.MongoDb.TimeSeries.IntraDay;
using AlphaVantage.DataAccess.MongoDb.TimeSeries.Monthly;
using AlphaVantage.DataAccess.MongoDb.TimeSeries.MonthlyAdjusted;
using AlphaVantage.DataAccess.MongoDb.TimeSeries.Weekly;
using AlphaVantage.DataAccess.MongoDb.TimeSeries.WeeklyAdjusted;
using AlphaVantage.Utilities.Common;
using Autofac;
using Autofac.Core.Registration;
using System;
using Xunit;

namespace AlphaVantage.Core.Test
{
    public class AvRepositoryFactoryIntegrationTest
    {

        public AvRepositoryFactoryIntegrationTest() { }

        [Theory]
        [ClassData(typeof(ProcessFactoryTestData))]
        public void ShouldCreateValidRepository(string url, System.Type expectedType)
        {
            var container = new AutoFacBootStrapper()
                        .RepositorySetup()
                        .MongoDbSetup()
                        .Build();

            var repositoryFactory = container.Resolve<IAvRepositoryFactory>();
            var repository = DataAccessHelper.ConvertToRepositoryResource(url, repositoryFactory);

            Assert.NotNull(repository);
            Assert.IsType(expectedType, (dynamic)repository);
        }

        [Fact]
        public void ShouldThrowComponentNotRegisteredException()
        {
            var container = new AutoFacBootStrapper()
                        .RepositorySetup()
                        .MongoDbSetup()
                        .Build();

            var factory = container.Resolve<IAvRepositoryFactory>();
            var url = "https://www.alphavantage.co/query?function=__UNKNOWN__&symbol=MSFT&apikey=demo";
            Assert.Throws<ComponentNotRegisteredException>(() =>
                DataAccessHelper.ConvertToRepositoryResource(url, factory));
        }

        [Fact]
        public void ShouldThrowInvalidOperationException()
        {
            var container = new AutoFacBootStrapper()
                        .RepositorySetup()
                        .MongoDbSetup()
                        .Build();

            var factory = container.Resolve<IAvRepositoryFactory>();
            var url = "https://www.alphavantage.co/query?function=EMPTY&symbol=MSFT&apikey=demo";
            Assert.Throws<InvalidOperationException>(() =>
                DataAccessHelper.ConvertToRepositoryResource(url, factory));
        }

        [Fact]
        public void ShouldThrowExceptionOnNullFactory()
        {

            var url = "https://www.alphavantage.co/query?function=EMPTY&symbol=MSFT&apikey=demo";
            Assert.Throws<ArgumentNullException>(() =>
                DataAccessHelper.ConvertToRepositoryResource(url, null));
        }

        [Fact]
        public void ShouldThrowExceptionOnNullOrEmptyUrl()
        {
            var container = new AutoFacBootStrapper()
                        .RepositorySetup()
                        .MongoDbSetup()
                        .Build();

            var factory = container.Resolve<IAvRepositoryFactory>();

            Assert.Throws<ArgumentNullException>(() =>
                DataAccessHelper.ConvertToRepositoryResource(null, factory));

            Assert.Throws<ArgumentNullException>(() =>
                DataAccessHelper.ConvertToRepositoryResource(string.Empty, factory));
        }

        protected class ProcessFactoryTestData : TheoryData<string, System.Type>
        {
            // all possible URLs found in AlphaVantage should be brought over as part of regression testing.
            public ProcessFactoryTestData()
            {
                Add("https://www.alphavantage.co/query?function=TIME_SERIES_INTRADAY&symbol=MSFT&interval=5min&apikey=demo",
                    typeof(AvIntraDayTimeSeriesDefaultRepository));
                Add("https://www.alphavantage.co/query?function=TIME_SERIES_DAILY&symbol=MSFT&apikey=demo",
                    typeof(AvDailyTimeSeriesDefaultRepository));
                Add("https://www.alphavantage.co/query?function=TIME_SERIES_DAILY_ADJUSTED&symbol=MSFT&apikey=demo",
                    typeof(AvDailyAdjTimeSeriesDefaultRepository));
                Add("https://www.alphavantage.co/query?function=TIME_SERIES_DAILY_ADJUSTED&symbol=MSFT&outputsize=full&apikey=demo",
                    typeof(AvDailyAdjTimeSeriesDefaultRepository));
                Add("https://www.alphavantage.co/query?function=TIME_SERIES_WEEKLY&symbol=MSFT&apikey=demo",
                    typeof(AvWeeklyTimeSeriesDefaultRepository));
                Add("https://www.alphavantage.co/query?function=TIME_SERIES_WEEKLY_ADJUSTED&symbol=MSFT&apikey=demo",
                    typeof(AvWeeklyAdjTimeSeriesDefaultRepository));
                Add("https://www.alphavantage.co/query?function=TIME_SERIES_MONTHLY&symbol=MSFT&apikey=demo",
                    typeof(AvMonthlyTimeSeriesDefaultRepository));
                Add("https://www.alphavantage.co/query?function=TIME_SERIES_MONTHLY_ADJUSTED&symbol=MSFT&apikey=demo",
                    typeof(AvMonthlyAdjTimeSeriesDefaultRepository));

                Add("https://www.alphavantage.co/query?function=SMA&symbol=MSFT&interval=weekly&time_period=10&series_type=open&apikey=demo", typeof(AvSMAWeeklyRepository));
                Add("https://www.alphavantage.co/query?function=EMA&symbol=MSFT&interval=weekly&time_period=10&series_type=open&apikey=demo", typeof(AvEMAWeeklyRepository));
                Add("https://www.alphavantage.co/query?function=WMA&symbol=MSFT&interval=weekly&time_period=10&series_type=open&apikey=demo", typeof(AvWMAWeeklyRepository));
                Add("https://www.alphavantage.co/query?function=DEMA&symbol=MSFT&interval=weekly&time_period=10&series_type=open&apikey=demo", typeof(AvDEMAWeeklyRepository));
                Add("https://www.alphavantage.co/query?function=TEMA&symbol=MSFT&interval=weekly&time_period=10&series_type=open&apikey=demo", typeof(AvTEMAWeeklyRepository));
                Add("https://www.alphavantage.co/query?function=TRIMA&symbol=MSFT&interval=weekly&time_period=10&series_type=open&apikey=demo", typeof(AvTRIMAWeeklyRepository));
                Add("https://www.alphavantage.co/query?function=KAMA&symbol=MSFT&interval=weekly&time_period=10&series_type=open&apikey=demo", typeof(AvKAMAWeeklyRepository));
                Add("https://www.alphavantage.co/query?function=MAMA&symbol=MSFT&interval=daily&series_type=close&fastlimit=0.02&apikey=demo", typeof(AvMAMADefaultRepository));
                Add("https://www.alphavantage.co/query?function=VWAP&symbol=MSFT&interval=15min&apikey=demo", typeof(AvVWAPDefaultRepository));
                Add("https://www.alphavantage.co/query?function=T3&symbol=MSFT&interval=weekly&time_period=10&series_type=open&apikey=demo", typeof(AvT3WeeklyRepository));
                Add("https://www.alphavantage.co/query?function=MACD&symbol=MSFT&interval=daily&series_type=open&apikey=demo", typeof(AvMACDDefaultRepository));
                Add("https://www.alphavantage.co/query?function=MACDEXT&symbol=MSFT&interval=daily&series_type=open&apikey=demo", typeof(AvMACDEXTDefaultRepository));
                Add("https://www.alphavantage.co/query?function=STOCH&symbol=MSFT&interval=daily&apikey=demo", typeof(AvSTOCHDefaultRepository));
                Add("https://www.alphavantage.co/query?function=STOCHF&symbol=MSFT&interval=daily&apikey=demo", typeof(AvSTOCHFDefaultRepository));
                Add("https://www.alphavantage.co/query?function=RSI&symbol=MSFT&interval=weekly&time_period=10&series_type=open&apikey=demo", typeof(AvRSIWeeklyRepository));
                Add("https://www.alphavantage.co/query?function=STOCHRSI&symbol=MSFT&interval=daily&time_period=10&series_type=close&fastkperiod=6&fastdmatype=1&apikey=demo", typeof(AvSTOCHRSIDefaultRepository));
                Add("https://www.alphavantage.co/query?function=WILLR&symbol=MSFT&interval=daily&time_period=10&apikey=demo", typeof(AvWILLRDefaultRepository));
                Add("https://www.alphavantage.co/query?function=ADX&symbol=MSFT&interval=daily&time_period=10&apikey=demo", typeof(AvADXDefaultRepository));
                Add("https://www.alphavantage.co/query?function=ADXR&symbol=MSFT&interval=daily&time_period=10&apikey=demo", typeof(AvADXRDefaultRepository));
                Add("https://www.alphavantage.co/query?function=APO&symbol=MSFT&interval=daily&series_type=close&fastperiod=10&matype=1&apikey=demo", typeof(AvAPODefaultRepository));
                Add("https://www.alphavantage.co/query?function=PPO&symbol=MSFT&interval=daily&series_type=close&fastperiod=10&matype=1&apikey=demo", typeof(AvPPODefaultRepository));
                Add("https://www.alphavantage.co/query?function=MOM&symbol=MSFT&interval=daily&time_period=10&series_type=close&apikey=demo", typeof(AvMOMDefaultRepository));
                Add("https://www.alphavantage.co/query?function=BOP&symbol=MSFT&interval=daily&apikey=demo", typeof(AvBOPDefaultRepository));
                Add("https://www.alphavantage.co/query?function=CCI&symbol=MSFT&interval=daily&time_period=10&apikey=demo", typeof(AvCCIDefaultRepository));
                Add("https://www.alphavantage.co/query?function=CMO&symbol=MSFT&interval=weekly&time_period=10&series_type=close&apikey=demo", typeof(AvCMOWeeklyRepository));
                Add("https://www.alphavantage.co/query?function=ROC&symbol=MSFT&interval=weekly&time_period=10&series_type=close&apikey=demo", typeof(AvROCWeeklyRepository));
                Add("https://www.alphavantage.co/query?function=ROCR&symbol=MSFT&interval=daily&time_period=10&series_type=close&apikey=demo", typeof(AvROCRDefaultRepository));
                Add("https://www.alphavantage.co/query?function=AROON&symbol=MSFT&interval=daily&time_period=14&apikey=demo", typeof(AvAROONDefaultRepository));
                Add("https://www.alphavantage.co/query?function=AROONOSC&symbol=MSFT&interval=daily&time_period=10&apikey=demo", typeof(AvAROONOSCDefaultRepository));
                Add("https://www.alphavantage.co/query?function=MFI&symbol=MSFT&interval=weekly&time_period=10&apikey=demo", typeof(AvMFIWeeklyRepository));
                Add("https://www.alphavantage.co/query?function=TRIX&symbol=MSFT&interval=daily&time_period=10&series_type=close&apikey=demo", typeof(AvTRIXDefaultRepository));
                Add("https://www.alphavantage.co/query?function=ULTOSC&symbol=MSFT&interval=daily&timeperiod1=8&apikey=demo", typeof(AvULTOSCDefaultRepository));
                Add("https://www.alphavantage.co/query?function=DX&symbol=MSFT&interval=daily&time_period=10&apikey=demo", typeof(AvDXDefaultRepository));
                Add("https://www.alphavantage.co/query?function=MINUS_DI&symbol=MSFT&interval=weekly&time_period=10&apikey=demo", typeof(AvMINUS_DIWeeklyRepository));
                Add("https://www.alphavantage.co/query?function=PLUS_DI&symbol=MSFT&interval=daily&time_period=10&apikey=demo", typeof(AvPLUS_DIDefaultRepository));
                Add("https://www.alphavantage.co/query?function=MINUS_DM&symbol=MSFT&interval=daily&time_period=10&apikey=demo", typeof(AvMINUS_DMDefaultRepository));
                Add("https://www.alphavantage.co/query?function=PLUS_DM&symbol=MSFT&interval=daily&time_period=10&apikey=demo", typeof(AvPLUS_DMDefaultRepository));
                Add("https://www.alphavantage.co/query?function=BBANDS&symbol=MSFT&interval=weekly&time_period=5&series_type=close&nbdevup=3&nbdevdn=3&apikey=demo", typeof(AvBBANDSWeeklyRepository));
                Add("https://www.alphavantage.co/query?function=MIDPOINT&symbol=MSFT&interval=daily&time_period=10&series_type=close&apikey=demo", typeof(AvMIDPOINTDefaultRepository));
                Add("https://www.alphavantage.co/query?function=MIDPRICE&symbol=MSFT&interval=daily&time_period=10&apikey=demo", typeof(AvMIDPRICEDefaultRepository));
                Add("https://www.alphavantage.co/query?function=SAR&symbol=MSFT&interval=weekly&acceleration=0.05&maximum=0.25&apikey=demo", typeof(AvSARWeeklyRepository));
                Add("https://www.alphavantage.co/query?function=TRANGE&symbol=MSFT&interval=daily&apikey=demo", typeof(AvTRANGEDefaultRepository));
                Add("https://www.alphavantage.co/query?function=ATR&symbol=MSFT&interval=daily&time_period=14&apikey=demo", typeof(AvATRDefaultRepository));
                Add("https://www.alphavantage.co/query?function=NATR&symbol=MSFT&interval=weekly&time_period=14&apikey=demo", typeof(AvNATRWeeklyRepository));
                Add("https://www.alphavantage.co/query?function=AD&symbol=MSFT&interval=daily&apikey=demo", typeof(AvADDefaultRepository));
                Add("https://www.alphavantage.co/query?function=ADOSC&symbol=MSFT&interval=daily&fastperiod=5&apikey=demo", typeof(AvADOSCDefaultRepository));
                Add("https://www.alphavantage.co/query?function=OBV&symbol=MSFT&interval=weekly&apikey=demo", typeof(AvOBVWeeklyRepository));
                Add("https://www.alphavantage.co/query?function=HT_TRENDLINE&symbol=MSFT&interval=daily&series_type=close&apikey=demo", typeof(AvHT_TRENDLINEDefaultRepository));
                Add("https://www.alphavantage.co/query?function=HT_SINE&symbol=MSFT&interval=daily&series_type=close&apikey=demo", typeof(AvHT_SINEDefaultRepository));
                Add("https://www.alphavantage.co/query?function=HT_TRENDMODE&symbol=MSFT&interval=weekly&series_type=close&apikey=demo", typeof(AvHT_TRENDMODEWeeklyRepository));
                Add("https://www.alphavantage.co/query?function=HT_DCPERIOD&symbol=MSFT&interval=daily&series_type=close&apikey=demo", typeof(AvHT_DCPERIODDefaultRepository));
                Add("https://www.alphavantage.co/query?function=HT_DCPHASE&symbol=MSFT&interval=daily&series_type=close&apikey=demo", typeof(AvHT_DCPHASEDefaultRepository));
                Add("https://www.alphavantage.co/query?function=HT_PHASOR&symbol=MSFT&interval=weekly&series_type=close&apikey=demo", typeof(AvHT_PHASORWeeklyRepository));

            }

        }
    }
}
