using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Workflow.DataObject;
using Workflow.Service;
using Workflow.Service.Interfaces;

namespace Workflow.Web.Service.Controllers
{
    public class VAFServicesController : ApiController
    {
        private IRequestHeaderService requestHeaderService;

        public VAFServicesController() {
            requestHeaderService = new RequestHeaderService();
        }

        public HttpResponseMessage Get([FromUri]QueryParameter queryParam) {
            return Request.CreateResponse(HttpStatusCode.OK, requestHeaderService.GetIncidentRequestHeaders(queryParam));
        }
    }
}
