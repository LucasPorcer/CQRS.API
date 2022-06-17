using Microsoft.EntityFrameworkCore;
using Orders.API.Domain.Interfaces.Repository;
using Orders.API.InfraData.Database.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Orders.API.InfraData.Repositories
{
    public class DatabaseRepositoryBase<TEntity> : IDatabaseRepositoryBase<TEntity> where TEntity : class
    {
        private DbSet<TEntity> _dbSet;

        public DatabaseRepositoryBase(OrderContext context)
        {
            _dbSet = context.Set<TEntity>();
        }

        public TEntity Add(TEntity obj)
        {
            return _dbSet.Add(obj).Entity;
        }

        public void Add(IEnumerable<TEntity> objs)
        {
            _dbSet.AddRange(objs);
        }

        public IList<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.Where(predicate).ToList();
        }

        public IList<TEntity> GetAll()
        {
            return _dbSet.ToList();
        }

        public TEntity GetById(long id)
        {
            return _dbSet.Find(id);
        }

        public TEntity Remove(TEntity obj)
        {
            return _dbSet.Remove(obj).Entity;
        }

        public TEntity Remove(long id)
        {
            TEntity obj = _dbSet.Find(id);
            return _dbSet.Remove(obj).Entity;
        }

        public void Remove(IEnumerable<TEntity> objs)
        {
            _dbSet.RemoveRange(objs);
        }

        public void Update(TEntity obj)
        {
            _dbSet.Update(obj);            
        }
    }
}
