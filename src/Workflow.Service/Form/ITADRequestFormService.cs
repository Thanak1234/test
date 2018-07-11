using Workflow.Business.ITADRequestForm;
using Workflow.DataAcess.Infrastructure;
using Workflow.Domain.Entities.BatchData;
using Workflow.Service.Interfaces;

namespace Workflow.Service
{
    public class ITADRequestFormService : AbstractRequestFormService<IRequestForm, ITADRequestWorkflowInstance>, IITADRequestFormService
    {
        public ITADRequestFormService() {
            var workflow = DbContextSelector.getInstance().getDbFactory(DbContextSelector.DbName.Workflow);
            var docWorkflow = DbContextSelector.getInstance().getDbFactory(DbContextSelector.DbName.WorkflowDoc);
            BC = new RequestForm(workflow, docWorkflow);
        }
    }
}
