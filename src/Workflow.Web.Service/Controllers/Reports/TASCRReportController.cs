using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Workflow.ReportingService.Report;
using Workflow.DataObject.Reports;
using Workflow.Web.Service.Controllers.Common;

namespace Workflow.Web.Service.Controllers
{
    [RoutePrefix("api/tascrreport")]
    public class TASCRReportController : ReportController<TASCRProcInst, ProcInstTASCRParam>
    {
        [HttpGet]
        public HttpResponseMessage Get()
        {
            var param = ParseProcInstParam<ProcInstTASCRParam>();
            var procInsts = processInst.GetList(ParseProcInstParam<ProcInstTASCRParam>(), ReportPath);
            return Request.CreateResponse(HttpStatusCode.OK, new { count = param.TotalRecord, result = procInsts });
        }

        [HttpGet]
        [Route("export")]
        public HttpResponseMessage Export()
        {
            byte[] buffer = processInst.Export(ParseProcInstParam<ProcInstTASCRParam>(), ReportPath, Extension);
            return ExportFile(buffer, "TASCR_PROCESS_INSTANCE");
        }
    }
}