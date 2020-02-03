using AlphaVantage.Common.Models.TechnicalIndicators.SMA;
using AlphaVantage.Common.Models.TechnicalIndicators.EMA;
using AlphaVantage.Common.Models.TechnicalIndicators.WMA;
using AlphaVantage.Common.Models.TechnicalIndicators.DEMA;
using AlphaVantage.Common.Models.TechnicalIndicators.TRIMA;
using AlphaVantage.Common.Models.TechnicalIndicators.KAMA;
using AlphaVantage.Common.Models.TechnicalIndicators.TEMA;
using AlphaVantage.Common.Models.TechnicalIndicators.MAMA;
using AlphaVantage.Common.Models.TechnicalIndicators.T3;
using AlphaVantage.Common.Models.TechnicalIndicators.MACD;
using AlphaVantage.Common.Models.TechnicalIndicators.STOCHRSI;
using AlphaVantage.Common.Models.TechnicalIndicators.WILLR;
using AlphaVantage.Common.Models.TechnicalIndicators.ADX;
using AlphaVantage.Common.Models.TechnicalIndicators.ADXR;
using AlphaVantage.Common.Models.TechnicalIndicators.APO;
using AlphaVantage.Common.Models.TechnicalIndicators.PPO;
using AlphaVantage.Common.Models.TechnicalIndicators.MOM;
using AlphaVantage.Common.Models.TechnicalIndicators.BOP;
using AlphaVantage.Common.Models.TechnicalIndicators.CCI;
using AlphaVantage.Common.Models.TechnicalIndicators.CMO;
using AlphaVantage.Common.Models.TechnicalIndicators.ROC;
using AlphaVantage.Common.Models.TechnicalIndicators.ROCR;
using AlphaVantage.Common.Models.TechnicalIndicators.AROON;
using AlphaVantage.Common.Models.TechnicalIndicators.AROONOSC;
using AlphaVantage.Common.Models.TechnicalIndicators.MFI;
using AlphaVantage.Common.Models.TechnicalIndicators.TRIX;
using AlphaVantage.Common.Models.TechnicalIndicators.ULTOSC;
using AlphaVantage.Common.Models.TechnicalIndicators.DX;
using AlphaVantage.Common.Models.TechnicalIndicators.MINUS_DI;
using AlphaVantage.Common.Models.TechnicalIndicators.PLUS_DI;
using AlphaVantage.Common.Models.TechnicalIndicators.MINUS_DM;
using AlphaVantage.Common.Models.TechnicalIndicators.PLUS_DM;
using AlphaVantage.Common.Models.TechnicalIndicators.MIDPOINT;
using AlphaVantage.Common.Models.TechnicalIndicators.MIDPRICE;
using AlphaVantage.Common.Models.TechnicalIndicators.SAR;
using AlphaVantage.Common.Models.TechnicalIndicators.TRANGE;
using AlphaVantage.Common.Models.TechnicalIndicators.ATR;
using AlphaVantage.Common.Models.TechnicalIndicators.NATR;
using AlphaVantage.Common.Models.TechnicalIndicators.ADOSC;
using AlphaVantage.Common.Models.TechnicalIndicators.OBV;
using AlphaVantage.Common.Models.TechnicalIndicators.HT_TRENDLINE;
using AlphaVantage.Common.Models.TechnicalIndicators.HT_SINE;
using AlphaVantage.Common.Models.TechnicalIndicators.HT_TRENDMODE;
using AlphaVantage.Common.Models.TechnicalIndicators.HT_DCPERIOD;
using AlphaVantage.Common.Models.TechnicalIndicators.HT_DCPHASE;
using AlphaVantage.Common.Models.TechnicalIndicators.HT_PHASOR;
using AlphaVantage.Common.Models.TechnicalIndicators.STOCH;
using AlphaVantage.Common.Models.TechnicalIndicators.VWAP;
using AlphaVantage.Common.Models.TechnicalIndicators.AD;
using AlphaVantage.Common.Models.TechnicalIndicators.MACDEXT;
using AlphaVantage.Common.Models.TechnicalIndicators.STOCHF;
using AlphaVantage.Common.Models.TechnicalIndicators.RSI;
using AlphaVantage.Common.Models.TechnicalIndicators.BBANDS;

using AlphaVantage.Common.Models.TimeSeries.Daily;
using AlphaVantage.Common.Models.TimeSeries.DailyAdjusted;
using AlphaVantage.Common.Models.TimeSeries.IntraDay;
using AlphaVantage.Common.Models.TimeSeries.Monthly;
using AlphaVantage.Common.Models.TimeSeries.MonthlyAdjusted;
using AlphaVantage.Common.Models.TimeSeries.WeeklyAdjusted;
using AlphaVantage.Common.Models.TimeSeries.Weekly;

using AlphaVantage.Core.Interfaces;
using Xunit;
using AlphaVantage.Utilities.Common;
using AlphaVantage.Core.Common;
using Autofac;

namespace AlphaVantage.Core.Test
{
    public class ConvertToMapResourceSnapshotTest
    {
        [Theory]
        [ClassData(typeof(ProcessFactoryTestData))]
        public void ShouldConvertToMapResource(string url, System.Type expectedType)
        {
            var container = new AutoFacBootStrapper()
            .MapResourceSetup()
            .Build();

            var downloadFactory = container.Resolve<IAvMapFactory>();
            var mapper = CoreHelper.ConvertToMapResource(url, downloadFactory);

            Assert.IsAssignableFrom(expectedType, (dynamic) mapper);
        }


        protected class ProcessFactoryTestData : TheoryData<string, System.Type>
        {
            // all possible URLs found in AlphaVantage should be brought over as part of regression testing.
            public ProcessFactoryTestData()
            {
                Add("https://www.alphavantage.co/query?function=TIME_SERIES_INTRADAY&symbol=MSFT&interval=5min&apikey=demo", typeof(IMapResource<AvIntraDayTimeSeries>));
                Add("https://www.alphavantage.co/query?function=TIME_SERIES_DAILY&symbol=MSFT&apikey=demo", typeof(IMapResource<AvDailyTimeSeries>));
                Add("https://www.alphavantage.co/query?function=TIME_SERIES_DAILY_ADJUSTED&symbol=MSFT&apikey=demo", typeof(IMapResource<AvDailyAdjTimeSeries>));
                Add("https://www.alphavantage.co/query?function=TIME_SERIES_DAILY_ADJUSTED&symbol=MSFT&outputsize=full&apikey=demo", typeof(IMapResource<AvDailyAdjTimeSeries>));
                Add("https://www.alphavantage.co/query?function=TIME_SERIES_WEEKLY&symbol=MSFT&apikey=demo", typeof(IMapResource<AvWeeklyTimeSeries>));
                Add("https://www.alphavantage.co/query?function=TIME_SERIES_WEEKLY_ADJUSTED&symbol=MSFT&apikey=demo", typeof(IMapResource<AvWeeklyAdjTimeSeries>));
                Add("https://www.alphavantage.co/query?function=TIME_SERIES_MONTHLY&symbol=MSFT&apikey=demo", typeof(IMapResource<AvMonthlyTimeSeries>));
                Add("https://www.alphavantage.co/query?function=TIME_SERIES_MONTHLY_ADJUSTED&symbol=MSFT&apikey=demo", typeof(IMapResource<AvMonthlyAdjTimeSeries>));

                Add("https://www.alphavantage.co/query?function=SMA&symbol=MSFT&interval=weekly&time_period=10&series_type=open&apikey=demo", typeof(IMapResource<AvSMA>));
                Add("https://www.alphavantage.co/query?function=EMA&symbol=MSFT&interval=weekly&time_period=10&series_type=open&apikey=demo", typeof(IMapResource<AvEMA>));
                Add("https://www.alphavantage.co/query?function=WMA&symbol=MSFT&interval=weekly&time_period=10&series_type=open&apikey=demo", typeof(IMapResource<AvWMA>));
                Add("https://www.alphavantage.co/query?function=DEMA&symbol=MSFT&interval=weekly&time_period=10&series_type=open&apikey=demo", typeof(IMapResource<AvDEMA>));
                Add("https://www.alphavantage.co/query?function=TEMA&symbol=MSFT&interval=weekly&time_period=10&series_type=open&apikey=demo", typeof(IMapResource<AvTEMA>));
                Add("https://www.alphavantage.co/query?function=TRIMA&symbol=MSFT&interval=weekly&time_period=10&series_type=open&apikey=demo", typeof(IMapResource<AvTRIMA>));
                Add("https://www.alphavantage.co/query?function=KAMA&symbol=MSFT&interval=weekly&time_period=10&series_type=open&apikey=demo", typeof(IMapResource<AvKAMA>));
                Add("https://www.alphavantage.co/query?function=MAMA&symbol=MSFT&interval=daily&series_type=close&fastlimit=0.02&apikey=demo", typeof(IMapResource<AvMAMA>));
                Add("https://www.alphavantage.co/query?function=VWAP&symbol=MSFT&interval=15min&apikey=demo", typeof(IMapResource<AvVWAP>));
                Add("https://www.alphavantage.co/query?function=T3&symbol=MSFT&interval=weekly&time_period=10&series_type=open&apikey=demo", typeof(IMapResource<AvT3>));
                Add("https://www.alphavantage.co/query?function=MACD&symbol=MSFT&interval=daily&series_type=open&apikey=demo", typeof(IMapResource<AvMACD>));
                Add("https://www.alphavantage.co/query?function=MACDEXT&symbol=MSFT&interval=daily&series_type=open&apikey=demo", typeof(IMapResource<AvMACDEXT>));
                Add("https://www.alphavantage.co/query?function=STOCH&symbol=MSFT&interval=daily&apikey=demo", typeof(IMapResource<AvSTOCH>));
                Add("https://www.alphavantage.co/query?function=STOCHF&symbol=MSFT&interval=daily&apikey=demo", typeof(IMapResource<AvSTOCHF>));
                Add("https://www.alphavantage.co/query?function=RSI&symbol=MSFT&interval=weekly&time_period=10&series_type=open&apikey=demo", typeof(IMapResource<AvRSI>));
                Add("https://www.alphavantage.co/query?function=STOCHRSI&symbol=MSFT&interval=daily&time_period=10&series_type=close&fastkperiod=6&fastdmatype=1&apikey=demo", typeof(IMapResource<AvSTOCHRSI>));
                Add("https://www.alphavantage.co/query?function=WILLR&symbol=MSFT&interval=daily&time_period=10&apikey=demo", typeof(IMapResource<AvWILLR>));
                Add("https://www.alphavantage.co/query?function=ADX&symbol=MSFT&interval=daily&time_period=10&apikey=demo", typeof(IMapResource<AvADX>));
                Add("https://www.alphavantage.co/query?function=ADXR&symbol=MSFT&interval=daily&time_period=10&apikey=demo", typeof(IMapResource<AvADXR>));
                Add("https://www.alphavantage.co/query?function=APO&symbol=MSFT&interval=daily&series_type=close&fastperiod=10&matype=1&apikey=demo", typeof(IMapResource<AvAPO>));
                Add("https://www.alphavantage.co/query?function=PPO&symbol=MSFT&interval=daily&series_type=close&fastperiod=10&matype=1&apikey=demo", typeof(IMapResource<AvPPO>));
                Add("https://www.alphavantage.co/query?function=MOM&symbol=MSFT&interval=daily&time_period=10&series_type=close&apikey=demo", typeof(IMapResource<AvMOM>));
                Add("https://www.alphavantage.co/query?function=BOP&symbol=MSFT&interval=daily&apikey=demo", typeof(IMapResource<AvBOP>));
                Add("https://www.alphavantage.co/query?function=CCI&symbol=MSFT&interval=daily&time_period=10&apikey=demo", typeof(IMapResource<AvCCI>));
                Add("https://www.alphavantage.co/query?function=CMO&symbol=MSFT&interval=weekly&time_period=10&series_type=close&apikey=demo", typeof(IMapResource<AvCMO>));
                Add("https://www.alphavantage.co/query?function=ROC&symbol=MSFT&interval=weekly&time_period=10&series_type=close&apikey=demo", typeof(IMapResource<AvROC>));
                Add("https://www.alphavantage.co/query?function=ROCR&symbol=MSFT&interval=daily&time_period=10&series_type=close&apikey=demo", typeof(IMapResource<AvROCR>));
                Add("https://www.alphavantage.co/query?function=AROON&symbol=MSFT&interval=daily&time_period=14&apikey=demo", typeof(IMapResource<AvAROON>));
                Add("https://www.alphavantage.co/query?function=AROONOSC&symbol=MSFT&interval=daily&time_period=10&apikey=demo", typeof(IMapResource<AvAROONOSC>));
                Add("https://www.alphavantage.co/query?function=MFI&symbol=MSFT&interval=weekly&time_period=10&apikey=demo", typeof(IMapResource<AvMFI>));
                Add("https://www.alphavantage.co/query?function=TRIX&symbol=MSFT&interval=daily&time_period=10&series_type=close&apikey=demo", typeof(IMapResource<AvTRIX>));
                Add("https://www.alphavantage.co/query?function=ULTOSC&symbol=MSFT&interval=daily&timeperiod1=8&apikey=demo", typeof(IMapResource<AvULTOSC>));
                Add("https://www.alphavantage.co/query?function=DX&symbol=MSFT&interval=daily&time_period=10&apikey=demo", typeof(IMapResource<AvDX>));
                Add("https://www.alphavantage.co/query?function=MINUS_DI&symbol=MSFT&interval=weekly&time_period=10&apikey=demo", typeof(IMapResource<AvMINUS_DI>));
                Add("https://www.alphavantage.co/query?function=PLUS_DI&symbol=MSFT&interval=daily&time_period=10&apikey=demo", typeof(IMapResource<AvPLUS_DI>));
                Add("https://www.alphavantage.co/query?function=MINUS_DM&symbol=MSFT&interval=daily&time_period=10&apikey=demo", typeof(IMapResource<AvMINUS_DM>));
                Add("https://www.alphavantage.co/query?function=PLUS_DM&symbol=MSFT&interval=daily&time_period=10&apikey=demo", typeof(IMapResource<AvPLUS_DM>));
                Add("https://www.alphavantage.co/query?function=BBANDS&symbol=MSFT&interval=weekly&time_period=5&series_type=close&nbdevup=3&nbdevdn=3&apikey=demo", typeof(IMapResource<AvBBANDS>));
                Add("https://www.alphavantage.co/query?function=MIDPOINT&symbol=MSFT&interval=daily&time_period=10&series_type=close&apikey=demo", typeof(IMapResource<AvMIDPOINT>));
                Add("https://www.alphavantage.co/query?function=MIDPRICE&symbol=MSFT&interval=daily&time_period=10&apikey=demo", typeof(IMapResource<AvMIDPRICE>));
                Add("https://www.alphavantage.co/query?function=SAR&symbol=MSFT&interval=weekly&acceleration=0.05&maximum=0.25&apikey=demo", typeof(IMapResource<AvSAR>));
                Add("https://www.alphavantage.co/query?function=TRANGE&symbol=MSFT&interval=daily&apikey=demo", typeof(IMapResource<AvTRANGE>));
                Add("https://www.alphavantage.co/query?function=ATR&symbol=MSFT&interval=daily&time_period=14&apikey=demo", typeof(IMapResource<AvATR>));
                Add("https://www.alphavantage.co/query?function=NATR&symbol=MSFT&interval=weekly&time_period=14&apikey=demo", typeof(IMapResource<AvNATR>));
                Add("https://www.alphavantage.co/query?function=AD&symbol=MSFT&interval=daily&apikey=demo", typeof(IMapResource<AvAD>));
                Add("https://www.alphavantage.co/query?function=ADOSC&symbol=MSFT&interval=daily&fastperiod=5&apikey=demo", typeof(IMapResource<AvADOSC>));
                Add("https://www.alphavantage.co/query?function=OBV&symbol=MSFT&interval=weekly&apikey=demo", typeof(IMapResource<AvOBV>));
                Add("https://www.alphavantage.co/query?function=HT_TRENDLINE&symbol=MSFT&interval=daily&series_type=close&apikey=demo", typeof(IMapResource<AvHT_TRENDLINE>));
                Add("https://www.alphavantage.co/query?function=HT_SINE&symbol=MSFT&interval=daily&series_type=close&apikey=demo", typeof(IMapResource<AvHT_SINE>));
                Add("https://www.alphavantage.co/query?function=HT_TRENDMODE&symbol=MSFT&interval=weekly&series_type=close&apikey=demo", typeof(IMapResource<AvHT_TRENDMODE>));
                Add("https://www.alphavantage.co/query?function=HT_DCPERIOD&symbol=MSFT&interval=daily&series_type=close&apikey=demo", typeof(IMapResource<AvHT_DCPERIOD>));
                Add("https://www.alphavantage.co/query?function=HT_DCPHASE&symbol=MSFT&interval=daily&series_type=close&apikey=demo", typeof(IMapResource<AvHT_DCPHASE>));
                Add("https://www.alphavantage.co/query?function=HT_PHASOR&symbol=MSFT&interval=weekly&series_type=close&apikey=demo", typeof(IMapResource<AvHT_PHASOR>));

            }
        }

    }
}
