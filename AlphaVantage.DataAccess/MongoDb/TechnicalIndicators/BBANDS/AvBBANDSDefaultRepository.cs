using AlphaVantage.Common.Models.TechnicalIndicators.BBANDS;
using AlphaVantage.DataAccess.Interfaces;
using MongoDB.Driver;
using System;
using System.Linq.Expressions;
using AlphaVantage.DataAccess.EventArguments;

namespace AlphaVantage.DataAccess.MongoDb.TechnicalIndicators.BBANDS
{
    public class AvBBANDSDefaultRepository : AvDefaultRepositoryAbs<AvBBANDS, AvBBANDSMetaData, AvBBANDSBlock>
    {
        public override event EventHandler<RepositoryArgs> BeginExecute;
        public override event EventHandler<RepositoryArgs> EndExecute;
        public override event EventHandler<RepositoryArgs> Info;
        
        public AvBBANDSDefaultRepository(IContext<IMongoClient, IMongoDatabase> context,
                                         string dbName,
                                         string collectionName) : base(context: context, dbName: dbName, collectionName: collectionName)

        {
        }

        public override Expression<Func<AvBBANDS, bool>> CompareExpression(AvBBANDS rhs)
        {
            return ts =>
                    ts.MetaData.Function == rhs.MetaData.Function &&
                    ts.MetaData.Symbol == rhs.MetaData.Symbol &&
                    ts.MetaData.Interval == rhs.MetaData.Interval &&
                    ts.MetaData.TimePeriod == rhs.MetaData.TimePeriod &&
                    ts.MetaData.SeriesType == rhs.MetaData.SeriesType &&
                    ts.MetaData.DeviationMultiplierUpperBand == rhs.MetaData.DeviationMultiplierUpperBand &&
                    ts.MetaData.DeviationMultiplierLowerBand == rhs.MetaData.DeviationMultiplierLowerBand &&
                    ts.MetaData.MovingAvgType == rhs.MetaData.MovingAvgType;
        }

    }
}
