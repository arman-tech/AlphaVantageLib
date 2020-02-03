using AlphaVantage.Common.Models.TechnicalIndicators.BBANDS;
using AlphaVantage.DataAccess.Interfaces;
using MongoDB.Driver;
using System;
using System.Linq.Expressions;
using AlphaVantage.DataAccess.EventArguments;
using AlphaVantage.Common.Models.TechnicalIndicators.PPO;

namespace AlphaVantage.DataAccess.MongoDb.TechnicalIndicators.PPO
{
    public class AvPPOWeeklyRepository : AvWeeklyRepositoryAbs<AvPPO, AvPPOMetaData, AvPPOBlock>
    {
        public override event EventHandler<RepositoryArgs> BeginExecute;
        public override event EventHandler<RepositoryArgs> EndExecute;
        public override event EventHandler<RepositoryArgs> Info;
        
        public AvPPOWeeklyRepository(IContext<IMongoClient, IMongoDatabase> context,
                                         string dbName,
                                         string collectionName) : base(context: context, dbName: dbName, collectionName: collectionName)

        {
        }

        public override Expression<Func<AvPPO, bool>> CompareExpression(AvPPO rhs)
        {
            return ts =>
                    ts.MetaData.Function == rhs.MetaData.Function &&
                    ts.MetaData.Symbol == rhs.MetaData.Symbol &&
                    ts.MetaData.Interval == rhs.MetaData.Interval &&
                    ts.MetaData.SeriesType == rhs.MetaData.SeriesType &&
                    ts.MetaData.FastPeriod == rhs.MetaData.FastPeriod &&
                    ts.MetaData.SlowPeriod == rhs.MetaData.SlowPeriod &&
                    ts.MetaData.MAType == rhs.MetaData.MAType;
        }

    }
}
