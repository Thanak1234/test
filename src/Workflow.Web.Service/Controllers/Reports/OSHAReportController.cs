using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Workflow.ReportingService.Report;
using Workflow.DataAcess.Repositories;
using Workflow.DataObject.Reports;
using Workflow.Web.Service.Controllers.Common;

namespace Workflow.Web.Service.Controllers
{
    [RoutePrefix("api/OSHAreport")]
    public class OSHAReportController : ReportController<OSHAProcInst, ProcInstOSHAParam>
    {
        [HttpGet]
        public HttpResponseMessage Get()
        {
            var param = ParseProcInstParam<ProcInstOSHAParam>();
            var procInsts = processInst.GetList(param, ReportPath);
            return Request.CreateResponse(HttpStatusCode.OK, new { count = param.TotalRecord, result = procInsts });
        }

        [HttpGet]
        [Route("export")]
        public HttpResponseMessage Export()
        {
            byte[] buffer = processInst.Export(ParseProcInstParam<ProcInstOSHAParam>(), ReportPath, Extension);
            return ExportFile(buffer, "OSHA_PROCESS_INSTANCE");
        }

        [HttpGet]
        [Route("victim-witness")]
        public HttpResponseMessage GetVictimWitness(string type) {
            var repo = new Repository();
            string sql = @"SELECT EMP_NO + ' - ' + EMP_NAME FROM [OSHA].[EMPLOYEES] 
                WHERE EMP_TYPE = '{0}' /*VICTIMS WITHNESS*/
                GROUP BY EMP_NO, EMP_NAME";

            var result = repo.ExecDynamicSqlQuery(string.Format(sql, type));
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
    }
}