using System;
using System.Collections.Generic;
using Workflow.Business.PBFRequestForm;
using Workflow.DataAcess.Infrastructure;
using Workflow.Domain.Entities.PBF;
using Workflow.Domain.Entities.BatchData;
using Workflow.Service.Interfaces;

namespace Workflow.Service
{
    public class PBFRequestFormService : AbstractRequestFormService<IPBFRequestFormBC, PBFRequestWorkflowInstance>, IPBFRequestFormService
    {
        
        public PBFRequestFormService()
        {
            var workflow = DbContextSelector.getInstance().getDbFactory(DbContextSelector.DbName.Workflow);
            var docWorkflow = DbContextSelector.getInstance().getDbFactory(DbContextSelector.DbName.WorkflowDoc);
            BC  = new PBFRequestFormBC(workflow, docWorkflow);
        }
        
        public IEnumerable<Specification> GetSpecItems(int projectDetailId)
        {
            throw new NotImplementedException();
        }
    }
}
