using AlphaVantage.DataAccess.Interfaces;
using MongoDB.Driver;
using System;
using System.Linq.Expressions;
using AlphaVantage.DataAccess.EventArguments;
using AlphaVantage.Common.Models.TechnicalIndicators.AROON;

namespace AlphaVantage.DataAccess.MongoDb.TechnicalIndicators.AROON
{
    public class AvAROONMonthlyRepository : AvMonthlyRepositoryAbs<AvAROON, AvAROONMetaData, AvAROONBlock>
    {
        public override event EventHandler<RepositoryArgs> BeginExecute;
        public override event EventHandler<RepositoryArgs> EndExecute;
        public override event EventHandler<RepositoryArgs> Info;
        
        public AvAROONMonthlyRepository(IContext<IMongoClient, IMongoDatabase> context,
                                         string dbName,
                                         string collectionName) : base(context: context, dbName: dbName, collectionName: collectionName)

        {
        }

        public override Expression<Func<AvAROON, bool>> CompareExpression(AvAROON rhs)
        {
            return ts =>
                    ts.MetaData.Function == rhs.MetaData.Function &&
                    ts.MetaData.Symbol == rhs.MetaData.Symbol &&
                    ts.MetaData.Interval == rhs.MetaData.Interval &&
                    ts.MetaData.TimePeriod == rhs.MetaData.TimePeriod;
        }

    }
}
