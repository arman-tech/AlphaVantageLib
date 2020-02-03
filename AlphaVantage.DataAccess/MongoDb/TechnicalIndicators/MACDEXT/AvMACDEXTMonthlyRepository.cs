using AlphaVantage.DataAccess.Interfaces;
using MongoDB.Driver;
using System;
using System.Linq.Expressions;
using AlphaVantage.DataAccess.EventArguments;
using AlphaVantage.Common.Models.TechnicalIndicators.MACDEXT;

namespace AlphaVantage.DataAccess.MongoDb.TechnicalIndicators.MACDEXT
{
    public class AvMACDEXTMonthlyRepository : AvMonthlyRepositoryAbs<AvMACDEXT, AvMACDEXTMetaData, AvMACDEXTBlock>
    {
        public override event EventHandler<RepositoryArgs> BeginExecute;
        public override event EventHandler<RepositoryArgs> EndExecute;
        public override event EventHandler<RepositoryArgs> Info;
        
        public AvMACDEXTMonthlyRepository(IContext<IMongoClient, IMongoDatabase> context,
                                         string dbName,
                                         string collectionName) : base(context: context, dbName: dbName, collectionName: collectionName)

        {
        }

        public override Expression<Func<AvMACDEXT, bool>> CompareExpression(AvMACDEXT rhs)
        {
            return ts =>
                    ts.MetaData.Function == rhs.MetaData.Function &&
                    ts.MetaData.Symbol == rhs.MetaData.Symbol &&
                    ts.MetaData.Interval == rhs.MetaData.Interval &&
                    ts.MetaData.SeriesType == rhs.MetaData.SeriesType &&
                    ts.MetaData.FastPeriod == rhs.MetaData.FastPeriod &&
                    ts.MetaData.SlowPeriod == rhs.MetaData.SlowPeriod &&
                    ts.MetaData.SignalPeriod == rhs.MetaData.SignalPeriod &&
                    ts.MetaData.FastMAType == rhs.MetaData.FastMAType &&
                    ts.MetaData.SlowMAType == rhs.MetaData.SlowMAType &&
                    ts.MetaData.SignalMAType == rhs.MetaData.SignalMAType;
        }

    }
}
