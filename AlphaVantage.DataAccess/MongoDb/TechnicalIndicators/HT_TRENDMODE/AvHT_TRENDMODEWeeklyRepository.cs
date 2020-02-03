using AlphaVantage.DataAccess.Interfaces;
using MongoDB.Driver;
using System;
using System.Linq.Expressions;
using AlphaVantage.DataAccess.EventArguments;
using AlphaVantage.Common.Models.TechnicalIndicators.HT_TRENDMODE;

namespace AlphaVantage.DataAccess.MongoDb.TechnicalIndicators.HT_TRENDMODE
{
    public class AvHT_TRENDMODEWeeklyRepository : AvWeeklyRepositoryAbs<AvHT_TRENDMODE, AvHT_TRENDMODEMetaData, AvHT_TRENDMODEBlock>
    {
        public override event EventHandler<RepositoryArgs> BeginExecute;
        public override event EventHandler<RepositoryArgs> EndExecute;
        public override event EventHandler<RepositoryArgs> Info;
        
        public AvHT_TRENDMODEWeeklyRepository(IContext<IMongoClient, IMongoDatabase> context,
                                         string dbName,
                                         string collectionName) : base(context: context, dbName: dbName, collectionName: collectionName)

        {
        }

        public override Expression<Func<AvHT_TRENDMODE, bool>> CompareExpression(AvHT_TRENDMODE rhs)
        {
            return ts =>
                    ts.MetaData.Function == rhs.MetaData.Function &&
                    ts.MetaData.Symbol == rhs.MetaData.Symbol &&
                    ts.MetaData.Interval == rhs.MetaData.Interval &&
                    ts.MetaData.SeriesType == rhs.MetaData.SeriesType;
        }

    }
}
