using Autofac.Extras.NLog;
using AutoMapper;
using SculptorWebApi.DataAccess;
using SculptorWebApi.DataAccess.Entities;
using SculptorWebApi.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SculptorWebApi.Services.DataAccessServices
{
    public abstract class BaseService : IDisposable
    {
        protected ILogger Log { get; private set; }

        protected IUnitOfWork Unit
        {
            get
            {
                return m_unit ?? new UnitOfWork(ContextManager);
            }
        }

        private IUnitOfWork m_unit;
        protected DbContext ContextManager { get; private set; }

        public BaseService(DbContext context, ILogger logger)
        {
            ContextManager = context;
            m_unit = new UnitOfWork(ContextManager);
            //Log = LogManager.GetLogger(GetType().FullName);
            Log = logger;
        }

        public BaseService(DbContext context, IUnitOfWork unit, ILogger logger)
        {
            ContextManager = context;
            m_unit = unit;
            //Log = LogManager.GetLogger(GetType().FullName);
            Log = logger;
        }

        protected virtual TEntity CopytDtoToEntity<TDto, TEntity>(TDto dto, TEntity entity) where TEntity: BaseEntity
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<TDto, TEntity>());
            var mapper = config.CreateMapper();
            TEntity copied = mapper.Map<TDto, TEntity>(dto, entity);

            return copied;
        }

        protected virtual List<TEntity> CopytDtoToEntity<TDto, TEntity>(IEnumerable<TDto> dtos, TEntity entity) where TEntity : BaseEntity
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TDto, TEntity>();
                cfg.ShouldMapField = fi => false;
            });
            var mapper = config.CreateMapper();            

            IEnumerable<TEntity> entities = mapper.Map<IEnumerable<TDto>, IEnumerable<TEntity>>(dtos);

            return entities.ToList();
        }

        protected virtual TEntity ConvertDtoToEntity<TEntity, TDto>(TDto dto)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<TDto, TEntity>());
            var mapper = config.CreateMapper();
            TEntity entity = mapper.Map<TEntity>(dto);

            return entity;
        }

        protected virtual TDto ConvertEntityToDTO<TEntity, TDto>(TEntity entity)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TEntity, TDto>();
                cfg.ShouldMapField = fi => false;
            });
            var mapper = config.CreateMapper();
            mapper.Map<TDto>(entity);

            TDto dto = mapper.Map<TDto>(entity);

            return dto;
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

        public void Dispose()
        {
            Unit.Dispose();
            ContextManager.Dispose();
        }
    }
}