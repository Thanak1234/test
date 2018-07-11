﻿using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Workflow.ReportingService.Report;
using Workflow.DataObject.Reports;
using Workflow.Web.Service.Controllers.Common;

namespace Workflow.Web.Service.Controllers
{
    [RoutePrefix("api/rmdreport")]
    public class RMDReportController : ReportController<RMDProcInst, ProcInstRMDParam>
    {
        [HttpGet]
        public HttpResponseMessage Get()
        {
            var param = ParseProcInstParam<ProcInstRMDParam>();
            var procInsts = processInst.GetList(param, ReportPath);
            return Request.CreateResponse(HttpStatusCode.OK, new { count = param.TotalRecord, result = procInsts });
        }

        [HttpGet]
        [Route("export")]
        public HttpResponseMessage Export()
        {
            byte[] buffer = processInst.Export(ParseProcInstParam<ProcInstRMDParam>(), ReportPath, Extension);
            return ExportFile(buffer, "RMD_PROCESS_INSTANCE");
        }
    }
}