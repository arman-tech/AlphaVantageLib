using AlphaVantage.DataAccess.Interfaces;
using MongoDB.Driver;
using System;
using System.Linq.Expressions;
using AlphaVantage.DataAccess.EventArguments;
using AlphaVantage.Common.Models.TechnicalIndicators.SAR;

namespace AlphaVantage.DataAccess.MongoDb.TechnicalIndicators.SAR
{
    public class AvSARWeeklyRepository : AvWeeklyRepositoryAbs<AvSAR, AvSARMetaData, AvSARBlock>
    {
        public override event EventHandler<RepositoryArgs> BeginExecute;
        public override event EventHandler<RepositoryArgs> EndExecute;
        public override event EventHandler<RepositoryArgs> Info;
        
        public AvSARWeeklyRepository(IContext<IMongoClient, IMongoDatabase> context,
                                         string dbName,
                                         string collectionName) : base(context: context, dbName: dbName, collectionName: collectionName)

        {
        }

        public override Expression<Func<AvSAR, bool>> CompareExpression(AvSAR rhs)
        {
            return ts =>
                    ts.MetaData.Function == rhs.MetaData.Function &&
                    ts.MetaData.Symbol == rhs.MetaData.Symbol &&
                    ts.MetaData.Interval == rhs.MetaData.Interval &&
                    ts.MetaData.Acceleration == rhs.MetaData.Acceleration &&
                    ts.MetaData.Maximum == rhs.MetaData.Maximum;
        }

    }
}
