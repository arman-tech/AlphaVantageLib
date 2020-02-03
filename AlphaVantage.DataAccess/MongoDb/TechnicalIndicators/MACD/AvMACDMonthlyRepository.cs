using AlphaVantage.DataAccess.Interfaces;
using MongoDB.Driver;
using System;
using System.Linq.Expressions;
using AlphaVantage.DataAccess.EventArguments;
using AlphaVantage.Common.Models.TechnicalIndicators.MACD;

namespace AlphaVantage.DataAccess.MongoDb.TechnicalIndicators.MACD
{
    public class AvMACDMonthlyRepository : AvMonthlyRepositoryAbs<AvMACD, AvMACDMetaData, AvMACDBlock>
    {
        public override event EventHandler<RepositoryArgs> BeginExecute;
        public override event EventHandler<RepositoryArgs> EndExecute;
        public override event EventHandler<RepositoryArgs> Info;
        
        public AvMACDMonthlyRepository(IContext<IMongoClient, IMongoDatabase> context,
                                         string dbName,
                                         string collectionName) : base(context: context, dbName: dbName, collectionName: collectionName)

        {
        }

        public override Expression<Func<AvMACD, bool>> CompareExpression(AvMACD rhs)
        {
            return ts =>
                    ts.MetaData.Function == rhs.MetaData.Function &&
                    ts.MetaData.Symbol == rhs.MetaData.Symbol &&
                    ts.MetaData.Interval == rhs.MetaData.Interval &&
                    ts.MetaData.SeriesType == rhs.MetaData.SeriesType &&
                    ts.MetaData.FastPeriod == rhs.MetaData.FastPeriod &&
                    ts.MetaData.SlowPeriod == rhs.MetaData.SlowPeriod &&
                    ts.MetaData.SignalPeriod == rhs.MetaData.SignalPeriod;
        }

    }
}
