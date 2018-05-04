using Autofac.Extras.NLog;
using SculptorWebApi.DataAccess.Classes;
using SculptorWebApi.DataAccess.Entities;
using SculptorWebApi.DataAccess.Interfaces;
using SculptorWebApi.Services.DataAccessServices.Classes.Interfaces;
using SculptorWebApi.Services.DataAccessServices.DTOs;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;

namespace SculptorWebApi.Services.DataAccessServices.Classes
{
    public class PredictiveModelService : BaseService, IPredictiveModelService
    {
        private IPredictiveModelRepository CurrentRepository { get; set; }

        public PredictiveModelService(DbContext context, ILogger logger)
            : base(context, logger)
        {
            CurrentRepository = Unit.Repository<PredictiveModelRepository, IPredictiveModelRepository>();
        }

        public PredictiveModelService(DbContext context, IUnitOfWork unit, ILogger logger)
            : base(context, unit, logger)
        {
            CurrentRepository = Unit.Repository<PredictiveModelRepository, IPredictiveModelRepository>();
        }

        public List<PredictiveModelDTO> FindAll()
        {
            var entity = CurrentRepository.FindAll();

            return ConvertEntityToDTO<PredictiveModel, PredictiveModelDTO>(entity);
        }

        public PredictiveModelDTO FindByID(int id)
        {
            var entity = CurrentRepository.FindByID(id);

            return ConvertEntityToDTO<PredictiveModel, PredictiveModelDTO>(entity);
        }

        public List<PredictiveModelDTO> Create(List<PredictiveModelDTO> dtos)
        {
            var entities = ConvertDtoToEntity<PredictiveModel, PredictiveModelDTO>(dtos);
            var created = CurrentRepository.Create(entities);
            Unit.SaveChanges();

            return ConvertEntityToDTO<PredictiveModel, PredictiveModelDTO>(created);
        }

        public PredictiveModelDTO Create(PredictiveModelDTO dto)
        {
            var entity = ConvertDtoToEntity<PredictiveModel, PredictiveModelDTO>(dto);
            CurrentRepository.Create(entity);
            Unit.SaveChanges();

            return ConvertEntityToDTO<PredictiveModel, PredictiveModelDTO>(entity);
        }

        public PredictiveModelDTO Update(PredictiveModelDTO dto)
        {
            var entity = CurrentRepository.FindByID(dto.ID);

            if (entity != null)
            {
                var copied = CopytDtoToEntity<PredictiveModelDTO, PredictiveModel>(dto, entity);
                CurrentRepository.Update(copied);
                Unit.SaveChanges();
            }

            return ConvertEntityToDTO<PredictiveModel, PredictiveModelDTO>(entity);
        }

        public bool Delete(int id)
        {
            var result = CurrentRepository.Delete(id);
            Unit.SaveChanges();

            return result;
        }
    }
}