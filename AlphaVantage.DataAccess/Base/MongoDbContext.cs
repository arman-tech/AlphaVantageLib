using AlphaVantage.DataAccess.Interfaces;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using System;

namespace AlphaVantage.DataAccess.Base
{
    public class MongoDbContext : IContext<IMongoClient, IMongoDatabase>
    {
        private IMongoClient _client;
        private IMongoDatabase _database;

        public MongoDbContext(IMongoClient client)
        {
            _client = client;

            BsonSerializer.RegisterSerializationProvider(new DecimalSerializationProvider());
        }



        public void Dispose()
        {
        }

        public IMongoClient GetProvider()
        {
            return _client;
        }

        public IMongoDatabase GetDatabase()
        {
            return _database;
        }

        public bool SetDatabase(string databaseName)
        {
            if (_client == null) return false;

            _database = _client.GetDatabase(databaseName);
            return true;
        }
    }

    public class DecimalSerializationProvider : IBsonSerializationProvider
    {
        private static readonly DecimalSerializer DecimalSerializer = new DecimalSerializer(BsonType.Decimal128);
        private static readonly NullableSerializer<decimal> NullableSerializer = new NullableSerializer<decimal>(new DecimalSerializer(BsonType.Decimal128));

        public IBsonSerializer GetSerializer(Type type)
        {
            if (type == typeof(decimal)) return DecimalSerializer;
            if (type == typeof(decimal?)) return NullableSerializer;

            return null; // falls back to Mongo defaults
        }
    }
}
