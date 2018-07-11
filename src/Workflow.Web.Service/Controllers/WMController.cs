using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Workflow.DataObject.WM;
using Workflow.Service;
using Workflow.Service.Interfaces;
using Workflow.Web.Service.Controllers.Common;

namespace Workflow.Web.Service.Controllers
{
    [RoutePrefix("api/wms")]
    public class WMController : BaseController {

        private IWorklistService service;

        public WMController() {
            service = new WorklistService();
        }

        [Route("byCriteria")]
        public HttpResponseMessage Get([FromUri] WMQueryParameter parameter) {
            try {
                return Request.CreateResponse(HttpStatusCode.OK, service.GetWorklists(parameter));
            } catch(Exception ex) {
                return CreateErrorMessageResponse(ex);
            }
        }
    }
}
