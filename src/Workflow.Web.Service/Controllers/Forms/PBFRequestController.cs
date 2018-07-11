using System;
using System.Collections.Generic;
using System.Web.Http;
using Workflow.Domain.Entities.PBF;
using Workflow.Domain.Entities.BatchData;
using Workflow.Service;
using Workflow.Service.Interfaces;
using Workflow.Web.Service.Controllers.Common;
using Workflow.Web.Models.PBFRequestForm;

namespace Workflow.Web.Service.Controllers
{
    public class PBFRequestController : AbstractServiceController<PBFRequestWorkflowInstance, PBFRequestFormViewModel>
    {
        protected override PBFRequestFormViewModel CreateNewFormDataViewModel()
        {
            return new PBFRequestFormViewModel();
        }

        protected override IRequestFormService<PBFRequestWorkflowInstance> GetRequestformService()
        {
            return new PBFRequestFormService();
        }

        protected override void MoreMapDataBC(PBFRequestFormViewModel viewData, PBFRequestWorkflowInstance workflowInstance)
        {
            workflowInstance.ProjectBrief = viewData.dataItem.projectBrief.TypeAs<ProjectBrief>();
            workflowInstance.ProjectBrief.DraftComment = viewData.comment; // save comment as draft

            workflowInstance.AddSpecifications = MapViewModel(viewData.dataItem.addRequestItems);
            workflowInstance.DelSpecifications = MapViewModel(viewData.dataItem.delRequestItems);
            workflowInstance.EditSpecifications = MapViewModel(viewData.dataItem.editRequestItems);
        }
        
        protected override void MoreMapDataView(PBFRequestWorkflowInstance workflowInstance, PBFRequestFormViewModel viewData)
        {
            var projectBrief = workflowInstance.ProjectBrief.TypeAs<ProjectBriefViewModel>();
            viewData.dataItem.projectBrief = projectBrief;
            viewData.comment = projectBrief.DraftComment;
            
            var specifications = new List<SpecItemViewModel>();
            if (workflowInstance.Specifications != null) {
                foreach (var item in workflowInstance.Specifications)
                {
                    item.ProjectBrief = null;
                    var model = item.TypeAs<SpecItemViewModel>();
                    specifications.Add(model);
                }
            }
            
            viewData.dataItem.specifications = specifications;
        }

        #region Private Method

        private IEnumerable<Specification> MapViewModel(IEnumerable<SpecItemViewModel> items)
        {
            var requestItems = new List<Specification>();

            foreach (var item in items)
            {
                requestItems.Add(item.TypeAs<Specification>());
            }

            return requestItems;
        }
        
        #endregion
    }
}