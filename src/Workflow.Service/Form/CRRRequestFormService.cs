using Workflow.Business.CRRRequestForm;
using Workflow.DataAcess.Infrastructure;
using Workflow.Domain.Entities.BatchData;
using Workflow.Service.Interfaces;

namespace Workflow.Service
{
    public class CRRRequestFormService : AbstractRequestFormService<ICRRRequestFormBC, CRRRequestWorkflowInstance>, ICRRRequestFormService
    {
        private IDbFactory workflow;

        public CRRRequestFormService()
        {
            workflow = DbContextSelector.getInstance().getDbFactory(DbContextSelector.DbName.Workflow);
            var docWorkflow = DbContextSelector.getInstance().getDbFactory(DbContextSelector.DbName.WorkflowDoc);
            BC  = new CRRRequestFormBC(workflow, docWorkflow);
        }
    }
}
