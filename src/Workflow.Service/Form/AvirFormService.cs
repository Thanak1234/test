using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.Business.AvdrForm;
using Workflow.Business.AvirForm;
using Workflow.Business.MwoRequestForm;
using Workflow.DataAcess.Infrastructure;
using Workflow.DataAcess.Repositories;
using Workflow.DataAcess.Repositories.Interfaces;
using Workflow.DataAcess.Repositories.MWO;
using Workflow.DataObject;
using Workflow.Domain.Entities.MWO;
using Workflow.Domain.Entities.BatchData;
using Workflow.Service.Interfaces;

namespace Workflow.Service {
    public class AvirFormService : AbstractRequestFormService<IAvirFormBC, AvirFormWorkflowInstance>, IAvirFormService {

        public AvirFormService() {
            var workflow = DbContextSelector.getInstance().getDbFactory(DbContextSelector.DbName.Workflow);
            var docWorkflow = DbContextSelector.getInstance().getDbFactory(DbContextSelector.DbName.WorkflowDoc);
            BC = new AvirFormBC(workflow, docWorkflow);
        }
    }
}
