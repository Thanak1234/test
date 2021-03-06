﻿using System;
using System.Collections.Generic;
using Workflow.Business;
using Workflow.DataAcess.Infrastructure;
using Workflow.Domain.Entities.Finance;
using Workflow.Domain.Entities.BatchData;
using Workflow.Service.Interfaces;

namespace Workflow.Service
{
    public class RMDRequestFormService : AbstractRequestFormService<IRMDRequestFormBC, RMDRequestWorkflowInstance>, IRMDRequestFormService
    {
        
        public RMDRequestFormService()
        {
            var workflow = DbContextSelector.getInstance().getDbFactory(DbContextSelector.DbName.Workflow);
            var docWorkflow = DbContextSelector.getInstance().getDbFactory(DbContextSelector.DbName.WorkflowDoc);
            BC  = new RMDRequestFormBC(workflow, docWorkflow);
        }
        
    }
}
