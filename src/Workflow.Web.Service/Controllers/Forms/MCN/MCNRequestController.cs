using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Workflow.Domain.Entities;
using Workflow.Domain.Entities.EGM;
using Workflow.Domain.Entities.BatchData.EGMInstance;
using Workflow.Service;
using Workflow.Service.Interfaces;
using Workflow.Web.Service.Controllers.Common;
using Workflow.Web.Models.MachineRequestForm;
using Workflow.Web.Models.ItRequestForm;

namespace Workflow.Web.Service.Controllers
{
    public class MCNRequestController : AbstractServiceController<MCNRequestWorkflowInstance,MCNFormViewModel>
    {
        public MCNRequestController() : base()
        {

        }

        protected override MCNFormViewModel CreateNewFormDataViewModel()
        {
            return new MCNFormViewModel();
        }

        protected override IRequestFormService<MCNRequestWorkflowInstance> GetRequestformService()
        {
            return new MCNRequestFormService();
        }

        protected override void MoreMapDataBC(MCNFormViewModel viewData, MCNRequestWorkflowInstance workflowInstance)
        {
            workflowInstance.Machine = viewData.dataItem.Machine.TypeAs<Machine>();

            workflowInstance.Machine.created_date = DateTime.Now;
            workflowInstance.Machine.created_by = workflowInstance.CurrentUser;
            workflowInstance.Machine.modified_by = workflowInstance.CurrentUser;
            workflowInstance.Machine.modified_date = DateTime.Now;

            workflowInstance.MachineEmployeeList = this.GetRequestEmployeeModel(viewData.dataItem.MachineEmployeeList);
            workflowInstance.AddMachineEmployeeList = this.GetRequestEmployeeModel(viewData.dataItem.AddMachineEmployee);
            workflowInstance.EditMachineEmployeeList = this.GetRequestEmployeeModel(viewData.dataItem.EditMachineEmployee);
            workflowInstance.DelMachineEmployeeList = this.GetRequestEmployeeModel(viewData.dataItem.DelMachineEmployee);
        }

        protected override void MoreMapDataView(MCNRequestWorkflowInstance workflowInstance, MCNFormViewModel viewData)
        {
            viewData.dataItem.Machine = workflowInstance.Machine.TypeAs<Machine>();
            viewData.dataItem.MachineEmployeeList = this.GetEmployeeList(workflowInstance.MachineEmployeeList);
        }

        private IEnumerable<RequestUserViewModel> GetEmployeeList(IEnumerable<RequestUserExt> employeelist)
        {

            var empVMList = new List<RequestUserViewModel>();

            foreach (RequestUserExt ie in employeelist)
            {
                empVMList.Add(new RequestUserViewModel()
                {
                    empNo = ie.EmpNo,
                    empName = ie.EmpName,
                    email = ie.Email,
                    empId = ie.EmpId,
                    hiredDate = ie.HiredDate,
                    id = ie.Id,
                    manager = ie.Manager,
                    phone = ie.Phone,
                    position = ie.Position,
                    teamId = ie.TeamId,
                    teamName = ie.teamName
                });
            }

            return empVMList;

        }

        private IEnumerable<RequestUserExt> GetRequestEmployeeModel(IEnumerable<RequestUserViewModel> IEVM)
        {

            List<RequestUserExt> EmployeeList = new List<RequestUserExt>();

            if (IEVM != null)
            {
                foreach (RequestUserViewModel vm in IEVM)
                {
                    EmployeeList.Add(new RequestUserExt()
                    {
                        Email = vm.email,
                        EmpId = vm.empId,
                        TeamId = vm.teamId,
                        EmpName = vm.empName,
                        EmpNo = vm.empNo,
                        HiredDate = vm.hiredDate,
                        Id = vm.id,
                        Manager = vm.manager,
                        Phone = vm.phone,
                        Position = vm.position,
                        Version = 1
                    });
                }
            }

            return EmployeeList;
        }
    }
}