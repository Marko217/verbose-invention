using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;


namespace AgreementManagement.Repo
{
    public interface IRepository<TEntity, TEntityArh> where TEntity : class where TEntityArh : class
    {
        TEntity Get<TKey>(TKey id);
        IEnumerable<TEntity> GetAll();
        IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> filter);
        TEntity SingleOrDefault(Expression<Func<TEntity, bool>> filter);
        void Add(TEntity entity);
        void Add(IEnumerable<TEntity> entities);
        void Update(TEntity entity, TEntityArh entityArh);
        void UpdateNoArh(TEntity entity);
        void Remove<TKey>(TKey id);
        void Remove(TEntity entity);
        void Remove(IEnumerable<TEntity> entities);
        void SaveChanges();
    }
}
