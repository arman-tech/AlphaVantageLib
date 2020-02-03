using AlphaVantage.Common.Models.TimeSeries.MonthlyAdjusted;
using AlphaVantage.DataAccess.Interfaces;
using MongoDB.Driver;
using System;
using System.Linq.Expressions;

namespace AlphaVantage.DataAccess.MongoDb.TimeSeries.MonthlyAdjusted
{
    public class AvMonthlyAdjTimeSeriesDefaultRepository : AvMonthlyRepositoryAbs<AvMonthlyAdjTimeSeries, AvMonthlyAdjTimeSeriesMetaData, AvMonthlyAdjTimeSeriesBlock>
    {
        public AvMonthlyAdjTimeSeriesDefaultRepository(IContext<IMongoClient, IMongoDatabase> context,
                      string dbName,
                      string collectionName) : base(context: context, dbName: dbName, collectionName: collectionName)
        {
        }

        public override Expression<Func<AvMonthlyAdjTimeSeries, bool>> CompareExpression(AvMonthlyAdjTimeSeries rhs)
        {
            return ts =>
                ts.MetaData.Function == rhs.MetaData.Function &&
                ts.MetaData.Symbol == rhs.MetaData.Symbol;
        }
    }
}
