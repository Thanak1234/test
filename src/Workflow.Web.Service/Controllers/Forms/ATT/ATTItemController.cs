/**
*@author : Phanny
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Workflow.DataObject.AV;
using Workflow.Service;
using Workflow.Service.Interfaces;

namespace Workflow.Web.Service.Controllers
{
    [RoutePrefix("api/attitem")]
    public class ATTItemController : ApiController
    {
        [HttpGet]
        [Route("tokenByRequestor/{requestorId}/{purposeId}")]
        public int GetTokenThisYear(int requestorId, int purposeId) {
            IATTRequestFormService _RequestFormService = new ATTRequestFormService();
            return _RequestFormService.GetTakenYears(requestorId, purposeId);
        }
    }
}