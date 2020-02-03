using AlphaVantage.DataAccess.Interfaces;
using MongoDB.Driver;
using System;
using System.Linq.Expressions;
using AlphaVantage.DataAccess.EventArguments;
using AlphaVantage.Common.Models.TechnicalIndicators.HT_DCPHASE;

namespace AlphaVantage.DataAccess.MongoDb.TechnicalIndicators.HT_DCPHASE
{
    public class AvHT_DCPHASEWeeklyRepository : AvWeeklyRepositoryAbs<AvHT_DCPHASE, AvHT_DCPHASEMetaData, AvHT_DCPHASEBlock>
    {
        public override event EventHandler<RepositoryArgs> BeginExecute;
        public override event EventHandler<RepositoryArgs> EndExecute;
        public override event EventHandler<RepositoryArgs> Info;
        
        public AvHT_DCPHASEWeeklyRepository(IContext<IMongoClient, IMongoDatabase> context,
                                         string dbName,
                                         string collectionName) : base(context: context, dbName: dbName, collectionName: collectionName)

        {
        }

        public override Expression<Func<AvHT_DCPHASE, bool>> CompareExpression(AvHT_DCPHASE rhs)
        {
            return ts =>
                    ts.MetaData.Function == rhs.MetaData.Function &&
                    ts.MetaData.Symbol == rhs.MetaData.Symbol &&
                    ts.MetaData.Interval == rhs.MetaData.Interval &&
                    ts.MetaData.SeriesType == rhs.MetaData.SeriesType;
        }

    }
}
