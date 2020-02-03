using AlphaVantage.Common.Models.TimeSeries.DailyAdjusted;
using AlphaVantage.DataAccess.Interfaces;
using MongoDB.Driver;
using System;
using System.Linq.Expressions;

namespace AlphaVantage.DataAccess.MongoDb.TimeSeries.DailyAdjusted
{
    public class AvDailyAdjTimeSeriesDefaultRepository : AvDefaultRepositoryAbs<AvDailyAdjTimeSeries, AvDailyAdjTimeSeriesMetaData, AvDailyAdjTimeSeriesBlock>
    {
        public AvDailyAdjTimeSeriesDefaultRepository(IContext<IMongoClient, IMongoDatabase> context,
                                              string dbName,
                                              string collectionName) : base(context: context, dbName: dbName, collectionName: collectionName)
        {
        }

        public override Expression<Func<AvDailyAdjTimeSeries, bool>> CompareExpression(AvDailyAdjTimeSeries rhs)
        {
            return ts =>
                ts.MetaData.Function == rhs.MetaData.Function &&
                ts.MetaData.Symbol == rhs.MetaData.Symbol;
        }
    }
}
