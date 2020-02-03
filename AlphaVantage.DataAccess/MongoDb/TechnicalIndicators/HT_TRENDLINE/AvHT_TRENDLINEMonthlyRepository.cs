using AlphaVantage.Common.Models.TechnicalIndicators.BBANDS;
using AlphaVantage.DataAccess.Interfaces;
using MongoDB.Driver;
using System;
using System.Linq.Expressions;
using AlphaVantage.DataAccess.EventArguments;
using AlphaVantage.Common.Models.TechnicalIndicators.HT_TRENDLINE;

namespace AlphaVantage.DataAccess.MongoDb.TechnicalIndicators.HT_TRENDLINE
{
    public class AvHT_TRENDLINEMonthlyRepository : AvMonthlyRepositoryAbs<AvHT_TRENDLINE, AvHT_TRENDLINEMetaData, AvHT_TRENDLINEBlock>
    {
        public override event EventHandler<RepositoryArgs> BeginExecute;
        public override event EventHandler<RepositoryArgs> EndExecute;
        public override event EventHandler<RepositoryArgs> Info;
        
        public AvHT_TRENDLINEMonthlyRepository(IContext<IMongoClient, IMongoDatabase> context,
                                         string dbName,
                                         string collectionName) : base(context: context, dbName: dbName, collectionName: collectionName)

        {
        }

        public override Expression<Func<AvHT_TRENDLINE, bool>> CompareExpression(AvHT_TRENDLINE rhs)
        {
            return ts =>
                    ts.MetaData.Function == rhs.MetaData.Function &&
                    ts.MetaData.Symbol == rhs.MetaData.Symbol &&
                    ts.MetaData.Interval == rhs.MetaData.Interval &&
                    ts.MetaData.SeriesType == rhs.MetaData.SeriesType;
        }

    }
}
