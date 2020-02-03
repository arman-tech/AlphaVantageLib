using AlphaVantage.Common.Models.TechnicalIndicators.RSI;
using AlphaVantage.DataAccess.Interfaces;
using MongoDB.Driver;
using System;
using System.Linq.Expressions;

namespace AlphaVantage.DataAccess.MongoDb.TechnicalIndicators.RSI
{
    public class AvRSIDefaultRepository : AvDefaultRepositoryAbs<AvRSI, AvRSIMetaData, AvRSIBlock>
    {
        public AvRSIDefaultRepository(IContext<IMongoClient, IMongoDatabase> context,
                                string dbName,
                                string collectionName) : base(context: context, dbName: dbName, collectionName: collectionName)

        {
        }
        public override Expression<Func<AvRSI, bool>> CompareExpression(AvRSI rhs)
        {
            return ts =>
                    ts.MetaData.Function == rhs.MetaData.Function &&
                    ts.MetaData.Symbol == rhs.MetaData.Symbol &&
                    ts.MetaData.Interval == rhs.MetaData.Interval &&
                    ts.MetaData.TimePeriod == rhs.MetaData.TimePeriod &&
                    ts.MetaData.SeriesType == rhs.MetaData.SeriesType;
        }
    }
}
