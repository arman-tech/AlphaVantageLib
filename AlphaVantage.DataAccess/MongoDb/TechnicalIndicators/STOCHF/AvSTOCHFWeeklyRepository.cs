using AlphaVantage.DataAccess.Interfaces;
using MongoDB.Driver;
using System;
using System.Linq.Expressions;
using AlphaVantage.DataAccess.EventArguments;
using AlphaVantage.Common.Models.TechnicalIndicators.STOCHF;

namespace AlphaVantage.DataAccess.MongoDb.TechnicalIndicators.STOCHF
{
    public class AvSTOCHFWeeklyRepository : AvWeeklyRepositoryAbs<AvSTOCHF, AvSTOCHFMetaData, AvSTOCHFBlock>
    {
        public override event EventHandler<RepositoryArgs> BeginExecute;
        public override event EventHandler<RepositoryArgs> EndExecute;
        public override event EventHandler<RepositoryArgs> Info;
        
        public AvSTOCHFWeeklyRepository(IContext<IMongoClient, IMongoDatabase> context,
                                         string dbName,
                                         string collectionName) : base(context: context, dbName: dbName, collectionName: collectionName)

        {
        }

        public override Expression<Func<AvSTOCHF, bool>> CompareExpression(AvSTOCHF rhs)
        {
            return ts =>
                    ts.MetaData.Function == rhs.MetaData.Function &&
                    ts.MetaData.Symbol == rhs.MetaData.Symbol &&
                    ts.MetaData.Interval == rhs.MetaData.Interval &&
                    ts.MetaData.FastKPeriod == rhs.MetaData.FastKPeriod &&
                    ts.MetaData.FastDPeriod == rhs.MetaData.FastDPeriod &&
                    ts.MetaData.FastDMAType == rhs.MetaData.FastDMAType;
        }

    }
}
