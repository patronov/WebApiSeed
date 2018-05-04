using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SculptorWebApi.DataAccess.Entities
{
    public class PredictiveModel : BaseEntity
    {        
        public string Name { get; set; }
        public string ModelData { get; set; }
    }
}