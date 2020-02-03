using AlphaVantage.Common.Models.TimeSeries.Daily;
using AlphaVantage.DataAccess.Interfaces;
using MongoDB.Driver;
using System;
using System.Linq.Expressions;

namespace AlphaVantage.DataAccess.MongoDb.TimeSeries.Daily
{
    public class AvDailyTimeSeriesDefaultRepository : AvDefaultRepositoryAbs<AvDailyTimeSeries, AvDailyTimeSeriesMetaData, AvDailyTimeSeriesBlock>
    {
        public AvDailyTimeSeriesDefaultRepository(IContext<IMongoClient, IMongoDatabase> context,
                                      string dbName,
                                      string collectionName) : base(context: context, dbName: dbName, collectionName: collectionName)
        {
        }

        public override Expression<Func<AvDailyTimeSeries, bool>> CompareExpression(AvDailyTimeSeries rhs)
        {
            return ts =>
                ts.MetaData.Function == rhs.MetaData.Function &&
                ts.MetaData.Symbol == rhs.MetaData.Symbol;
        }
    }
}
