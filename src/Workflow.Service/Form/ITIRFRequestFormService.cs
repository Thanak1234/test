using Workflow.Business.IRFRequestForm;
using Workflow.Business.ITCRRequestForm;
using Workflow.DataAcess.Infrastructure;
using Workflow.Domain.Entities.BatchData;
using Workflow.Service.Interfaces;

namespace Workflow.Service
{
    public class ITIRFRequestFormService : AbstractRequestFormService<IIRFRequestForm, IRFWorkflowInstance>, IITIRFRequestFormService {
        public ITIRFRequestFormService() {
            var workflow = DbContextSelector.getInstance().getDbFactory(DbContextSelector.DbName.Workflow);
            var docWorkflow = DbContextSelector.getInstance().getDbFactory(DbContextSelector.DbName.WorkflowDoc);
            BC = new IRFRequestForm(workflow, docWorkflow);
        }
    }
}
