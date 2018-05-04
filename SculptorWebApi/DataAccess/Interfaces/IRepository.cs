using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace SculptorWebApi.DataAccess.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        //TEntity Get(int id);
        //IQueryable<TEntity> Include(Expression<Func<TEntity, object>> predicate);
        //IEnumerable<TEntity> GetAll();
        //bool Exists(Expression<Func<TEntity, bool>> predicate);
        //IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        //TEntity Add(TEntity entity);
        //IEnumerable<TEntity> AddRange(IEnumerable<TEntity> entities);
        //void Update(TEntity entity);
        //void Remove(int id);
        //void Remove(TEntity entity);
        //void RemoveRange(IEnumerable<TEntity> entities);
    }
}