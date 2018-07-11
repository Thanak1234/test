using System.Linq;
using System.Web.Http;
using Workflow.Core.Attributes;
using Workflow.Domain.Entities.RMD;
using Workflow.Domain.Entities.BatchData;
using Workflow.Service;
using Workflow.Service.Interfaces;
using Workflow.Web.Models.RMD;
using Workflow.Web.Service.Controllers.Common;

namespace Workflow.Web.Service.Controllers
{
    [JsonFormat]
    [RoutePrefix("api/rmdrequest")]
    public class RMDRequestController : AbstractServiceController<RMDRequestWorkflowInstance, RMDRequestFormViewModel>
    {
        #region Business Form Logic
        protected override void MoreMapDataBC(RMDRequestFormViewModel viewData, RMDRequestWorkflowInstance workflowInstance)
        {
            // RiskAssessment Form Base
            workflowInstance.RiskAssessment = viewData.dataItem.riskAssessment.TypeAs<RiskAssessment>();
            
            // Worksheet1 - Collection
            workflowInstance.AddWorksheet1s = (from p in viewData.dataItem.addWorksheet1s select p.TypeAs<Worksheet1>());
            workflowInstance.DelWorksheet1s = (from p in viewData.dataItem.delWorksheet1s select p.TypeAs<Worksheet1>());
            workflowInstance.EditWorksheet1s = (from p in viewData.dataItem.editWorksheet1s select p.TypeAs<Worksheet1>());

            // Worksheet2 - Collection
            workflowInstance.AddWorksheet2s = (from p in viewData.dataItem.addWorksheet2s select p.TypeAs<Worksheet2>());
            workflowInstance.DelWorksheet2s = (from p in viewData.dataItem.delWorksheet2s select p.TypeAs<Worksheet2>());
            workflowInstance.EditWorksheet2s = (from p in viewData.dataItem.editWorksheet2s select p.TypeAs<Worksheet2>());

            //// Worksheet3 - Collection
            //workflowInstance.AddWorksheet3s = (from p in viewData.dataItem.addWorksheet3s select p.TypeAs<Worksheet3>());
            //workflowInstance.DelWorksheet3s = (from p in viewData.dataItem.delWorksheet3s select p.TypeAs<Worksheet3>());
            //workflowInstance.EditWorksheet3s = (from p in viewData.dataItem.editWorksheet3s select p.TypeAs<Worksheet3>());
        }

        protected override void MoreMapDataView(RMDRequestWorkflowInstance workflowInstance, RMDRequestFormViewModel viewData)
        {
            // Bind RiskAssessment data to view model
            viewData.dataItem.riskAssessment = workflowInstance.RiskAssessment.TypeAs<RiskAssessmentViewModel>();

            // Cast and bind [Asset Control Details] data list to view model
            if (workflowInstance.Worksheet1s != null && workflowInstance.Worksheet1s.Count() > 0)
            {
                viewData.dataItem.worksheet1s = (from p in workflowInstance.Worksheet1s select p.TypeAs<Worksheet1ViewModel>());
            }
            if (workflowInstance.Worksheet2s != null && workflowInstance.Worksheet2s.Count() > 0)
            {
                viewData.dataItem.worksheet2s = (from p in workflowInstance.Worksheet2s select p.TypeAs<Worksheet2ViewModel>());
            }
            //if (workflowInstance.Worksheet3s != null && workflowInstance.Worksheet3s.Count() > 0)
            //{
            //    viewData.dataItem.worksheet3s = (from p in workflowInstance.Worksheet3s select p.TypeAs<Worksheet3ViewModel>());
            //}
        }
        
        #endregion

        #region Constructor
        public RMDRequestController()
        {

        }
        // Initialize constructor of view model
        protected override RMDRequestFormViewModel CreateNewFormDataViewModel()
        {
            return new RMDRequestFormViewModel();
        }

        protected override IRequestFormService<RMDRequestWorkflowInstance> GetRequestformService()
        {
            return new RMDRequestFormService();
        }
        #endregion

    }
}