﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using Workflow.DataAcess.Repositories;
using Workflow.DataObject;
using Workflow.Domain.Entities.MWO;
using Workflow.Service;
using Workflow.Service.Interfaces;
using Workflow.Web.Service.Controllers.Common;

namespace Workflow.Web.Service.Controllers
{
    [RoutePrefix("api/mwoitem")]
    public class MwoItemController : BaseController 
    {
        private IMwoRequestFormService _MwoRequestFormService;

        public MwoItemController() {
            _MwoRequestFormService = new MwoRequestFormService();
        }

        [HttpGet]
        [Route("workTypes")]
        public IEnumerable<WorkType> GetWorkTypes() {
            return _MwoRequestFormService.GetWorkTypes();
        }


        [HttpGet]
        [Route("requestTypes")]
        public IEnumerable<RequestType> GetRequestTypes() {
            return _MwoRequestFormService.GetRequestTypes();
        }

        [HttpGet]
        [Route("modes")]
        public IEnumerable<Mode> GetModes() {
            return _MwoRequestFormService.GetModes();
        }

        [HttpGet]
        [Route("departmentChargables")]
        public HttpResponseMessage GetDepartmentChargables() {
            return Request.CreateResponse(HttpStatusCode.OK, new Repository().ExecDynamicSqlQuery(@"SELECT ID id, [LOCATION] loc,[CCD] ccd,[DEPARTMENT] dept,[SEQUENCE] seq,[CURRENT_NUMBER] curnum FROM [MAINTENANCE].[MWO_DEPARTMENT_CHARGABLE]"));
        }

        [HttpGet]
        [Route("sublocations")]
        public HttpResponseMessage GetSubLocations()
        {
            return Request.CreateResponse(HttpStatusCode.OK, new Repository().ExecDynamicSqlQuery(@"SELECT LOCATION loc, SUBLOCATION sub FROM MAINTENANCE.MWO_SUBLOCATION ORDER BY SUBLOCATION"));
        }

        [HttpGet]
        [Route("technicians")]
        public HttpResponseMessage GetTechnicians()
        {
            return Request.CreateResponse(HttpStatusCode.OK, new Repository().ExecDynamicSqlQuery(@"SELECT (EMP_NO + ' - ' + LTRIM(RTRIM(DISPLAY_NAME))) displayMember, ID valueMember FROM HR.VIEW_EMPLOYEE WHERE TEAM_ID IN (212,371)"));
        }

        [HttpGet]
        [Route("refNumByCCD")]
        public HttpResponseMessage GetReferenceNumber(string ccd) {
            try {
                var response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(_MwoRequestFormService.GetReferenceNumber(ccd));
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/plain");
                return response;
            } catch (SmartException ex) {
                return CreateErrorMessageResponse(ex);
            }
        }
    }
}
