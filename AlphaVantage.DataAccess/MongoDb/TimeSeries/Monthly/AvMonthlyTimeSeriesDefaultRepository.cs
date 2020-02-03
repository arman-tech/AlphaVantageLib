using AlphaVantage.Common.Models.TimeSeries.Monthly;
using AlphaVantage.DataAccess.Interfaces;
using MongoDB.Driver;
using System;
using System.Linq.Expressions;

namespace AlphaVantage.DataAccess.MongoDb.TimeSeries.Monthly
{
    public class AvMonthlyTimeSeriesDefaultRepository : AvMonthlyRepositoryAbs<AvMonthlyTimeSeries, AvMonthlyTimeSeriesMetaData, AvMonthlyTimeSeriesBlock>
    {
        public AvMonthlyTimeSeriesDefaultRepository(IContext<IMongoClient, IMongoDatabase> context,
              string dbName,
              string collectionName) : base(context: context, dbName: dbName, collectionName: collectionName)
        {
        }

        public override Expression<Func<AvMonthlyTimeSeries, bool>> CompareExpression(AvMonthlyTimeSeries rhs)
        {
            return ts =>
                ts.MetaData.Function == rhs.MetaData.Function &&
                ts.MetaData.Symbol == rhs.MetaData.Symbol;
        }

    }
}
