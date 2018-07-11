using Workflow.DataAcess.Infrastructure;
using Workflow.Domain.Entities.BatchData;
using System.Linq;
using System.Collections.Generic;
using Workflow.DataAcess.Repositories.Admsr;
using Workflow.Domain.Entities.Admsr;
using Workflow.DataAcess.Repositories.ITAD;
using Workflow.Domain.Entities.ITAD;

namespace Workflow.Business.ITADRequestForm
{
    public class RequestForm : AbstractRequestFormBC<ITADRequestWorkflowInstance, IDataProcessing>, IRequestForm {

        private IITADEmployeeRepository employeeRepo;
        private Dictionary<string, object> _dataField = new Dictionary<string, object>();

        public RequestForm(IDbFactory dbFactory, IDbFactory dbDocFactory) : base(dbFactory, dbDocFactory) {
            employeeRepo = new ITADEmployeeRepository(dbFactory);
        }

        protected override void InitActivityConfiguration() {
            AddActivities(new ActivityEngine());
            ActivityList.Each(p => { AddActivities(new ActivityEngine(p)); });
            AddActivities(new ActivityEngine(
                () => {
                    return CreateEmailData("MODIFICATION");
                },
                new FormDataProcessing() {
                    IsAddNewRequestHeader = false,
                    IsEditPriority = false,
                    IsEditRequestor = false,
                    IsSaveActivityHistory = true,
                    IsUpdateLastActivity = true,
                    IsSaveRequestData = true,
                    IsSaveAttachments = true,
                    TriggerWorkflow = false
                })
            );
        }

        protected override string GetRequestCode() {
            return PROCESSCODE.ITAD;
        }

        #region Load/Save Form

        protected override void LoadFormData() {
            WorkflowInstance.Employee = employeeRepo.Get(p => p.RequestHeaderId == RequestHeader.Id);
        }

        protected override void TakeFormAction() {
            if (CurrentActivity().CurrAction.FormDataProcessing.IsSaveRequestData) {
                var entity = employeeRepo.Get(p => p.RequestHeaderId == RequestHeader.Id);
                if (entity == null && WorkflowInstance.Employee != null)
                {
                    var employee = new ITADEmployee();
                    employee.RequestHeaderId = RequestHeader.Id;
                    employee.Department = WorkflowInstance.Employee.Department;
                    employee.EmployeeName = WorkflowInstance.Employee.EmployeeName;
                    employee.EmployeeNo = WorkflowInstance.Employee.EmployeeNo;
                    employee.FirstName = WorkflowInstance.Employee.FirstName;
                    employee.Position = WorkflowInstance.Employee.Position;
                    employee.LastName = WorkflowInstance.Employee.LastName;
                    employee.Mobile = WorkflowInstance.Employee.Mobile;
                    employee.NoEmail = WorkflowInstance.Employee.NoEmail ?? false;
                    employee.Location = WorkflowInstance.Employee.Location;
                    employee.Phone = WorkflowInstance.Employee.Phone;
                    employee.Email = WorkflowInstance.Employee.Email;
                    employee.Remark = WorkflowInstance.Employee.Remark;
                    employeeRepo.Add(employee);
                }
                else
                {
                    entity.RequestHeaderId = RequestHeader.Id;
                    entity.Department = WorkflowInstance.Employee.Department;
                    entity.EmployeeName = WorkflowInstance.Employee.EmployeeName;
                    entity.EmployeeNo = WorkflowInstance.Employee.EmployeeNo;
                    entity.FirstName = WorkflowInstance.Employee.FirstName;
                    entity.Position = WorkflowInstance.Employee.Position;
                    entity.LastName = WorkflowInstance.Employee.LastName;
                    entity.Mobile = WorkflowInstance.Employee.Mobile;
                    entity.NoEmail = WorkflowInstance.Employee.NoEmail ?? false;
                    entity.Location = WorkflowInstance.Employee.Location;
                    entity.Phone = WorkflowInstance.Employee.Phone;
                    entity.Email = WorkflowInstance.Employee.Email;
                    entity.Remark = WorkflowInstance.Employee.Remark;
                    employeeRepo.Update(entity);
                }
            }
        }

        protected override Dictionary<string, object> GetDataField() {
            return _dataField;
        }

        #endregion
    }
}
