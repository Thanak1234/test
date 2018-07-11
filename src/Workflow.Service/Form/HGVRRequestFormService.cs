using System;
using System.Collections.Generic;
using Workflow.Business;
using Workflow.DataAcess.Infrastructure;
using Workflow.Domain.Entities.Forms;
using Workflow.Domain.Entities.BatchData;
using Workflow.Service.Interfaces;

namespace Workflow.Service
{
    public class HGVRRequestFormService : AbstractRequestFormService<IHGVRRequestFormBC, HGVRRequestWorkflowInstance>, IHGVRRequestFormService
    {
        
        public HGVRRequestFormService()
        {
            var workflow = DbContextSelector.getInstance().getDbFactory(DbContextSelector.DbName.Workflow);
            var docWorkflow = DbContextSelector.getInstance().getDbFactory(DbContextSelector.DbName.WorkflowDoc);
            BC  = new HGVRRequestFormBC(workflow, docWorkflow);
        }
        
    }
}
