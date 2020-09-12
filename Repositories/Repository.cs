using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace WhereIsMyOrderAPI.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private DbSet<T> DbSet;
        private DbContext _dbContext;

        public Repository(DbContext dbContext)
        {
            if (dbContext != null)
            {
                _dbContext = dbContext;
                DbSet = dbContext.Set<T>();
            }
        }

        public void Insert(T entity)
        {
            DbSet.Add(entity);
        }

        public void Update(T entity)
        {
            DbSet.Update(entity);
        }

        public void Delete(T entity)
        {
            DbSet.Remove(entity);
        }

        public IQueryable<T> SearchFor(Expression<Func<T, bool>> predicate)
        {
            return DbSet.Where(predicate);
        }

        public IQueryable<T> SearchForNotTracked(Expression<Func<T, bool>> predicate)
        {
            _dbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            return DbSet.Where(predicate);
        }

        public IQueryable<T> GetAll()
        {
            return DbSet;
        }

        public T GetById(int id)
        {
            return DbSet.Find(id);
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
    }
}
