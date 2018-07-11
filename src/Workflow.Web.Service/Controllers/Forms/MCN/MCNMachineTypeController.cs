using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using Workflow.DataObject;
using Workflow.Domain.Entities.EGM;
using Workflow.Service;
using Workflow.Service.Interfaces;
using Workflow.Web.Service.Controllers.Common;

namespace Workflow.Web.Service.Controllers
{
    [RoutePrefix("api/mcn")]
    public class MCNMachineTypeController : BaseController
    {

        MCNRequestFormService _mcnRequestFormService = null;
        public MCNMachineTypeController()
        {
            _mcnRequestFormService = new MCNRequestFormService();
        }

        [HttpGet]
        [Route("issuetype")]
        public IEnumerable<MachineIssueType> GetWorkTypes()
        {
            return _mcnRequestFormService.GetMachineIssueTypeList();
        }
    }
}
