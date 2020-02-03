using System;
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
using AlphaVantage.Core.Interfaces;
using Newtonsoft.Json.Linq;
using AlphaVantage.DataAccess.Interfaces;


namespace AlphaVantage.TimedTask.Runner
{
    public partial class Helm
    {
        public AvSMA MapInvocation(IMapResource<AvSMA> downloader, JObject jobj, string uri)
        {
            return downloader.Map(jobj, uri);
        }

        public void SaveInvocation(IRepository<AvSMA> repository, AvSMA data)
        {
            Console.WriteLine($"saving { data.MetaData.Symbol}:{ data.MetaData.Function.Name} - { data.MetaData.LastRefreshed}");
            repository.Upsert(data);
        }

        public AvEMA MapInvocation(IMapResource<AvEMA> downloader, JObject jobj, string uri)
        {
            return downloader.Map(jobj, uri);
        }

        public void SaveInvocation(IRepository<AvEMA> repository, AvEMA data)
        {
            Console.WriteLine($"saving { data.MetaData.Symbol}:{ data.MetaData.Function.Name} - { data.MetaData.LastRefreshed}");
            repository.Upsert(data);
        }

        public AvWMA MapInvocation(IMapResource<AvWMA> downloader, JObject jobj, string uri)
        {
            return downloader.Map(jobj, uri);
        }

        public void SaveInvocation(IRepository<AvWMA> repository, AvWMA data)
        {
            Console.WriteLine($"saving { data.MetaData.Symbol}:{ data.MetaData.Function.Name} - { data.MetaData.LastRefreshed}");
            repository.Upsert(data);
        }

        public AvDEMA MapInvocation(IMapResource<AvDEMA> downloader, JObject jobj, string uri)
        {
            return downloader.Map(jobj, uri);
        }

        public void SaveInvocation(IRepository<AvDEMA> repository, AvDEMA data)
        {
            Console.WriteLine($"saving { data.MetaData.Symbol}:{ data.MetaData.Function.Name} - { data.MetaData.LastRefreshed}");
            repository.Upsert(data);
        }

        public AvTEMA MapInvocation(IMapResource<AvTEMA> downloader, JObject jobj, string uri)
        {
            return downloader.Map(jobj, uri);
        }

        public void SaveInvocation(IRepository<AvTEMA> repository, AvTEMA data)
        {
            Console.WriteLine($"saving { data.MetaData.Symbol}:{ data.MetaData.Function.Name} - { data.MetaData.LastRefreshed}");
            repository.Upsert(data);
        }

        public AvTRIMA MapInvocation(IMapResource<AvTRIMA> downloader, JObject jobj, string uri)
        {
            return downloader.Map(jobj, uri);
        }

        public void SaveInvocation(IRepository<AvTRIMA> repository, AvTRIMA data)
        {
            Console.WriteLine($"saving { data.MetaData.Symbol}:{ data.MetaData.Function.Name} - { data.MetaData.LastRefreshed}");
            repository.Upsert(data);
        }

        public AvKAMA MapInvocation(IMapResource<AvKAMA> downloader, JObject jobj, string uri)
        {
            return downloader.Map(jobj, uri);
        }

        public void SaveInvocation(IRepository<AvKAMA> repository, AvKAMA data)
        {
            Console.WriteLine($"saving { data.MetaData.Symbol}:{ data.MetaData.Function.Name} - { data.MetaData.LastRefreshed}");
            repository.Upsert(data);
        }

        public AvMAMA MapInvocation(IMapResource<AvMAMA> downloader, JObject jobj, string uri)
        {
            return downloader.Map(jobj, uri);
        }

        public void SaveInvocation(IRepository<AvMAMA> repository, AvMAMA data)
        {
            Console.WriteLine($"saving { data.MetaData.Symbol}:{ data.MetaData.Function.Name} - { data.MetaData.LastRefreshed}");
            repository.Upsert(data);
        }

        public AvVWAP MapInvocation(IMapResource<AvVWAP> downloader, JObject jobj, string uri)
        {
            return downloader.Map(jobj, uri);
        }

        public void SaveInvocation(IRepository<AvVWAP> repository, AvVWAP data)
        {
            Console.WriteLine($"saving { data.MetaData.Symbol}:{ data.MetaData.Function.Name} - { data.MetaData.LastRefreshed}");
            repository.Upsert(data);
        }

        public AvT3 MapInvocation(IMapResource<AvT3> downloader, JObject jobj, string uri)
        {
            return downloader.Map(jobj, uri);
        }

        public void SaveInvocation(IRepository<AvT3> repository, AvT3 data)
        {
            Console.WriteLine($"saving { data.MetaData.Symbol}:{ data.MetaData.Function.Name} - { data.MetaData.LastRefreshed}");
            repository.Upsert(data);
        }

        public AvMACD MapInvocation(IMapResource<AvMACD> downloader, JObject jobj, string uri)
        {
            return downloader.Map(jobj, uri);
        }

        public void SaveInvocation(IRepository<AvMACD> repository, AvMACD data)
        {
            Console.WriteLine($"saving { data.MetaData.Symbol}:{ data.MetaData.Function.Name} - { data.MetaData.LastRefreshed}");
            repository.Upsert(data);
        }

        public AvMACDEXT MapInvocation(IMapResource<AvMACDEXT> downloader, JObject jobj, string uri)
        {
            return downloader.Map(jobj, uri);
        }

        public void SaveInvocation(IRepository<AvMACDEXT> repository, AvMACDEXT data)
        {
            Console.WriteLine($"saving { data.MetaData.Symbol}:{ data.MetaData.Function.Name} - { data.MetaData.LastRefreshed}");
            repository.Upsert(data);
        }

        public AvSTOCH MapInvocation(IMapResource<AvSTOCH> downloader, JObject jobj, string uri)
        {
            return downloader.Map(jobj, uri);
        }

        public void SaveInvocation(IRepository<AvSTOCH> repository, AvSTOCH data)
        {
            Console.WriteLine($"saving { data.MetaData.Symbol}:{ data.MetaData.Function.Name} - { data.MetaData.LastRefreshed}");
            repository.Upsert(data);
        }

        public AvSTOCHF MapInvocation(IMapResource<AvSTOCHF> downloader, JObject jobj, string uri)
        {
            return downloader.Map(jobj, uri);
        }

        public void SaveInvocation(IRepository<AvSTOCHF> repository, AvSTOCHF data)
        {
            Console.WriteLine($"saving { data.MetaData.Symbol}:{ data.MetaData.Function.Name} - { data.MetaData.LastRefreshed}");
            repository.Upsert(data);
        }

        public AvRSI MapInvocation(IMapResource<AvRSI> downloader, JObject jobj, string uri)
        {
            return downloader.Map(jobj, uri);
        }

        public void SaveInvocation(IRepository<AvRSI> repository, AvRSI data)
        {
            Console.WriteLine($"saving { data.MetaData.Symbol}:{ data.MetaData.Function.Name} - { data.MetaData.LastRefreshed}");
            repository.Upsert(data);
        }

        public AvSTOCHRSI MapInvocation(IMapResource<AvSTOCHRSI> downloader, JObject jobj, string uri)
        {
            return downloader.Map(jobj, uri);
        }

        public void SaveInvocation(IRepository<AvSTOCHRSI> repository, AvSTOCHRSI data)
        {
            Console.WriteLine($"saving { data.MetaData.Symbol}:{ data.MetaData.Function.Name} - { data.MetaData.LastRefreshed}");
            repository.Upsert(data);
        }

        public AvWILLR MapInvocation(IMapResource<AvWILLR> downloader, JObject jobj, string uri)
        {
            return downloader.Map(jobj, uri);
        }

        public void SaveInvocation(IRepository<AvWILLR> repository, AvWILLR data)
        {
            Console.WriteLine($"saving { data.MetaData.Symbol}:{ data.MetaData.Function.Name} - { data.MetaData.LastRefreshed}");
            repository.Upsert(data);
        }

        public AvADX MapInvocation(IMapResource<AvADX> downloader, JObject jobj, string uri)
        {
            return downloader.Map(jobj, uri);
        }

        public void SaveInvocation(IRepository<AvADX> repository, AvADX data)
        {
            Console.WriteLine($"saving { data.MetaData.Symbol}:{ data.MetaData.Function.Name} - { data.MetaData.LastRefreshed}");
            repository.Upsert(data);
        }

        public AvADXR MapInvocation(IMapResource<AvADXR> downloader, JObject jobj, string uri)
        {
            return downloader.Map(jobj, uri);
        }

        public void SaveInvocation(IRepository<AvADXR> repository, AvADXR data)
        {
            Console.WriteLine($"saving { data.MetaData.Symbol}:{ data.MetaData.Function.Name} - { data.MetaData.LastRefreshed}");
            repository.Upsert(data);
        }

        public AvAPO MapInvocation(IMapResource<AvAPO> downloader, JObject jobj, string uri)
        {
            return downloader.Map(jobj, uri);
        }

        public void SaveInvocation(IRepository<AvAPO> repository, AvAPO data)
        {
            Console.WriteLine($"saving { data.MetaData.Symbol}:{ data.MetaData.Function.Name} - { data.MetaData.LastRefreshed}");
            repository.Upsert(data);
        }

        public AvPPO MapInvocation(IMapResource<AvPPO> downloader, JObject jobj, string uri)
        {
            return downloader.Map(jobj, uri);
        }

        public void SaveInvocation(IRepository<AvPPO> repository, AvPPO data)
        {
            Console.WriteLine($"saving { data.MetaData.Symbol}:{ data.MetaData.Function.Name} - { data.MetaData.LastRefreshed}");
            repository.Upsert(data);
        }

        public AvMOM MapInvocation(IMapResource<AvMOM> downloader, JObject jobj, string uri)
        {
            return downloader.Map(jobj, uri);
        }

        public void SaveInvocation(IRepository<AvMOM> repository, AvMOM data)
        {
            Console.WriteLine($"saving { data.MetaData.Symbol}:{ data.MetaData.Function.Name} - { data.MetaData.LastRefreshed}");
            repository.Upsert(data);
        }

        public AvBOP MapInvocation(IMapResource<AvBOP> downloader, JObject jobj, string uri)
        {
            return downloader.Map(jobj, uri);
        }

        public void SaveInvocation(IRepository<AvBOP> repository, AvBOP data)
        {
            Console.WriteLine($"saving { data.MetaData.Symbol}:{ data.MetaData.Function.Name} - { data.MetaData.LastRefreshed}");
            repository.Upsert(data);
        }

        public AvCCI MapInvocation(IMapResource<AvCCI> downloader, JObject jobj, string uri)
        {
            return downloader.Map(jobj, uri);
        }

        public void SaveInvocation(IRepository<AvCCI> repository, AvCCI data)
        {
            Console.WriteLine($"saving { data.MetaData.Symbol}:{ data.MetaData.Function.Name} - { data.MetaData.LastRefreshed}");
            repository.Upsert(data);
        }

        public AvCMO MapInvocation(IMapResource<AvCMO> downloader, JObject jobj, string uri)
        {
            return downloader.Map(jobj, uri);
        }

        public void SaveInvocation(IRepository<AvCMO> repository, AvCMO data)
        {
            Console.WriteLine($"saving { data.MetaData.Symbol}:{ data.MetaData.Function.Name} - { data.MetaData.LastRefreshed}");
            repository.Upsert(data);
        }

        public AvROC MapInvocation(IMapResource<AvROC> downloader, JObject jobj, string uri)
        {
            return downloader.Map(jobj, uri);
        }

        public void SaveInvocation(IRepository<AvROC> repository, AvROC data)
        {
            Console.WriteLine($"saving { data.MetaData.Symbol}:{ data.MetaData.Function.Name} - { data.MetaData.LastRefreshed}");
            repository.Upsert(data);
        }

        public AvROCR MapInvocation(IMapResource<AvROCR> downloader, JObject jobj, string uri)
        {
            return downloader.Map(jobj, uri);
        }

        public void SaveInvocation(IRepository<AvROCR> repository, AvROCR data)
        {
            Console.WriteLine($"saving { data.MetaData.Symbol}:{ data.MetaData.Function.Name} - { data.MetaData.LastRefreshed}");
            repository.Upsert(data);
        }

        public AvAROON MapInvocation(IMapResource<AvAROON> downloader, JObject jobj, string uri)
        {
            return downloader.Map(jobj, uri);
        }

        public void SaveInvocation(IRepository<AvAROON> repository, AvAROON data)
        {
            Console.WriteLine($"saving { data.MetaData.Symbol}:{ data.MetaData.Function.Name} - { data.MetaData.LastRefreshed}");
            repository.Upsert(data);
        }

        public AvAROONOSC MapInvocation(IMapResource<AvAROONOSC> downloader, JObject jobj, string uri)
        {
            return downloader.Map(jobj, uri);
        }

        public void SaveInvocation(IRepository<AvAROONOSC> repository, AvAROONOSC data)
        {
            Console.WriteLine($"saving { data.MetaData.Symbol}:{ data.MetaData.Function.Name} - { data.MetaData.LastRefreshed}");
            repository.Upsert(data);
        }

        public AvMFI MapInvocation(IMapResource<AvMFI> downloader, JObject jobj, string uri)
        {
            return downloader.Map(jobj, uri);
        }

        public void SaveInvocation(IRepository<AvMFI> repository, AvMFI data)
        {
            Console.WriteLine($"saving { data.MetaData.Symbol}:{ data.MetaData.Function.Name} - { data.MetaData.LastRefreshed}");
            repository.Upsert(data);
        }

        public AvTRIX MapInvocation(IMapResource<AvTRIX> downloader, JObject jobj, string uri)
        {
            return downloader.Map(jobj, uri);
        }

        public void SaveInvocation(IRepository<AvTRIX> repository, AvTRIX data)
        {
            Console.WriteLine($"saving { data.MetaData.Symbol}:{ data.MetaData.Function.Name} - { data.MetaData.LastRefreshed}");
            repository.Upsert(data);
        }

        public AvULTOSC MapInvocation(IMapResource<AvULTOSC> downloader, JObject jobj, string uri)
        {
            return downloader.Map(jobj, uri);
        }

        public void SaveInvocation(IRepository<AvULTOSC> repository, AvULTOSC data)
        {
            Console.WriteLine($"saving { data.MetaData.Symbol}:{ data.MetaData.Function.Name} - { data.MetaData.LastRefreshed}");
            repository.Upsert(data);
        }

        public AvDX MapInvocation(IMapResource<AvDX> downloader, JObject jobj, string uri)
        {
            return downloader.Map(jobj, uri);
        }

        public void SaveInvocation(IRepository<AvDX> repository, AvDX data)
        {
            Console.WriteLine($"saving { data.MetaData.Symbol}:{ data.MetaData.Function.Name} - { data.MetaData.LastRefreshed}");
            repository.Upsert(data);
        }

        public AvMINUS_DI MapInvocation(IMapResource<AvMINUS_DI> downloader, JObject jobj, string uri)
        {
            return downloader.Map(jobj, uri);
        }

        public void SaveInvocation(IRepository<AvMINUS_DI> repository, AvMINUS_DI data)
        {
            Console.WriteLine($"saving { data.MetaData.Symbol}:{ data.MetaData.Function.Name} - { data.MetaData.LastRefreshed}");
            repository.Upsert(data);
        }

        public AvPLUS_DI MapInvocation(IMapResource<AvPLUS_DI> downloader, JObject jobj, string uri)
        {
            return downloader.Map(jobj, uri);
        }

        public void SaveInvocation(IRepository<AvPLUS_DI> repository, AvPLUS_DI data)
        {
            Console.WriteLine($"saving { data.MetaData.Symbol}:{ data.MetaData.Function.Name} - { data.MetaData.LastRefreshed}");
            repository.Upsert(data);
        }

        public AvMINUS_DM MapInvocation(IMapResource<AvMINUS_DM> downloader, JObject jobj, string uri)
        {
            return downloader.Map(jobj, uri);
        }

        public void SaveInvocation(IRepository<AvMINUS_DM> repository, AvMINUS_DM data)
        {
            Console.WriteLine($"saving { data.MetaData.Symbol}:{ data.MetaData.Function.Name} - { data.MetaData.LastRefreshed}");
            repository.Upsert(data);
        }

        public AvPLUS_DM MapInvocation(IMapResource<AvPLUS_DM> downloader, JObject jobj, string uri)
        {
            return downloader.Map(jobj, uri);
        }

        public void SaveInvocation(IRepository<AvPLUS_DM> repository, AvPLUS_DM data)
        {
            Console.WriteLine($"saving { data.MetaData.Symbol}:{ data.MetaData.Function.Name} - { data.MetaData.LastRefreshed}");
            repository.Upsert(data);
        }

        public AvBBANDS MapInvocation(IMapResource<AvBBANDS> downloader, JObject jobj, string uri)
        {
            return downloader.Map(jobj, uri);
        }

        public void SaveInvocation(IRepository<AvBBANDS> repository, AvBBANDS data)
        {
            Console.WriteLine($"saving { data.MetaData.Symbol}:{ data.MetaData.Function.Name} - { data.MetaData.LastRefreshed}");
            repository.Upsert(data);
        }

        public AvMIDPOINT MapInvocation(IMapResource<AvMIDPOINT> downloader, JObject jobj, string uri)
        {
            return downloader.Map(jobj, uri);
        }

        public void SaveInvocation(IRepository<AvMIDPOINT> repository, AvMIDPOINT data)
        {
            Console.WriteLine($"saving { data.MetaData.Symbol}:{ data.MetaData.Function.Name} - { data.MetaData.LastRefreshed}");
            repository.Upsert(data);
        }

        public AvMIDPRICE MapInvocation(IMapResource<AvMIDPRICE> downloader, JObject jobj, string uri)
        {
            return downloader.Map(jobj, uri);
        }

        public void SaveInvocation(IRepository<AvMIDPRICE> repository, AvMIDPRICE data)
        {
            Console.WriteLine($"saving { data.MetaData.Symbol}:{ data.MetaData.Function.Name} - { data.MetaData.LastRefreshed}");
            repository.Upsert(data);
        }

        public AvSAR MapInvocation(IMapResource<AvSAR> downloader, JObject jobj, string uri)
        {
            return downloader.Map(jobj, uri);
        }

        public void SaveInvocation(IRepository<AvSAR> repository, AvSAR data)
        {
            Console.WriteLine($"saving { data.MetaData.Symbol}:{ data.MetaData.Function.Name} - { data.MetaData.LastRefreshed}");
            repository.Upsert(data);
        }

        public AvTRANGE MapInvocation(IMapResource<AvTRANGE> downloader, JObject jobj, string uri)
        {
            return downloader.Map(jobj, uri);
        }

        public void SaveInvocation(IRepository<AvTRANGE> repository, AvTRANGE data)
        {
            Console.WriteLine($"saving { data.MetaData.Symbol}:{ data.MetaData.Function.Name} - { data.MetaData.LastRefreshed}");
            repository.Upsert(data);
        }

        public AvATR MapInvocation(IMapResource<AvATR> downloader, JObject jobj, string uri)
        {
            return downloader.Map(jobj, uri);
        }

        public void SaveInvocation(IRepository<AvATR> repository, AvATR data)
        {
            Console.WriteLine($"saving { data.MetaData.Symbol}:{ data.MetaData.Function.Name} - { data.MetaData.LastRefreshed}");
            repository.Upsert(data);
        }

        public AvNATR MapInvocation(IMapResource<AvNATR> downloader, JObject jobj, string uri)
        {
            return downloader.Map(jobj, uri);
        }

        public void SaveInvocation(IRepository<AvNATR> repository, AvNATR data)
        {
            Console.WriteLine($"saving { data.MetaData.Symbol}:{ data.MetaData.Function.Name} - { data.MetaData.LastRefreshed}");
            repository.Upsert(data);
        }

        public AvAD MapInvocation(IMapResource<AvAD> downloader, JObject jobj, string uri)
        {
            return downloader.Map(jobj, uri);
        }

        public void SaveInvocation(IRepository<AvAD> repository, AvAD data)
        {
            Console.WriteLine($"saving { data.MetaData.Symbol}:{ data.MetaData.Function.Name} - { data.MetaData.LastRefreshed}");
            repository.Upsert(data);
        }

        public AvADOSC MapInvocation(IMapResource<AvADOSC> downloader, JObject jobj, string uri)
        {
            return downloader.Map(jobj, uri);
        }

        public void SaveInvocation(IRepository<AvADOSC> repository, AvADOSC data)
        {
            Console.WriteLine($"saving { data.MetaData.Symbol}:{ data.MetaData.Function.Name} - { data.MetaData.LastRefreshed}");
            repository.Upsert(data);
        }

        public AvOBV MapInvocation(IMapResource<AvOBV> downloader, JObject jobj, string uri)
        {
            return downloader.Map(jobj, uri);
        }

        public void SaveInvocation(IRepository<AvOBV> repository, AvOBV data)
        {
            Console.WriteLine($"saving { data.MetaData.Symbol}:{ data.MetaData.Function.Name} - { data.MetaData.LastRefreshed}");
            repository.Upsert(data);
        }

        public AvHT_TRENDLINE MapInvocation(IMapResource<AvHT_TRENDLINE> downloader, JObject jobj, string uri)
        {
            return downloader.Map(jobj, uri);
        }

        public void SaveInvocation(IRepository<AvHT_TRENDLINE> repository, AvHT_TRENDLINE data)
        {
            Console.WriteLine($"saving { data.MetaData.Symbol}:{ data.MetaData.Function.Name} - { data.MetaData.LastRefreshed}");
            repository.Upsert(data);
        }

        public AvHT_SINE MapInvocation(IMapResource<AvHT_SINE> downloader, JObject jobj, string uri)
        {
            return downloader.Map(jobj, uri);
        }

        public void SaveInvocation(IRepository<AvHT_SINE> repository, AvHT_SINE data)
        {
            Console.WriteLine($"saving { data.MetaData.Symbol}:{ data.MetaData.Function.Name} - { data.MetaData.LastRefreshed}");
            repository.Upsert(data);
        }

        public AvHT_TRENDMODE MapInvocation(IMapResource<AvHT_TRENDMODE> downloader, JObject jobj, string uri)
        {
            return downloader.Map(jobj, uri);
        }

        public void SaveInvocation(IRepository<AvHT_TRENDMODE> repository, AvHT_TRENDMODE data)
        {
            Console.WriteLine($"saving { data.MetaData.Symbol}:{ data.MetaData.Function.Name} - { data.MetaData.LastRefreshed}");
            repository.Upsert(data);
        }

        public AvHT_DCPERIOD MapInvocation(IMapResource<AvHT_DCPERIOD> downloader, JObject jobj, string uri)
        {
            return downloader.Map(jobj, uri);
        }

        public void SaveInvocation(IRepository<AvHT_DCPERIOD> repository, AvHT_DCPERIOD data)
        {
            Console.WriteLine($"saving { data.MetaData.Symbol}:{ data.MetaData.Function.Name} - { data.MetaData.LastRefreshed}");
            repository.Upsert(data);
        }

        public AvHT_DCPHASE MapInvocation(IMapResource<AvHT_DCPHASE> downloader, JObject jobj, string uri)
        {
            return downloader.Map(jobj, uri);
        }

        public void SaveInvocation(IRepository<AvHT_DCPHASE> repository, AvHT_DCPHASE data)
        {
            Console.WriteLine($"saving { data.MetaData.Symbol}:{ data.MetaData.Function.Name} - { data.MetaData.LastRefreshed}");
            repository.Upsert(data);
        }

        public AvHT_PHASOR MapInvocation(IMapResource<AvHT_PHASOR> downloader, JObject jobj, string uri)
        {
            return downloader.Map(jobj, uri);
        }

        public void SaveInvocation(IRepository<AvHT_PHASOR> repository, AvHT_PHASOR data)
        {
            Console.WriteLine($"saving { data.MetaData.Symbol}:{ data.MetaData.Function.Name} - { data.MetaData.LastRefreshed}");
            repository.Upsert(data);
        }

    }
}
