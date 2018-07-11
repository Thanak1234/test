using System.Web.Http;
using Workflow.Domain.Entities.BatchData;
using Workflow.Service.Interfaces;
using Workflow.Web.Service.Controllers.Common;
using Workflow.Web.Models.EOMBP;
using Workflow.Domain.Entities.Finance;
using System.Linq;
using Workflow.Service;
using System.Net.Http;
using Workflow.Core.Attributes;
using Workflow.Domain.Entities.Forms;

namespace Workflow.Web.Service.Controllers
{
    [JsonFormat]
    [RoutePrefix("api/eombprequest")]
    public class EOMBPRequestController : AbstractServiceController<EOMBPRequestWorkflowInstance, EOMBPRequestFormViewModel>
    {
        #region Constructor
        public EOMBPRequestController()
        {

        }
        // Initialize constructor of view model
        protected override EOMBPRequestFormViewModel CreateNewFormDataViewModel()
        {
            return new EOMBPRequestFormViewModel();
        }

        protected override IRequestFormService<EOMBPRequestWorkflowInstance> GetRequestformService()
        {
            return new EOMBPRequestFormService();
        }
        #endregion

        #region Business Form Logic
        protected override void MoreMapDataBC(EOMBPRequestFormViewModel viewData, EOMBPRequestWorkflowInstance workflowInstance)
        {
            // BestPerformance Form Base
            workflowInstance.BestPerformance = viewData.dataItem.bestPerformance.TypeAs<BestPerformance>();

            // BestPerformanceDetails - Collection
            workflowInstance.AddBestPerformanceDetails = (from p in viewData.dataItem.addBestPerformanceDetails select p.TypeAs<BestPerformanceDetail>());
            workflowInstance.DelBestPerformanceDetails = (from p in viewData.dataItem.delBestPerformanceDetails select p.TypeAs<BestPerformanceDetail>());
            workflowInstance.EditBestPerformanceDetails = (from p in viewData.dataItem.editBestPerformanceDetails select p.TypeAs<BestPerformanceDetail>());

            workflowInstance.AddEmployeeOfMonthDetails = (from p in viewData.dataItem.addEmployeeOfMonthDetails select p.TypeAs<BestPerformanceDetail>());
            workflowInstance.DelEmployeeOfMonthDetails = (from p in viewData.dataItem.delEmployeeOfMonthDetails select p.TypeAs<BestPerformanceDetail>());
            workflowInstance.EditEmployeeOfMonthDetails = (from p in viewData.dataItem.editEmployeeOfMonthDetails select p.TypeAs<BestPerformanceDetail>());
        }

        protected override void MoreMapDataView(EOMBPRequestWorkflowInstance workflowInstance, EOMBPRequestFormViewModel viewData)
        {
            // Bind BestPerformance data to view model
            viewData.dataItem.bestPerformance = workflowInstance.BestPerformance.TypeAs<BestPerformanceViewModel>();

            // Cast and bind data list to view model
            if (workflowInstance.EmployeeOfMonthDetails != null && workflowInstance.EmployeeOfMonthDetails.Count() > 0)
            {
                var employees = LoadEmployeeList();
                viewData.dataItem.employeeOfMonthDetails =
                    (from p in workflowInstance.EmployeeOfMonthDetails
                     join e in employees on p.EmployeeId equals e.Id
                     where p.Type == "EOM"
                     select new EmployeeOfMonthDetailViewModel()
                     {
                         Id = p.Id,
                         RequestHeaderId = p.RequestHeaderId,
                         EmployeeId = p.EmployeeId,
                         EmployeeName = e.EmployeeName,
                         EmployeeNo = e.EmployeeNo,
                         Department = e.Department,
                         Position = e.Position,
                         Gender = p.Gender,
                         Division = p.Division,
                         ContactNo = p.ContactNo,
                         Type = p.Type
                     });
            }

            if (workflowInstance.BestPerformanceDetails != null && workflowInstance.BestPerformanceDetails.Count() > 0)
            {
                var employees = LoadEmployeeList();
                viewData.dataItem.bestPerformanceDetails =
                    (from p in workflowInstance.BestPerformanceDetails
                     join e in employees on p.EmployeeId equals e.Id
                     where p.Type == "BP"
                     select new BestPerformanceDetailViewModel()
                     {
                         Id = p.Id,
                         RequestHeaderId = p.RequestHeaderId,
                         EmployeeId = p.EmployeeId,
                         EmployeeName = e.EmployeeName,
                         EmployeeNo = e.EmployeeNo,
                         Department = e.Department,
                         Position = e.Position,
                         Gender = p.Gender,
                         Division = p.Division,
                         ContactNo = p.ContactNo,
                         Type = p.Type
                     });
            }

        }
        #endregion
    }
}