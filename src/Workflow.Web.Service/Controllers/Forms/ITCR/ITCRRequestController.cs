using Workflow.Domain.Entities.BatchData;
using Workflow.Service;
using Workflow.Web.Service.Controllers.Common;
using Workflow.Core.Attributes;

namespace Workflow.Web.Service.Controllers
{
    [JsonFormat]
    public class ITCRRequestController : GenericController<ITCRequestWorkflowInstance, ITCRRequestFormService> {
        public ITCRRequestController() { }
    }
}
