using AlphaVantage.DataAccess.Interfaces;
using MongoDB.Driver;
using System;
using System.Linq.Expressions;
using AlphaVantage.DataAccess.EventArguments;
using AlphaVantage.Common.Models.TechnicalIndicators.HT_PHASOR;

namespace AlphaVantage.DataAccess.MongoDb.TechnicalIndicators.HT_PHASOR
{
    public class AvHT_PHASORMonthlyRepository : AvMonthlyRepositoryAbs<AvHT_PHASOR, AvHT_PHASORMetaData, AvHT_PHASORBlock>
    {
        public override event EventHandler<RepositoryArgs> BeginExecute;
        public override event EventHandler<RepositoryArgs> EndExecute;
        public override event EventHandler<RepositoryArgs> Info;
        
        public AvHT_PHASORMonthlyRepository(IContext<IMongoClient, IMongoDatabase> context,
                                         string dbName,
                                         string collectionName) : base(context: context, dbName: dbName, collectionName: collectionName)

        {
        }

        public override Expression<Func<AvHT_PHASOR, bool>> CompareExpression(AvHT_PHASOR rhs)
        {
            return ts =>
                    ts.MetaData.Function == rhs.MetaData.Function &&
                    ts.MetaData.Symbol == rhs.MetaData.Symbol &&
                    ts.MetaData.Interval == rhs.MetaData.Interval &&
                    ts.MetaData.SeriesType == rhs.MetaData.SeriesType;
        }

    }
}
