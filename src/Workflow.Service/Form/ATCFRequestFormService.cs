using System;
using System.Collections.Generic;
using Workflow.Business;
using Workflow.DataAcess.Infrastructure;
using Workflow.Domain.Entities.Forms;
using Workflow.Domain.Entities.BatchData;
using Workflow.Service.Interfaces;

namespace Workflow.Service
{
    public class ATCFRequestFormService : AbstractRequestFormService<IATCFRequestFormBC, ATCFRequestWorkflowInstance>, IATCFRequestFormService
    {
        
        public ATCFRequestFormService()
        {
            var workflow = DbContextSelector.getInstance().getDbFactory(DbContextSelector.DbName.Workflow);
            var docWorkflow = DbContextSelector.getInstance().getDbFactory(DbContextSelector.DbName.WorkflowDoc);
            BC  = new ATCFRequestFormBC(workflow, docWorkflow);
        }

        public IEnumerable<AdditionalTimeWorked> GetAdditionalTimeWorkeds(int requestHeaderId)
        {
            throw new NotImplementedException();
        }

    }
}
