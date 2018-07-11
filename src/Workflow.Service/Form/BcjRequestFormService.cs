using System;
using System.Collections.Generic;
using Workflow.Business.BcjRequestForm;
using Workflow.DataAcess.Infrastructure;
using Workflow.DataAcess.Repositories.BCJ;
using Workflow.Domain.Entities.BCJ;
using Workflow.Domain.Entities.BatchData;
using Workflow.Service.Interfaces;

namespace Workflow.Service {
    public class BcjRequestFormService : AbstractRequestFormService<IBcjRequestFormBC, BcjRequestWorkflowInstance>, IBcjRequestFormService {

        public BcjRequestFormService() {
            var workflow = DbContextSelector.getInstance().getDbFactory(DbContextSelector.DbName.Workflow);
            var docWorkflow = DbContextSelector.getInstance().getDbFactory(DbContextSelector.DbName.WorkflowDoc);
            BC = new BcjRequestFormBC(workflow, docWorkflow);

        }

        public AnalysisItem GetAnalysisItem(int id) {
            throw new NotImplementedException();
        }

        public IEnumerable<AnalysisItem> GetAnalysisItems(int projectDetailId) {
            throw new NotImplementedException();
        }

        public BcjRequestItem GetRequestItem(int id) {
            throw new NotImplementedException();
        }

        public IEnumerable<BcjRequestItem> GetRequestItems(int projectDetailId) {
            throw new NotImplementedException();
        }
    }
}
