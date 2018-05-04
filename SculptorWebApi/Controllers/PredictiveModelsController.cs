using Autofac.Extras.NLog;
using SculptorWebApi.Services.DataAccessServices.Classes.Interfaces;
using SculptorWebApi.Services.DataAccessServices.DTOs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;

namespace SculptorWebApi.Controllers
{
    [RoutePrefix("PredictiveModels")]
    public class PredictiveModelsController : BaseController
    {
        IPredictiveModelService m_predictiveModelService;

        public PredictiveModelsController(IPredictiveModelService predictiveModelService, ILogger logger)
            : base(logger)
        {
            m_predictiveModelService = predictiveModelService;
        }


        [HttpGet, Route("all"), ResponseType(typeof(List<PredictiveModelDTO>))]
        public async Task<IHttpActionResult> GetAll()
        {
            var result = await Task.Run(() =>
            {
                var predictiveModel = m_predictiveModelService.FindAll();

                return predictiveModel;
            });

            return ActionResultEvaluator(result, new HttpResponseMessage { StatusCode = HttpStatusCode.NotFound });
        }


        [HttpGet, Route("{id}"), ResponseType(typeof(PredictiveModelDTO))]
        public async Task<IHttpActionResult> GetByID(int id)
        {
            var result = await Task.Run(() =>
            {
                var predictiveModel = m_predictiveModelService.FindByID(id);

                return predictiveModel;
            });

            return ActionResultEvaluator(result, new HttpResponseMessage { StatusCode = HttpStatusCode.NotFound });
        }


        [HttpPost, Route(""), ResponseType(typeof(PredictiveModelDTO))]
        public async Task<IHttpActionResult> Create(PredictiveModelDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (m_predictiveModelService.FindByID(dto.ID) != null)
            {
                return BadRequest("This model exists");
            }
            else
            {
                var result = await Task.Run(() => m_predictiveModelService.Create(dto));

                return Ok(result);
            }
        }


        [HttpPut, Route(""), ResponseType(typeof(PredictiveModelDTO))]
        public async Task<IHttpActionResult> Update(PredictiveModelDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (m_predictiveModelService.FindByID(dto.ID) == null)
            {
                return BadRequest("This model is missing");
            }
            else
            {
                var result = await Task.Run(() => m_predictiveModelService.Update(dto));

                return Ok(result);
            }
        }


        [HttpDelete, Route("{id}"), ResponseType(typeof(bool))]
        public async Task<IHttpActionResult> Delete(int id)
        {
            bool result = await Task.Run(() => m_predictiveModelService.Delete(id));

            return Ok(result);
        }

        [HttpPost, Route("upload"), ResponseType(typeof(List<PredictiveModelDTO>))]
        public async Task<IHttpActionResult> UploadFile()
        {
            var inputs = new List<PredictiveModelDTO>();

            if (!Request.Content.IsMimeMultipartContent())
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);

            var provider = new MultipartMemoryStreamProvider();
            await Request.Content.ReadAsMultipartAsync(provider);

            foreach (var file in provider.Contents)
            {
                var filename = file.Headers.ContentDisposition.FileName.Trim('\"');
                var content = await file.ReadAsStringAsync();
                inputs.Add(new PredictiveModelDTO { Name = filename, ModelData = content });
            }

            var results  = await Task.Run(() => m_predictiveModelService.Create(inputs));

            return Ok(results);
        }
    }
}
