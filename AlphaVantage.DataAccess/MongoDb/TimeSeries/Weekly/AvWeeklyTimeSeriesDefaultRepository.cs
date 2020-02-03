using AlphaVantage.Common.Models.TimeSeries.Weekly;
using AlphaVantage.DataAccess.Interfaces;
using MongoDB.Driver;
using System;
using System.Linq.Expressions;

namespace AlphaVantage.DataAccess.MongoDb.TimeSeries.Weekly
{
    public class AvWeeklyTimeSeriesDefaultRepository : AvWeeklyRepositoryAbs<AvWeeklyTimeSeries, AvWeeklyTimeSeriesMetaData, AvWeeklyTimeSeriesBlock>
    {
        public AvWeeklyTimeSeriesDefaultRepository(IContext<IMongoClient, IMongoDatabase> context,
                      string dbName,
                      string collectionName) : base(context: context, dbName: dbName, collectionName: collectionName)
        {
        }

        public override Expression<Func<AvWeeklyTimeSeries, bool>> CompareExpression(AvWeeklyTimeSeries rhs)
        {
            return ts =>
                ts.MetaData.Function == rhs.MetaData.Function &&
                ts.MetaData.Symbol == rhs.MetaData.Symbol;
        }

    }
}
