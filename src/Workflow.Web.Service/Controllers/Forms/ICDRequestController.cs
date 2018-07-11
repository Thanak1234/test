using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Workflow.Domain.Entities;
using Workflow.Domain.Entities.INCIDENT;
using Workflow.Domain.Entities.BatchData.IncidentInstance;
using Workflow.Service;
using Workflow.Service.Interfaces;
using Workflow.Web.Service.Controllers.Common;
using Workflow.Web.Models.IncidentRequestForm;
using Workflow.Web.Models.ItRequestForm;

namespace Workflow.Web.Service.Controllers
{
    public class ICDRequestController : AbstractServiceController<ICDRequestWorkflowInstance, ICDFormViewModel>
    {
        public ICDRequestController() : base()
        {

        }

        protected override ICDFormViewModel CreateNewFormDataViewModel()
        {
            return new ICDFormViewModel();
        }

        protected override IRequestFormService<ICDRequestWorkflowInstance> GetRequestformService()
        {
            return new ICDRequestFormService();
        }

        protected override void MoreMapDataBC(ICDFormViewModel viewData, ICDRequestWorkflowInstance workflowInstance)
        {
            workflowInstance.Incident = viewData.dataItem.Incident.TypeAs<Incident>();

            workflowInstance.Incident.created_date = DateTime.Now;
            workflowInstance.Incident.modified_date = DateTime.Now;
            workflowInstance.Incident.created_by = workflowInstance.CurrentUser;
            workflowInstance.Incident.modified_by = workflowInstance.CurrentUser;
            workflowInstance.Incident.requestheaderid = viewData.requestHeaderId;

            workflowInstance.IncidentEmployeeList = this.GetRequestEmployeeModel(viewData.dataItem.IncidentEmployeeList);

            workflowInstance.AddIncidentEmployee = this.GetRequestEmployeeModel(viewData.dataItem.AddIncidentEmployee);
            workflowInstance.DelIncidentEmployee = this.GetRequestEmployeeModel(viewData.dataItem.DelIncidentEmployee);
            workflowInstance.EditIncidentEmployee = this.GetRequestEmployeeModel(viewData.dataItem.EditIncidentEmployee);
            
        }

        protected override void MoreMapDataView(ICDRequestWorkflowInstance workflowInstance, ICDFormViewModel viewData)
        {
            viewData.dataItem.Incident = workflowInstance.Incident.TypeAs<IncidentViewModel>();
            viewData.dataItem.IncidentEmployeeList = this.GetEmployeeList(workflowInstance.IncidentEmployeeList);
        }

        private IEnumerable<RequestUserViewModel> GetEmployeeList(IEnumerable<RequestUserExt> employeelist)
        {

            var empVMList = new List<RequestUserViewModel>();

            foreach(RequestUserExt ie in employeelist)
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

            if(IEVM != null)
            {
                foreach(RequestUserViewModel vm in IEVM)
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