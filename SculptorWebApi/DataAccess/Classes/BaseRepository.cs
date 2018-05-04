using AutoMapper;
using SculptorWebApi.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace SculptorWebApi.DataAccess.Classes
{
    public abstract class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext CurrentContext;
        protected DbSet<TEntity> EntitySet { get; set; }

        public BaseRepository(DbContext context)
        {
            CurrentContext = context;
            EntitySet = CurrentContext.Set<TEntity>();
        }

        protected TEntity Get(int id)
        {
            return EntitySet.Find(id);
        }

        protected IQueryable<TEntity> Include(Expression<Func<TEntity, object>> predicate)
        {
            return EntitySet.Include(predicate);
        }

        protected IEnumerable<TEntity> GetAll()
        {
            return EntitySet.ToList();
        }

        protected bool Exists(Expression<Func<TEntity, bool>> predicate)
        {
            return EntitySet.Any(predicate);
        }

        protected IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return EntitySet.Where(predicate);
        }

        protected TEntity Add(TEntity entity)
        {
            return EntitySet.Add(entity);
        }

        protected IEnumerable<TEntity> AddRange(IEnumerable<TEntity> entities)
        {
            return EntitySet.AddRange(entities);
        }

        protected void Update(TEntity entity)
        {
            CurrentContext.Entry<TEntity>(entity).State = EntityState.Modified;
        }

        protected virtual bool Remove(int id)
        {
            var entity = Get(id);
            return Remove(entity);
        }

        protected bool Remove(TEntity entity)
        {
            if (entity != null)
            {
                EntitySet.Remove(entity);

                return true;
            }
            else
            {
                return false;
            }

        }

        protected void RemoveRange(IEnumerable<TEntity> entities)
        {
            EntitySet.RemoveRange(entities);
        }

        protected void Reload(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                CurrentContext.Entry(entity).Reload();
            }

        }

        protected virtual Entity ConvertDtoToEntity<Entity, TDto>(TDto dto)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<TDto, Entity>());
            var mapper = config.CreateMapper();
            Entity entity = mapper.Map<Entity>(dto);

            return entity;
        }

        protected virtual List<Entity> ConvertDtoToEntity<Entity, TDto>(IEnumerable<TDto> dtos)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TDto, Entity>();
                cfg.ShouldMapField = fi => false;
            });
            var mapper = config.CreateMapper();

            IEnumerable<Entity> entities = mapper.Map<IEnumerable<TDto>, IEnumerable<Entity>>(dtos);

            return entities.ToList();
        }

        protected virtual TDto ConvertEntityToDTO<Entity, TDto>(Entity entity)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Entity, TDto>();
                cfg.ShouldMapField = fi => false;
            });
            var mapper = config.CreateMapper();

            TDto dto = mapper.Map<TDto>(entity);

            return dto;
        }

        protected virtual List<TDto> ConvertEntityToDTO<Entity, TDto>(IEnumerable<Entity> entities)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Entity, TDto>();
                cfg.ShouldMapField = fi => false;
            });
            var mapper = config.CreateMapper();

            IEnumerable<TDto> dtos = mapper.Map<IEnumerable<Entity>, IEnumerable<TDto>>(entities);

            return dtos.ToList();
        }
    }
}