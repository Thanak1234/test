using System.Web.Http;
using Workflow.Domain.Entities.BatchData;
using Workflow.Service.Interfaces;
using Workflow.Web.Service.Controllers.Common;
using Workflow.Web.Models.TAS;
using System.Linq;
using Workflow.Service;
using Workflow.Core.Attributes;
using Workflow.Domain.Entities.Forms;

namespace Workflow.Web.Service.Controllers
{
    [JsonFormat]
    [RoutePrefix("api/tascrrequest")]
    public class TASCRRequestController : AbstractServiceController<TASCRRequestWorkflowInstance, TASCRRequestFormViewModel>
    {
        #region Constructor
        public TASCRRequestController()
        {

        }
        // Initialize constructor of view model
        protected override TASCRRequestFormViewModel CreateNewFormDataViewModel()
        {
            return new TASCRRequestFormViewModel();
        }

        protected override IRequestFormService<TASCRRequestWorkflowInstance> GetRequestformService()
        {
            return new TASRequestFormService();
        }
        #endregion

        #region Business Form Logic
        protected override void MoreMapDataBC(TASCRRequestFormViewModel viewData, TASCRRequestWorkflowInstance workflowInstance)
        {
            // BestPerformance Form Base
            workflowInstance.CourseRegistration = viewData.dataItem.courseRegistration.TypeAs<CourseRegistration>();

            // CourseEmployees - Collection
            workflowInstance.AddCourseEmployees = (from p in viewData.dataItem.addCourseEmployees select p.TypeAs<CourseEmployee>());
            workflowInstance.DelCourseEmployees = (from p in viewData.dataItem.delCourseEmployees select p.TypeAs<CourseEmployee>());
            workflowInstance.EditCourseEmployees = (from p in viewData.dataItem.editCourseEmployees select p.TypeAs<CourseEmployee>());
        }

        protected override void MoreMapDataView(TASCRRequestWorkflowInstance workflowInstance, TASCRRequestFormViewModel viewData)
        {
            // Bind BestPerformance data to view model
            viewData.dataItem.courseRegistration = workflowInstance.CourseRegistration.TypeAs<CourseRegistrationViewModel>();

            // Cast and bind Unfit To Work data list to view model
            if (workflowInstance.CourseEmployees != null && workflowInstance.CourseEmployees.Count() > 0)
            {
                var employees = LoadEmployeeList();
                viewData.dataItem.courseEmployees =
                    (from p in workflowInstance.CourseEmployees
                     join e in employees on p.EmployeeId equals e.Id
                     select new CourseEmployeeViewModel()
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
                         ContactNo = p.ContactNo
                     });
            }
        }
        #endregion
    }
}