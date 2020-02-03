using AlphaVantage.Common.Models.TimeSeries.WeeklyAdjusted;
using AlphaVantage.DataAccess.Interfaces;
using MongoDB.Driver;
using System;
using System.Linq.Expressions;

namespace AlphaVantage.DataAccess.MongoDb.TimeSeries.WeeklyAdjusted
{
    public class AvWeeklyAdjTimeSeriesDefaultRepository : AvWeeklyRepositoryAbs<AvWeeklyAdjTimeSeries, AvWeeklyAdjTimeSeriesMetaData, AvWeeklyAdjTimeSeriesBlock>
    {
        public AvWeeklyAdjTimeSeriesDefaultRepository(IContext<IMongoClient, IMongoDatabase> context,
                              string dbName,
                              string collectionName) : base(context: context, dbName: dbName, collectionName: collectionName)
        {
        }

        public override Expression<Func<AvWeeklyAdjTimeSeries, bool>> CompareExpression(AvWeeklyAdjTimeSeries rhs)
        {
            return ts =>
                ts.MetaData.Function == rhs.MetaData.Function &&
                ts.MetaData.Symbol == rhs.MetaData.Symbol;
        }

    }
}
