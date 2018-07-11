/**
*@author : Phanny
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Workflow.DataAcess.Repositories;
using Workflow.Domain.Entities;
using Workflow.Domain.Entities.MTF;
using Workflow.Service;
using Workflow.Service.Interfaces;
using Workflow.Web.Service.Models;

namespace Workflow.Web.Service.Controllers
{
 
    [RoutePrefix("api/lookup")]
    public class LookupController : ApiController
    {
        private ILookupService _service;
        public LookupController() {
            _service = new LookupService();
        }

        [HttpGet]
        public IEnumerable<Lookup> Get(string name = "[NAMESPACE]", int parentId = 0, int id = 0)
        {
            var lookupService = new LookupService();

            if (id > 0)
            {
                return lookupService.LookupByName(name, parentId, id);
            }
            else
            {
                return lookupService.LookupByName(name, parentId);
            }
        }
        

        [HttpGet]
        [Route("menu")]
        public HttpResponseMessage getMenu()
        {
            ILookupService lookupService = new LookupService();
            return Request.CreateResponse(HttpStatusCode.OK, lookupService.getMenus(RequestContext.Principal.Identity.Name));
        }

        [HttpGet]
        [Route("medicines")] // MTF
        public HttpResponseMessage GetMedicine()
        {
            var medicines = _service.GetLookups<Medicine>();
            var lookups = (from p in medicines where (p.Active == true) select new {
                medicineId = p.Id,
                name = p.ItemCode,
                label = p.ItemCode + " - " + p.Description
            });

            return Request.CreateResponse(HttpStatusCode.OK, lookups);
        }

        [HttpGet]
        [Route("employees")]
        public HttpResponseMessage GetEmployees()
        {
            return Request.CreateResponse(HttpStatusCode.OK, new Repository().ExecDynamicSqlQuery(
            @"SELECT 
	            E.Id id, 
	            (E.[EMP_NO] + ' - ' + E.[DISPLAY_NAME]) [value],
	            E.[EMP_NO] employeeNo,
	            E.[DISPLAY_NAME] employeeName,
	            LTRIM(RTRIM(D.DEPT_NAME)) department,
	            E.JOB_TITLE position,
	            E.FIRST_NAME firstName,
	            E.LAST_NAME lastName,
	            E.ADDRESS location,
	            E.TELEPHONE phone,
	            E.MOBILE_PHONE mobile,
	            E.EMAIL email
            FROM [HR].[EMPLOYEE] E INNER JOIN [HR].[VIEW_DEPARTMENT] D ON D.TEAM_ID = E.DEPT_ID
            WHERE [ACTIVE] = 1 ORDER BY [DISPLAY_NAME]"));
        }
        
        [HttpGet]
        [Route("it/sessions")]
        public HttpResponseMessage GetITSessions()
        {
            return Request.CreateResponse(HttpStatusCode.OK, new Repository().ExecDynamicSqlQuery(
            @"SELECT ID id, DEPT_SESSION_NAME [name] FROM [ITCR].[DEPT_SESSION] ORDER BY [name]"));
        }

        [HttpGet]
        [Route("itcr/change_request_types")]
        public HttpResponseMessage GetITCRChangeRequestType()
        {
            return Request.CreateResponse(HttpStatusCode.OK, new Repository().ExecDynamicSqlQuery(
            @"SELECT[id], [name] FROM ITCR.CHANGE_REQUEST_TYPE ORDER BY [ORDER]"));
        }

        [HttpGet]
        [Route("itcr/results")]
        public HttpResponseMessage GetResults()
        {
            return Request.CreateResponse(HttpStatusCode.OK, new Repository().ExecDynamicSqlQuery(
            @"SELECT 
                    [RESULT] name,
                    [DESCRIPTION] descr
              FROM [ITCR].[AK_RESULT]")
              );
        }

        [HttpGet]
        [Route("itirf/items")]
        public HttpResponseMessage GetITRIFItems() {
            return Request.CreateResponse(HttpStatusCode.OK, new Repository().ExecDynamicSqlQuery(@"SELECT [ID] id, [ITEM_NAME] [name] FROM [IT].[ITIRF_ITEM]"));
        }

        [HttpGet]
        [Route("itirf/item/models")]
        public HttpResponseMessage GetITRIFItemModels() {
            return Request.CreateResponse(HttpStatusCode.OK, new Repository().ExecDynamicSqlQuery(@"SELECT [ID] id, [ITEM_ID] itemId, [MODEL_NAME] [name] FROM [IT].[ITIRF_ITEM_MODEL]"));
        }

        [HttpGet]
        [Route("report/process")]
        public HttpResponseMessage GetProcess()
        {
            return Request.CreateResponse(HttpStatusCode.OK, new Repository().ExecDynamicSqlQuery(
            @"SELECT 
                (ISNULL(M.MENU_NAME + ' - ','') + REQUEST_DESC) [name],
                REQUEST_CODE [value]
            FROM BPMDATA.REQUEST_APPLICATION A
            LEFT JOIN(
                SELECT DISTINCT M1.MENU_NAME, M.WORK_FLOW FROM [ADMIN].[MENU] M 
                INNER JOIN [ADMIN].[MENU] M1 ON M1.ID = M.PARENT_ID
                WHERE M.IS_WORK_FLOW = 1
            ) M ON M.WORK_FLOW = A.PROCESS_PATH
            WHERE A.ACTIVE = 1
            ORDER BY M.MENU_NAME, REQUEST_DESC"
            ));
        }
    }
}