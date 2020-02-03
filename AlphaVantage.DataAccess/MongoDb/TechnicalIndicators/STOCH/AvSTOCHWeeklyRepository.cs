using AlphaVantage.DataAccess.Interfaces;
using MongoDB.Driver;
using System;
using System.Linq.Expressions;
using AlphaVantage.DataAccess.EventArguments;
using AlphaVantage.Common.Models.TechnicalIndicators.STOCH;

namespace AlphaVantage.DataAccess.MongoDb.TechnicalIndicators.STOCH
{
    public class AvSTOCHWeeklyRepository : AvWeeklyRepositoryAbs<AvSTOCH, AvSTOCHMetaData, AvSTOCHBlock>
    {
        public override event EventHandler<RepositoryArgs> BeginExecute;
        public override event EventHandler<RepositoryArgs> EndExecute;
        public override event EventHandler<RepositoryArgs> Info;
        
        public AvSTOCHWeeklyRepository(IContext<IMongoClient, IMongoDatabase> context,
                                         string dbName,
                                         string collectionName) : base(context: context, dbName: dbName, collectionName: collectionName)

        {
        }

        public override Expression<Func<AvSTOCH, bool>> CompareExpression(AvSTOCH rhs)
        {
            return ts =>
                    ts.MetaData.Function == rhs.MetaData.Function &&
                    ts.MetaData.Symbol == rhs.MetaData.Symbol &&
                    ts.MetaData.Interval == rhs.MetaData.Interval &&
                    ts.MetaData.FastKPeriod == rhs.MetaData.FastKPeriod &&
                    ts.MetaData.SlowKPeriod == rhs.MetaData.SlowKPeriod &&
                    ts.MetaData.SlowKMAType == rhs.MetaData.SlowKMAType &&
                    ts.MetaData.SlowDPeriod == rhs.MetaData.SlowDPeriod &&
                    ts.MetaData.SlowDMAType == rhs.MetaData.SlowDMAType;
        }

    }
}
