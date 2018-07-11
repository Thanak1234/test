using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.Business.ATDRequestForm;
using Workflow.DataAcess.Infrastructure;
using Workflow.DataAcess.Repositories;
using Workflow.DataAcess.Repositories.Interfaces;
using Workflow.DataAcess.Repositories.EGMAttandance;
using Workflow.DataObject;
using Workflow.Domain.Entities.EGM;
using Workflow.Domain.Entities.BatchData.EGMInstance;
using Workflow.Service.Interfaces;

namespace Workflow.Service
{
    public class ATDRequestFormService : AbstractRequestFormService<IATDRequestFormBC,ATDRequestWorkflowInstance>
    {
        public ATDRequestFormService()
        {
            BC = new ATDRequestFormBC(
                            DbContextSelector.getInstance().getDbFactory(DbContextSelector.DbName.Workflow), 
                            DbContextSelector.getInstance().getDbFactory(DbContextSelector.DbName.WorkflowDoc)
                            );
        }

        public IEnumerable<AttandanceDetailType> GetAttandanceDetailTypeList()
        {
            ATDDetailTypeRepository rep = new ATDDetailTypeRepository(DbContextSelector.getInstance().getDbFactory(DbContextSelector.DbName.Workflow));
            return rep.GetAll().ToList();
        }
    }
}
