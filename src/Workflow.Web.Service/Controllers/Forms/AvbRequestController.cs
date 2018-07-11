/**
*@author : Phanny
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Workflow.Domain.Entities.AV;
using Workflow.Domain.Entities.BatchData;
using Workflow.Service;
using Workflow.Service.Interfaces;
using Workflow.Web.Service.Controllers.Common;
using Workflow.Web.Models.AvbRequestForm;

namespace Workflow.Web.Service.Controllers
{
    public class AvbRequestController : AbstractServiceController<AvbRequestWorkflowInstance, AvbRequestFormViewModel>
    {


        public AvbRequestController() : base(){ }

        protected override AvbRequestFormViewModel CreateNewFormDataViewModel()
        {
            return new AvbRequestFormViewModel();
        }

        protected override void MoreMapDataBC(AvbRequestFormViewModel viewData, AvbRequestWorkflowInstance workflowInstance)
        {
            workflowInstance.AvbJobHistory = new AvbJobHistory()
            {
                Id = viewData.dataItem.jobDetail.id,
                ProjectName = viewData.dataItem.jobDetail.projectName,
                Locaiton    = viewData.dataItem.jobDetail.location,
                SetupDate = viewData.dataItem.jobDetail.setupDate,
                ActualDate = viewData.dataItem.jobDetail.actualDate,
                ProjectBrief = viewData.dataItem.jobDetail.projectBrief,
                Other = viewData.dataItem.jobDetail.other
            };

            workflowInstance.AddAvbRequestItems = createRequestItem(viewData.dataItem.addItems);
            workflowInstance.DelAvbRequestItems = createRequestItem(viewData.dataItem.delItems);
            workflowInstance.EditAvbRequestItems = createRequestItem(viewData.dataItem.editItems);
        }



        private IEnumerable<AvbRequestItem> createRequestItem(IEnumerable<RequestItemViewModel> items)
        {
            var requestItems = new List<AvbRequestItem>();

            foreach (RequestItemViewModel item in items)
            {
                requestItems.Add(new AvbRequestItem()
                {
                    Id =item.id,
                    ItemId = item.itemId,
                    Quantity = item.quantity,
                    Comment = item.comment
                });
            }

            return requestItems;
        }

        protected override void MoreMapDataView(AvbRequestWorkflowInstance workflowInstance, AvbRequestFormViewModel viewData)
        {
            viewData.dataItem.jobDetail = new AvbJobDetailViewModel()
            {
                id          = workflowInstance.AvbJobHistory.Id,
                projectName = workflowInstance.AvbJobHistory.ProjectName,
                location    = workflowInstance.AvbJobHistory.Locaiton,
                setupDate   = workflowInstance.AvbJobHistory.SetupDate,
                actualDate  = workflowInstance.AvbJobHistory.ActualDate,
                projectBrief = workflowInstance.AvbJobHistory.ProjectBrief,
                other = workflowInstance.AvbJobHistory.Other
            };

            var requestItems = new List<RequestItemViewModel>();
            foreach (AvbRequestItem item in workflowInstance.AvbRequestItems)
            {
                requestItems.Add(new RequestItemViewModel()
                {
                    id = item.Id,
                    itemId = item.ItemId,
                    itemName = item.Item.ItemName,
                    itemTypeId = item.Item.ItemTypeId,
                    itemTypeName = item.Item.ItemType.ItemTypeName,
                    itemTypeDescription = item.Item.ItemType.Description,
                    quantity = item.Quantity,
                    comment = item.Comment
                });
            }

            viewData.dataItem.items = requestItems;
        }

        protected override IRequestFormService<AvbRequestWorkflowInstance> GetRequestformService()
        {
            return new AvbRequestFormService();
        }
    }
}