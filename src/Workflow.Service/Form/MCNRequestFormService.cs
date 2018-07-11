using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.Business.MCNRequestForm;
using Workflow.DataAcess.Infrastructure;
using Workflow.DataAcess.Repositories;
using Workflow.DataAcess.Repositories.Interfaces;
using Workflow.DataAcess.Repositories.EGMMachine;
using Workflow.DataObject;
using Workflow.Domain.Entities.EGM;
using Workflow.Domain.Entities.BatchData.EGMInstance;
using Workflow.Service.Interfaces;

namespace Workflow.Service
{

    public class MCNRequestFormService : AbstractRequestFormService<IMCNRequestFormBC,MCNRequestWorkflowInstance>,IMCNFormService
    {

        public MCNRequestFormService()
        {
            BC = new MCNRequestFormBC(
                        DbContextSelector.getInstance().getDbFactory(DbContextSelector.DbName.Workflow), 
                        DbContextSelector.getInstance().getDbFactory(DbContextSelector.DbName.WorkflowDoc)
                     );
        }


        public IEnumerable<MachineIssueType> GetMachineIssueTypeList()
        {
            MCNMachineIssueTypeRepository rep = new MCNMachineIssueTypeRepository(DbContextSelector.getInstance().getDbFactory(DbContextSelector.DbName.Workflow));
            return rep.GetAll().ToList();
        }

    }

}
