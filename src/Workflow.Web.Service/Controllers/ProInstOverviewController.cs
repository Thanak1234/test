using System.Web.Http;
using Workflow.DataObject;
using Workflow.DataObject.ProcInstOverview;
//using Workflow.SmartObjects;
//using Workflow.SmartObjects.SO;

namespace Workflow.Web.Service.Controllers
{
    [RoutePrefix("api/procInstOv")]
    public class ProInstOverviewController : ApiController
    {
        [HttpGet]
        [Route("activities")]
        public ResourceWrapper GetActivities([FromUri] ProcInstOverviewQueryParam param) {
            //ISmartObjectFactory soFactory = new SmartObjectFactory();
            //ActivityInstance actInst = soFactory.GetActivityInstance();
            //actInst.ProcessInstanceID = param.procInstId;
            //DataTable result = actInst.ActivityInstanceDetail();
            //var records = result.ToList<ActivityInstanceDto>();
            ResourceWrapper resource = new ResourceWrapper();
            //resource.Records = records;
            //resource.TotalRecords = records.Count;
            return resource;
        }

        [HttpGet]
        [Route("audits")]
        public ResourceWrapper GetAudits([FromUri] ProcInstOverviewQueryParam param) {
            //ISmartObjectFactory soFactory = new SmartObjectFactory();
            //ActivityInstanceAudit actInstAudit = soFactory.GetActivityInstanceAudit();
            //actInstAudit.ProcInstID = param.procInstId;
            //actInstAudit.ActInstID = param.actInstId;
            //DataTable result = actInstAudit.ListActivityInstanceAudit();
            //var records = result.ToList<ActivityInstanceAuditDto>();
            ResourceWrapper resource = new ResourceWrapper();
            //resource.Records = records;
            //resource.TotalRecords = records.Count;
            return resource;
        }

        [HttpGet]
        [Route("performances")]
        public ResourceWrapper GetUserPerforms([FromUri] ProcInstOverviewQueryParam param) {
            //ISmartObjectFactory soFactory = new SmartObjectFactory();
            //UserPerformance userPerformance = soFactory.GetUserPerformance();
            //userPerformance.ProcessInstanceID = param.procInstId;
            //userPerformance.ActivityName = param.actName;
            //userPerformance.ProcessSetFullName = param.procFullName;
            //DataTable result = userPerformance.UserPerformanceDetail(param.procFullName, param.actName);
            //var records = result.ToList<UserPerformanceDto>();
            ResourceWrapper resource = new ResourceWrapper();
            //resource.Records = records;
            //resource.TotalRecords = records.Count;
            return resource;
        }
    }
}
