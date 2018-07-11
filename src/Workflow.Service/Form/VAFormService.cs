using System.Collections.Generic;
using Workflow.Business.ITAppRequestForm;
using Workflow.Business.VARequestForm;
using Workflow.DataAcess.Infrastructure;
using Workflow.DataAcess.Repositories.Interfaces;
using Workflow.DataAcess.Repositories.VAF;
using Workflow.Domain.Entities.VAF;
using Workflow.Domain.Entities.BatchData;
using Workflow.Service.Interfaces;

namespace Workflow.Service {
    public class VAFormService : AbstractRequestFormService<IRequestForm, VAFWorkflowInstance>, IVAFormService {

        public VAFormService() {
            var workflow = DbContextSelector.getInstance().getDbFactory(DbContextSelector.DbName.Workflow);
            var docWorkflow = DbContextSelector.getInstance().getDbFactory(DbContextSelector.DbName.WorkflowDoc);
            BC = new RequestForm(workflow, docWorkflow);
        }        
    }
}
