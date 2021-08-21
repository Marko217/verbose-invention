using AgreementManagement.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace AgreementManagement.Repo
{
    public class Repository<TEntity, TEntityArh> : IRepository<TEntity, TEntityArh> where TEntity : class where TEntityArh : class
    {
        protected AgreementManagementDbContext agreetManagDbContext;
        protected DbSet<TEntity> entities;
        protected DbSet<TEntityArh> entitiesArh;

        public Repository(AgreementManagementDbContext context)
        {
            agreetManagDbContext = context;
            entities = agreetManagDbContext.Set<TEntity>();
        }

        public TEntity Get<TKey>(TKey key)
        {
            return entities.Find(key);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return entities.AsNoTracking().ToList();
        }

        public IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> whereClause)
        {
            return entities.Where(whereClause);
        }

        public TEntity SingleOrDefault(Expression<Func<TEntity, bool>> whereClause)
        {
            return entities.SingleOrDefault(whereClause);
        }

        public void Add(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("Entity is NULL!");
            }
            entities.Add(entity);
            agreetManagDbContext.SaveChanges();
        }

        public void Add(IEnumerable<TEntity> entitiesRange)
        {
            if (entitiesRange == null || entitiesRange.Count() == 0)
            {
                throw new ArgumentNullException("Entity range is NULL or empty!");
            }
            entities.AddRange(entitiesRange);
            agreetManagDbContext.SaveChanges();
        }

        public void Update(TEntity entity, TEntityArh entityArh)
        {
            if (entity == null || entityArh == null)
            {
                throw new ArgumentNullException("Entity or EntityArh is NULL!");
            }
            agreetManagDbContext.Entry(entity).State = EntityState.Modified;
            agreetManagDbContext.SaveChanges();
            entitiesArh.Add(entityArh);
            agreetManagDbContext.SaveChanges();
        }

        public void UpdateNoArh(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("Entity is NULL!");
            }
            agreetManagDbContext.Entry(entity).State = EntityState.Modified;
            agreetManagDbContext.SaveChanges();
        }

        public void Remove<TKey>(TKey id)
        {
            TEntity entity = entities.Find(id);
            this.Remove(entity);
        }

        public void Remove(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("Entity is NULL!");
            }

            entities.Remove(entity);
            agreetManagDbContext.Entry(entity).State = EntityState.Deleted;
            agreetManagDbContext.SaveChanges();
        }

        public void Remove(IEnumerable<TEntity> entitiesRange)
        {
            if (entitiesRange == null || entitiesRange.Count() == 0)
            {
                throw new ArgumentNullException("Entity range is NULL or empty!");
            }
            entities.RemoveRange(entitiesRange);
        }

        public void SaveChanges()
        {
            agreetManagDbContext.SaveChanges();
        }
    }
}
