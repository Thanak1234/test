﻿using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Workflow.ReportingService.Report;
using Workflow.DataObject.Reports;
using Workflow.Web.Service.Controllers.Common;

namespace Workflow.Web.Service.Controllers
{
    [RoutePrefix("api/rsvncrprocessinstant")]
    public class RSVNCRProcessInstantController : ReportController<RSVNCRProcInst, ProcInstRSVNCRParam>
    {
        
        [HttpGet]
        public HttpResponseMessage Get()
        {
            var param = ParseProcInstParam<ProcInstRSVNCRParam>();
            var procInsts = processInst.GetList(param, ReportPath);
            return Request.CreateResponse(HttpStatusCode.OK, new { count = param.TotalRecord, result = procInsts });
        }

        [HttpGet]
        [Route("export")]
        public HttpResponseMessage Export()
        {
            byte[] buffer = processInst.Export(ParseProcInstParam<ProcInstRSVNCRParam>(), ReportPath, Extension);
            return ExportFile(buffer, "RSVNCR_PROCESS_INSTANCE");
        }
    }
}