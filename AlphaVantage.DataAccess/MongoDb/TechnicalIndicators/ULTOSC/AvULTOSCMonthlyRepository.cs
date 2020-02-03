using AlphaVantage.DataAccess.Interfaces;
using MongoDB.Driver;
using System;
using System.Linq.Expressions;
using AlphaVantage.DataAccess.EventArguments;
using AlphaVantage.Common.Models.TechnicalIndicators.ULTOSC;

namespace AlphaVantage.DataAccess.MongoDb.TechnicalIndicators.ULTOSC
{
    public class AvULTOSCMonthlyRepository : AvMonthlyRepositoryAbs<AvULTOSC, AvULTOSCMetaData, AvULTOSCBlock>
    {
        public override event EventHandler<RepositoryArgs> BeginExecute;
        public override event EventHandler<RepositoryArgs> EndExecute;
        public override event EventHandler<RepositoryArgs> Info;
        
        public AvULTOSCMonthlyRepository(IContext<IMongoClient, IMongoDatabase> context,
                                         string dbName,
                                         string collectionName) : base(context: context, dbName: dbName, collectionName: collectionName)

        {
        }

        public override Expression<Func<AvULTOSC, bool>> CompareExpression(AvULTOSC rhs)
        {
            return ts =>
                    ts.MetaData.Function == rhs.MetaData.Function &&
                    ts.MetaData.Symbol == rhs.MetaData.Symbol &&
                    ts.MetaData.Interval == rhs.MetaData.Interval &&
                    ts.MetaData.TimePeriodOne == rhs.MetaData.TimePeriodOne &&
                    ts.MetaData.TimePeriodTwo == rhs.MetaData.TimePeriodTwo &&
                    ts.MetaData.TimePeriodThree == rhs.MetaData.TimePeriodThree;
        }

    }
}
