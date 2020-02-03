// gives you the specific time series only.
db.Equities.aggregate(
    [
        { $match: { "MetaData.Function.Name": { $eq: "TIME_SERIES_MONTHLY" } } },
        { $unwind: "$TimeSeries" },

        { $unwind: "$TimeSeries.TimeStamp" },

        { $match: { "TimeSeries.TimeStamp": { $gte: ISODate("2019-09-01T00:00:00.000Z") } } }

    ]).pretty()

// give you the whole collection.
db.getCollection('Equities').find(
    {
        "MetaData.Symbol": { $eq: "MMM" },
        "MetaData.Function.Name": { $eq: "TIME_SERIES_MONTHLY" },
        "TimeSeries.TimeStamp": { $gte: ISODate("2019-08-01T00:00:00.000Z") }
    }).pretty()

