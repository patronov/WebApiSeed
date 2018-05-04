using System.Collections.Generic;
using SculptorWebApi.Services.DataAccessServices.DTOs;
using System.Web;

namespace SculptorWebApi.Services.DataAccessServices.Classes.Interfaces
{
    public interface IPredictiveModelService
    {
        List<PredictiveModelDTO> Create(List<PredictiveModelDTO> dtos);
        PredictiveModelDTO Create(PredictiveModelDTO dto);
        bool Delete(int id);
        List<PredictiveModelDTO> FindAll();
        PredictiveModelDTO FindByID(int id);
        PredictiveModelDTO Update(PredictiveModelDTO dto);
    }
}