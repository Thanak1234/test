/**
*@author : Phanny
*/

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.Business.AdmCppRequestForm;
using Workflow.Business.AVRequestForm;
using Workflow.Business.EOMRequestForm;
using Workflow.DataAcess.Infrastructure;
using Workflow.DataAcess.Repositories;
using Workflow.DataAcess.Repositories.Interfaces;
using Workflow.DataObject.AV;
using Workflow.Domain.Entities.AV;
using Workflow.Domain.Entities.BatchData;
using Workflow.Service.Interfaces;

namespace Workflow.Service {
    public class AdmCppRequestFormService : AbstractRequestFormService<IAdmCppRequestFormBC, AdmCppRequestWorkflowInstance>, IAdmCppRequestFormService {
        public AdmCppRequestFormService() {
            var workflow = DbContextSelector.getInstance().getDbFactory(DbContextSelector.DbName.Workflow);
            var docWorkflow = DbContextSelector.getInstance().getDbFactory(DbContextSelector.DbName.WorkflowDoc);
            BC = new AdmCppRequestFormBC(workflow, docWorkflow);
        }
    }
}
