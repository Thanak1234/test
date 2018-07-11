using Workflow.DataAcess.Infrastructure;
using Workflow.Domain.Entities.BatchData;
using Workflow.Service.Interfaces;
using Workflow.Business.CCRRequestForm;

namespace Workflow.Service {
    public class CCRRequestFormService : AbstractRequestFormService<ICCRRequestForm, CCRWorkflowInstance>, ICCRRequestFormService {

        public CCRRequestFormService() {
            var workflow = DbContextSelector.getInstance().getDbFactory(DbContextSelector.DbName.Workflow);
            var docWorkflow = DbContextSelector.getInstance().getDbFactory(DbContextSelector.DbName.WorkflowDoc);
            BC = new CCRRequestForm(workflow, docWorkflow);
        }        
    }
}
