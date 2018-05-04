using SculptorWebApi.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SculptorWebApi.DataAccess.Interfaces
{
    public interface IPredictiveModelRepository : IRepository<PredictiveModel>
    {
        List<PredictiveModel> FindAll();
        PredictiveModel FindByID(int id);
        IEnumerable<PredictiveModel> Create(List<PredictiveModel> entities);
        PredictiveModel Create(PredictiveModel entity);
        void Update(PredictiveModel entity);
        bool Delete(int ids);
        void Delete(List<int> ids);
    }
}