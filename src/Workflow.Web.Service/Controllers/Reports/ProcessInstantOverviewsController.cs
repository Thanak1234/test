using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Workflow.ReportingService.Report;
using Workflow.DataObject.Reports;
using Workflow.Web.Service.Controllers.Common;

namespace Workflow.Web.Service.Controllers
{
    [RoutePrefix("api/processinstantoverviews")]
    public class ProcessInstantOverviewsController : ReportController<ProcInst, ProcInstParam>
    {
        private readonly string _reportPath = "/K2Report/Reports/PROC_INST_OVERVIEW";
        [HttpGet]
        public HttpResponseMessage Get()
        {
            var param = ParseProcInstParam<ProcInstParam>();
            IQueryable<ProcInst> procInsts = processInst.GetList(param, _reportPath);
            return Request.CreateResponse(HttpStatusCode.OK, new { count = param.TotalRecord, result = procInsts });
        }

        [HttpGet]
        [Route("export")]
        public HttpResponseMessage Export()
        {
            byte[] buffer = processInst.Export(ParseProcInstParam<ProcInstParam>(), _reportPath, Extension);
            
            return ExportFile(buffer, "PROCESS_INSTANCES_OVERVIEW");
        }
    }
}