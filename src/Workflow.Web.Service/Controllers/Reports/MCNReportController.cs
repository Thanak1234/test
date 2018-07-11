﻿using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Workflow.ReportingService.Report;
using Workflow.DataObject.Reports;
using Workflow.Web.Service.Controllers.Common;

namespace Workflow.Web.Service.Controllers.Reports
{
    [RoutePrefix("api/mcnreport")]
    public class MCNReportController : ReportController<EGMMRProcInst,ProcInstMCNParam>
    {
        [HttpGet]
        public HttpResponseMessage Get()
        {
            var param = ParseProcInstParam<ProcInstMCNParam>();
            var procInsts = processInst.GetList(param, ReportPath);
            return Request.CreateResponse(HttpStatusCode.OK, new { count = param.TotalRecord, result = procInsts });
        }

        [HttpGet]
        [Route("export")]
        public HttpResponseMessage Export()
        {
            byte[] buffer = processInst.Export(ParseProcInstParam<ProcInstMCNParam>(), ReportPath, Extension);
            return ExportFile(buffer, "EGMMR_PROCESS_INSTANCE");
        }
    }
}