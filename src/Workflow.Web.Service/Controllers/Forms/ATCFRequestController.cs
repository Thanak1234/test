using System.Web.Http;
using Workflow.Domain.Entities.BatchData;
using Workflow.Service.Interfaces;
using Workflow.Web.Service.Controllers.Common;
using Workflow.Web.Models.ATCF;
using System.Linq;
using Workflow.Core.Attributes;
using Workflow.Domain.Entities.Forms;
using Workflow.Service;
using System.Net.Http;
using Workflow.DataAcess.Repositories;
using System.Net;
using System.Collections.Generic;
using System;

namespace Workflow.Web.Service.Controllers
{
    [JsonFormat]
    [RoutePrefix("api/atcfrequest")]
    public class ATCFRequestController : AbstractServiceController<ATCFRequestWorkflowInstance, ATCFRequestFormViewModel>
    {
        Repository _repository = null;

        #region Constructor
        public ATCFRequestController()
        {
            _repository = new Repository();
        }
        // Initialize constructor of view model
        protected override ATCFRequestFormViewModel CreateNewFormDataViewModel()
        {
            return new ATCFRequestFormViewModel();
        }

        protected override IRequestFormService<ATCFRequestWorkflowInstance> GetRequestformService()
        {
            return new ATCFRequestFormService();
        }
        #endregion

        #region Business Form Logic
        protected override void MoreMapDataBC(ATCFRequestFormViewModel viewData, ATCFRequestWorkflowInstance workflowInstance)
        {
            // AdditionalTimeWorkeds - Collection
            workflowInstance.AddAdditionalTimeWorkeds = (from p in viewData.dataItem.addAdditionalTimeWorkeds select p.TypeAs<AdditionalTimeWorked>());
            workflowInstance.DelAdditionalTimeWorkeds = (from p in viewData.dataItem.delAdditionalTimeWorkeds select p.TypeAs<AdditionalTimeWorked>());
            workflowInstance.EditAdditionalTimeWorkeds = (from p in viewData.dataItem.editAdditionalTimeWorkeds select p.TypeAs<AdditionalTimeWorked>());
        }

        protected override void MoreMapDataView(ATCFRequestWorkflowInstance workflowInstance, ATCFRequestFormViewModel viewData)
        {
            var employees = LoadEmployeeList();
            // Cast and bind Unfit To Work data list to view model
            if (workflowInstance.AdditionalTimeWorkeds != null && workflowInstance.AdditionalTimeWorkeds.Count() > 0)
            {
                viewData.dataItem.additionalTimeWorkeds = 
                    (from p in workflowInstance.AdditionalTimeWorkeds
                     join e in employees on p.EmployeeId equals e.Id
                     select new AdditionalTimeWorkedViewModel() {
						 Id = p.Id,
                         RequestHeaderId = p.RequestHeaderId,
                         EmployeeId = p.EmployeeId,
                         WorkingDate = p.WorkingDate,
                         WorkOn = p.WorkOn,
                         NumberOfHour = p.NumberOfHour,
                         Comment = p.Comment,
                         EmployeeName = e.EmployeeName,
                         EmployeeNo = e.EmployeeNo
                     });
            }
        }
        #endregion
        [Route("working-date-list")]
        public object GetWorkingDatesLastMonth(int empId) {
            return _repository.ExecDynamicSqlQuery(string.Format(
                @"SELECT WorkingDate FROM HR.ADDITIONAL_WORK WHERE 
                WorkingDate > DATEADD(MONTH, -1, GETDATE()) AND 
                EmployeeId = {0} AND
                NOT EXISTS(
                    SELECT TOP 1 1 FROM [BPMDATA].[REQUEST_HEADER] WHERE [STATUS] IN ('Rejected', 'Cancelled') AND ID = RequestHeaderId
                )", empId));
            
        }
    }
}