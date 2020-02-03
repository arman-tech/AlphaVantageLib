using System;
using System.Collections.Generic;
using System.Linq;
using AlphaVantage.Common.Models;
using AlphaVantage.DataAccess.Base;
using AlphaVantage.DataAccess.EventArguments;
using AlphaVantage.DataAccess.Exceptions;
using AlphaVantage.DataAccess.Interfaces;
using MongoDB.Driver;

namespace AlphaVantage.DataAccess.MongoDb
{
    public abstract class AvMonthlyRepositoryAbs<T, K, X> : MongoRepositoryBaseAbs<T> where T : class, IAvSeriesObj<T, K, X>, new()   // T > IAvSeriesObj 
                                                                                    where K : IAvMetaData<K>                        // K > IAvMetaData
                                                                                    where X : IAvBlock<X>                           // X > IAvBlock

    {
        public override event EventHandler<RepositoryArgs> BeginExecute;
        public override event EventHandler<RepositoryArgs> EndExecute;
        public override event EventHandler<RepositoryArgs> Info;

        public AvMonthlyRepositoryAbs(IContext<IMongoClient, IMongoDatabase> context,
                                         string dbName,
                                         string collectionName) : base(context: context, dbName: dbName, collectionName: collectionName)

        {
        }

        public override bool DoesRecordExist(T record)
        {
            return this.Collection.Find(CompareExpression(record)).Any();
        }

        public override void Upsert(T item)
        {
            this.BeginExecute?.Invoke(this, new RepositoryArgs(this.Guid,
                $"Beginning [UPSERT]: [{item.MetaData.Symbol}:{item.MetaData.Function.Name} - ({item.MetaData.LastRefreshed.ToString(OracleDateFormat)})]"));

            var dbRecord = Single(CompareExpression(item));

            if(dbRecord == null)
            {
                this.Info?.Invoke(this, new RepositoryArgs(this.Guid, "Calling [Saving All] for Monthly."));
                SaveAll(item);
            }
            else
            {
                this.BeginExecute?.Invoke(this, new RepositoryArgs(this.Guid, "Calling [Saving Delta] for Monthly."));
                SaveDelta(item, dbRecord);
            }

            this.BeginExecute?.Invoke(this, new RepositoryArgs(this.Guid, "Ending [UPSERT] for Monthly"));
        }

        protected void SaveAll(T item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(SaveAll));
            }

            item.TimeSeries = item.TimeSeries.OrderBy(o => o.TimeStamp).ToList();

            Add(item);
        }

        protected void SaveDelta(T newRecord, T oldRecord)
        {
            if(newRecord == null || oldRecord == null)
            {
                throw new ArgumentNullException(nameof(SaveDelta));
            }

            // Determine the latest time series blocks on the database.
            var latestDataPoint = oldRecord.TimeSeries.Max(d => d.TimeStamp);

            // grab all time series from timeSeries that are new, as well as the latest month found in database.
            var newBlocks = newRecord.TimeSeries.Where(
                                n => n.TimeStamp.Year >= latestDataPoint.Year && n.TimeStamp.Month >= latestDataPoint.Month)
                                .OrderBy(o => o.TimeStamp).ToList();

            // if there are no new blocks to update with then end the this process.
            if (!newBlocks.Any())
            {
                return;
            }

            // we must ensure the first item in new blocks is the last day found in our database.
            if (newBlocks[ExistingElementIndex].TimeStamp.Year != latestDataPoint.Year
                || newBlocks[ExistingElementIndex].TimeStamp.Month != latestDataPoint.Month)
            {
                throw new AvDataPointDoesNotExistException(nameof(SaveDelta));
            }

            // if everything is exactly the same (day, time, etc.) then we shouldn't capture it again
            if (newBlocks[ExistingElementIndex].TimeStamp.Date == latestDataPoint.Date &&
                newBlocks.Count == 1)
            {
                return;
            }

            var diffTimeSeries = oldRecord.MemberClone();
            diffTimeSeries.MetaData = oldRecord.MetaData.MemberClone();
            diffTimeSeries.TimeSeries = newBlocks;

            // Upsert the difference.
            UpdateOrAddMonth(diffTimeSeries);
        }

        private void UpdateOrAddMonth(T item)
        {
            // sanity check
            if (item == null)
            {
                throw new ArgumentNullException(nameof(Upsert));
            }

            if (item.TimeSeries != null && !item.TimeSeries.Any())
            {
                throw new AvTimeSeriesEmptyException(nameof(Upsert));
            }

            // 1. Determine the earliest time series blocks in the 'item'
            // parameter TimeSeries set.
            var earliestMonth = item.TimeSeries.Min(d => d.TimeStamp);
            var blockToUpdate = item.TimeSeries.First(i => i.TimeStamp == earliestMonth);
            var monthBeforeEarliestMonth = blockToUpdate.TimeStamp.AddMonths(-1);

            // ADD the missing months.
            var upsertMonthlyTimeSeriesObj = AddMonthly(item, earliestMonth);

            var bulkOps = new List<UpdateOneModel<T>>();

            if (null != upsertMonthlyTimeSeriesObj)
            {
                bulkOps.Add(upsertMonthlyTimeSeriesObj);
            }

            var shouldUpdate = item.TimeSeries.Any(dp => dp.TimeStamp <= blockToUpdate.TimeStamp &&
            dp.TimeStamp > monthBeforeEarliestMonth);

            if (shouldUpdate)
            {
                var upsertTimeSeriesObj = UpdateLatestMonth(monthBeforeEarliestMonth, blockToUpdate, item);
                bulkOps.Add(upsertTimeSeriesObj);
            }

            if (bulkOps.Any()) { this.Collection.BulkWrite(bulkOps); }

        }

        private UpdateOneModel<T> UpdateLatestMonth(DateTime monthBefore,
            X blockToUpdate, T item)
        {
            // 4. in another operation, update the earliest time series data point 
            // NOTE: since Mongo has an issue with using .NET DateTime comparison of Month, 
            // we have to use this approach to find the element in TimeSeries.
            var filterTimeSeries = Builders<T>.Filter.And(
                Builders<T>.Filter.Where(CompareExpression(item)),
                Builders<T>.Filter.ElemMatch(e =>
                    e.TimeSeries,
                    dp => dp.TimeStamp < blockToUpdate.TimeStamp &&
                          dp.TimeStamp > monthBefore)
            );

            var updateTimeSeriesObj = Builders<T>.Update
                //equivalent to .Set("TimeSeries.$", blockToUpdate); '.$' is positional operator for Mongo
                .Set(o => o.TimeSeries[-1], blockToUpdate);

            return new UpdateOneModel<T>(filterTimeSeries, updateTimeSeriesObj)
            { IsUpsert = true };
        }

        private UpdateOneModel<T> AddMonthly(T item, DateTime earliestMonth)
        {
            // get all data points that are new and don't need to be updated.
            var newDataPoints = (from ts in item.TimeSeries
                                 where ts.TimeStamp > earliestMonth
                                 select ts);

            if (newDataPoints.Count() == 0) { return null; }

            // 2. update metadata for last refreshed
            // 3. add to set each time series item excluding the earliest time series data point.
            var updateMonthlyTimeSeriesObj = Builders<T>.Update
                                                .AddToSetEach(items => items.TimeSeries, newDataPoints)
                                                .Set(o => o.MetaData.LastRefreshed, DateTime.UtcNow);

            var filterAvMonthlyTimeSeries = Builders<T>.Filter.Where(CompareExpression(item));

            return new UpdateOneModel<T>(filterAvMonthlyTimeSeries, updateMonthlyTimeSeriesObj)
            { IsUpsert = true };
        }
    }
}
