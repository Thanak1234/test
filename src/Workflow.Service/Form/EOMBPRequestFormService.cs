using Workflow.Business;
using Workflow.DataAcess.Infrastructure;
using Workflow.Domain.Entities.BatchData;

namespace Workflow.Service
{
    public class EOMBPRequestFormService : AbstractRequestFormService<IEOMBPRequestFormBC, EOMBPRequestWorkflowInstance>
    {
        public EOMBPRequestFormService()
        {
            var workflow = DbContextSelector.getInstance().getDbFactory(DbContextSelector.DbName.Workflow);
            var docWorkflow = DbContextSelector.getInstance().getDbFactory(DbContextSelector.DbName.WorkflowDoc);
            BC  = new EOMBPRequestFormBC(workflow, docWorkflow);
        }
    }
}
