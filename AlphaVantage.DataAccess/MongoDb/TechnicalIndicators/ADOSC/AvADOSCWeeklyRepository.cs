using AlphaVantage.DataAccess.Interfaces;
using MongoDB.Driver;
using System;
using System.Linq.Expressions;
using AlphaVantage.DataAccess.EventArguments;
using AlphaVantage.Common.Models.TechnicalIndicators.ADOSC;

namespace AlphaVantage.DataAccess.MongoDb.TechnicalIndicators.ADOSC
{
    public class AvADOSCWeeklyRepository : AvWeeklyRepositoryAbs<AvADOSC, AvADOSCMetaData, AvADOSCBlock>
    {
        public override event EventHandler<RepositoryArgs> BeginExecute;
        public override event EventHandler<RepositoryArgs> EndExecute;
        public override event EventHandler<RepositoryArgs> Info;
        
        public AvADOSCWeeklyRepository(IContext<IMongoClient, IMongoDatabase> context,
                                         string dbName,
                                         string collectionName) : base(context: context, dbName: dbName, collectionName: collectionName)

        {
        }

        public override Expression<Func<AvADOSC, bool>> CompareExpression(AvADOSC rhs)
        {
            return ts =>
                    ts.MetaData.Function == rhs.MetaData.Function &&
                    ts.MetaData.Symbol == rhs.MetaData.Symbol &&
                    ts.MetaData.Interval == rhs.MetaData.Interval &&
                    ts.MetaData.FastKPeriod == rhs.MetaData.FastKPeriod &&
                    ts.MetaData.SlowKPeriod == rhs.MetaData.SlowKPeriod;
        }

    }
}
