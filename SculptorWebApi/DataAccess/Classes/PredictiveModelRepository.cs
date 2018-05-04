using SculptorWebApi.DataAccess.Entities;
using SculptorWebApi.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SculptorWebApi.DataAccess.Classes
{
    public class PredictiveModelRepository : BaseRepository<PredictiveModel>, IPredictiveModelRepository
    {
        public PredictiveModelRepository(DbContext context)
            : base(context)
        { }

        public List<PredictiveModel> FindAll()
        {
            var entitities = GetAll().ToList();

            return entitities;
        }

        public PredictiveModel FindByID(int id)
        {
            var entity = Get(id);

            return entity;
        }

        public IEnumerable<PredictiveModel> Create(List<PredictiveModel> entities)
        {
            return AddRange(entities);
        }

        public PredictiveModel Create(PredictiveModel entity)
        {
            return Add(entity);
        }

        public new void Update(PredictiveModel entity)
        {
            if (Exists(n=> n.ID == entity.ID))
            {
                base.Update(entity);
            }
        }

        public bool Delete(int id)
        {
            return Remove(id);
        }

        public void Delete(List<int> ids)
        {
            ids.ForEach(id => Remove(id));
        }
    }
}