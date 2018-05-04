using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace SculptorWebApi.Main.Logging
{
    public class CustomLogHandler : DelegatingHandler
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var logMetadata = BuildRequestMetadata(request);
            var response = await base.SendAsync(request, cancellationToken);
            logMetadata = BuildResponseMetadata(logMetadata, response);
            await SendToLog(logMetadata);

            return response;
        }

        private LogMetadataDTO BuildRequestMetadata(HttpRequestMessage request)
        {
            LogMetadataDTO log = new LogMetadataDTO
            {
                RequestMethod = request.Method.Method,
                RequestTimestamp = DateTime.Now,
                RequestUri = request.RequestUri.ToString()
            };

            return log;
        }

        private LogMetadataDTO BuildResponseMetadata(LogMetadataDTO logMetadata, HttpResponseMessage response)
        {
            logMetadata.ResponseStatusCode = response.StatusCode;
            logMetadata.ResponseTimestamp = DateTime.Now;
            logMetadata.ResponseContentType = response.Content?.Headers?.ContentType?.MediaType;

            return logMetadata;
        }

        private async Task<bool> SendToLog(LogMetadataDTO logMetadata)
        {
            // TODO: Write code here to store the logMetadata instance to a pre-configured log store...
            //var tmpLog = string.Format(@"{0}logs.txt", System.Web.Hosting.HostingEnvironment.MapPath(Configuration.LOGS));
            //Utils.WriteToFileAsync(tmpLog, logMetadata.RequestUri);

            await Task.Run(() =>
            {
                logger.Info("Request: {0}", logMetadata.RequestUri);

                if (logMetadata.ResponseStatusCode != HttpStatusCode.OK)
                {
                    logger.Error("{0}, {1}", logMetadata.ResponseStatusCode, logMetadata.ResponseContentType);
                }
            });

            return true;
        }

    }
}