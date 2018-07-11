using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Workflow.Business;
using Workflow.Core.Attributes;
using Workflow.DataAcess.Repositories;
using Workflow.DataContract.K2;
using Workflow.DataObject;
using Workflow.DataObject.Worklists;
using Workflow.Service;
using Workflow.Service.Interfaces;
using Workflow.Web.Models.Worklists;
using Workflow.Web.Service.Controllers.Common;

namespace Workflow.Web.Service.Controllers
{
    [RoutePrefix("api/worklists")]
    public class WorklistsController : BaseController {

        private IWorklistService _service;

        private EmployeeDto _employee;

        public WorklistsController() {        
            _service = new WorklistService(RequestContext.Principal.Identity.Name);
            _employee = new EmployeeService().GetEmpByLoginName(RequestContext.Principal.Identity.Name);
        }


        
        [Route("test")]
        public object GetTest(string param)
        {
            return _service.RunTest(param);
        }

        [Route("imageflow")]
        public HttpResponseMessage GetViewFlowImage(int procInstId) {
            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new StringContent(Convert.ToBase64String(_service.GetImageStream(procInstId)));
            return result;
        }

        [Route("flowjson")]
        public HttpResponseMessage GetJson(int procInstId) {
            return Request.CreateResponse(_service.GetJson(procInstId));
        }

        [Route("approvers")]
        public HttpResponseMessage GetApprovers(int procInstId) {
            var approvers = _service.GetApprovers(procInstId);
            return Request.CreateResponse(HttpStatusCode.OK, approvers);
        }

        [Route("participants")]
        public HttpResponseMessage GetParticipants(int procInstId, int actInstId = 0) {
            var participants = _service.GetParticipants(procInstId, actInstId);
            return Request.CreateResponse(HttpStatusCode.OK, participants);
        }

        [Route("audits")]
        public HttpResponseMessage GetProcInstAudits(int procInstId) {
            try {
                return Request.CreateResponse(HttpStatusCode.OK, _service.GetProcInstAudits(procInstId));
            } catch (SmartException ex) {
                return CreateErrorMessageResponse(ex);
            }
        }

        [JsonFormat]
        public async Task<HttpResponseMessage> Get() {

            try {
                return Request.CreateResponse(HttpStatusCode.OK, await _service.GetWorklistWrapper());
            } catch (SmartException ex) {
                return CreateErrorMessageResponse(ex);
            }
        }

        [Route("worklist-item")]
        public object GetWorklistItem(int procInstId)
        {
            try
            {
                var worklistItem = _service.GetWorklistItem(procInstId);
                return Request.CreateResponse(HttpStatusCode.OK, worklistItem);
            }
            catch (SmartException ex)
            {
                return CreateErrorMessageResponse(ex);
            }
        }

        [Route("workflows")]
        public List<WorkflowDto> GetWorkflows() {
            return _service.GetWorkflows();
        }

        [Route("processes")]
        public List<ProcessDto> GetProcesses() {
            return _service.GetProcesses();
        }

        [Route("activities")]
        public List<ActivityDto> GetActivities() {
            return _service.GetActivities();
        }

        [Route("oofs/{limit}")]
        public HttpResponseMessage GetOOFs(int limit) {
            try {
                return Request.CreateResponse(HttpStatusCode.OK, _service.GetShareWorklist());
            } catch (SmartException ex) {
                return CreateErrorMessageResponse(ex);
            }       
        }

        [HttpPost]
        [Route("SetOutOfOffice")]
        public HttpResponseMessage SetOutOfOffice([FromBody] OOFWrapper wrapper) {
            try {
                return Request.CreateResponse(HttpStatusCode.OK, _service.SetOutOfOffice(wrapper));
            } catch (SmartException ex) {
                return CreateErrorMessageResponse(ex);
            }
        }

        [HttpGet]
        [Route("SharedUsers")]
        public HttpResponseMessage GetSharedUsers(string serialNumber) {
            try {
                return Request.CreateResponse(HttpStatusCode.OK, _service.GetSharedUsers(serialNumber)); 
            } catch (SmartException ex) {
                return CreateErrorMessageResponse(ex);
            }            
        }

        [HttpPost]
        [Route("SetSharedUsers")]
        public HttpResponseMessage SetSharedUsers(string serialNumber, List<DestinationDto> destinations, string comment, int requestHeaderId) {
            try {
                var act = new Domain.Entities.ActivityHistory
                {
                    RequestHeaderId = requestHeaderId,
                    ActInstId = 0,
                    Activity = "Escalation",
                    Approver = RequestContext.Principal.Identity.Name,
                    Decision = "Shared",
                    Comments = comment
                };
                if (_employee != null) {
                    act.ApproverDisplayName = _employee.fullName;
                }
                
                return Request.CreateResponse(HttpStatusCode.OK, _service.SetSharedUsers(serialNumber, destinations, act));
            } catch (SmartException ex) {
                return CreateErrorMessageResponse(ex);
            }
        }

        [HttpPost]
        [Route("Redirect")]
        public HttpResponseMessage Redirect(string serialNumber, [FromBody] DestinationDto user, string comment, int requestHeaderId) {
            try {
                var act = new Domain.Entities.ActivityHistory
                {
                    RequestHeaderId = requestHeaderId,
                    ActInstId = 0,
                    Activity = "Escalation",
                    Approver = RequestContext.Principal.Identity.Name,
                    Decision = "Redirected",
                    Comments = comment
                };
                if (_employee != null)
                {
                    act.ApproverDisplayName = _employee.fullName;
                }

                return Request.CreateResponse(HttpStatusCode.OK, _service.Redirect(serialNumber, user, act));
            } catch (SmartException ex) {
                return CreateErrorMessageResponse(ex);
            }
        }

        [HttpGet]
        [Route("Release")]
        public HttpResponseMessage Release(string serialNumber) {
            try {
                _service.Release(serialNumber);
                return Request.CreateResponse(HttpStatusCode.OK, "Success");
            } catch (SmartException ex) {
                return CreateErrorMessageResponse(ex);
            }            
        }

        //[HttpPost]
        //[Route("forcerelease/{serialNumber}")]
        //public HttpResponseMessage ForceRelease(string serialNumber) {
        //    try {
        //        return Request.CreateResponse(HttpStatusCode.OK, _WorklistService.ForceRelease(serialNumber));
        //    } catch (SmartException ex) {
        //        return CreateErrorMessageResponse(ex);
        //    }
        //}

        [HttpPost]
        [Route("Sleep")]
        public HttpResponseMessage SleepWorklistItem(SleepViewModel viewModel) {
            try {
                return Request.CreateResponse(HttpStatusCode.OK, "Success");
            } catch (SmartException ex) {
                return CreateErrorMessageResponse(ex);
            }
        }

        [HttpGet]
        [Route("ExecuteAction")]
        public HttpResponseMessage ExecuteAction(string serialNumber, string actionName, string sharedUser, string managedUser) {
            try {
                _service.ExecuteAction(serialNumber, actionName, sharedUser, managedUser);
                return Request.CreateResponse(HttpStatusCode.OK, "Success");
            } catch (SmartException ex) {
                return CreateErrorMessageResponse(ex);
            }         
        }

        [HttpGet]
        [Route("BasicOptions")]
        public ResourceWrapper GetSleepBasicOptions() {
            ResourceWrapper wrapper = new ResourceWrapper();

            IList<object> options = new List<object>();
            options.Add(new { Name = "One day", Duration = 24 });
            options.Add(new { Name = "Two days", Duration = 48 });
            options.Add(new { Name = "Three days", Duration = 72 });

            wrapper.TotalRecords = options.Count;

            wrapper.Records = options;

            return wrapper;
        }

        [HttpGet]
        [Route("retryError")]
        public object RetryError(int procInstId, int procId)
        {
            return new { success = _service.RetryError(procInstId, procId) };
        }

        [HttpGet]
        [Route("execute")]
        public object ImpassinatAction(int procInstId, string user, string actionName)
        {
            var engin = new WorkflowEngine("K2:NAGAWORLD\\" + user);
            string sqlString = @"SELECT 
	            (CONVERT(VARCHAR(20), ProcInstID) + '_' + CONVERT(VARCHAR(20), ActInstDestID)) SerialNo
            FROM K2.[ServerLog].Worklist WHERE [Status] = 0 AND ProcInstID IN ({0}) AND [Destination] = 'K2:NAGAWORLD\{1}'";
            
            string serialNo = new Repository().ExecSingle<string>(String.Format(sqlString, procInstId, user));
            engin.Execute(serialNo, actionName);
            return new { success = true, serialNo, actionName, user, procInstId };
        }

        

        [HttpGet]
        [JsonFormat]
        [Route("procVersions")]
        public object procVersions(int procInstId = 0)
        {
            var sql = @"SELECT 
	            P.Id,
	            PS.[Name],
	            PS.[FullName],
	            ('With - V' + CONVERT(VARCHAR(5), P.Ver)) [Version]
            FROM [K2].[Server].[Proc] P
            INNER JOIN [K2].[Server].[ProcSet] PS ON PS.ID = P.ProcSetID
            WHERE 0 = {0} OR FullName IN (
	            SELECT A.PROCESS_PATH FROM BPMDATA.REQUEST_HEADER H 
	            INNER JOIN BPMDATA.REQUEST_APPLICATION A ON A.REQUEST_CODE = H.REQUEST_CODE 
	            WHERE H.PROCESS_INSTANCE_ID = {1}
            )
            ORDER BY FullName ASC, P.Ver DESC";
            
            return ExecDynamicSqlQuery(string.Format(sql, procInstId, procInstId));
        }
    }
}
