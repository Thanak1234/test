using Workflow.Business;
using Workflow.DataAcess.Infrastructure;
using Workflow.Domain.Entities.BatchData;

namespace Workflow.Service
{
    public class TASRequestFormService : AbstractRequestFormService<ITASRequestFormBC, TASCRRequestWorkflowInstance>
    {
        public TASRequestFormService()
        {
            var workflow = DbContextSelector.getInstance().getDbFactory(DbContextSelector.DbName.Workflow);
            var docWorkflow = DbContextSelector.getInstance().getDbFactory(DbContextSelector.DbName.WorkflowDoc);
            BC  = new TASCRRequestFormBC(workflow, docWorkflow);
        }
    }
}
