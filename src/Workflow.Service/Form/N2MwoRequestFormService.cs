using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.Business.N2MwoRequestForm;
using Workflow.DataAcess.Infrastructure;
using Workflow.DataAcess.Repositories;
using Workflow.DataAcess.Repositories.Interfaces;
using Workflow.DataAcess.Repositories.N2MWO;
using Workflow.DataObject;
using Workflow.Domain.Entities.N2MWO;
using Workflow.Domain.Entities.BatchData;
using Workflow.Service.Interfaces;

namespace Workflow.Service {
    public class N2MwoRequestFormService: AbstractRequestFormService<IN2MwoRequestFormBC, N2MwoRequestWorkflowInstance>, IN2MwoRequestFormService
    {

        public N2MwoRequestFormService() {
            var workflow = DbContextSelector.getInstance().getDbFactory(DbContextSelector.DbName.Workflow);
            var docWorkflow = DbContextSelector.getInstance().getDbFactory(DbContextSelector.DbName.WorkflowDoc);
            BC = new N2MwoRequestFormBC(workflow, docWorkflow);
        }

        public IEnumerable<N2DepartmentChargable> GetDepartmentChargables() {
            IN2DepartmentChargableRepository departmentChargableRepository = new N2DepartmentChargableRepository(DbContextSelector.getInstance().getDbFactory(DbContextSelector.DbName.Workflow));
            return departmentChargableRepository.GetAll().OrderBy(p => p.Sequence).ToList();
        }

        public IEnumerable<N2Mode> GetModes() {
            IN2ModeRepository modeRepository = new N2ModeRepository(DbContextSelector.getInstance().getDbFactory(DbContextSelector.DbName.Workflow));
            return modeRepository.GetAll().OrderBy(p => p.Sequence).ToList();
        }

        public string GetReferenceNumber(string ccd) {
            IN2RequestInformationRepository requestInformationRepository = new N2RequestInformationRepository(DbContextSelector.getInstance().getDbFactory(DbContextSelector.DbName.Workflow));
            return requestInformationRepository.GetReferenceNumber(ccd);
        }

        public IEnumerable<N2RequestType> GetRequestTypes() {
            IN2RequestTypeRepository requestTypeRepository = new N2RequestTypeRepository(DbContextSelector.getInstance().getDbFactory(DbContextSelector.DbName.Workflow));
            return requestTypeRepository.GetAll().OrderBy(p => p.Sequence).ToList();
        }

        public IEnumerable<N2WorkType> GetWorkTypes() {
            IN2WorkTypeRepository workTypeRepository = new N2WorkTypeRepository(DbContextSelector.getInstance().getDbFactory(DbContextSelector.DbName.Workflow));
            return workTypeRepository.GetAll().OrderBy(p => p.Sequence).ToList();
        }
    }
}
