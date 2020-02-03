using AlphaVantage.DataAccess.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using MongoDB.Bson.Serialization;
using AlphaVantage.DataAccess.MongoDb;
using AlphaVantage.DataAccess.EventArguments;

namespace AlphaVantage.DataAccess.Base
{
    /* NOTE: by this point we know that we are implementing a Mongo repository.  Therefore it makes sense to have the concrete class of MongoContext.
     * However, since we are using dependency injection we can use IContext<IMongoClient, IMongoDatabase>.
     * Unfortunately, MongoRepositoryBase is an abstract class that has yet to be concretely defined.  Hence why it's abstract and generic.
     * Later on, per table we will create a concrete class that inherits MongoRepositoryBase.
     * 
     */

    public abstract class MongoRepositoryBaseAbs<T> : IRepository<T> where T : class, new()
    {
        protected virtual IContext<IMongoClient, IMongoDatabase> Context { get; set; }
        protected virtual IMongoCollection<T> Collection { get; set; }
        protected virtual string DbName { get; set; }
        protected virtual string CollectionName { get; set; }
        protected const int ExistingElementIndex = 0;
        protected Guid Guid { get; set; }
        protected string OracleDateFormat = "dd-MMM-yyyy";

        public virtual event EventHandler<RepositoryArgs> BeginExecute;
        public virtual event EventHandler<RepositoryArgs> EndExecute;
        public virtual event EventHandler<RepositoryArgs> Info;

        bool disposed = false;

        protected MongoRepositoryBaseAbs(IContext<IMongoClient, IMongoDatabase> context,
                                         string dbName, 
                                         string collectionName)
        {
            Context = context;
            DbName = dbName;
            CollectionName = collectionName;
            Collection = Initialize<T>(dbName, collectionName);
            Guid = Guid.NewGuid();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                // dispose unmanaged code here.
            }

            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual IMongoCollection<K> Initialize<K>(string dbName, string collectionName) where K : class
        {
            if (string.IsNullOrWhiteSpace(dbName))
            {
                throw new ArgumentNullException(nameof(dbName));
            }

            if (string.IsNullOrWhiteSpace(collectionName))
            {
                throw new ArgumentNullException(nameof(Collection));
            }

            // If the type is not registered then we need to ensure we set 'SetIgnoreExtraElements' to true.
            var isRegistered = BsonClassMap.IsClassMapRegistered(typeof(T));

            if (!isRegistered)
            {
                BsonClassMap.RegisterClassMap<T>(cm =>
                {
                    cm.AutoMap();
                    cm.SetIgnoreExtraElements(true);
                });
            }

            // set database.
            this.Context.SetDatabase(dbName);

            // ensure collection is set up.  If it is not then create it.
            if (!this.DoesCollectionExist(collectionName))
            {
                this.Context.GetDatabase().CreateCollection(collectionName, new CreateCollectionOptions { Capped = false });
            }

            return this.Context.GetDatabase().GetCollection<K>(collectionName);
        }

        public void Delete(Expression<Func<T, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public void Delete(T item)
        {
            throw new NotImplementedException();
        }

        public void DeleteAll()
        {
            throw new NotImplementedException();
        }

        public T Single(Expression<Func<T, bool>> expression)
        {
            return this.Collection.Find<T>(expression).FirstOrDefault();
        }

        public IEnumerable<T> Many(Expression<Func<T, bool >> expression)
        {
            return this.Collection.Find<T>(expression).ToEnumerable();
        }

        public virtual IQueryable<T> All()
        {
            return this.Collection.AsQueryable();
        }

        public IQueryable<T> All(int page, int pageSize)
        {
            throw new NotImplementedException();
        }

        public virtual void Add(T item)
        {
            this.Collection.InsertOne(item);
        }

        public virtual void Add(IEnumerable<T> items)
        {
            this.Collection.InsertMany(items);
        }

        protected bool DoesCollectionExist(string collectionName)
        {
            var db = Context.GetProvider().GetDatabase(DbName);
            var filter = new BsonDocument(MongoDbCommon.CollectionName, collectionName);
            var collections = db.ListCollections(new ListCollectionsOptions { Filter = filter });
            return collections.Any();
        }

        public virtual void Upsert(T item)
        {
            throw new NotImplementedException();
        }

        public virtual void Upsert(IEnumerable<T> items)
        {
	        foreach(var item in items) {
 	            Upsert(item);
	        }
        }

        public virtual bool DoesRecordExist(T record)
        {
            throw new NotImplementedException();
        }

        public abstract Expression<Func<T, bool>> CompareExpression(T rhs);

    }
}
