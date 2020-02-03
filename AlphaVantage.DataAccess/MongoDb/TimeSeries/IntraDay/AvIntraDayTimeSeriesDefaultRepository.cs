using AlphaVantage.Common.Models.TimeSeries.IntraDay;
using AlphaVantage.DataAccess.Base;
using AlphaVantage.DataAccess.Interfaces;
using MongoDB.Driver;
using System;
using System.Linq.Expressions;

namespace AlphaVantage.DataAccess.MongoDb.TimeSeries.IntraDay
{
    public class AvIntraDayTimeSeriesDefaultRepository : AvDefaultRepositoryAbs<AvIntraDayTimeSeries, AvIntraDayTimeSeriesMetaData, AvIntraDayTimeSeriesBlock>
    {
        public AvIntraDayTimeSeriesDefaultRepository(IContext<IMongoClient, IMongoDatabase> context,
                              string dbName,
                              string collectionName) : base(context: context, dbName: dbName, collectionName: collectionName)
        {
        }

        public override Expression<Func<AvIntraDayTimeSeries, bool>> CompareExpression(AvIntraDayTimeSeries rhs)
        {
            return ts =>
                ts.MetaData.Function == rhs.MetaData.Function &&
                ts.MetaData.Symbol == rhs.MetaData.Symbol &&
                ts.MetaData.Interval == rhs.MetaData.Interval;
        }
    }
}
