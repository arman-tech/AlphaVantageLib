using AlphaVantage.Common.Models.TimeSeries.Daily;
using AlphaVantage.Common.Models.TimeSeries.DailyAdjusted;
using AlphaVantage.Common.Models.TimeSeries.IntraDay;
using AlphaVantage.Common.Models.TimeSeries.Monthly;
using AlphaVantage.Common.Models.TimeSeries.MonthlyAdjusted;
using AlphaVantage.Common.Models.TimeSeries.Weekly;
using AlphaVantage.Common.Models.TimeSeries.WeeklyAdjusted;
using AlphaVantage.Core.Interfaces;
using AlphaVantage.DataAccess.Interfaces;
using Newtonsoft.Json.Linq;
using System;

namespace AlphaVantage.TimedTask.Runner
{
    public partial class Helm
    {
        public AvMonthlyTimeSeries MapInvocation(IMapResource<AvMonthlyTimeSeries> downloader, JObject jobj, string uri)
        {
            return downloader.Map(jobj, uri);
        }

        public void SaveInvocation(IRepository<AvMonthlyTimeSeries> repository, AvMonthlyTimeSeries data)
        {
            Console.WriteLine($"saving {data.MetaData.Symbol}:{data.MetaData.Function.Name} - {data.MetaData.LastRefreshed}");
            repository.Upsert(data);
        }

        public AvMonthlyAdjTimeSeries MapInvocation(IMapResource<AvMonthlyAdjTimeSeries> downloader, JObject jobj, string uri)
        {
            return downloader.Map(jobj, uri);
        }

        public void SaveInvocation(IRepository<AvMonthlyAdjTimeSeries> repository, AvMonthlyAdjTimeSeries data)
        {
            Console.WriteLine($"saving {data.MetaData.Symbol}:{data.MetaData.Function.Name} - {data.MetaData.LastRefreshed}");
            repository.Upsert(data);
        }

        public AvWeeklyTimeSeries MapInvocation(IMapResource<AvWeeklyTimeSeries> downloader, JObject jobj, string uri)
        {
            return downloader.Map(jobj, uri);
        }

        public void SaveInvocation(IRepository<AvWeeklyTimeSeries> repository, AvWeeklyTimeSeries data)
        {
            Console.WriteLine($"saving {data.MetaData.Symbol}:{data.MetaData.Function.Name} - {data.MetaData.LastRefreshed}");
            repository.Upsert(data);
        }

        public AvWeeklyAdjTimeSeries MapInvocation(IMapResource<AvWeeklyAdjTimeSeries> downloader, JObject jobj, string uri)
        {
            return downloader.Map(jobj, uri);
        }

        public void SaveInvocation(IRepository<AvWeeklyAdjTimeSeries> repository, AvWeeklyAdjTimeSeries data)
        {
            Console.WriteLine($"saving {data.MetaData.Symbol}:{data.MetaData.Function.Name} - {data.MetaData.LastRefreshed}");
            repository.Upsert(data);
        }

        public AvIntraDayTimeSeries MapInvocation(IMapResource<AvIntraDayTimeSeries> downloader, JObject jobj, string uri)
        {
            return downloader.Map(jobj, uri);
        }

        public void SaveInvocation(IRepository<AvIntraDayTimeSeries> repository, AvIntraDayTimeSeries data)
        {
            Console.WriteLine($"saving {data.MetaData.Symbol}:{data.MetaData.Function.Name} - {data.MetaData.LastRefreshed}");
            repository.Upsert(data);
        }

        public AvDailyTimeSeries MapInvocation(IMapResource<AvDailyTimeSeries> downloader, JObject jobj, string uri)
        {
            return downloader.Map(jobj, uri);
        }

        public void SaveInvocation(IRepository<AvDailyTimeSeries> repository, AvDailyTimeSeries data)
        {
            Console.WriteLine($"saving {data.MetaData.Symbol}:{data.MetaData.Function.Name} - {data.MetaData.LastRefreshed}");
            repository.Upsert(data);
        }

        public AvDailyAdjTimeSeries MapInvocation(IMapResource<AvDailyAdjTimeSeries> downloader, JObject jobj, string uri)
        {
            return downloader.Map(jobj, uri);
        }

        public void SaveInvocation(IRepository<AvDailyAdjTimeSeries> repository, AvDailyAdjTimeSeries data)
        {
            Console.WriteLine($"saving {data.MetaData.Symbol}:{data.MetaData.Function.Name} - {data.MetaData.LastRefreshed}");
            repository.Upsert(data);
        }
    }
}
