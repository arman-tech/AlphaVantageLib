﻿using AlphaVantage.Common.Models.TechnicalIndicators.MAMA;
using AlphaVantage.DataAccess.Interfaces;
using MongoDB.Driver;
using System;
using System.Linq.Expressions;

namespace AlphaVantage.DataAccess.MongoDb.TechnicalIndicators.MAMA
{
    public class AvMAMADefaultRepository : AvDefaultRepositoryAbs<AvMAMA, AvMAMAMetaData, AvMAMABlock>
    {
        public AvMAMADefaultRepository(IContext<IMongoClient, IMongoDatabase> context,
                                string dbName,
                                string collectionName) : base(context: context, dbName: dbName, collectionName: collectionName)

        {
        }
        public override Expression<Func<AvMAMA, bool>> CompareExpression(AvMAMA rhs)
        {
            return ts =>
                    ts.MetaData.Function == rhs.MetaData.Function &&
                    ts.MetaData.Symbol == rhs.MetaData.Symbol &&
                    ts.MetaData.Interval == rhs.MetaData.Interval &&
                    ts.MetaData.SlowLimit == rhs.MetaData.SlowLimit &&
                    ts.MetaData.FastLimit == rhs.MetaData.FastLimit &&
                    ts.MetaData.SeriesType == rhs.MetaData.SeriesType;
        }
    }
}
