using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SculptorWebApi.Services.DataAccessServices.DTOs
{
    public class PredictiveModelDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string ModelData { get; set; }
    }
}