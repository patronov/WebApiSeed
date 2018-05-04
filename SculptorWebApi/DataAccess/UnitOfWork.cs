using SculptorWebApi.DataAccess.Entities;
using SculptorWebApi.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
//using System.Transactions;

namespace SculptorWebApi.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext m_context;
        private DbContextTransaction m_scope;

        public UnitOfWork(DbContext context)
        {
            m_context = context;
        }

        private Dictionary<Type, object> m_repositories = new Dictionary<Type, object>();

        public I Repository<T, I>() where T : class
        {
            Type type = typeof(T);

            if (m_repositories.ContainsKey(type))
            {
                return (I)m_repositories[type];
            }

            var instance = Activator.CreateInstance(type, new object[] { m_context });
            m_repositories.Add(type, instance);

            return (I)instance;
        }

        public List<BaseEntity> SaveChanges()
        {
            List<BaseEntity> changed = new List<BaseEntity>();
            m_scope = m_context.Database.BeginTransaction();

            foreach (var entry in m_context.ChangeTracker.Entries<BaseEntity>())
            {
                var entity = entry.Entity;
                var now = DateTimeOffset.Now;

                switch (entry.State)
                {
                    case EntityState.Added:
                        entity.CreationDate = now;
                        entity.LastUpdated = now;
                        changed.Add(entity);
                        break;
                    case EntityState.Modified:
                        entity.LastUpdated = now;
                        changed.Add(entity);
                        break;
                }
            }

            int changeID = m_context.SaveChanges();
            m_context.ChangeTracker.DetectChanges();

            Complete();

            return changed;
        }

        public void Dispose()
        {
            m_context.Dispose();
        }

        public void Complete()
        {
            m_scope.Commit();
        }

        public void Rollback()
        {
            m_scope.Rollback();
        }
    }
}