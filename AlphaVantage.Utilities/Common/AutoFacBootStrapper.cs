using System;
using Autofac;
using MongoDB.Driver;

using AlphaVantage.Common;
using AlphaVantage.Core.Interfaces;
using AlphaVantage.DataAccess.Base;
using AlphaVantage.DataAccess.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Autofac.Extensions.DependencyInjection;
using Autofac.Core;
using AlphaVantage.Core.Common;
using AlphaVantage.Core.TimeSeries.DailyAdjusted;
using AlphaVantage.Core.TechnicalIndicators.RSI;
using AlphaVantage.Core.TimeSeries.IntraDay;
using AlphaVantage.Core.TimeSeries.Monthly;
using AlphaVantage.Core.TimeSeries.Daily;
using AlphaVantage.Core.TimeSeries.MonthlyAdjusted;
using AlphaVantage.Core.TimeSeries.Weekly;
using AlphaVantage.Core.TimeSeries.WeeklyAdjusted;
using AlphaVantage.Utilities.Interfaces;

using AlphaVantage.Core.TechnicalIndicators.SMA;
using AlphaVantage.Core.TechnicalIndicators.EMA;
using AlphaVantage.Core.TechnicalIndicators.WMA;
using AlphaVantage.Core.TechnicalIndicators.DEMA;
using AlphaVantage.Core.TechnicalIndicators.TRIMA;
using AlphaVantage.Core.TechnicalIndicators.KAMA;
using AlphaVantage.Core.TechnicalIndicators.TEMA;
using AlphaVantage.Core.TechnicalIndicators.MAMA;
using AlphaVantage.Core.TechnicalIndicators.T3;
using AlphaVantage.Core.TechnicalIndicators.MACD;
using AlphaVantage.Core.TechnicalIndicators.STOCHRSI;
using AlphaVantage.Core.TechnicalIndicators.WILLR;
using AlphaVantage.Core.TechnicalIndicators.ADX;
using AlphaVantage.Core.TechnicalIndicators.ADXR;
using AlphaVantage.Core.TechnicalIndicators.APO;
using AlphaVantage.Core.TechnicalIndicators.PPO;
using AlphaVantage.Core.TechnicalIndicators.MOM;
using AlphaVantage.Core.TechnicalIndicators.BOP;
using AlphaVantage.Core.TechnicalIndicators.CCI;
using AlphaVantage.Core.TechnicalIndicators.CMO;
using AlphaVantage.Core.TechnicalIndicators.ROC;
using AlphaVantage.Core.TechnicalIndicators.ROCR;
using AlphaVantage.Core.TechnicalIndicators.AROON;
using AlphaVantage.Core.TechnicalIndicators.AROONOSC;
using AlphaVantage.Core.TechnicalIndicators.MFI;
using AlphaVantage.Core.TechnicalIndicators.TRIX;
using AlphaVantage.Core.TechnicalIndicators.ULTOSC;
using AlphaVantage.Core.TechnicalIndicators.DX;
using AlphaVantage.Core.TechnicalIndicators.MINUS_DI;
using AlphaVantage.Core.TechnicalIndicators.PLUS_DI;
using AlphaVantage.Core.TechnicalIndicators.MINUS_DM;
using AlphaVantage.Core.TechnicalIndicators.PLUS_DM;
using AlphaVantage.Core.TechnicalIndicators.MIDPOINT;
using AlphaVantage.Core.TechnicalIndicators.MIDPRICE;
using AlphaVantage.Core.TechnicalIndicators.SAR;
using AlphaVantage.Core.TechnicalIndicators.TRANGE;
using AlphaVantage.Core.TechnicalIndicators.ATR;
using AlphaVantage.Core.TechnicalIndicators.NATR;
using AlphaVantage.Core.TechnicalIndicators.ADOSC;
using AlphaVantage.Core.TechnicalIndicators.OBV;
using AlphaVantage.Core.TechnicalIndicators.HT_TRENDLINE;
using AlphaVantage.Core.TechnicalIndicators.HT_SINE;
using AlphaVantage.Core.TechnicalIndicators.HT_TRENDMODE;
using AlphaVantage.Core.TechnicalIndicators.HT_DCPERIOD;
using AlphaVantage.Core.TechnicalIndicators.HT_DCPHASE;
using AlphaVantage.Core.TechnicalIndicators.HT_PHASOR;
using AlphaVantage.Core.TechnicalIndicators.STOCH;
using AlphaVantage.Core.TechnicalIndicators.VWAP;
using AlphaVantage.Core.TechnicalIndicators.AD;
using AlphaVantage.Core.TechnicalIndicators.MACDEXT;

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
using AlphaVantage.DataAccess.MongoDb.TimeSeries.DailyAdjusted;
using AlphaVantage.DataAccess.MongoDb.TimeSeries.Daily;
using AlphaVantage.DataAccess.MongoDb.TimeSeries.IntraDay;
using AlphaVantage.DataAccess.MongoDb.TimeSeries.Weekly;
using AlphaVantage.DataAccess.MongoDb.TimeSeries.WeeklyAdjusted;
using AlphaVantage.DataAccess.MongoDb.TimeSeries.Monthly;
using AlphaVantage.DataAccess.MongoDb.TimeSeries.MonthlyAdjusted;
using AlphaVantage.Core.TechnicalIndicators.BBANDS;
using AlphaVantage.Core.TechnicalIndicators.STOCHF;

namespace AlphaVantage.Utilities.Common
{
    public class AutoFacBootStrapper : IBootStrap, IDisposable
    {
        readonly ContainerBuilder _builder = new ContainerBuilder();
        readonly IServiceCollection _services = new ServiceCollection();
        readonly IConfigurationRoot _config;

        public AutoFacBootStrapper()
        {
            _config = new SystemConfig(SystemConfigRes.AppSettingsFileName).AppSetttings;
            _services.AddLogging();
        }

        protected ContainerBuilder Builder => _builder;

        public virtual AutoFacBootStrapper MongoDbSetup()
        {
            
            string connectionString = _config[SystemConfigRes.AvConnectionStringKey];
            string dbName = _config[SystemConfigRes.MongoDbNameKey];


            _builder.RegisterType<MongoClient>().As<IMongoClient>()
                .WithParameter("connectionString", connectionString);
            _builder.RegisterType<MongoDatabaseBase>().As<IMongoDatabase>();
            _builder.RegisterType<MongoDbContext>().As<IContext<IMongoClient, IMongoDatabase>>();


            return this;
        }

        public virtual AutoFacBootStrapper MapResourceSetup()
        {
            // Technical Indicators
            _builder.RegisterType<AvSMAProcess>().Keyed<IMapResourceAnchor>(AvFunctionEnum.SMA);
            _builder.RegisterType<AvEMAProcess>().Keyed<IMapResourceAnchor>(AvFunctionEnum.EMA);
            _builder.RegisterType<AvWMAProcess>().Keyed<IMapResourceAnchor>(AvFunctionEnum.WMA);
            _builder.RegisterType<AvDEMAProcess>().Keyed<IMapResourceAnchor>(AvFunctionEnum.DEMA);
            _builder.RegisterType<AvTEMAProcess>().Keyed<IMapResourceAnchor>(AvFunctionEnum.TEMA);
            _builder.RegisterType<AvTRIMAProcess>().Keyed<IMapResourceAnchor>(AvFunctionEnum.TRIMA);
            _builder.RegisterType<AvKAMAProcess>().Keyed<IMapResourceAnchor>(AvFunctionEnum.KAMA);
            _builder.RegisterType<AvMAMAProcess>().Keyed<IMapResourceAnchor>(AvFunctionEnum.MAMA);
            _builder.RegisterType<AvT3Process>().Keyed<IMapResourceAnchor>(AvFunctionEnum.T3);
            _builder.RegisterType<AvMACDProcess>().Keyed<IMapResourceAnchor>(AvFunctionEnum.MACD);
            _builder.RegisterType<AvRSIProcess>().Keyed<IMapResourceAnchor>(AvFunctionEnum.RSI);
            _builder.RegisterType<AvSTOCHRSIProcess>().Keyed<IMapResourceAnchor>(AvFunctionEnum.STOCHRSI);
            _builder.RegisterType<AvWILLRProcess>().Keyed<IMapResourceAnchor>(AvFunctionEnum.WILLR);
            _builder.RegisterType<AvADXProcess>().Keyed<IMapResourceAnchor>(AvFunctionEnum.ADX);
            _builder.RegisterType<AvADXRProcess>().Keyed<IMapResourceAnchor>(AvFunctionEnum.ADXR);
            _builder.RegisterType<AvAPOProcess>().Keyed<IMapResourceAnchor>(AvFunctionEnum.APO);
            _builder.RegisterType<AvPPOProcess>().Keyed<IMapResourceAnchor>(AvFunctionEnum.PPO);
            _builder.RegisterType<AvMOMProcess>().Keyed<IMapResourceAnchor>(AvFunctionEnum.MOM);
            _builder.RegisterType<AvBOPProcess>().Keyed<IMapResourceAnchor>(AvFunctionEnum.BOP);
            _builder.RegisterType<AvCCIProcess>().Keyed<IMapResourceAnchor>(AvFunctionEnum.CCI);
            _builder.RegisterType<AvCMOProcess>().Keyed<IMapResourceAnchor>(AvFunctionEnum.CMO);
            _builder.RegisterType<AvROCProcess>().Keyed<IMapResourceAnchor>(AvFunctionEnum.ROC);
            _builder.RegisterType<AvROCRProcess>().Keyed<IMapResourceAnchor>(AvFunctionEnum.ROCR);
            _builder.RegisterType<AvAROONProcess>().Keyed<IMapResourceAnchor>(AvFunctionEnum.AROON);
            _builder.RegisterType<AvAROONOSCProcess>().Keyed<IMapResourceAnchor>(AvFunctionEnum.AROONOSC);
            _builder.RegisterType<AvMFIProcess>().Keyed<IMapResourceAnchor>(AvFunctionEnum.MFI);
            _builder.RegisterType<AvTRIXProcess>().Keyed<IMapResourceAnchor>(AvFunctionEnum.TRIX);
            _builder.RegisterType<AvULTOSCProcess>().Keyed<IMapResourceAnchor>(AvFunctionEnum.ULTOSC);
            _builder.RegisterType<AvDXProcess>().Keyed<IMapResourceAnchor>(AvFunctionEnum.DX);
            _builder.RegisterType<AvMINUS_DIProcess>().Keyed<IMapResourceAnchor>(AvFunctionEnum.MINUS_DI);
            _builder.RegisterType<AvPLUS_DIProcess>().Keyed<IMapResourceAnchor>(AvFunctionEnum.PLUS_DI);
            _builder.RegisterType<AvMINUS_DMProcess>().Keyed<IMapResourceAnchor>(AvFunctionEnum.MINUS_DM);
            _builder.RegisterType<AvPLUS_DMProcess>().Keyed<IMapResourceAnchor>(AvFunctionEnum.PLUS_DM);
            _builder.RegisterType<AvBBANDSProcess>().Keyed<IMapResourceAnchor>(AvFunctionEnum.BBANDS);
            _builder.RegisterType<AvMIDPOINTProcess>().Keyed<IMapResourceAnchor>(AvFunctionEnum.MIDPOINT);
            _builder.RegisterType<AvMIDPRICEProcess>().Keyed<IMapResourceAnchor>(AvFunctionEnum.MIDPRICE);
            _builder.RegisterType<AvSARProcess>().Keyed<IMapResourceAnchor>(AvFunctionEnum.SAR);
            _builder.RegisterType<AvTRANGEProcess>().Keyed<IMapResourceAnchor>(AvFunctionEnum.TRANGE);
            _builder.RegisterType<AvATRProcess>().Keyed<IMapResourceAnchor>(AvFunctionEnum.ATR);
            _builder.RegisterType<AvNATRProcess>().Keyed<IMapResourceAnchor>(AvFunctionEnum.NATR);
            _builder.RegisterType<AvADOSCProcess>().Keyed<IMapResourceAnchor>(AvFunctionEnum.ADOSC);
            _builder.RegisterType<AvOBVProcess>().Keyed<IMapResourceAnchor>(AvFunctionEnum.OBV);
            _builder.RegisterType<AvHT_TRENDLINEProcess>().Keyed<IMapResourceAnchor>(AvFunctionEnum.HT_TRENDLINE);
            _builder.RegisterType<AvHT_SINEProcess>().Keyed<IMapResourceAnchor>(AvFunctionEnum.HT_SINE);
            _builder.RegisterType<AvHT_TRENDMODEProcess>().Keyed<IMapResourceAnchor>(AvFunctionEnum.HT_TRENDMODE);
            _builder.RegisterType<AvHT_DCPERIODProcess>().Keyed<IMapResourceAnchor>(AvFunctionEnum.HT_DCPERIOD);
            _builder.RegisterType<AvHT_DCPHASEProcess>().Keyed<IMapResourceAnchor>(AvFunctionEnum.HT_DCPHASE);
            _builder.RegisterType<AvSTOCHProcess>().Keyed<IMapResourceAnchor>(AvFunctionEnum.STOCH);
            _builder.RegisterType<AvSTOCHFProcess>().Keyed<IMapResourceAnchor>(AvFunctionEnum.STOCHF);
            _builder.RegisterType<AvVWAPProcess>().Keyed<IMapResourceAnchor>(AvFunctionEnum.VWAP);
            _builder.RegisterType<AvADProcess>().Keyed<IMapResourceAnchor>(AvFunctionEnum.AD);
            _builder.RegisterType<AvMACDEXTProcess>().Keyed<IMapResourceAnchor>(AvFunctionEnum.MACDEXT);
            _builder.RegisterType<AvHT_PHASORProcess>().Keyed<IMapResourceAnchor>(AvFunctionEnum.HT_PHASOR);

            // Time Series
            _builder.RegisterType<AvDailyAdjTimeSeriesProcess>().Keyed<IMapResourceAnchor>(AvFunctionEnum.DailyAdjusted);
            _builder.RegisterType<AvIntraDayTimeSeriesProcess>().Keyed<IMapResourceAnchor>(AvFunctionEnum.InteraDay);
            _builder.RegisterType<AvDailyTimeSeriesProcess>().Keyed<IMapResourceAnchor>(AvFunctionEnum.Daily);
            _builder.RegisterType<AvMonthlyTimeSeriesProcess>().Keyed<IMapResourceAnchor>(AvFunctionEnum.Monthly);
            _builder.RegisterType<AvMonthlyAdjTimeSeriesProcess>().Keyed<IMapResourceAnchor>(AvFunctionEnum.MonthlyAdjusted);
            _builder.RegisterType<AvWeeklyTimeSeriesProcess>().Keyed<IMapResourceAnchor>(AvFunctionEnum.Weekly);
            _builder.RegisterType<AvWeeklyAdjTimeSeriesProcess>().Keyed<IMapResourceAnchor>(AvFunctionEnum.WeeklyAdjusted);


            _builder.RegisterType<AvMapFactory>().As<IAvMapFactory>();

            return this;
        }

        public virtual AutoFacBootStrapper RepositorySetup()
        {
            // technical indicators.
            #region SMA
            _builder.RegisterType<AvSMADefaultRepository>()
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.SMA, AvIntervalEnum.Daily))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.SMA, AvIntervalEnum.OneMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.SMA, AvIntervalEnum.FiveMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.SMA, AvIntervalEnum.FifteenMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.SMA, AvIntervalEnum.ThirtyMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.SMA, AvIntervalEnum.SixtyMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.SMA, AvIntervalEnum.Default))
                    .WithParameter(
                        new ResolvedParameter(
                            (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                            (pi, ctx) => MongoDbCommon.DatabaseName))
                    .WithParameter(
                        new ResolvedParameter(
                            (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                            (pi, ctx) => MongoDbCommon.CollectionSMAName))
                    .InstancePerLifetimeScope();

            _builder.RegisterType<AvSMAWeeklyRepository>()
                        .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.SMA, AvIntervalEnum.Weekly))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                                (pi, ctx) => MongoDbCommon.DatabaseName))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                                (pi, ctx) => MongoDbCommon.CollectionSMAName))
                        .InstancePerLifetimeScope();

            _builder.RegisterType<AvSMAMonthlyRepository>()
                        .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.SMA, AvIntervalEnum.Monthly))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                                (pi, ctx) => MongoDbCommon.DatabaseName))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                                (pi, ctx) => MongoDbCommon.CollectionSMAName))
                        .InstancePerLifetimeScope();
            #endregion SMA
            #region EMA
            _builder.RegisterType<AvEMADefaultRepository>()
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.EMA, AvIntervalEnum.Daily))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.EMA, AvIntervalEnum.OneMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.EMA, AvIntervalEnum.FiveMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.EMA, AvIntervalEnum.FifteenMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.EMA, AvIntervalEnum.ThirtyMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.EMA, AvIntervalEnum.SixtyMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.EMA, AvIntervalEnum.Default))
                    .WithParameter(
                        new ResolvedParameter(
                            (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                            (pi, ctx) => MongoDbCommon.DatabaseName))
                    .WithParameter(
                        new ResolvedParameter(
                            (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                            (pi, ctx) => MongoDbCommon.CollectionEMAName))
                    .InstancePerLifetimeScope();

            _builder.RegisterType<AvEMAWeeklyRepository>()
                        .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.EMA, AvIntervalEnum.Weekly))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                                (pi, ctx) => MongoDbCommon.DatabaseName))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                                (pi, ctx) => MongoDbCommon.CollectionEMAName))
                        .InstancePerLifetimeScope();

            _builder.RegisterType<AvEMAMonthlyRepository>()
                        .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.EMA, AvIntervalEnum.Monthly))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                                (pi, ctx) => MongoDbCommon.DatabaseName))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                                (pi, ctx) => MongoDbCommon.CollectionEMAName))
                        .InstancePerLifetimeScope();
            #endregion EMA
            #region WMA
            _builder.RegisterType<AvWMADefaultRepository>()
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.WMA, AvIntervalEnum.Daily))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.WMA, AvIntervalEnum.OneMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.WMA, AvIntervalEnum.FiveMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.WMA, AvIntervalEnum.FifteenMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.WMA, AvIntervalEnum.ThirtyMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.WMA, AvIntervalEnum.SixtyMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.WMA, AvIntervalEnum.Default))
                    .WithParameter(
                        new ResolvedParameter(
                            (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                            (pi, ctx) => MongoDbCommon.DatabaseName))
                    .WithParameter(
                        new ResolvedParameter(
                            (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                            (pi, ctx) => MongoDbCommon.CollectionWMAName))
                    .InstancePerLifetimeScope();

            _builder.RegisterType<AvWMAWeeklyRepository>()
                        .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.WMA, AvIntervalEnum.Weekly))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                                (pi, ctx) => MongoDbCommon.DatabaseName))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                                (pi, ctx) => MongoDbCommon.CollectionWMAName))
                        .InstancePerLifetimeScope();

            _builder.RegisterType<AvWMAMonthlyRepository>()
                        .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.WMA, AvIntervalEnum.Monthly))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                                (pi, ctx) => MongoDbCommon.DatabaseName))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                                (pi, ctx) => MongoDbCommon.CollectionWMAName))
                        .InstancePerLifetimeScope();
            #endregion WMA
            #region DEMA
            _builder.RegisterType<AvDEMADefaultRepository>()
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.DEMA, AvIntervalEnum.Daily))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.DEMA, AvIntervalEnum.OneMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.DEMA, AvIntervalEnum.FiveMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.DEMA, AvIntervalEnum.FifteenMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.DEMA, AvIntervalEnum.ThirtyMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.DEMA, AvIntervalEnum.SixtyMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.DEMA, AvIntervalEnum.Default))
                    .WithParameter(
                        new ResolvedParameter(
                            (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                            (pi, ctx) => MongoDbCommon.DatabaseName))
                    .WithParameter(
                        new ResolvedParameter(
                            (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                            (pi, ctx) => MongoDbCommon.CollectionDEMAName))
                    .InstancePerLifetimeScope();

            _builder.RegisterType<AvDEMAWeeklyRepository>()
                        .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.DEMA, AvIntervalEnum.Weekly))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                                (pi, ctx) => MongoDbCommon.DatabaseName))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                                (pi, ctx) => MongoDbCommon.CollectionDEMAName))
                        .InstancePerLifetimeScope();

            _builder.RegisterType<AvDEMAMonthlyRepository>()
                        .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.DEMA, AvIntervalEnum.Monthly))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                                (pi, ctx) => MongoDbCommon.DatabaseName))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                                (pi, ctx) => MongoDbCommon.CollectionDEMAName))
                        .InstancePerLifetimeScope();
            #endregion DEMA
            #region TEMA
            _builder.RegisterType<AvTEMADefaultRepository>()
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.TEMA, AvIntervalEnum.Daily))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.TEMA, AvIntervalEnum.OneMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.TEMA, AvIntervalEnum.FiveMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.TEMA, AvIntervalEnum.FifteenMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.TEMA, AvIntervalEnum.ThirtyMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.TEMA, AvIntervalEnum.SixtyMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.TEMA, AvIntervalEnum.Default))
                    .WithParameter(
                        new ResolvedParameter(
                            (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                            (pi, ctx) => MongoDbCommon.DatabaseName))
                    .WithParameter(
                        new ResolvedParameter(
                            (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                            (pi, ctx) => MongoDbCommon.CollectionTEMAName))
                    .InstancePerLifetimeScope();

            _builder.RegisterType<AvTEMAWeeklyRepository>()
                        .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.TEMA, AvIntervalEnum.Weekly))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                                (pi, ctx) => MongoDbCommon.DatabaseName))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                                (pi, ctx) => MongoDbCommon.CollectionTEMAName))
                        .InstancePerLifetimeScope();

            _builder.RegisterType<AvTEMAMonthlyRepository>()
                        .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.TEMA, AvIntervalEnum.Monthly))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                                (pi, ctx) => MongoDbCommon.DatabaseName))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                                (pi, ctx) => MongoDbCommon.CollectionTEMAName))
                        .InstancePerLifetimeScope();
            #endregion TEMA
            #region TRIMA
            _builder.RegisterType<AvTRIMADefaultRepository>()
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.TRIMA, AvIntervalEnum.Daily))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.TRIMA, AvIntervalEnum.OneMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.TRIMA, AvIntervalEnum.FiveMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.TRIMA, AvIntervalEnum.FifteenMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.TRIMA, AvIntervalEnum.ThirtyMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.TRIMA, AvIntervalEnum.SixtyMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.TRIMA, AvIntervalEnum.Default))
                    .WithParameter(
                        new ResolvedParameter(
                            (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                            (pi, ctx) => MongoDbCommon.DatabaseName))
                    .WithParameter(
                        new ResolvedParameter(
                            (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                            (pi, ctx) => MongoDbCommon.CollectionTRIMAName))
                    .InstancePerLifetimeScope();

            _builder.RegisterType<AvTRIMAWeeklyRepository>()
                        .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.TRIMA, AvIntervalEnum.Weekly))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                                (pi, ctx) => MongoDbCommon.DatabaseName))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                                (pi, ctx) => MongoDbCommon.CollectionTRIMAName))
                        .InstancePerLifetimeScope();

            _builder.RegisterType<AvTRIMAMonthlyRepository>()
                        .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.TRIMA, AvIntervalEnum.Monthly))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                                (pi, ctx) => MongoDbCommon.DatabaseName))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                                (pi, ctx) => MongoDbCommon.CollectionTRIMAName))
                        .InstancePerLifetimeScope();
            #endregion TRIMA
            #region KAMA
            _builder.RegisterType<AvKAMADefaultRepository>()
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.KAMA, AvIntervalEnum.Daily))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.KAMA, AvIntervalEnum.OneMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.KAMA, AvIntervalEnum.FiveMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.KAMA, AvIntervalEnum.FifteenMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.KAMA, AvIntervalEnum.ThirtyMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.KAMA, AvIntervalEnum.SixtyMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.KAMA, AvIntervalEnum.Default))
                    .WithParameter(
                        new ResolvedParameter(
                            (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                            (pi, ctx) => MongoDbCommon.DatabaseName))
                    .WithParameter(
                        new ResolvedParameter(
                            (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                            (pi, ctx) => MongoDbCommon.CollectionKAMAName))
                    .InstancePerLifetimeScope();

            _builder.RegisterType<AvKAMAWeeklyRepository>()
                        .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.KAMA, AvIntervalEnum.Weekly))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                                (pi, ctx) => MongoDbCommon.DatabaseName))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                                (pi, ctx) => MongoDbCommon.CollectionKAMAName))
                        .InstancePerLifetimeScope();

            _builder.RegisterType<AvKAMAMonthlyRepository>()
                        .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.KAMA, AvIntervalEnum.Monthly))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                                (pi, ctx) => MongoDbCommon.DatabaseName))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                                (pi, ctx) => MongoDbCommon.CollectionKAMAName))
                        .InstancePerLifetimeScope();
            #endregion KAMA
            #region MAMA
            _builder.RegisterType<AvMAMADefaultRepository>()
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.MAMA, AvIntervalEnum.Daily))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.MAMA, AvIntervalEnum.OneMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.MAMA, AvIntervalEnum.FiveMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.MAMA, AvIntervalEnum.FifteenMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.MAMA, AvIntervalEnum.ThirtyMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.MAMA, AvIntervalEnum.SixtyMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.MAMA, AvIntervalEnum.Default))
                    .WithParameter(
                        new ResolvedParameter(
                            (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                            (pi, ctx) => MongoDbCommon.DatabaseName))
                    .WithParameter(
                        new ResolvedParameter(
                            (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                            (pi, ctx) => MongoDbCommon.CollectionMAMAName))
                    .InstancePerLifetimeScope();

            _builder.RegisterType<AvMAMAWeeklyRepository>()
                        .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.MAMA, AvIntervalEnum.Weekly))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                                (pi, ctx) => MongoDbCommon.DatabaseName))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                                (pi, ctx) => MongoDbCommon.CollectionMAMAName))
                        .InstancePerLifetimeScope();

            _builder.RegisterType<AvMAMAMonthlyRepository>()
                        .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.MAMA, AvIntervalEnum.Monthly))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                                (pi, ctx) => MongoDbCommon.DatabaseName))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                                (pi, ctx) => MongoDbCommon.CollectionMAMAName))
                        .InstancePerLifetimeScope();
            #endregion MAMA
            #region VWAP
            _builder.RegisterType<AvVWAPDefaultRepository>()
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.VWAP, AvIntervalEnum.Daily))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.VWAP, AvIntervalEnum.OneMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.VWAP, AvIntervalEnum.FiveMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.VWAP, AvIntervalEnum.FifteenMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.VWAP, AvIntervalEnum.ThirtyMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.VWAP, AvIntervalEnum.SixtyMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.VWAP, AvIntervalEnum.Default))
                    .WithParameter(
                        new ResolvedParameter(
                            (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                            (pi, ctx) => MongoDbCommon.DatabaseName))
                    .WithParameter(
                        new ResolvedParameter(
                            (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                            (pi, ctx) => MongoDbCommon.CollectionVWAPName))
                    .InstancePerLifetimeScope();

            _builder.RegisterType<AvVWAPWeeklyRepository>()
                        .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.VWAP, AvIntervalEnum.Weekly))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                                (pi, ctx) => MongoDbCommon.DatabaseName))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                                (pi, ctx) => MongoDbCommon.CollectionVWAPName))
                        .InstancePerLifetimeScope();

            _builder.RegisterType<AvVWAPMonthlyRepository>()
                        .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.VWAP, AvIntervalEnum.Monthly))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                                (pi, ctx) => MongoDbCommon.DatabaseName))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                                (pi, ctx) => MongoDbCommon.CollectionVWAPName))
                        .InstancePerLifetimeScope();
            #endregion VWAP
            #region T3
            _builder.RegisterType<AvT3DefaultRepository>()
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.T3, AvIntervalEnum.Daily))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.T3, AvIntervalEnum.OneMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.T3, AvIntervalEnum.FiveMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.T3, AvIntervalEnum.FifteenMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.T3, AvIntervalEnum.ThirtyMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.T3, AvIntervalEnum.SixtyMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.T3, AvIntervalEnum.Default))
                    .WithParameter(
                        new ResolvedParameter(
                            (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                            (pi, ctx) => MongoDbCommon.DatabaseName))
                    .WithParameter(
                        new ResolvedParameter(
                            (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                            (pi, ctx) => MongoDbCommon.CollectionT3Name))
                    .InstancePerLifetimeScope();

            _builder.RegisterType<AvT3WeeklyRepository>()
                        .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.T3, AvIntervalEnum.Weekly))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                                (pi, ctx) => MongoDbCommon.DatabaseName))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                                (pi, ctx) => MongoDbCommon.CollectionT3Name))
                        .InstancePerLifetimeScope();

            _builder.RegisterType<AvT3MonthlyRepository>()
                        .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.T3, AvIntervalEnum.Monthly))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                                (pi, ctx) => MongoDbCommon.DatabaseName))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                                (pi, ctx) => MongoDbCommon.CollectionT3Name))
                        .InstancePerLifetimeScope();
            #endregion T3
            #region MACD
            _builder.RegisterType<AvMACDDefaultRepository>()
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.MACD, AvIntervalEnum.Daily))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.MACD, AvIntervalEnum.OneMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.MACD, AvIntervalEnum.FiveMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.MACD, AvIntervalEnum.FifteenMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.MACD, AvIntervalEnum.ThirtyMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.MACD, AvIntervalEnum.SixtyMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.MACD, AvIntervalEnum.Default))
                    .WithParameter(
                        new ResolvedParameter(
                            (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                            (pi, ctx) => MongoDbCommon.DatabaseName))
                    .WithParameter(
                        new ResolvedParameter(
                            (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                            (pi, ctx) => MongoDbCommon.CollectionMACDName))
                    .InstancePerLifetimeScope();

            _builder.RegisterType<AvMACDWeeklyRepository>()
                        .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.MACD, AvIntervalEnum.Weekly))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                                (pi, ctx) => MongoDbCommon.DatabaseName))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                                (pi, ctx) => MongoDbCommon.CollectionMACDName))
                        .InstancePerLifetimeScope();

            _builder.RegisterType<AvMACDMonthlyRepository>()
                        .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.MACD, AvIntervalEnum.Monthly))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                                (pi, ctx) => MongoDbCommon.DatabaseName))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                                (pi, ctx) => MongoDbCommon.CollectionMACDName))
                        .InstancePerLifetimeScope();
            #endregion MACD
            #region MACDEXT
            _builder.RegisterType<AvMACDEXTDefaultRepository>()
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.MACDEXT, AvIntervalEnum.Daily))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.MACDEXT, AvIntervalEnum.OneMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.MACDEXT, AvIntervalEnum.FiveMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.MACDEXT, AvIntervalEnum.FifteenMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.MACDEXT, AvIntervalEnum.ThirtyMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.MACDEXT, AvIntervalEnum.SixtyMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.MACDEXT, AvIntervalEnum.Default))
                    .WithParameter(
                        new ResolvedParameter(
                            (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                            (pi, ctx) => MongoDbCommon.DatabaseName))
                    .WithParameter(
                        new ResolvedParameter(
                            (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                            (pi, ctx) => MongoDbCommon.CollectionMACDEXTName))
                    .InstancePerLifetimeScope();

            _builder.RegisterType<AvMACDEXTWeeklyRepository>()
                        .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.MACDEXT, AvIntervalEnum.Weekly))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                                (pi, ctx) => MongoDbCommon.DatabaseName))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                                (pi, ctx) => MongoDbCommon.CollectionMACDEXTName))
                        .InstancePerLifetimeScope();

            _builder.RegisterType<AvMACDEXTMonthlyRepository>()
                        .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.MACDEXT, AvIntervalEnum.Monthly))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                                (pi, ctx) => MongoDbCommon.DatabaseName))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                                (pi, ctx) => MongoDbCommon.CollectionMACDEXTName))
                        .InstancePerLifetimeScope();
            #endregion MACDEXT
            #region STOCH
            _builder.RegisterType<AvSTOCHDefaultRepository>()
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.STOCH, AvIntervalEnum.Daily))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.STOCH, AvIntervalEnum.OneMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.STOCH, AvIntervalEnum.FiveMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.STOCH, AvIntervalEnum.FifteenMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.STOCH, AvIntervalEnum.ThirtyMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.STOCH, AvIntervalEnum.SixtyMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.STOCH, AvIntervalEnum.Default))
                    .WithParameter(
                        new ResolvedParameter(
                            (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                            (pi, ctx) => MongoDbCommon.DatabaseName))
                    .WithParameter(
                        new ResolvedParameter(
                            (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                            (pi, ctx) => MongoDbCommon.CollectionSTOCHName))
                    .InstancePerLifetimeScope();

            _builder.RegisterType<AvSTOCHWeeklyRepository>()
                        .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.STOCH, AvIntervalEnum.Weekly))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                                (pi, ctx) => MongoDbCommon.DatabaseName))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                                (pi, ctx) => MongoDbCommon.CollectionSTOCHName))
                        .InstancePerLifetimeScope();

            _builder.RegisterType<AvSTOCHMonthlyRepository>()
                        .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.STOCH, AvIntervalEnum.Monthly))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                                (pi, ctx) => MongoDbCommon.DatabaseName))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                                (pi, ctx) => MongoDbCommon.CollectionSTOCHName))
                        .InstancePerLifetimeScope();
            #endregion STOCH
            #region STOCHF
            _builder.RegisterType<AvSTOCHFDefaultRepository>()
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.STOCHF, AvIntervalEnum.Daily))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.STOCHF, AvIntervalEnum.OneMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.STOCHF, AvIntervalEnum.FiveMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.STOCHF, AvIntervalEnum.FifteenMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.STOCHF, AvIntervalEnum.ThirtyMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.STOCHF, AvIntervalEnum.SixtyMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.STOCHF, AvIntervalEnum.Default))
                    .WithParameter(
                        new ResolvedParameter(
                            (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                            (pi, ctx) => MongoDbCommon.DatabaseName))
                    .WithParameter(
                        new ResolvedParameter(
                            (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                            (pi, ctx) => MongoDbCommon.CollectionSTOCHFName))
                    .InstancePerLifetimeScope();

            _builder.RegisterType<AvSTOCHFWeeklyRepository>()
                        .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.STOCHF, AvIntervalEnum.Weekly))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                                (pi, ctx) => MongoDbCommon.DatabaseName))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                                (pi, ctx) => MongoDbCommon.CollectionSTOCHFName))
                        .InstancePerLifetimeScope();

            _builder.RegisterType<AvSTOCHFMonthlyRepository>()
                        .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.STOCHF, AvIntervalEnum.Monthly))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                                (pi, ctx) => MongoDbCommon.DatabaseName))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                                (pi, ctx) => MongoDbCommon.CollectionSTOCHFName))
                        .InstancePerLifetimeScope();
            #endregion STOCHF
            #region RSI
            _builder.RegisterType<AvRSIDefaultRepository>()
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.RSI, AvIntervalEnum.Daily))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.RSI, AvIntervalEnum.OneMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.RSI, AvIntervalEnum.FiveMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.RSI, AvIntervalEnum.FifteenMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.RSI, AvIntervalEnum.ThirtyMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.RSI, AvIntervalEnum.SixtyMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.RSI, AvIntervalEnum.Default))
                    .WithParameter(
                        new ResolvedParameter(
                            (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                            (pi, ctx) => MongoDbCommon.DatabaseName))
                    .WithParameter(
                        new ResolvedParameter(
                            (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                            (pi, ctx) => MongoDbCommon.CollectionRSIName))
                    .InstancePerLifetimeScope();

            _builder.RegisterType<AvRSIWeeklyRepository>()
                        .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.RSI, AvIntervalEnum.Weekly))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                                (pi, ctx) => MongoDbCommon.DatabaseName))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                                (pi, ctx) => MongoDbCommon.CollectionRSIName))
                        .InstancePerLifetimeScope();

            _builder.RegisterType<AvRSIMonthlyRepository>()
                        .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.RSI, AvIntervalEnum.Monthly))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                                (pi, ctx) => MongoDbCommon.DatabaseName))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                                (pi, ctx) => MongoDbCommon.CollectionRSIName))
                        .InstancePerLifetimeScope();
            #endregion RSI
            #region STOCHRSI
            _builder.RegisterType<AvSTOCHRSIDefaultRepository>()
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.STOCHRSI, AvIntervalEnum.Daily))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.STOCHRSI, AvIntervalEnum.OneMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.STOCHRSI, AvIntervalEnum.FiveMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.STOCHRSI, AvIntervalEnum.FifteenMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.STOCHRSI, AvIntervalEnum.ThirtyMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.STOCHRSI, AvIntervalEnum.SixtyMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.STOCHRSI, AvIntervalEnum.Default))
                    .WithParameter(
                        new ResolvedParameter(
                            (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                            (pi, ctx) => MongoDbCommon.DatabaseName))
                    .WithParameter(
                        new ResolvedParameter(
                            (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                            (pi, ctx) => MongoDbCommon.CollectionSTOCHRSIName))
                    .InstancePerLifetimeScope();

            _builder.RegisterType<AvSTOCHRSIWeeklyRepository>()
                        .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.STOCHRSI, AvIntervalEnum.Weekly))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                                (pi, ctx) => MongoDbCommon.DatabaseName))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                                (pi, ctx) => MongoDbCommon.CollectionSTOCHRSIName))
                        .InstancePerLifetimeScope();

            _builder.RegisterType<AvSTOCHRSIMonthlyRepository>()
                        .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.STOCHRSI, AvIntervalEnum.Monthly))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                                (pi, ctx) => MongoDbCommon.DatabaseName))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                                (pi, ctx) => MongoDbCommon.CollectionSTOCHRSIName))
                        .InstancePerLifetimeScope();
            #endregion STOCHRSI
            #region WILLR
            _builder.RegisterType<AvWILLRDefaultRepository>()
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.WILLR, AvIntervalEnum.Daily))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.WILLR, AvIntervalEnum.OneMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.WILLR, AvIntervalEnum.FiveMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.WILLR, AvIntervalEnum.FifteenMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.WILLR, AvIntervalEnum.ThirtyMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.WILLR, AvIntervalEnum.SixtyMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.WILLR, AvIntervalEnum.Default))
                    .WithParameter(
                        new ResolvedParameter(
                            (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                            (pi, ctx) => MongoDbCommon.DatabaseName))
                    .WithParameter(
                        new ResolvedParameter(
                            (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                            (pi, ctx) => MongoDbCommon.CollectionWILLRName))
                    .InstancePerLifetimeScope();

            _builder.RegisterType<AvWILLRWeeklyRepository>()
                        .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.WILLR, AvIntervalEnum.Weekly))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                                (pi, ctx) => MongoDbCommon.DatabaseName))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                                (pi, ctx) => MongoDbCommon.CollectionWILLRName))
                        .InstancePerLifetimeScope();

            _builder.RegisterType<AvWILLRMonthlyRepository>()
                        .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.WILLR, AvIntervalEnum.Monthly))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                                (pi, ctx) => MongoDbCommon.DatabaseName))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                                (pi, ctx) => MongoDbCommon.CollectionWILLRName))
                        .InstancePerLifetimeScope();
            #endregion WILLR
            #region ADX
            _builder.RegisterType<AvADXDefaultRepository>()
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.ADX, AvIntervalEnum.Daily))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.ADX, AvIntervalEnum.OneMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.ADX, AvIntervalEnum.FiveMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.ADX, AvIntervalEnum.FifteenMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.ADX, AvIntervalEnum.ThirtyMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.ADX, AvIntervalEnum.SixtyMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.ADX, AvIntervalEnum.Default))
                    .WithParameter(
                        new ResolvedParameter(
                            (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                            (pi, ctx) => MongoDbCommon.DatabaseName))
                    .WithParameter(
                        new ResolvedParameter(
                            (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                            (pi, ctx) => MongoDbCommon.CollectionADXName))
                    .InstancePerLifetimeScope();

            _builder.RegisterType<AvADXWeeklyRepository>()
                        .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.ADX, AvIntervalEnum.Weekly))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                                (pi, ctx) => MongoDbCommon.DatabaseName))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                                (pi, ctx) => MongoDbCommon.CollectionADXName))
                        .InstancePerLifetimeScope();

            _builder.RegisterType<AvADXMonthlyRepository>()
                        .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.ADX, AvIntervalEnum.Monthly))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                                (pi, ctx) => MongoDbCommon.DatabaseName))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                                (pi, ctx) => MongoDbCommon.CollectionADXName))
                        .InstancePerLifetimeScope();
            #endregion ADX
            #region ADXR
            _builder.RegisterType<AvADXRDefaultRepository>()
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.ADXR, AvIntervalEnum.Daily))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.ADXR, AvIntervalEnum.OneMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.ADXR, AvIntervalEnum.FiveMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.ADXR, AvIntervalEnum.FifteenMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.ADXR, AvIntervalEnum.ThirtyMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.ADXR, AvIntervalEnum.SixtyMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.ADXR, AvIntervalEnum.Default))
                    .WithParameter(
                        new ResolvedParameter(
                            (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                            (pi, ctx) => MongoDbCommon.DatabaseName))
                    .WithParameter(
                        new ResolvedParameter(
                            (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                            (pi, ctx) => MongoDbCommon.CollectionADXRName))
                    .InstancePerLifetimeScope();

            _builder.RegisterType<AvADXRWeeklyRepository>()
                        .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.ADXR, AvIntervalEnum.Weekly))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                                (pi, ctx) => MongoDbCommon.DatabaseName))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                                (pi, ctx) => MongoDbCommon.CollectionADXRName))
                        .InstancePerLifetimeScope();

            _builder.RegisterType<AvADXRMonthlyRepository>()
                        .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.ADXR, AvIntervalEnum.Monthly))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                                (pi, ctx) => MongoDbCommon.DatabaseName))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                                (pi, ctx) => MongoDbCommon.CollectionADXRName))
                        .InstancePerLifetimeScope();
            #endregion ADXR
            #region APO
            _builder.RegisterType<AvAPODefaultRepository>()
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.APO, AvIntervalEnum.Daily))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.APO, AvIntervalEnum.OneMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.APO, AvIntervalEnum.FiveMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.APO, AvIntervalEnum.FifteenMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.APO, AvIntervalEnum.ThirtyMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.APO, AvIntervalEnum.SixtyMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.APO, AvIntervalEnum.Default))
                    .WithParameter(
                        new ResolvedParameter(
                            (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                            (pi, ctx) => MongoDbCommon.DatabaseName))
                    .WithParameter(
                        new ResolvedParameter(
                            (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                            (pi, ctx) => MongoDbCommon.CollectionAPOName))
                    .InstancePerLifetimeScope();

            _builder.RegisterType<AvAPOWeeklyRepository>()
                        .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.APO, AvIntervalEnum.Weekly))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                                (pi, ctx) => MongoDbCommon.DatabaseName))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                                (pi, ctx) => MongoDbCommon.CollectionAPOName))
                        .InstancePerLifetimeScope();

            _builder.RegisterType<AvAPOMonthlyRepository>()
                        .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.APO, AvIntervalEnum.Monthly))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                                (pi, ctx) => MongoDbCommon.DatabaseName))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                                (pi, ctx) => MongoDbCommon.CollectionAPOName))
                        .InstancePerLifetimeScope();
            #endregion APO
            #region PPO
            _builder.RegisterType<AvPPODefaultRepository>()
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.PPO, AvIntervalEnum.Daily))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.PPO, AvIntervalEnum.OneMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.PPO, AvIntervalEnum.FiveMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.PPO, AvIntervalEnum.FifteenMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.PPO, AvIntervalEnum.ThirtyMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.PPO, AvIntervalEnum.SixtyMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.PPO, AvIntervalEnum.Default))
                    .WithParameter(
                        new ResolvedParameter(
                            (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                            (pi, ctx) => MongoDbCommon.DatabaseName))
                    .WithParameter(
                        new ResolvedParameter(
                            (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                            (pi, ctx) => MongoDbCommon.CollectionPPOName))
                    .InstancePerLifetimeScope();

            _builder.RegisterType<AvPPOWeeklyRepository>()
                        .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.PPO, AvIntervalEnum.Weekly))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                                (pi, ctx) => MongoDbCommon.DatabaseName))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                                (pi, ctx) => MongoDbCommon.CollectionPPOName))
                        .InstancePerLifetimeScope();

            _builder.RegisterType<AvPPOMonthlyRepository>()
                        .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.PPO, AvIntervalEnum.Monthly))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                                (pi, ctx) => MongoDbCommon.DatabaseName))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                                (pi, ctx) => MongoDbCommon.CollectionPPOName))
                        .InstancePerLifetimeScope();
            #endregion PPO
            #region MOM
            _builder.RegisterType<AvMOMDefaultRepository>()
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.MOM, AvIntervalEnum.Daily))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.MOM, AvIntervalEnum.OneMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.MOM, AvIntervalEnum.FiveMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.MOM, AvIntervalEnum.FifteenMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.MOM, AvIntervalEnum.ThirtyMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.MOM, AvIntervalEnum.SixtyMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.MOM, AvIntervalEnum.Default))
                    .WithParameter(
                        new ResolvedParameter(
                            (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                            (pi, ctx) => MongoDbCommon.DatabaseName))
                    .WithParameter(
                        new ResolvedParameter(
                            (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                            (pi, ctx) => MongoDbCommon.CollectionMOMName))
                    .InstancePerLifetimeScope();

            _builder.RegisterType<AvMOMWeeklyRepository>()
                        .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.MOM, AvIntervalEnum.Weekly))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                                (pi, ctx) => MongoDbCommon.DatabaseName))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                                (pi, ctx) => MongoDbCommon.CollectionMOMName))
                        .InstancePerLifetimeScope();

            _builder.RegisterType<AvMOMMonthlyRepository>()
                        .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.MOM, AvIntervalEnum.Monthly))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                                (pi, ctx) => MongoDbCommon.DatabaseName))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                                (pi, ctx) => MongoDbCommon.CollectionMOMName))
                        .InstancePerLifetimeScope();
            #endregion MOM
            #region BOP
            _builder.RegisterType<AvBOPDefaultRepository>()
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.BOP, AvIntervalEnum.Daily))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.BOP, AvIntervalEnum.OneMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.BOP, AvIntervalEnum.FiveMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.BOP, AvIntervalEnum.FifteenMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.BOP, AvIntervalEnum.ThirtyMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.BOP, AvIntervalEnum.SixtyMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.BOP, AvIntervalEnum.Default))
                    .WithParameter(
                        new ResolvedParameter(
                            (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                            (pi, ctx) => MongoDbCommon.DatabaseName))
                    .WithParameter(
                        new ResolvedParameter(
                            (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                            (pi, ctx) => MongoDbCommon.CollectionBOPName))
                    .InstancePerLifetimeScope();

            _builder.RegisterType<AvBOPWeeklyRepository>()
                        .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.BOP, AvIntervalEnum.Weekly))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                                (pi, ctx) => MongoDbCommon.DatabaseName))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                                (pi, ctx) => MongoDbCommon.CollectionBOPName))
                        .InstancePerLifetimeScope();

            _builder.RegisterType<AvBOPMonthlyRepository>()
                        .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.BOP, AvIntervalEnum.Monthly))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                                (pi, ctx) => MongoDbCommon.DatabaseName))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                                (pi, ctx) => MongoDbCommon.CollectionBOPName))
                        .InstancePerLifetimeScope();
            #endregion BOP
            #region CCI
            _builder.RegisterType<AvCCIDefaultRepository>()
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.CCI, AvIntervalEnum.Daily))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.CCI, AvIntervalEnum.OneMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.CCI, AvIntervalEnum.FiveMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.CCI, AvIntervalEnum.FifteenMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.CCI, AvIntervalEnum.ThirtyMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.CCI, AvIntervalEnum.SixtyMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.CCI, AvIntervalEnum.Default))
                    .WithParameter(
                        new ResolvedParameter(
                            (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                            (pi, ctx) => MongoDbCommon.DatabaseName))
                    .WithParameter(
                        new ResolvedParameter(
                            (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                            (pi, ctx) => MongoDbCommon.CollectionCCIName))
                    .InstancePerLifetimeScope();

            _builder.RegisterType<AvCCIWeeklyRepository>()
                        .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.CCI, AvIntervalEnum.Weekly))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                                (pi, ctx) => MongoDbCommon.DatabaseName))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                                (pi, ctx) => MongoDbCommon.CollectionCCIName))
                        .InstancePerLifetimeScope();

            _builder.RegisterType<AvCCIMonthlyRepository>()
                        .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.CCI, AvIntervalEnum.Monthly))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                                (pi, ctx) => MongoDbCommon.DatabaseName))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                                (pi, ctx) => MongoDbCommon.CollectionCCIName))
                        .InstancePerLifetimeScope();
            #endregion CCI
            #region CMO
            _builder.RegisterType<AvCMODefaultRepository>()
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.CMO, AvIntervalEnum.Daily))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.CMO, AvIntervalEnum.OneMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.CMO, AvIntervalEnum.FiveMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.CMO, AvIntervalEnum.FifteenMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.CMO, AvIntervalEnum.ThirtyMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.CMO, AvIntervalEnum.SixtyMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.CMO, AvIntervalEnum.Default))
                    .WithParameter(
                        new ResolvedParameter(
                            (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                            (pi, ctx) => MongoDbCommon.DatabaseName))
                    .WithParameter(
                        new ResolvedParameter(
                            (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                            (pi, ctx) => MongoDbCommon.CollectionCMOName))
                    .InstancePerLifetimeScope();

            _builder.RegisterType<AvCMOWeeklyRepository>()
                        .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.CMO, AvIntervalEnum.Weekly))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                                (pi, ctx) => MongoDbCommon.DatabaseName))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                                (pi, ctx) => MongoDbCommon.CollectionCMOName))
                        .InstancePerLifetimeScope();

            _builder.RegisterType<AvCMOMonthlyRepository>()
                        .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.CMO, AvIntervalEnum.Monthly))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                                (pi, ctx) => MongoDbCommon.DatabaseName))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                                (pi, ctx) => MongoDbCommon.CollectionCMOName))
                        .InstancePerLifetimeScope();
            #endregion CMO
            #region ROC
            _builder.RegisterType<AvROCDefaultRepository>()
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.ROC, AvIntervalEnum.Daily))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.ROC, AvIntervalEnum.OneMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.ROC, AvIntervalEnum.FiveMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.ROC, AvIntervalEnum.FifteenMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.ROC, AvIntervalEnum.ThirtyMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.ROC, AvIntervalEnum.SixtyMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.ROC, AvIntervalEnum.Default))
                    .WithParameter(
                        new ResolvedParameter(
                            (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                            (pi, ctx) => MongoDbCommon.DatabaseName))
                    .WithParameter(
                        new ResolvedParameter(
                            (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                            (pi, ctx) => MongoDbCommon.CollectionROCName))
                    .InstancePerLifetimeScope();

            _builder.RegisterType<AvROCWeeklyRepository>()
                        .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.ROC, AvIntervalEnum.Weekly))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                                (pi, ctx) => MongoDbCommon.DatabaseName))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                                (pi, ctx) => MongoDbCommon.CollectionROCName))
                        .InstancePerLifetimeScope();

            _builder.RegisterType<AvROCMonthlyRepository>()
                        .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.ROC, AvIntervalEnum.Monthly))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                                (pi, ctx) => MongoDbCommon.DatabaseName))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                                (pi, ctx) => MongoDbCommon.CollectionROCName))
                        .InstancePerLifetimeScope();
            #endregion ROC
            #region ROCR
            _builder.RegisterType<AvROCRDefaultRepository>()
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.ROCR, AvIntervalEnum.Daily))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.ROCR, AvIntervalEnum.OneMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.ROCR, AvIntervalEnum.FiveMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.ROCR, AvIntervalEnum.FifteenMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.ROCR, AvIntervalEnum.ThirtyMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.ROCR, AvIntervalEnum.SixtyMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.ROCR, AvIntervalEnum.Default))
                    .WithParameter(
                        new ResolvedParameter(
                            (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                            (pi, ctx) => MongoDbCommon.DatabaseName))
                    .WithParameter(
                        new ResolvedParameter(
                            (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                            (pi, ctx) => MongoDbCommon.CollectionROCRName))
                    .InstancePerLifetimeScope();

            _builder.RegisterType<AvROCRWeeklyRepository>()
                        .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.ROCR, AvIntervalEnum.Weekly))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                                (pi, ctx) => MongoDbCommon.DatabaseName))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                                (pi, ctx) => MongoDbCommon.CollectionROCRName))
                        .InstancePerLifetimeScope();

            _builder.RegisterType<AvROCRMonthlyRepository>()
                        .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.ROCR, AvIntervalEnum.Monthly))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                                (pi, ctx) => MongoDbCommon.DatabaseName))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                                (pi, ctx) => MongoDbCommon.CollectionROCRName))
                        .InstancePerLifetimeScope();
            #endregion ROCR
            #region AROON
            _builder.RegisterType<AvAROONDefaultRepository>()
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.AROON, AvIntervalEnum.Daily))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.AROON, AvIntervalEnum.OneMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.AROON, AvIntervalEnum.FiveMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.AROON, AvIntervalEnum.FifteenMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.AROON, AvIntervalEnum.ThirtyMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.AROON, AvIntervalEnum.SixtyMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.AROON, AvIntervalEnum.Default))
                    .WithParameter(
                        new ResolvedParameter(
                            (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                            (pi, ctx) => MongoDbCommon.DatabaseName))
                    .WithParameter(
                        new ResolvedParameter(
                            (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                            (pi, ctx) => MongoDbCommon.CollectionAROONName))
                    .InstancePerLifetimeScope();

            _builder.RegisterType<AvAROONWeeklyRepository>()
                        .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.AROON, AvIntervalEnum.Weekly))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                                (pi, ctx) => MongoDbCommon.DatabaseName))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                                (pi, ctx) => MongoDbCommon.CollectionAROONName))
                        .InstancePerLifetimeScope();

            _builder.RegisterType<AvAROONMonthlyRepository>()
                        .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.AROON, AvIntervalEnum.Monthly))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                                (pi, ctx) => MongoDbCommon.DatabaseName))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                                (pi, ctx) => MongoDbCommon.CollectionAROONName))
                        .InstancePerLifetimeScope();
            #endregion AROON
            #region AROONOSC
            _builder.RegisterType<AvAROONOSCDefaultRepository>()
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.AROONOSC, AvIntervalEnum.Daily))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.AROONOSC, AvIntervalEnum.OneMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.AROONOSC, AvIntervalEnum.FiveMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.AROONOSC, AvIntervalEnum.FifteenMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.AROONOSC, AvIntervalEnum.ThirtyMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.AROONOSC, AvIntervalEnum.SixtyMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.AROONOSC, AvIntervalEnum.Default))
                    .WithParameter(
                        new ResolvedParameter(
                            (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                            (pi, ctx) => MongoDbCommon.DatabaseName))
                    .WithParameter(
                        new ResolvedParameter(
                            (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                            (pi, ctx) => MongoDbCommon.CollectionAROONOSCName))
                    .InstancePerLifetimeScope();

            _builder.RegisterType<AvAROONOSCWeeklyRepository>()
                        .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.AROONOSC, AvIntervalEnum.Weekly))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                                (pi, ctx) => MongoDbCommon.DatabaseName))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                                (pi, ctx) => MongoDbCommon.CollectionAROONOSCName))
                        .InstancePerLifetimeScope();

            _builder.RegisterType<AvAROONOSCMonthlyRepository>()
                        .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.AROONOSC, AvIntervalEnum.Monthly))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                                (pi, ctx) => MongoDbCommon.DatabaseName))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                                (pi, ctx) => MongoDbCommon.CollectionAROONOSCName))
                        .InstancePerLifetimeScope();
            #endregion AROONOSC
            #region MFI
            _builder.RegisterType<AvMFIDefaultRepository>()
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.MFI, AvIntervalEnum.Daily))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.MFI, AvIntervalEnum.OneMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.MFI, AvIntervalEnum.FiveMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.MFI, AvIntervalEnum.FifteenMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.MFI, AvIntervalEnum.ThirtyMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.MFI, AvIntervalEnum.SixtyMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.MFI, AvIntervalEnum.Default))
                    .WithParameter(
                        new ResolvedParameter(
                            (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                            (pi, ctx) => MongoDbCommon.DatabaseName))
                    .WithParameter(
                        new ResolvedParameter(
                            (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                            (pi, ctx) => MongoDbCommon.CollectionMFIName))
                    .InstancePerLifetimeScope();

            _builder.RegisterType<AvMFIWeeklyRepository>()
                        .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.MFI, AvIntervalEnum.Weekly))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                                (pi, ctx) => MongoDbCommon.DatabaseName))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                                (pi, ctx) => MongoDbCommon.CollectionMFIName))
                        .InstancePerLifetimeScope();

            _builder.RegisterType<AvMFIMonthlyRepository>()
                        .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.MFI, AvIntervalEnum.Monthly))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                                (pi, ctx) => MongoDbCommon.DatabaseName))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                                (pi, ctx) => MongoDbCommon.CollectionMFIName))
                        .InstancePerLifetimeScope();
            #endregion MFI
            #region TRIX
            _builder.RegisterType<AvTRIXDefaultRepository>()
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.TRIX, AvIntervalEnum.Daily))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.TRIX, AvIntervalEnum.OneMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.TRIX, AvIntervalEnum.FiveMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.TRIX, AvIntervalEnum.FifteenMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.TRIX, AvIntervalEnum.ThirtyMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.TRIX, AvIntervalEnum.SixtyMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.TRIX, AvIntervalEnum.Default))
                    .WithParameter(
                        new ResolvedParameter(
                            (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                            (pi, ctx) => MongoDbCommon.DatabaseName))
                    .WithParameter(
                        new ResolvedParameter(
                            (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                            (pi, ctx) => MongoDbCommon.CollectionTRIXName))
                    .InstancePerLifetimeScope();

            _builder.RegisterType<AvTRIXWeeklyRepository>()
                        .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.TRIX, AvIntervalEnum.Weekly))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                                (pi, ctx) => MongoDbCommon.DatabaseName))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                                (pi, ctx) => MongoDbCommon.CollectionTRIXName))
                        .InstancePerLifetimeScope();

            _builder.RegisterType<AvTRIXMonthlyRepository>()
                        .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.TRIX, AvIntervalEnum.Monthly))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                                (pi, ctx) => MongoDbCommon.DatabaseName))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                                (pi, ctx) => MongoDbCommon.CollectionTRIXName))
                        .InstancePerLifetimeScope();
            #endregion TRIX
            #region ULTOSC
            _builder.RegisterType<AvULTOSCDefaultRepository>()
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.ULTOSC, AvIntervalEnum.Daily))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.ULTOSC, AvIntervalEnum.OneMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.ULTOSC, AvIntervalEnum.FiveMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.ULTOSC, AvIntervalEnum.FifteenMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.ULTOSC, AvIntervalEnum.ThirtyMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.ULTOSC, AvIntervalEnum.SixtyMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.ULTOSC, AvIntervalEnum.Default))
                    .WithParameter(
                        new ResolvedParameter(
                            (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                            (pi, ctx) => MongoDbCommon.DatabaseName))
                    .WithParameter(
                        new ResolvedParameter(
                            (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                            (pi, ctx) => MongoDbCommon.CollectionULTOSCName))
                    .InstancePerLifetimeScope();

            _builder.RegisterType<AvULTOSCWeeklyRepository>()
                        .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.ULTOSC, AvIntervalEnum.Weekly))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                                (pi, ctx) => MongoDbCommon.DatabaseName))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                                (pi, ctx) => MongoDbCommon.CollectionULTOSCName))
                        .InstancePerLifetimeScope();

            _builder.RegisterType<AvULTOSCMonthlyRepository>()
                        .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.ULTOSC, AvIntervalEnum.Monthly))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                                (pi, ctx) => MongoDbCommon.DatabaseName))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                                (pi, ctx) => MongoDbCommon.CollectionULTOSCName))
                        .InstancePerLifetimeScope();
            #endregion ULTOSC
            #region DX
            _builder.RegisterType<AvDXDefaultRepository>()
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.DX, AvIntervalEnum.Daily))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.DX, AvIntervalEnum.OneMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.DX, AvIntervalEnum.FiveMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.DX, AvIntervalEnum.FifteenMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.DX, AvIntervalEnum.ThirtyMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.DX, AvIntervalEnum.SixtyMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.DX, AvIntervalEnum.Default))
                    .WithParameter(
                        new ResolvedParameter(
                            (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                            (pi, ctx) => MongoDbCommon.DatabaseName))
                    .WithParameter(
                        new ResolvedParameter(
                            (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                            (pi, ctx) => MongoDbCommon.CollectionDXName))
                    .InstancePerLifetimeScope();

            _builder.RegisterType<AvDXWeeklyRepository>()
                        .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.DX, AvIntervalEnum.Weekly))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                                (pi, ctx) => MongoDbCommon.DatabaseName))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                                (pi, ctx) => MongoDbCommon.CollectionDXName))
                        .InstancePerLifetimeScope();

            _builder.RegisterType<AvDXMonthlyRepository>()
                        .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.DX, AvIntervalEnum.Monthly))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                                (pi, ctx) => MongoDbCommon.DatabaseName))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                                (pi, ctx) => MongoDbCommon.CollectionDXName))
                        .InstancePerLifetimeScope();
            #endregion DX
            #region MINUS_DI
            _builder.RegisterType<AvMINUS_DIDefaultRepository>()
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.MINUS_DI, AvIntervalEnum.Daily))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.MINUS_DI, AvIntervalEnum.OneMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.MINUS_DI, AvIntervalEnum.FiveMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.MINUS_DI, AvIntervalEnum.FifteenMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.MINUS_DI, AvIntervalEnum.ThirtyMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.MINUS_DI, AvIntervalEnum.SixtyMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.MINUS_DI, AvIntervalEnum.Default))
                    .WithParameter(
                        new ResolvedParameter(
                            (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                            (pi, ctx) => MongoDbCommon.DatabaseName))
                    .WithParameter(
                        new ResolvedParameter(
                            (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                            (pi, ctx) => MongoDbCommon.CollectionMINUS_DIName))
                    .InstancePerLifetimeScope();

            _builder.RegisterType<AvMINUS_DIWeeklyRepository>()
                        .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.MINUS_DI, AvIntervalEnum.Weekly))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                                (pi, ctx) => MongoDbCommon.DatabaseName))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                                (pi, ctx) => MongoDbCommon.CollectionMINUS_DIName))
                        .InstancePerLifetimeScope();

            _builder.RegisterType<AvMINUS_DIMonthlyRepository>()
                        .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.MINUS_DI, AvIntervalEnum.Monthly))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                                (pi, ctx) => MongoDbCommon.DatabaseName))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                                (pi, ctx) => MongoDbCommon.CollectionMINUS_DIName))
                        .InstancePerLifetimeScope();
            #endregion MINUS_DI
            #region PLUS_DI
            _builder.RegisterType<AvPLUS_DIDefaultRepository>()
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.PLUS_DI, AvIntervalEnum.Daily))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.PLUS_DI, AvIntervalEnum.OneMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.PLUS_DI, AvIntervalEnum.FiveMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.PLUS_DI, AvIntervalEnum.FifteenMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.PLUS_DI, AvIntervalEnum.ThirtyMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.PLUS_DI, AvIntervalEnum.SixtyMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.PLUS_DI, AvIntervalEnum.Default))
                    .WithParameter(
                        new ResolvedParameter(
                            (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                            (pi, ctx) => MongoDbCommon.DatabaseName))
                    .WithParameter(
                        new ResolvedParameter(
                            (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                            (pi, ctx) => MongoDbCommon.CollectionPLUS_DIName))
                    .InstancePerLifetimeScope();

            _builder.RegisterType<AvPLUS_DIWeeklyRepository>()
                        .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.PLUS_DI, AvIntervalEnum.Weekly))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                                (pi, ctx) => MongoDbCommon.DatabaseName))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                                (pi, ctx) => MongoDbCommon.CollectionPLUS_DIName))
                        .InstancePerLifetimeScope();

            _builder.RegisterType<AvPLUS_DIMonthlyRepository>()
                        .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.PLUS_DI, AvIntervalEnum.Monthly))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                                (pi, ctx) => MongoDbCommon.DatabaseName))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                                (pi, ctx) => MongoDbCommon.CollectionPLUS_DIName))
                        .InstancePerLifetimeScope();
            #endregion PLUS_DI
            #region MINUS_DM
            _builder.RegisterType<AvMINUS_DMDefaultRepository>()
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.MINUS_DM, AvIntervalEnum.Daily))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.MINUS_DM, AvIntervalEnum.OneMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.MINUS_DM, AvIntervalEnum.FiveMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.MINUS_DM, AvIntervalEnum.FifteenMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.MINUS_DM, AvIntervalEnum.ThirtyMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.MINUS_DM, AvIntervalEnum.SixtyMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.MINUS_DM, AvIntervalEnum.Default))
                    .WithParameter(
                        new ResolvedParameter(
                            (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                            (pi, ctx) => MongoDbCommon.DatabaseName))
                    .WithParameter(
                        new ResolvedParameter(
                            (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                            (pi, ctx) => MongoDbCommon.CollectionMINUS_DMName))
                    .InstancePerLifetimeScope();

            _builder.RegisterType<AvMINUS_DMWeeklyRepository>()
                        .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.MINUS_DM, AvIntervalEnum.Weekly))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                                (pi, ctx) => MongoDbCommon.DatabaseName))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                                (pi, ctx) => MongoDbCommon.CollectionMINUS_DMName))
                        .InstancePerLifetimeScope();

            _builder.RegisterType<AvMINUS_DMMonthlyRepository>()
                        .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.MINUS_DM, AvIntervalEnum.Monthly))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                                (pi, ctx) => MongoDbCommon.DatabaseName))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                                (pi, ctx) => MongoDbCommon.CollectionMINUS_DMName))
                        .InstancePerLifetimeScope();
            #endregion MINUS_DM
            #region PLUS_DM
            _builder.RegisterType<AvPLUS_DMDefaultRepository>()
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.PLUS_DM, AvIntervalEnum.Daily))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.PLUS_DM, AvIntervalEnum.OneMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.PLUS_DM, AvIntervalEnum.FiveMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.PLUS_DM, AvIntervalEnum.FifteenMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.PLUS_DM, AvIntervalEnum.ThirtyMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.PLUS_DM, AvIntervalEnum.SixtyMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.PLUS_DM, AvIntervalEnum.Default))
                    .WithParameter(
                        new ResolvedParameter(
                            (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                            (pi, ctx) => MongoDbCommon.DatabaseName))
                    .WithParameter(
                        new ResolvedParameter(
                            (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                            (pi, ctx) => MongoDbCommon.CollectionPLUS_DMName))
                    .InstancePerLifetimeScope();

            _builder.RegisterType<AvPLUS_DMWeeklyRepository>()
                        .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.PLUS_DM, AvIntervalEnum.Weekly))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                                (pi, ctx) => MongoDbCommon.DatabaseName))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                                (pi, ctx) => MongoDbCommon.CollectionPLUS_DMName))
                        .InstancePerLifetimeScope();

            _builder.RegisterType<AvPLUS_DMMonthlyRepository>()
                        .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.PLUS_DM, AvIntervalEnum.Monthly))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                                (pi, ctx) => MongoDbCommon.DatabaseName))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                                (pi, ctx) => MongoDbCommon.CollectionPLUS_DMName))
                        .InstancePerLifetimeScope();
            #endregion PLUS_DM
            #region BBANDS
            _builder.RegisterType<AvBBANDSDefaultRepository>()
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.BBANDS, AvIntervalEnum.Daily))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.BBANDS, AvIntervalEnum.OneMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.BBANDS, AvIntervalEnum.FiveMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.BBANDS, AvIntervalEnum.FifteenMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.BBANDS, AvIntervalEnum.ThirtyMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.BBANDS, AvIntervalEnum.SixtyMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.BBANDS, AvIntervalEnum.Default))
                    .WithParameter(
                        new ResolvedParameter(
                            (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                            (pi, ctx) => MongoDbCommon.DatabaseName))
                    .WithParameter(
                        new ResolvedParameter(
                            (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                            (pi, ctx) => MongoDbCommon.CollectionBBANDSName))
                    .InstancePerLifetimeScope();

            _builder.RegisterType<AvBBANDSWeeklyRepository>()
                        .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.BBANDS, AvIntervalEnum.Weekly))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                                (pi, ctx) => MongoDbCommon.DatabaseName))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                                (pi, ctx) => MongoDbCommon.CollectionBBANDSName))
                        .InstancePerLifetimeScope();

            _builder.RegisterType<AvBBANDSMonthlyRepository>()
                        .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.BBANDS, AvIntervalEnum.Monthly))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                                (pi, ctx) => MongoDbCommon.DatabaseName))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                                (pi, ctx) => MongoDbCommon.CollectionBBANDSName))
                        .InstancePerLifetimeScope();
            #endregion BBANDS
            #region MIDPOINT
            _builder.RegisterType<AvMIDPOINTDefaultRepository>()
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.MIDPOINT, AvIntervalEnum.Daily))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.MIDPOINT, AvIntervalEnum.OneMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.MIDPOINT, AvIntervalEnum.FiveMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.MIDPOINT, AvIntervalEnum.FifteenMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.MIDPOINT, AvIntervalEnum.ThirtyMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.MIDPOINT, AvIntervalEnum.SixtyMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.MIDPOINT, AvIntervalEnum.Default))
                    .WithParameter(
                        new ResolvedParameter(
                            (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                            (pi, ctx) => MongoDbCommon.DatabaseName))
                    .WithParameter(
                        new ResolvedParameter(
                            (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                            (pi, ctx) => MongoDbCommon.CollectionMIDPOINTName))
                    .InstancePerLifetimeScope();

            _builder.RegisterType<AvMIDPOINTWeeklyRepository>()
                        .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.MIDPOINT, AvIntervalEnum.Weekly))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                                (pi, ctx) => MongoDbCommon.DatabaseName))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                                (pi, ctx) => MongoDbCommon.CollectionMIDPOINTName))
                        .InstancePerLifetimeScope();

            _builder.RegisterType<AvMIDPOINTMonthlyRepository>()
                        .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.MIDPOINT, AvIntervalEnum.Monthly))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                                (pi, ctx) => MongoDbCommon.DatabaseName))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                                (pi, ctx) => MongoDbCommon.CollectionMIDPOINTName))
                        .InstancePerLifetimeScope();
            #endregion MIDPOINT
            #region MIDPRICE
            _builder.RegisterType<AvMIDPRICEDefaultRepository>()
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.MIDPRICE, AvIntervalEnum.Daily))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.MIDPRICE, AvIntervalEnum.OneMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.MIDPRICE, AvIntervalEnum.FiveMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.MIDPRICE, AvIntervalEnum.FifteenMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.MIDPRICE, AvIntervalEnum.ThirtyMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.MIDPRICE, AvIntervalEnum.SixtyMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.MIDPRICE, AvIntervalEnum.Default))
                    .WithParameter(
                        new ResolvedParameter(
                            (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                            (pi, ctx) => MongoDbCommon.DatabaseName))
                    .WithParameter(
                        new ResolvedParameter(
                            (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                            (pi, ctx) => MongoDbCommon.CollectionMIDPRICEName))
                    .InstancePerLifetimeScope();

            _builder.RegisterType<AvMIDPRICEWeeklyRepository>()
                        .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.MIDPRICE, AvIntervalEnum.Weekly))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                                (pi, ctx) => MongoDbCommon.DatabaseName))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                                (pi, ctx) => MongoDbCommon.CollectionMIDPRICEName))
                        .InstancePerLifetimeScope();

            _builder.RegisterType<AvMIDPRICEMonthlyRepository>()
                        .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.MIDPRICE, AvIntervalEnum.Monthly))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                                (pi, ctx) => MongoDbCommon.DatabaseName))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                                (pi, ctx) => MongoDbCommon.CollectionMIDPRICEName))
                        .InstancePerLifetimeScope();
            #endregion MIDPRICE
            #region SAR
            _builder.RegisterType<AvSARDefaultRepository>()
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.SAR, AvIntervalEnum.Daily))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.SAR, AvIntervalEnum.OneMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.SAR, AvIntervalEnum.FiveMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.SAR, AvIntervalEnum.FifteenMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.SAR, AvIntervalEnum.ThirtyMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.SAR, AvIntervalEnum.SixtyMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.SAR, AvIntervalEnum.Default))
                    .WithParameter(
                        new ResolvedParameter(
                            (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                            (pi, ctx) => MongoDbCommon.DatabaseName))
                    .WithParameter(
                        new ResolvedParameter(
                            (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                            (pi, ctx) => MongoDbCommon.CollectionSARName))
                    .InstancePerLifetimeScope();

            _builder.RegisterType<AvSARWeeklyRepository>()
                        .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.SAR, AvIntervalEnum.Weekly))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                                (pi, ctx) => MongoDbCommon.DatabaseName))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                                (pi, ctx) => MongoDbCommon.CollectionSARName))
                        .InstancePerLifetimeScope();

            _builder.RegisterType<AvSARMonthlyRepository>()
                        .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.SAR, AvIntervalEnum.Monthly))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                                (pi, ctx) => MongoDbCommon.DatabaseName))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                                (pi, ctx) => MongoDbCommon.CollectionSARName))
                        .InstancePerLifetimeScope();
            #endregion SAR
            #region TRANGE
            _builder.RegisterType<AvTRANGEDefaultRepository>()
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.TRANGE, AvIntervalEnum.Daily))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.TRANGE, AvIntervalEnum.OneMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.TRANGE, AvIntervalEnum.FiveMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.TRANGE, AvIntervalEnum.FifteenMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.TRANGE, AvIntervalEnum.ThirtyMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.TRANGE, AvIntervalEnum.SixtyMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.TRANGE, AvIntervalEnum.Default))
                    .WithParameter(
                        new ResolvedParameter(
                            (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                            (pi, ctx) => MongoDbCommon.DatabaseName))
                    .WithParameter(
                        new ResolvedParameter(
                            (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                            (pi, ctx) => MongoDbCommon.CollectionTRANGEName))
                    .InstancePerLifetimeScope();

            _builder.RegisterType<AvTRANGEWeeklyRepository>()
                        .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.TRANGE, AvIntervalEnum.Weekly))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                                (pi, ctx) => MongoDbCommon.DatabaseName))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                                (pi, ctx) => MongoDbCommon.CollectionTRANGEName))
                        .InstancePerLifetimeScope();

            _builder.RegisterType<AvTRANGEMonthlyRepository>()
                        .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.TRANGE, AvIntervalEnum.Monthly))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                                (pi, ctx) => MongoDbCommon.DatabaseName))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                                (pi, ctx) => MongoDbCommon.CollectionTRANGEName))
                        .InstancePerLifetimeScope();
            #endregion TRANGE
            #region ATR
            _builder.RegisterType<AvATRDefaultRepository>()
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.ATR, AvIntervalEnum.Daily))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.ATR, AvIntervalEnum.OneMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.ATR, AvIntervalEnum.FiveMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.ATR, AvIntervalEnum.FifteenMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.ATR, AvIntervalEnum.ThirtyMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.ATR, AvIntervalEnum.SixtyMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.ATR, AvIntervalEnum.Default))
                    .WithParameter(
                        new ResolvedParameter(
                            (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                            (pi, ctx) => MongoDbCommon.DatabaseName))
                    .WithParameter(
                        new ResolvedParameter(
                            (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                            (pi, ctx) => MongoDbCommon.CollectionATRName))
                    .InstancePerLifetimeScope();

            _builder.RegisterType<AvATRWeeklyRepository>()
                        .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.ATR, AvIntervalEnum.Weekly))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                                (pi, ctx) => MongoDbCommon.DatabaseName))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                                (pi, ctx) => MongoDbCommon.CollectionATRName))
                        .InstancePerLifetimeScope();

            _builder.RegisterType<AvATRMonthlyRepository>()
                        .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.ATR, AvIntervalEnum.Monthly))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                                (pi, ctx) => MongoDbCommon.DatabaseName))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                                (pi, ctx) => MongoDbCommon.CollectionATRName))
                        .InstancePerLifetimeScope();
            #endregion ATR
            #region NATR
            _builder.RegisterType<AvNATRDefaultRepository>()
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.NATR, AvIntervalEnum.Daily))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.NATR, AvIntervalEnum.OneMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.NATR, AvIntervalEnum.FiveMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.NATR, AvIntervalEnum.FifteenMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.NATR, AvIntervalEnum.ThirtyMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.NATR, AvIntervalEnum.SixtyMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.NATR, AvIntervalEnum.Default))
                    .WithParameter(
                        new ResolvedParameter(
                            (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                            (pi, ctx) => MongoDbCommon.DatabaseName))
                    .WithParameter(
                        new ResolvedParameter(
                            (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                            (pi, ctx) => MongoDbCommon.CollectionNATRName))
                    .InstancePerLifetimeScope();

            _builder.RegisterType<AvNATRWeeklyRepository>()
                        .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.NATR, AvIntervalEnum.Weekly))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                                (pi, ctx) => MongoDbCommon.DatabaseName))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                                (pi, ctx) => MongoDbCommon.CollectionNATRName))
                        .InstancePerLifetimeScope();

            _builder.RegisterType<AvNATRMonthlyRepository>()
                        .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.NATR, AvIntervalEnum.Monthly))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                                (pi, ctx) => MongoDbCommon.DatabaseName))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                                (pi, ctx) => MongoDbCommon.CollectionNATRName))
                        .InstancePerLifetimeScope();
            #endregion NATR
            #region AD
            _builder.RegisterType<AvADDefaultRepository>()
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.AD, AvIntervalEnum.Daily))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.AD, AvIntervalEnum.OneMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.AD, AvIntervalEnum.FiveMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.AD, AvIntervalEnum.FifteenMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.AD, AvIntervalEnum.ThirtyMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.AD, AvIntervalEnum.SixtyMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.AD, AvIntervalEnum.Default))
                    .WithParameter(
                        new ResolvedParameter(
                            (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                            (pi, ctx) => MongoDbCommon.DatabaseName))
                    .WithParameter(
                        new ResolvedParameter(
                            (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                            (pi, ctx) => MongoDbCommon.CollectionADName))
                    .InstancePerLifetimeScope();

            _builder.RegisterType<AvADWeeklyRepository>()
                        .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.AD, AvIntervalEnum.Weekly))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                                (pi, ctx) => MongoDbCommon.DatabaseName))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                                (pi, ctx) => MongoDbCommon.CollectionADName))
                        .InstancePerLifetimeScope();

            _builder.RegisterType<AvADMonthlyRepository>()
                        .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.AD, AvIntervalEnum.Monthly))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                                (pi, ctx) => MongoDbCommon.DatabaseName))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                                (pi, ctx) => MongoDbCommon.CollectionADName))
                        .InstancePerLifetimeScope();
            #endregion AD
            #region ADOSC
            _builder.RegisterType<AvADOSCDefaultRepository>()
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.ADOSC, AvIntervalEnum.Daily))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.ADOSC, AvIntervalEnum.OneMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.ADOSC, AvIntervalEnum.FiveMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.ADOSC, AvIntervalEnum.FifteenMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.ADOSC, AvIntervalEnum.ThirtyMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.ADOSC, AvIntervalEnum.SixtyMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.ADOSC, AvIntervalEnum.Default))
                    .WithParameter(
                        new ResolvedParameter(
                            (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                            (pi, ctx) => MongoDbCommon.DatabaseName))
                    .WithParameter(
                        new ResolvedParameter(
                            (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                            (pi, ctx) => MongoDbCommon.CollectionADOSCName))
                    .InstancePerLifetimeScope();

            _builder.RegisterType<AvADOSCWeeklyRepository>()
                        .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.ADOSC, AvIntervalEnum.Weekly))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                                (pi, ctx) => MongoDbCommon.DatabaseName))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                                (pi, ctx) => MongoDbCommon.CollectionADOSCName))
                        .InstancePerLifetimeScope();

            _builder.RegisterType<AvADOSCMonthlyRepository>()
                        .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.ADOSC, AvIntervalEnum.Monthly))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                                (pi, ctx) => MongoDbCommon.DatabaseName))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                                (pi, ctx) => MongoDbCommon.CollectionADOSCName))
                        .InstancePerLifetimeScope();
            #endregion ADOSC
            #region OBV
            _builder.RegisterType<AvOBVDefaultRepository>()
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.OBV, AvIntervalEnum.Daily))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.OBV, AvIntervalEnum.OneMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.OBV, AvIntervalEnum.FiveMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.OBV, AvIntervalEnum.FifteenMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.OBV, AvIntervalEnum.ThirtyMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.OBV, AvIntervalEnum.SixtyMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.OBV, AvIntervalEnum.Default))
                    .WithParameter(
                        new ResolvedParameter(
                            (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                            (pi, ctx) => MongoDbCommon.DatabaseName))
                    .WithParameter(
                        new ResolvedParameter(
                            (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                            (pi, ctx) => MongoDbCommon.CollectionOBVName))
                    .InstancePerLifetimeScope();

            _builder.RegisterType<AvOBVWeeklyRepository>()
                        .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.OBV, AvIntervalEnum.Weekly))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                                (pi, ctx) => MongoDbCommon.DatabaseName))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                                (pi, ctx) => MongoDbCommon.CollectionOBVName))
                        .InstancePerLifetimeScope();

            _builder.RegisterType<AvOBVMonthlyRepository>()
                        .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.OBV, AvIntervalEnum.Monthly))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                                (pi, ctx) => MongoDbCommon.DatabaseName))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                                (pi, ctx) => MongoDbCommon.CollectionOBVName))
                        .InstancePerLifetimeScope();
            #endregion OBV
            #region HT_TRENDLINE
            _builder.RegisterType<AvHT_TRENDLINEDefaultRepository>()
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.HT_TRENDLINE, AvIntervalEnum.Daily))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.HT_TRENDLINE, AvIntervalEnum.OneMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.HT_TRENDLINE, AvIntervalEnum.FiveMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.HT_TRENDLINE, AvIntervalEnum.FifteenMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.HT_TRENDLINE, AvIntervalEnum.ThirtyMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.HT_TRENDLINE, AvIntervalEnum.SixtyMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.HT_TRENDLINE, AvIntervalEnum.Default))
                    .WithParameter(
                        new ResolvedParameter(
                            (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                            (pi, ctx) => MongoDbCommon.DatabaseName))
                    .WithParameter(
                        new ResolvedParameter(
                            (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                            (pi, ctx) => MongoDbCommon.CollectionHT_TRENDLINEName))
                    .InstancePerLifetimeScope();

            _builder.RegisterType<AvHT_TRENDLINEWeeklyRepository>()
                        .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.HT_TRENDLINE, AvIntervalEnum.Weekly))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                                (pi, ctx) => MongoDbCommon.DatabaseName))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                                (pi, ctx) => MongoDbCommon.CollectionHT_TRENDLINEName))
                        .InstancePerLifetimeScope();

            _builder.RegisterType<AvHT_TRENDLINEMonthlyRepository>()
                        .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.HT_TRENDLINE, AvIntervalEnum.Monthly))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                                (pi, ctx) => MongoDbCommon.DatabaseName))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                                (pi, ctx) => MongoDbCommon.CollectionHT_TRENDLINEName))
                        .InstancePerLifetimeScope();
            #endregion HT_TRENDLINE
            #region HT_SINE
            _builder.RegisterType<AvHT_SINEDefaultRepository>()
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.HT_SINE, AvIntervalEnum.Daily))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.HT_SINE, AvIntervalEnum.OneMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.HT_SINE, AvIntervalEnum.FiveMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.HT_SINE, AvIntervalEnum.FifteenMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.HT_SINE, AvIntervalEnum.ThirtyMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.HT_SINE, AvIntervalEnum.SixtyMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.HT_SINE, AvIntervalEnum.Default))
                    .WithParameter(
                        new ResolvedParameter(
                            (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                            (pi, ctx) => MongoDbCommon.DatabaseName))
                    .WithParameter(
                        new ResolvedParameter(
                            (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                            (pi, ctx) => MongoDbCommon.CollectionHT_SINEName))
                    .InstancePerLifetimeScope();

            _builder.RegisterType<AvHT_SINEWeeklyRepository>()
                        .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.HT_SINE, AvIntervalEnum.Weekly))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                                (pi, ctx) => MongoDbCommon.DatabaseName))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                                (pi, ctx) => MongoDbCommon.CollectionHT_SINEName))
                        .InstancePerLifetimeScope();

            _builder.RegisterType<AvHT_SINEMonthlyRepository>()
                        .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.HT_SINE, AvIntervalEnum.Monthly))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                                (pi, ctx) => MongoDbCommon.DatabaseName))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                                (pi, ctx) => MongoDbCommon.CollectionHT_SINEName))
                        .InstancePerLifetimeScope();
            #endregion HT_SINE
            #region HT_TRENDMODE
            _builder.RegisterType<AvHT_TRENDMODEDefaultRepository>()
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.HT_TRENDMODE, AvIntervalEnum.Daily))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.HT_TRENDMODE, AvIntervalEnum.OneMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.HT_TRENDMODE, AvIntervalEnum.FiveMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.HT_TRENDMODE, AvIntervalEnum.FifteenMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.HT_TRENDMODE, AvIntervalEnum.ThirtyMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.HT_TRENDMODE, AvIntervalEnum.SixtyMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.HT_TRENDMODE, AvIntervalEnum.Default))
                    .WithParameter(
                        new ResolvedParameter(
                            (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                            (pi, ctx) => MongoDbCommon.DatabaseName))
                    .WithParameter(
                        new ResolvedParameter(
                            (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                            (pi, ctx) => MongoDbCommon.CollectionHT_TRENDMODEName))
                    .InstancePerLifetimeScope();

            _builder.RegisterType<AvHT_TRENDMODEWeeklyRepository>()
                        .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.HT_TRENDMODE, AvIntervalEnum.Weekly))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                                (pi, ctx) => MongoDbCommon.DatabaseName))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                                (pi, ctx) => MongoDbCommon.CollectionHT_TRENDMODEName))
                        .InstancePerLifetimeScope();

            _builder.RegisterType<AvHT_TRENDMODEMonthlyRepository>()
                        .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.HT_TRENDMODE, AvIntervalEnum.Monthly))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                                (pi, ctx) => MongoDbCommon.DatabaseName))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                                (pi, ctx) => MongoDbCommon.CollectionHT_TRENDMODEName))
                        .InstancePerLifetimeScope();
            #endregion HT_TRENDMODE
            #region HT_DCPERIOD
            _builder.RegisterType<AvHT_DCPERIODDefaultRepository>()
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.HT_DCPERIOD, AvIntervalEnum.Daily))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.HT_DCPERIOD, AvIntervalEnum.OneMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.HT_DCPERIOD, AvIntervalEnum.FiveMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.HT_DCPERIOD, AvIntervalEnum.FifteenMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.HT_DCPERIOD, AvIntervalEnum.ThirtyMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.HT_DCPERIOD, AvIntervalEnum.SixtyMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.HT_DCPERIOD, AvIntervalEnum.Default))
                    .WithParameter(
                        new ResolvedParameter(
                            (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                            (pi, ctx) => MongoDbCommon.DatabaseName))
                    .WithParameter(
                        new ResolvedParameter(
                            (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                            (pi, ctx) => MongoDbCommon.CollectionHT_DCPERIODName))
                    .InstancePerLifetimeScope();

            _builder.RegisterType<AvHT_DCPERIODWeeklyRepository>()
                        .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.HT_DCPERIOD, AvIntervalEnum.Weekly))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                                (pi, ctx) => MongoDbCommon.DatabaseName))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                                (pi, ctx) => MongoDbCommon.CollectionHT_DCPERIODName))
                        .InstancePerLifetimeScope();

            _builder.RegisterType<AvHT_DCPERIODMonthlyRepository>()
                        .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.HT_DCPERIOD, AvIntervalEnum.Monthly))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                                (pi, ctx) => MongoDbCommon.DatabaseName))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                                (pi, ctx) => MongoDbCommon.CollectionHT_DCPERIODName))
                        .InstancePerLifetimeScope();
            #endregion HT_DCPERIOD
            #region HT_DCPHASE
            _builder.RegisterType<AvHT_DCPHASEDefaultRepository>()
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.HT_DCPHASE, AvIntervalEnum.Daily))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.HT_DCPHASE, AvIntervalEnum.OneMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.HT_DCPHASE, AvIntervalEnum.FiveMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.HT_DCPHASE, AvIntervalEnum.FifteenMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.HT_DCPHASE, AvIntervalEnum.ThirtyMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.HT_DCPHASE, AvIntervalEnum.SixtyMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.HT_DCPHASE, AvIntervalEnum.Default))
                    .WithParameter(
                        new ResolvedParameter(
                            (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                            (pi, ctx) => MongoDbCommon.DatabaseName))
                    .WithParameter(
                        new ResolvedParameter(
                            (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                            (pi, ctx) => MongoDbCommon.CollectionHT_DCPHASEName))
                    .InstancePerLifetimeScope();

            _builder.RegisterType<AvHT_DCPHASEWeeklyRepository>()
                        .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.HT_DCPHASE, AvIntervalEnum.Weekly))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                                (pi, ctx) => MongoDbCommon.DatabaseName))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                                (pi, ctx) => MongoDbCommon.CollectionHT_DCPHASEName))
                        .InstancePerLifetimeScope();

            _builder.RegisterType<AvHT_DCPHASEMonthlyRepository>()
                        .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.HT_DCPHASE, AvIntervalEnum.Monthly))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                                (pi, ctx) => MongoDbCommon.DatabaseName))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                                (pi, ctx) => MongoDbCommon.CollectionHT_DCPHASEName))
                        .InstancePerLifetimeScope();
            #endregion HT_DCPHASE
            #region HT_PHASOR
            _builder.RegisterType<AvHT_PHASORDefaultRepository>()
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.HT_PHASOR, AvIntervalEnum.Daily))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.HT_PHASOR, AvIntervalEnum.OneMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.HT_PHASOR, AvIntervalEnum.FiveMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.HT_PHASOR, AvIntervalEnum.FifteenMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.HT_PHASOR, AvIntervalEnum.ThirtyMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.HT_PHASOR, AvIntervalEnum.SixtyMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.HT_PHASOR, AvIntervalEnum.Default))
                    .WithParameter(
                        new ResolvedParameter(
                            (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                            (pi, ctx) => MongoDbCommon.DatabaseName))
                    .WithParameter(
                        new ResolvedParameter(
                            (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                            (pi, ctx) => MongoDbCommon.CollectionHT_PHASORName))
                    .InstancePerLifetimeScope();

            _builder.RegisterType<AvHT_PHASORWeeklyRepository>()
                        .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.HT_PHASOR, AvIntervalEnum.Weekly))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                                (pi, ctx) => MongoDbCommon.DatabaseName))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                                (pi, ctx) => MongoDbCommon.CollectionHT_PHASORName))
                        .InstancePerLifetimeScope();

            _builder.RegisterType<AvHT_PHASORMonthlyRepository>()
                        .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.HT_PHASOR, AvIntervalEnum.Monthly))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                                (pi, ctx) => MongoDbCommon.DatabaseName))
                        .WithParameter(
                            new ResolvedParameter(
                                (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                                (pi, ctx) => MongoDbCommon.CollectionHT_PHASORName))
                        .InstancePerLifetimeScope();
            #endregion HT_PHASOR


            // Time Series
            #region DailyAdjTimeSeries
            _builder.RegisterType<AvDailyAdjTimeSeriesDefaultRepository>()
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.DailyAdjusted, AvIntervalEnum.Default))
                    .WithParameter(
                        new ResolvedParameter(
                            (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                            (pi, ctx) => MongoDbCommon.DatabaseName))
                    .WithParameter(
                        new ResolvedParameter(
                            (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                            (pi, ctx) => MongoDbCommon.CollectionEquitiesName))
                    .InstancePerLifetimeScope();
            #endregion DailyAdjTimeSeries

            #region DailyTimeSeries
            _builder.RegisterType<AvDailyTimeSeriesDefaultRepository>()
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.Daily, AvIntervalEnum.Default))
                    .WithParameter(
                        new ResolvedParameter(
                            (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                            (pi, ctx) => MongoDbCommon.DatabaseName))
                    .WithParameter(
                        new ResolvedParameter(
                            (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                            (pi, ctx) => MongoDbCommon.CollectionEquitiesName))
                    .InstancePerLifetimeScope();
            #endregion DailyTimeSeries

            #region IntraDayTimeSeries
            _builder.RegisterType<AvIntraDayTimeSeriesDefaultRepository>()
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.InteraDay, AvIntervalEnum.OneMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.InteraDay, AvIntervalEnum.FiveMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.InteraDay, AvIntervalEnum.FifteenMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.InteraDay, AvIntervalEnum.ThirtyMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.InteraDay, AvIntervalEnum.SixtyMin))
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.InteraDay, AvIntervalEnum.Default))
                    .WithParameter(
                        new ResolvedParameter(
                            (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                            (pi, ctx) => MongoDbCommon.DatabaseName))
                    .WithParameter(
                        new ResolvedParameter(
                            (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                            (pi, ctx) => MongoDbCommon.CollectionEquitiesName))
                    .InstancePerLifetimeScope();
            #endregion IntraDayTimeSeries

            #region WeeklyTimeSeries
            _builder.RegisterType<AvWeeklyTimeSeriesDefaultRepository>()
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.Weekly, AvIntervalEnum.Default))
                    .WithParameter(
                        new ResolvedParameter(
                            (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                            (pi, ctx) => MongoDbCommon.DatabaseName))
                    .WithParameter(
                        new ResolvedParameter(
                            (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                            (pi, ctx) => MongoDbCommon.CollectionEquitiesName))
                    .InstancePerLifetimeScope();
            #endregion WeeklyTimeSeries

            #region WeeklyAdjTimeSeries
            _builder.RegisterType<AvWeeklyAdjTimeSeriesDefaultRepository>()
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.WeeklyAdjusted, AvIntervalEnum.Default))
                    .WithParameter(
                        new ResolvedParameter(
                            (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                            (pi, ctx) => MongoDbCommon.DatabaseName))
                    .WithParameter(
                        new ResolvedParameter(
                            (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                            (pi, ctx) => MongoDbCommon.CollectionEquitiesName))
                    .InstancePerLifetimeScope();
            #endregion WeeklyTimeSeries

            #region MonthlyTimeSeries
            _builder.RegisterType<AvMonthlyTimeSeriesDefaultRepository>()
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.Monthly, AvIntervalEnum.Default))
                    .WithParameter(
                        new ResolvedParameter(
                            (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                            (pi, ctx) => MongoDbCommon.DatabaseName))
                    .WithParameter(
                        new ResolvedParameter(
                            (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                            (pi, ctx) => MongoDbCommon.CollectionEquitiesName))
                    .InstancePerLifetimeScope();
            #endregion MonthlyTimeSeries

            #region MonthlyAdjTimeSeries
            _builder.RegisterType<AvMonthlyAdjTimeSeriesDefaultRepository>()
                    .Keyed<IRepositoryAnchor>(CommonHelper.GetRepositoryKeyedName(AvFunctionEnum.MonthlyAdjusted, AvIntervalEnum.Default))
                    .WithParameter(
                        new ResolvedParameter(
                            (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "dbName",
                            (pi, ctx) => MongoDbCommon.DatabaseName))
                    .WithParameter(
                        new ResolvedParameter(
                            (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "collectionName",
                            (pi, ctx) => MongoDbCommon.CollectionEquitiesName))
                    .InstancePerLifetimeScope();
            #endregion MonthlyAdjTimeSeries

            _builder.RegisterType<AvRepositoryFactory>().As<IAvRepositoryFactory>();

            return this;
        }

        public virtual AutoFacBootStrapper AlphaVantageCoreSetup()
        {
            _builder.RegisterType<DownloadWithRetry>().As<IDownloadWithRetry>();

            // .Populate requires Autofac.Extensions.DependencyInjection
            _builder.Populate(_services);

            return this;
        }

        public virtual AutoFacBootStrapper LoggerSetup()
        {

            _builder.RegisterModule(new AutoFacLogInjectionModule());   // required for Nlog

            // required for Nlog
            _builder.RegisterType<LoggerFactory>().As<ILoggerFactory>().SingleInstance();
            _builder.RegisterGeneric(typeof(Logger<>)).As(typeof(ILogger<>)).SingleInstance();

            return this;
        }

        public virtual AutoFacBootStrapper InfrastructureSetup()
        {
            return this;
        }

        public virtual IContainer Build()
        {
            return _builder.Build();
        }

        protected virtual void Dispose(bool value)
        {

        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
