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
    [RoutePrefix("api/atd")]
    public class ATDDetailTypeRequestController : BaseController
    {
        ATDRequestFormService atd = null;
        public ATDDetailTypeRequestController()
        {
            atd = new ATDRequestFormService();
        }

        [HttpGet]
        [Route("detailtype")]
        public IEnumerable<AttandanceDetailType> GetWorkTypes()
        {
            return atd.GetAttandanceDetailTypeList();
        }
    }
}