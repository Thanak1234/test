using Workflow.Business.AccessCardRequest;
using Workflow.DataAcess.Infrastructure;
using Workflow.Domain.Entities.BatchData;
using Workflow.Service.Interfaces;

namespace Workflow.Service {
    public class RACRequestFormService : AbstractRequestFormService<IRequestForm, RACWorkflowInstance>, IRACRequestFormService {

        public RACRequestFormService() {
            var workflow = DbContextSelector.getInstance().getDbFactory(DbContextSelector.DbName.Workflow);
            var docWorkflow = DbContextSelector.getInstance().getDbFactory(DbContextSelector.DbName.WorkflowDoc);
            BC = new RequestForm(workflow, docWorkflow);
        }        
    }
}
