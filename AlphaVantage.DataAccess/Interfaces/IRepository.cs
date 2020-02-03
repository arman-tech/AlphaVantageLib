using AlphaVantage.DataAccess.EventArguments;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace AlphaVantage.DataAccess.Interfaces
{
    public interface IRepositoryAnchor { }

    public interface IRepository<T> : IRepositoryLogging<RepositoryArgs>, IRepositoryAnchor, IDisposable where T : class, new()
    {
        void Delete(Expression<Func<T, bool>> expression);
        void Delete(T item);
        void DeleteAll();
        T Single(Expression<Func<T, bool>> expression);
        IEnumerable<T> Many(Expression<Func<T, bool>> expression);
        System.Linq.IQueryable<T> All();
        System.Linq.IQueryable<T> All(int page, int pageSize);
        void Add(T item);
        void Add(IEnumerable<T> items);
        void Upsert(T item);
        void Upsert(IEnumerable<T> items);

        bool DoesRecordExist(T record);
    }
}
