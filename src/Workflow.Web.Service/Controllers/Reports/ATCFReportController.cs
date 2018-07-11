using Newtonsoft.Json;
using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using Workflow.ReportingService.Report;
using Workflow.DataAcess.Repositories;
using Workflow.DataObject.Reports;
using Workflow.Web.Service.Controllers.Common;

namespace Workflow.Web.Service.Controllers
{
    [RoutePrefix("api/atcfreport")]
    public class ATCFReportController : ReportController<ATCFProcInst, ProcInstATCFParam>
    {
        private readonly string _summaryReportPath = "/REPORTS/SUMMARY/ADDITIONAL_WORK_REPORT";
        private readonly string SQL_STR_SUMMARY_RPT  = @"SELECT A.* FROM HR.ADDITIONAL_WORK_SUMMARY A 
                WHERE A.WorkingMonth = {0} AND A.WorkingYear = {1}
                {2} {3} ORDER BY EmployeeId, [WorkingYear], [WorkingMonth]";

        [HttpGet]
        public HttpResponseMessage Get()
        {
            var param = ParseProcInstParam<ProcInstATCFParam>();
            var procInsts = processInst.GetList(param, ReportPath);
            return Request.CreateResponse(HttpStatusCode.OK, new { count = param.TotalRecord, result = procInsts });
        }

        [HttpGet]
        [Route("export")]
        public HttpResponseMessage Export()
        {
            byte[] buffer = processInst.Export(ParseProcInstParam<ProcInstATCFParam>(), ReportPath, Extension);
            return ExportFile(buffer, "ATCF_PROCESS_INSTANCE");
        }

        [HttpGet]
        [Route("summary")]
        public HttpResponseMessage GetSummaryResult([FromUri] DateTime dateFrom, DateTime dateTo, 
            string deptIds, int? employeeId)
        {
            if (dateFrom != null && dateTo != null) {
                var param = new ProcInstATCFSummaryParam()
                {
                    MonthFrom = dateFrom.Month,
                    MonthTo = dateTo.Month,
                    YearFrom = dateFrom.Year,
                    YearTo = dateTo.Year
                };

               
                var repository = new Repository();
                
                
                var result = repository.ExecDynamicSqlQuery(
                    string.Format(SQL_STR_SUMMARY_RPT,
                    param.MonthTo, param.YearTo,
                    !string.IsNullOrEmpty(deptIds) ? (" AND A.DeptId IN (" + deptIds + ")") : string.Empty,
                    (employeeId ?? 0) > 0 ? (" AND A.EmployeeId = " + employeeId) : string.Empty
                    )).ToList();

                return Request.CreateResponse(HttpStatusCode.OK, new { result});
              
            }
            return Request.CreateResponse(HttpStatusCode.OK, new { });
        }

        [HttpGet]
        [Route("summary/export")]
        public HttpResponseMessage GetSummaryExcel([FromUri] DateTime dateFrom, DateTime dateTo,
            string deptIds, int? employeeId, string deptNames) {
            if (dateFrom != null && dateTo != null)
            {
                var param = new ProcInstATCFSummaryParam()
                {
                    MonthFrom = dateFrom.Month,
                    MonthTo = dateTo.Month,
                    YearFrom = dateFrom.Year,
                    YearTo = dateTo.Year,
                    DeptNames = deptNames,
                    SqlQuery = string.Format(SQL_STR_SUMMARY_RPT,
                        dateTo.Month, dateTo.Year,
                        !string.IsNullOrEmpty(deptIds) ? (" AND A.DeptId IN (" + deptIds + ")") : string.Empty,
                        (employeeId ?? 0) > 0 ? (" AND A.EmployeeId = " + employeeId) : string.Empty
                    )
                };
                byte[] buffer = processInst.Export(param, _summaryReportPath, Extension);
                return ExportFile(buffer, "ATCF_SUMMARY");
            }
            return Request.CreateResponse(HttpStatusCode.OK, new { });
        }
    }
}