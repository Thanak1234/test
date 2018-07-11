using Workflow.Domain.Entities.BatchData;
using Workflow.Service;
using Workflow.Web.Service.Controllers.Common;

namespace Workflow.Web.Service.Controllers.Forms {

    public class CCRRequestController : GenericController<CCRWorkflowInstance, CCRRequestFormService>   {

        public CCRRequestController() : base(){ }
    }
}