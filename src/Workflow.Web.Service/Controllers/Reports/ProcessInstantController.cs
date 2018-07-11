using System;
using System.Collections.Generic;
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
    [RoutePrefix("api/processinstant")]
    public class ProcessInstantController : ReportController<ProcInst, ProcInstParam>
    {
        private readonly string _reportPath = "/K2Report/Reports/PROC_INST_CLASSIC";
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
            
            return ExportFile(buffer, "PROCESS_INSTANCE");
        }

        [HttpGet]
        [Route("activities")]
        public HttpResponseMessage GetActivities(string reqCode)
        {  
            string sql = @"  SELECT A.[NAME], A.[NAME] LABEL, A.[TYPE] FROM (
                SELECT [NAME], [NAME] LABEL, WORKFLOW_ID P_ID, 'ACT' [TYPE] FROM [ADMIN].[ACTIVITY] 
                WHERE [NAME] NOT IN('Modification')
                UNION ALL
	            SELECT L.LOOKUP_VALUE [NAME], L.LOOKUP_VALUE [LABEL], L.FORM_ID P_ID, 'TAG' [TYPE]
	            FROM [FORM].[LOOKUP] L WHERE L.[NAMESPACE] = '[REPORT].[PROCESS_INSTANCES].[TAGS]'
                AND '{0}' IN ('NAGAWORLD\yimsamaune', 'NAGAWORLD\mengseakmouy')
            ) A INNER JOIN [BPMDATA].[REQUEST_APPLICATION] P ON P.ID = A.P_ID {1} ORDER BY A.[NAME]";

            Repository repo = new Repository();
            var list = repo.ExecDynamicSqlQuery(string.Format(sql,
                (RequestContext.Principal.Identity.Name),
                (string.IsNullOrEmpty(reqCode)? string.Empty: "WHERE P.REQUEST_CODE = '" + reqCode + "'")
                ));
            
            return Request.CreateResponse(HttpStatusCode.OK, list);
        }

        [HttpGet]
        [Route("tags")]
        public HttpResponseMessage GetTags(string reqCode)
        {
            string sql = @"SELECT L.ID, L.LOOKUP_VALUE [NAME], L.LOOKUP_VALUE [LABEL] FROM [FORM].[LOOKUP] L 
            INNER JOIN [BPMDATA].[REQUEST_APPLICATION] P ON P.ID = L.FORM_ID AND L.[NAMESPACE] = '[REPORT].[PROCESS_INSTANCES].[TAGS]' 
            {0} ORDER BY L.LOOKUP_VALUE";

            Repository repo = new Repository();
            var list = repo.ExecDynamicSqlQuery(string.Format(sql,
                string.IsNullOrEmpty(reqCode) ? string.Empty : "WHERE P.REQUEST_CODE = '" + reqCode + "'" // criteria
                ));

            return Request.CreateResponse(HttpStatusCode.OK, list);
        }

        [HttpGet]
        [Route("tags-request")]
        public HttpResponseMessage GetTagsByRequest(int requestHeaderId)
        {
            string sql = @"SELECT L.ID, L.LOOKUP_VALUE [NAME], L.LOOKUP_VALUE [LABEL] FROM BPMDATA.TAG T 
                INNER JOIN [FORM].[LOOKUP] L ON L.ID = T.LId 
                WHERE T.DeletedBy IS NULL AND T.DeletedDate IS NULL AND RequestHeaderId = {0} ORDER BY L.LOOKUP_VALUE";

            Repository repo = new Repository();
            var list = repo.ExecDynamicSqlQuery(string.Format(sql, requestHeaderId));

            return Request.CreateResponse(HttpStatusCode.OK, list);
        }

        [HttpPost]
        public HttpResponseMessage SaveTags(int requestHeaderId, string tagIds)
        {
            try
            {
                Repository repo = new Repository();
                var list = repo.ExecDynamicSqlQuery(
                    string.Format(@"EXEC [BPMDATA].[SAVE_TAGS] @RequestHeaderId = {0}, @TagIds = '{1}', @Username = '{2}'",
                    requestHeaderId, tagIds, (RequestContext.Principal.Identity.Name)
                    ));
                
                return Request.CreateResponse(HttpStatusCode.OK, list);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.ExpectationFailed, e.ToAllMessages());
            }
        }
    }
}