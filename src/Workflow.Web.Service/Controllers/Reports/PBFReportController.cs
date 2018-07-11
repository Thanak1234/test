using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Workflow.ReportingService.Report;
using Workflow.DataObject.Reports;
using Workflow.Web.Service.Controllers.Common;

namespace Workflow.Web.Service.Controllers
{
    [RoutePrefix("api/pbfreport")]
    public class PBFReportController : ReportController<PBFProcInst, ProcInstPBFParam>
    {
        [HttpGet]
        public HttpResponseMessage Get()
        {
            var param = ParseProcInstParam<ProcInstPBFParam>();
            var procInsts = processInst.GetList(ParseProcInstParam<ProcInstPBFParam>(), ReportPath);
            return Request.CreateResponse(HttpStatusCode.OK, new { count = param.TotalRecord, result = procInsts });
        }

        [HttpGet]
        [Route("export")]
        public HttpResponseMessage Export()
        {
            byte[] buffer = processInst.Export(ParseProcInstParam<ProcInstPBFParam>(), ReportPath, Extension);
            return ExportFile(buffer, "PBF_PROCESS_INSTANCE");
        }
    }
}