using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;
using Workflow.Cores.Utils;
using Workflow.DataAcess.Repositories;
using Workflow.DataObject.Dashboard;
using Workflow.Service;
using Workflow.Service.Interfaces;
using Workflow.Web.Service.Models;

namespace Workflow.Web.Service.Controllers
{
    [RoutePrefix("api/dashboard")]
    public class DashboardController : ApiController
    {

        private IDashboardService _DashboardService = null;

        public DashboardController() {
            _DashboardService = new DashboardService();
        }

        [Route("tasks/contributed")]
        public IHttpActionResult GetTasksByLoginName([FromUri] TaskQueryParameter queryParameter) {
            if( string.IsNullOrEmpty(RequestContext.Principal.Identity.Name)) {
                return BadRequest("No User login.");
            }
            if (queryParameter == null) {
                queryParameter = new TaskQueryParameter();
            }

            if(queryParameter.query == null) {
                queryParameter.query = ""; // Get all records from data source
            }

            queryParameter.ContributedBy = SecurityLabel.GetNameWithLabel(RequestContext.Principal.Identity.Name);
            queryParameter.IsAssigned = true;

            var resource = _DashboardService.GetTasksByLoginName(queryParameter);

            return Ok(resource);
        }

        [Route("tasks/submitted")]
        public IHttpActionResult GetSubmittedTasks([FromUri] TaskQueryParameter queryParameter) {
            if (string.IsNullOrEmpty(RequestContext.Principal.Identity.Name)) {
                return BadRequest("No User login.");
            }
            if (queryParameter == null) {
                queryParameter = new TaskQueryParameter();
            }

            if (queryParameter.query == null) {
                queryParameter.query = ""; // Get all records from data source
            }

            queryParameter.SubmittedBy = SecurityLabel.GetNameWithLabel(RequestContext.Principal.Identity.Name);
            queryParameter.IsAssigned = false;

            var resource = _DashboardService.GetTasksByLoginName(queryParameter);

            return Ok(resource);
        }

        [HttpPost]
        [Route("tasks/delete-drafted")]
        public HttpResponseMessage DeleteDraft(int requestHeaderId)
        {
            new Repository().ExecCommand(
                string.Format(@"UPDATE BPMDATA.REQUEST_HEADER SET PROCESS_INSTANCE_ID = -1 WHERE ID = {0}",
                requestHeaderId));

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [Route("tasks/drafted")]
        public HttpResponseMessage GetDraftList()
        {
            var userName = RequestContext.Principal.Identity.Name;
            var result = new Repository().ExecDynamicSqlQuery(string.Format(@"
            SELECT 
	            H.ID Id, 
	            A.PROCESS_NAME workflowName,
	            H.REQUEST_CODE RequestCode,
	            H.LAST_ACTION_DATE LastActionDate, 
	            (R.EMP_NO + ' - ' + R.DISPLAY_NAME) RequestorName,
	            ('/#' + A.NONE_SMART_FORM_URL + '/SN=' + CONVERT(VARCHAR(10), H.ID) + '_00000') RouteUrl
            FROM BPMDATA.REQUEST_HEADER H
            INNER JOIN BPMDATA.REQUEST_APPLICATION A ON A.REQUEST_CODE = H.REQUEST_CODE
            INNER JOIN HR.EMPLOYEE R ON R.ID = H.REQUESTOR
            WHERE H.TITLE IS NULL AND H.PROCESS_INSTANCE_ID = 0 AND REPLACE(H.SUBMITTED_BY, 'K2:', '') = '{0}'", userName));

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
    }
}
