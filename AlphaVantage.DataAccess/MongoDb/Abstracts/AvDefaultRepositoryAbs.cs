using AlphaVantage.DataAccess.Base;
using AlphaVantage.DataAccess.Interfaces;
using MongoDB.Driver;
using System;
using System.Linq;
using AlphaVantage.DataAccess.EventArguments;
using AlphaVantage.Common.Models;

namespace AlphaVantage.DataAccess.MongoDb
{
    public abstract class AvDefaultRepositoryAbs<T, K, X> : MongoRepositoryBaseAbs<T> where T : class, IAvSeriesObj<T, K, X>, new()   // T > IAvSeriesObj 
                                                                                    where K : IAvMetaData<K>                        // K > IAvMetaData
                                                                                    where X : IAvBlock<X>                           // X > IAvBlock

    {
        public override event EventHandler<RepositoryArgs> BeginExecute;
        public override event EventHandler<RepositoryArgs> EndExecute;
        public override event EventHandler<RepositoryArgs> Info;
        
        public AvDefaultRepositoryAbs(IContext<IMongoClient, IMongoDatabase> context,
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
                                        $"Beginning [UPSERT] for BollingerBand Default: [{item.MetaData.Symbol}:{item.MetaData.Function.Name} - ({item.MetaData.LastRefreshed.ToString(OracleDateFormat)})]"));

            var dbRecord = Single(CompareExpression(item));

            if (dbRecord == null)
            {
                this.Info?.Invoke(this, new RepositoryArgs(this.Guid, "Calling [Saving All] for Default."));
                SaveAll(item);
            }
            else
            {
                this.Info?.Invoke(this, new RepositoryArgs(this.Guid, "Calling [Saving Delta] for Default."));
                SaveDelta(item, dbRecord);
            }

            this.EndExecute?.Invoke(this, new RepositoryArgs(this.Guid, "Ending [UPSERT] for Default."));
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
            var latestDay = oldRecord.TimeSeries.Max(d => d.TimeStamp);

            // grab all time series from timeSeries that are new.
            var newBlocks = newRecord.TimeSeries.Where(n => n.TimeStamp > latestDay).OrderBy(o => o.TimeStamp).ToList();

            // if there are no new blocks to update with then end the this process.
            if (!newBlocks.Any())
            {
                this.Info?.Invoke(this, new RepositoryArgs(Guid, "(1) Nothing needs to be saved for Default."));

                return;
            }

            var diffTimeSeries = oldRecord.MemberClone();
            diffTimeSeries.MetaData = oldRecord.MetaData.MemberClone();
            diffTimeSeries.TimeSeries = newBlocks;

            // Upsert the difference.
            UpdateOrAddDefault(diffTimeSeries);
        }


        private void UpdateOrAddDefault(T item)
        {
            var filterBollingerBand = Builders<T>.Filter.Where(CompareExpression(item));

            var updateBollingerBand = Builders<T>.Update
                                        .AddToSetEach(items => items.TimeSeries, item.TimeSeries)
                                        .Set(o => o.MetaData.LastRefreshed, DateTime.UtcNow);

            this.Collection.UpdateOne(filterBollingerBand, updateBollingerBand, new UpdateOptions() { IsUpsert = true });
        }
    }
}
