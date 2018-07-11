using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.Business.ICDRequestForm;
using Workflow.DataAcess.Infrastructure;
using Workflow.DataAcess.Repositories;
using Workflow.DataAcess.Repositories.Interfaces;
using Workflow.DataAcess.Repositories.Incident;
using Workflow.DataObject;
using Workflow.Domain.Entities.MWO;
using Workflow.Domain.Entities.BatchData.IncidentInstance;
using Workflow.Service.Interfaces;


namespace Workflow.Service
{
    public class ICDRequestFormService : AbstractRequestFormService<IICDRequestFormBC,ICDRequestWorkflowInstance>,IICDFormService
    {
        public ICDRequestFormService()
        {
            BC = new ICDRequestFormBC(
                                        DbContextSelector.getInstance().getDbFactory(DbContextSelector.DbName.Workflow), 
                                        DbContextSelector.getInstance().getDbFactory(DbContextSelector.DbName.WorkflowDoc)
                                     );
        }
    }
}
