using System;
using System.Collections.Generic;
using Workflow.Business;
using Workflow.DataAcess.Infrastructure;
using Workflow.DataObject.MTF;
using Workflow.Domain.Entities.MTF;
using Workflow.Domain.Entities.BatchData;
using Workflow.Service.Interfaces;

namespace Workflow.Service
{
    public class MTFRequestFormService : AbstractRequestFormService<IMTFRequestFormBC, MTFRequestWorkflowInstance>, IMTFRequestFormService
    {
        
        public MTFRequestFormService()
        {
            var workflow = DbContextSelector.getInstance().getDbFactory(DbContextSelector.DbName.Workflow);
            var docWorkflow = DbContextSelector.getInstance().getDbFactory(DbContextSelector.DbName.WorkflowDoc);
            BC  = new MTFRequestFormBC(workflow, docWorkflow);
        }

        public IEnumerable<Prescription> GetPrescriptionItems(int requestHeaderId)
        {
            throw new NotImplementedException();
        }

    }
}
