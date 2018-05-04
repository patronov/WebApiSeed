using Autofac.Extras.NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SculptorWebApi.Controllers
{
    public abstract class BaseController : ApiController
    {
        protected ILogger Log { get; private set; }

        public BaseController(ILogger logger)
        {
            Log = logger;
        }

        protected IHttpActionResult ActionResultEvaluator(object dto, HttpResponseMessage message)
        {
            if (dto == null)
            {
                return ResponseMessage(message);
            }
            else
            {
                return Ok(dto);
            }
        }
    }
}
