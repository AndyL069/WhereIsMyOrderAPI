using System;
using System.Linq;
using System.Linq.Expressions;

namespace WhereIsMyOrderAPI.Repositories
{
    public interface IRepository<T>
    {
        void Insert(T entity);
        void Delete(T entity);
        void Update(T entity);
        void SaveChanges();
        IQueryable<T> SearchFor(Expression<Func<T, bool>> predicate);
        IQueryable<T> SearchForNotTracked(Expression<Func<T, bool>> predicate);
        IQueryable<T> GetAll();
        T GetById(int id);
    }
}
