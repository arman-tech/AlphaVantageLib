using AlphaVantage.Common.Common;
using AlphaVantage.DataAccess.Base;
using AlphaVantage.DataAccess.Interfaces;
using MongoDB.Driver;
using System;
using System.Linq;
using AlphaVantage.DataAccess.Exceptions;
using System.Collections.Generic;
using AlphaVantage.DataAccess.EventArguments;
using AlphaVantage.Common.Models;

namespace AlphaVantage.DataAccess.MongoDb
{
    public abstract class AvWeeklyRepositoryAbs<T, K, X> : MongoRepositoryBaseAbs<T>   where T : class, IAvSeriesObj<T, K, X>, new()   // T > IAvSeriesObj 
                                                                                    where K : IAvMetaData<K>                        // K > IAvMetaData
                                                                                    where X : IAvBlock<X>                           // X > IAvBlock

    {
        public override event EventHandler<RepositoryArgs> BeginExecute;
        public override event EventHandler<RepositoryArgs> EndExecute;
        public override event EventHandler<RepositoryArgs> Info;


        public AvWeeklyRepositoryAbs(IContext<IMongoClient, IMongoDatabase> context,
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

            if (dbRecord == null)
            {
                this.Info?.Invoke(this, new RepositoryArgs(this.Guid, "Calling [Saving All] for Weekly."));
                SaveAll(item);
            }
            else
            {
                this.BeginExecute?.Invoke(this, new RepositoryArgs(this.Guid, "Calling [Saving Delta] for Weekly."));
                SaveDelta(item, dbRecord);
            }

            this.BeginExecute?.Invoke(this, new RepositoryArgs(this.Guid, "Ending [UPSERT] for Weekly"));
        }

        protected void SaveAll(T bollingerBand)
        {
            // sanity check
            if (bollingerBand == null)
            {
                throw new ArgumentNullException(nameof(SaveAll));
            }

            // order time series by date
            bollingerBand.TimeSeries = bollingerBand.TimeSeries.OrderBy(o => o.TimeStamp).ToList();

            Add(bollingerBand);
        }

        protected void SaveDelta(T newRecord, T oldRecord)
        {
            // sanity check
            if (newRecord == null || oldRecord == null)
            {
                throw new ArgumentNullException(nameof(SaveDelta));
            }

            // Determine the latest time series blocks on the database.
            var latestDataPoint = oldRecord.TimeSeries.Max(d => d.TimeStamp);

            // The latest data point is the cumulative prices and volume information for the week 
            // (or partial week) that contains the current trading day, updated realtime.
            // https://www.alphavantage.co/documentation/#weekly
            // Based on the above statement, everytime we run a save delta we must grab 
            // information on the latest day in the database.

            // grab all time series from timeSeries that are new, as well as the latest month found in database.
            var newBlocks = newRecord.TimeSeries.Where(
                                n => n.TimeStamp.Date >= latestDataPoint.Date)
                                .OrderBy(o => o.TimeStamp).ToList();

            // if there are no new blocks to update with then end the this process.
            if (!newBlocks.Any())
            {
                this.Info?.Invoke(this, new RepositoryArgs(this.Guid, "(1) Nothing needs to be saved for Weekly."));
                return;
            }

            // we must ensure the first item in new blocks is the last day found in our database.
            if (newBlocks[ExistingElementIndex].TimeStamp.Year != latestDataPoint.Year
                || newBlocks[ExistingElementIndex].TimeStamp.GetIso8601WeekOfYear() != latestDataPoint.GetIso8601WeekOfYear())
            {
                throw new AvDataPointDoesNotExistException(nameof(SaveDelta));
            }

            // if everything is exactly the same (day, time, etc.) then we shouldn't capture it again
            if (newBlocks[ExistingElementIndex].TimeStamp.Date == latestDataPoint.Date &&
                newBlocks.Count == 1)
            {
                this.Info?.Invoke(this, new RepositoryArgs(this.Guid, "(2) Nothing needs to be saved for Weekly."));
                return;
            }

            var diffTimeSeries = oldRecord.MemberClone();
            diffTimeSeries.MetaData = oldRecord.MetaData.MemberClone();
            diffTimeSeries.TimeSeries = newBlocks;

            // Upsert the difference.
            UpdateOrAddWeekly(diffTimeSeries);
        }


        private void UpdateOrAddWeekly(T item)
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

            var earliestWeek = item.TimeSeries.Min(d => d.TimeStamp);
            var blockToUpdate = item.TimeSeries.First(i => i.TimeStamp == earliestWeek);

            // Get the Friday date for the week before.  Since in financial terms Friday is the last day of the week.
            // Get the Monday of the week, then reduce by one day.  Then get the Monday of the week before, 
            // and add 4 days to get to Friday of the week before.
            var fridayOfWeekBeforeEarliestWeek = blockToUpdate.TimeStamp.FirstDateOfWeekISO8601().AddDays(-1)
                                                    .FirstDateOfWeekISO8601().AddDays(4);

            var upsertWeeklyObj = AddWeeklyRecords(item, earliestWeek);

            var bulkOps = new List<UpdateOneModel<T>>();

            if (null != upsertWeeklyObj)
            {
                bulkOps.Add(upsertWeeklyObj);
            }

            var shouldUpdate = item.TimeSeries.Any(dp => dp.TimeStamp <= blockToUpdate.TimeStamp &&
                        dp.TimeStamp > fridayOfWeekBeforeEarliestWeek);

            if (shouldUpdate)
            {
                this.Info?.Invoke(this, new RepositoryArgs(this.Guid, $"Updating time line {earliestWeek}."));

                var updateObj = UpdateLatestWeek(fridayOfWeekBeforeEarliestWeek, blockToUpdate, item);
                bulkOps.Add(updateObj);
            }

            if (bulkOps.Any())
            {
                var result = this.Collection.BulkWrite(bulkOps);
            }
        }


        private UpdateOneModel<T> UpdateLatestWeek(DateTime weekBefore, X blockToUpdate,
            T item)
        {
            var filter = Builders<T>.Filter.And(
                Builders<T>.Filter.Where(CompareExpression(item)),
                Builders<T>.Filter.ElemMatch(e =>
                            e.TimeSeries,
                            dp => dp.TimeStamp < blockToUpdate.TimeStamp &&
                            dp.TimeStamp > weekBefore)
                );

            var updateObj = Builders<T>.Update
                .Set(o => o.TimeSeries[-1], blockToUpdate)
                .Set(o => o.MetaData.LastRefreshed, DateTime.UtcNow);

            return new UpdateOneModel<T>(filter, updateObj) { IsUpsert = true };
        }

        private UpdateOneModel<T> AddWeeklyRecords(T item, DateTime earliestWeek)
        {
            // get all data points that are new and don't need to be updated.
            var newDataPoints = (from ts in item.TimeSeries
                                 where ts.TimeStamp > earliestWeek
                                 select ts);

            if (!newDataPoints.Any()) { return null; }

            var earliestTimeLine = newDataPoints.Min(p => p.TimeStamp);
            var latestTimeLine = newDataPoints.Max(p => p.TimeStamp);

            this.Info?.Invoke(this, new RepositoryArgs(this.Guid, $"Adding time line {earliestTimeLine} to {latestTimeLine}."));

            // 2. update metadata for last refreshed
            // 3. add to set each time series item excluding the earliest time series data point.
            var updateWeeklyObj = Builders<T>.Update
                                                .AddToSetEach(items => items.TimeSeries, newDataPoints)
                                                .Set(o => o.MetaData.LastRefreshed, DateTime.UtcNow);

            var filterAvWeekly = Builders<T>.Filter.Where(CompareExpression(item));

            return new UpdateOneModel<T>(filterAvWeekly, updateWeeklyObj)
            { IsUpsert = true };
        }

    }
}
