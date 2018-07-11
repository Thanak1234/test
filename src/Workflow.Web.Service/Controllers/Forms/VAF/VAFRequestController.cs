using Newtonsoft.Json.Linq;
using Workflow.Domain.Entities.Core.ITApp;
using Workflow.Domain.Entities.BatchData;
using Workflow.Service;
using Workflow.Service.Interfaces;
using Workflow.Web.Service.Controllers.Common;
using Workflow.Web.Models;
using Workflow.Core.Collection;

namespace Workflow.Web.Service.Controllers {

    public class VAFRequestController : GenericController<VAFWorkflowInstance, VAFormService>   {

        public VAFRequestController() : base(){ }
    }
}