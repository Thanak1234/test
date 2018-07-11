using Workflow.Business.ITCRRequestForm;
using Workflow.DataAcess.Infrastructure;
using Workflow.Domain.Entities.BatchData;
using Workflow.Service.Interfaces;

namespace Workflow.Service
{
    public class ITCRRequestFormService : AbstractRequestFormService<IRequestForm, ITCRequestWorkflowInstance>, IITCRRequestFormService
    {
        public ITCRRequestFormService() {
            var workflow = DbContextSelector.getInstance().getDbFactory(DbContextSelector.DbName.Workflow);
            var docWorkflow = DbContextSelector.getInstance().getDbFactory(DbContextSelector.DbName.WorkflowDoc);
            BC = new RequestForm(workflow, docWorkflow);
        }
    }
}
