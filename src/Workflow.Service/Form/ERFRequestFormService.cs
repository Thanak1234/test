using Workflow.Business.ERFRequestForm;
using Workflow.DataAcess.Infrastructure;
using Workflow.Domain.Entities.BatchData;
using Workflow.Service.Interfaces;

namespace Workflow.Service
{
    public class ERFRequestFormService : AbstractRequestFormService<IERFRequestFormBC, ERFRequestWorkflowInstance>, IERFRequestFormService
    {
        private IDbFactory workflow;

        public ERFRequestFormService()
        {
            workflow = DbContextSelector.getInstance().getDbFactory(DbContextSelector.DbName.Workflow);
            var docWorkflow = DbContextSelector.getInstance().getDbFactory(DbContextSelector.DbName.WorkflowDoc);
            BC  = new ERFRequestFormBC(workflow, docWorkflow);
        }
    }
}
