using Workflow.Business.AdmsrRequestForm;
using Workflow.Business.OSHARequestForm;
using Workflow.DataAcess.Infrastructure;
using Workflow.Domain.Entities.BatchData;
using Workflow.Service.Interfaces;

namespace Workflow.Service
{
    public class AdmsrRequestFormService : AbstractRequestFormService<IRequestForm, AdmsrRequestWorkflowInstance>, IAdmsrRequestFormService
    {
        public AdmsrRequestFormService() {
            var workflow = DbContextSelector.getInstance().getDbFactory(DbContextSelector.DbName.Workflow);
            var docWorkflow = DbContextSelector.getInstance().getDbFactory(DbContextSelector.DbName.WorkflowDoc);
            BC = new RequestForm(workflow, docWorkflow);
        }
    }
}
