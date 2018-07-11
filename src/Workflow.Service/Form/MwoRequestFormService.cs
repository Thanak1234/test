using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.Business.MwoRequestForm;
using Workflow.DataAcess.Infrastructure;
using Workflow.DataAcess.Repositories;
using Workflow.DataAcess.Repositories.Interfaces;
using Workflow.DataAcess.Repositories.MWO;
using Workflow.DataObject;
using Workflow.Domain.Entities.MWO;
using Workflow.Domain.Entities.BatchData;
using Workflow.Service.Interfaces;

namespace Workflow.Service {
    public class MwoRequestFormService: AbstractRequestFormService<IMwoRequestFormBC, MwoRequestWorkflowInstance>, IMwoRequestFormService {

        public MwoRequestFormService() {
            var workflow = DbContextSelector.getInstance().getDbFactory(DbContextSelector.DbName.Workflow);
            var docWorkflow = DbContextSelector.getInstance().getDbFactory(DbContextSelector.DbName.WorkflowDoc);
            BC = new MwoRequestFormBC(workflow, docWorkflow);
        }

        public IEnumerable<DepartmentChargable> GetDepartmentChargables() {
            IDepartmentChargableRepository departmentChargableRepository = new DepartmentChargableRepository(DbContextSelector.getInstance().getDbFactory(DbContextSelector.DbName.Workflow));
            return departmentChargableRepository.GetAll().OrderBy(p => p.Sequence).ToList();
        }

        public IEnumerable<Mode> GetModes() {
            IModeRepository modeRepository = new ModeRepository(DbContextSelector.getInstance().getDbFactory(DbContextSelector.DbName.Workflow));
            return modeRepository.GetAll().OrderBy(p => p.Sequence).ToList();
        }

        public string GetReferenceNumber(string ccd) {
            IRequestInformationRepository requestInformationRepository = new RequestInformationRepository(DbContextSelector.getInstance().getDbFactory(DbContextSelector.DbName.Workflow));
            return requestInformationRepository.GetReferenceNumber(ccd);
        }

        public IEnumerable<RequestType> GetRequestTypes() {
            IRequestTypeRepository requestTypeRepository = new RequestTypeRepository(DbContextSelector.getInstance().getDbFactory(DbContextSelector.DbName.Workflow));
            return requestTypeRepository.GetAll().OrderBy(p => p.Sequence).ToList();
        }

        public IEnumerable<WorkType> GetWorkTypes() {
            IWorkTypeRepository workTypeRepository = new WorkTypeRepository(DbContextSelector.getInstance().getDbFactory(DbContextSelector.DbName.Workflow));
            return workTypeRepository.GetAll().OrderBy(p => p.Sequence).ToList();
        }
    }
}
