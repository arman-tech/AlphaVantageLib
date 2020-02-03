using AlphaVantage.Common.Models.TechnicalIndicators.BBANDS;
using AlphaVantage.DataAccess.Interfaces;
using MongoDB.Driver;
using System;
using System.Linq.Expressions;
using AlphaVantage.DataAccess.EventArguments;
using AlphaVantage.Common.Models.TechnicalIndicators.STOCHRSI;

namespace AlphaVantage.DataAccess.MongoDb.TechnicalIndicators.STOCHRSI
{
    public class AvSTOCHRSIDefaultRepository : AvDefaultRepositoryAbs<AvSTOCHRSI, AvSTOCHRSIMetaData, AvSTOCHRSIBlock>
    {
        public override event EventHandler<RepositoryArgs> BeginExecute;
        public override event EventHandler<RepositoryArgs> EndExecute;
        public override event EventHandler<RepositoryArgs> Info;
        
        public AvSTOCHRSIDefaultRepository(IContext<IMongoClient, IMongoDatabase> context,
                                         string dbName,
                                         string collectionName) : base(context: context, dbName: dbName, collectionName: collectionName)

        {
        }

        public override Expression<Func<AvSTOCHRSI, bool>> CompareExpression(AvSTOCHRSI rhs)
        {
            return ts =>
                    ts.MetaData.Function == rhs.MetaData.Function &&
                    ts.MetaData.Symbol == rhs.MetaData.Symbol &&
                    ts.MetaData.Interval == rhs.MetaData.Interval &&
                    ts.MetaData.TimePeriod == rhs.MetaData.TimePeriod &&
                    ts.MetaData.SeriesType == rhs.MetaData.SeriesType &&
                    ts.MetaData.FastKPeriod == rhs.MetaData.FastKPeriod &&
                    ts.MetaData.FastDPeriod == rhs.MetaData.FastDPeriod &&
                    ts.MetaData.FastDMAType == rhs.MetaData.FastDMAType;
        }

    }
}
