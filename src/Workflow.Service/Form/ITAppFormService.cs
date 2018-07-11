using Workflow.Business.ITAppRequestForm;
using Workflow.DataAcess.Infrastructure;
using Workflow.Domain.Entities.BatchData;
using Workflow.Service.Interfaces;

namespace Workflow.Service {
    public class ITAppFormService : AbstractRequestFormService<IITAppRequestForm, ITAppWorkflowInstance>, IITAppFormService {

        public ITAppFormService() {
            var workflow = DbContextSelector.getInstance().getDbFactory(DbContextSelector.DbName.Workflow);
            var docWorkflow = DbContextSelector.getInstance().getDbFactory(DbContextSelector.DbName.WorkflowDoc);
            BC = new ITAppRequestForm(workflow, docWorkflow);
        }
    }
}
