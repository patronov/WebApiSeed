using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Tracing;

namespace SculptorWebApi.Main.Tracing
{
    public class SimpleTracer : ITraceWriter
    {
        public void Trace(HttpRequestMessage request, string category, TraceLevel level, Action<TraceRecord> traceAction)
        {
            TraceRecord record = new TraceRecord(request, category, level);
            traceAction(record);
            WriteTrace(record);
        }

        protected void WriteTrace(TraceRecord record)
        {
            var message = string.Format("{0};{1};{2}",
                record.Operator, record.Operation, record.Message);
            System.Diagnostics.Trace.WriteLine(message, record.Category);
        }
    }
}