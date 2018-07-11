using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Workflow.ReportingService.Report;
using Workflow.DataObject.Reports;
using Workflow.Web.Service.Controllers.Common;

namespace Workflow.Web.Service.Controllers
{
    [RoutePrefix("api/eombpreport")]
    public class EOMBPReportController : ReportController<EOMBPProcInst, ProcInstEOMBPParam>
    {
        [HttpGet]
        public HttpResponseMessage Get()
        {
            var param = ParseProcInstParam<ProcInstEOMBPParam>();
            var procInsts = processInst.GetList(ParseProcInstParam<ProcInstEOMBPParam>(), ReportPath);
            return Request.CreateResponse(HttpStatusCode.OK, new { count = param.TotalRecord, result = procInsts });
        }

        [HttpGet]
        [Route("export")]
        public HttpResponseMessage Export()
        {
            byte[] buffer = processInst.Export(ParseProcInstParam<ProcInstEOMBPParam>(), ReportPath, Extension);
            return ExportFile(buffer, "EOMBP_PROCESS_INSTANCE");
        }
    }
}