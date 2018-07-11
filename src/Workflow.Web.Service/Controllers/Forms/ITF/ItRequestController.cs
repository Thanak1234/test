/**
*@author : Phanny
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Workflow.Domain.Entities;
using Workflow.Domain.Entities.IT;
using Workflow.Domain.Entities.BatchData;
using Workflow.Service;
using Workflow.Service.Interfaces;
using Workflow.Web.Service.Controllers.Common;
using Workflow.Web.Models;
using Workflow.Web.Models.ItRequestForm;

namespace Workflow.Web.Service.Controllers
{
    public class ItRequestController : AbstractServiceController<ItRequestWorkflowInstance, ItRequestFormViewModel>
    {
        public ItRequestController(): base() {}
       

        protected override ItRequestFormViewModel CreateNewFormDataViewModel()
        {
            return new ItRequestFormViewModel();
        }

        protected override void MoreMapDataBC(ItRequestFormViewModel viewData, ItRequestWorkflowInstance workflowInstance)
        {

            workflowInstance.RequestItems = GetRequestItemModel(viewData.dataItem.requestItems);
            workflowInstance.AddRequestItems = GetRequestItemModel(viewData.dataItem.addRequestItems);
            workflowInstance.DelRequestItems = GetRequestItemModel(viewData.dataItem.delRequestItems);
            workflowInstance.EditRequestItems = GetRequestItemModel(viewData.dataItem.editRequestItems);

            workflowInstance.RequestUsers = GetRequestUserModel(viewData.dataItem.requestUsers);
            workflowInstance.AddRequestUsers = GetRequestUserModel(viewData.dataItem.addRequestUsers);
            workflowInstance.DelRequestUsers = GetRequestUserModel(viewData.dataItem.delRequestUsers);
            workflowInstance.EditRequestUsers = GetRequestUserModel(viewData.dataItem.editRequestUsers);
        }

        protected override void MoreMapDataView(ItRequestWorkflowInstance workflowInstance, ItRequestFormViewModel viewData)
        {


            viewData.dataItem.requestItems = GetRequestItemVMs(workflowInstance.RequestItems);
            viewData.dataItem.requestUsers = GetRequestUserVMs(workflowInstance.RequestUsers);
        }

        private IEnumerable<RequestUserViewModel> GetRequestUserVMs(IEnumerable<RequestUser> requestUserList)
        {

            var reqUserVMs = new List<RequestUserViewModel>();
            RequestUserViewModel requestUserVM = null;
            foreach (RequestUser reqUser in requestUserList)
            {
                requestUserVM = new RequestUserViewModel();

                requestUserVM.id = reqUser.Id;
                requestUserVM.empId = reqUser.EmpId;
                requestUserVM.empName = reqUser.EmpName;
                requestUserVM.empNo = reqUser.EmpNo;
                requestUserVM.email = reqUser.Email;
                requestUserVM.phone = reqUser.Phone;
                requestUserVM.manager = reqUser.Manager;
                requestUserVM.teamId = reqUser.TeamId;
                requestUserVM.teamName = reqUser.Team.Deptname;
                requestUserVM.hiredDate = reqUser.HiredDate;
                requestUserVM.position = reqUser.Position;

                reqUserVMs.Add(requestUserVM);
            }

            return reqUserVMs;
        }


        private IEnumerable<RequestItemViewModel> GetRequestItemVMs(IEnumerable<RequestItem> requestItemList)
        {
            var requestItems = new List<RequestItemViewModel>();
            RequestItemViewModel requestItemVM = null;
            foreach (RequestItem requestItem in requestItemList)
            {
                requestItemVM = new RequestItemViewModel();

                requestItemVM.id = requestItem.Id;
                requestItemVM.itemId = requestItem.Item.Id;
                requestItemVM.itemName = requestItem.Item.ItemName;
                requestItemVM.itemTypeId = requestItem.ItemTypeId;
                requestItemVM.itemTypeName = requestItem.ItemType == null ? null : requestItem.ItemType.ItemTypeName;
                requestItemVM.itemRoleId = requestItem.ItemRoleId;
                requestItemVM.itemRoleName = requestItem.ItemRole == null ? null : requestItem.ItemRole.RoleName;
                requestItemVM.qty = requestItem.Qty;
                requestItemVM.comment = requestItem.Comment;
                requestItems.Add(requestItemVM);
                requestItemVM.sessionId = requestItem.Item != null? ((requestItem.Item.Session!= null)? requestItem.Item.Session.Id:0): 0;
                requestItemVM.session = requestItem.Item != null ? ((requestItem.Item.Session != null) ? requestItem.Item.Session.SessionName : string.Empty) : string.Empty ;
                
            }


            return requestItems;
        }

        private IEnumerable<RequestUser> GetRequestUserModel(IEnumerable<RequestUserViewModel> requestUserViewlModels)
        {
            var requestUsers = new List<RequestUser>();

            if (requestUserViewlModels != null)
            {
                foreach (RequestUserViewModel requestUserVM in requestUserViewlModels)
                {
                    var requestUser = new RequestUser()
                    {
                        Id = requestUserVM.id,
                        EmpNo = requestUserVM.empNo,
                        EmpName = requestUserVM.empName,
                        EmpId = requestUserVM.empId,
                        Manager = requestUserVM.manager,
                        TeamId = requestUserVM.teamId,
                        Email = requestUserVM.email,
                        Position = requestUserVM.position,
                        Phone = requestUserVM.phone,
                        HiredDate = requestUserVM.hiredDate,
                        Version=1
                    };
                    requestUsers.Add(requestUser);
                }
            }
            return requestUsers;
        }

        private IEnumerable<RequestItem> GetRequestItemModel(IEnumerable<RequestItemViewModel> requestItemViewModels)
        {
            var requestItems = new List<RequestItem>();

            if (requestItemViewModels != null)
            {
                foreach (RequestItemViewModel requestItemVM in requestItemViewModels)
                {
                    var requestItem = new RequestItem()
                    {
                        Id = requestItemVM.id,
                        ItemId = requestItemVM.itemId,
                        ItemTypeId = requestItemVM.itemTypeId,
                        ItemRoleId = requestItemVM.itemRoleId,
                        Qty = requestItemVM.qty,
                        Comment = requestItemVM.comment,
                        Version =1
                        
                    };

                    requestItems.Add(requestItem);
                }
            }

            return requestItems;
        }

        protected override IRequestFormService<ItRequestWorkflowInstance> GetRequestformService()
        {
           return new ItRequestFormService(); 
        }
    }
}
