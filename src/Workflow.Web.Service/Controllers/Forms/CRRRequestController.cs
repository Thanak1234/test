using System;
using System.Collections.Generic;
using Workflow.Domain.Entities.Reservation;
using Workflow.Domain.Entities.BatchData;
using Workflow.Service;
using Workflow.Service.Interfaces;
using Workflow.Web.Service.Controllers.Common;
using Workflow.Web.Models.Reservation;

namespace Workflow.Web.Service.Controllers
{
    public class CRRRequestController : AbstractServiceController<CRRRequestWorkflowInstance, CRRRequestFormViewModel>
    {
        private ICRRRequestFormService service;
        
        protected override CRRRequestFormViewModel CreateNewFormDataViewModel()
        {
            return new CRRRequestFormViewModel();
        }
        
        protected override IRequestFormService<CRRRequestWorkflowInstance> GetRequestformService()
        {
            service = new CRRRequestFormService();
            return service;
        }
        
        protected override void MoreMapDataBC(CRRRequestFormViewModel viewData, CRRRequestWorkflowInstance workflowInstance)
        {
            workflowInstance.Complimentary = viewData.dataItem.complimentary.TypeAs<Complimentary>();
            workflowInstance.CheckExpense = viewData.dataItem.checkExpense;

            workflowInstance.Guests = GetRequestGuestModel(viewData.dataItem.guests);
            workflowInstance.AddRequestGuests = GetRequestGuestModel(viewData.dataItem.addRequestGuests);
            workflowInstance.DelRequestGuests = GetRequestGuestModel(viewData.dataItem.delRequestGuests);
            workflowInstance.EditRequestGuests = GetRequestGuestModel(viewData.dataItem.editRequestGuests);
        }
        
        protected override void MoreMapDataView(CRRRequestWorkflowInstance workflowInstance, CRRRequestFormViewModel viewData)
        {
            viewData.dataItem.complimentary = workflowInstance.Complimentary.TypeAs<ComplimentaryViewModel>();
            viewData.dataItem.guests = GetGuestVMs(workflowInstance.Guests);
            viewData.dataItem.CheckExpenseItem = GetComplimentaryCheckItem(workflowInstance.CheckExpenseItem);

            viewData.dataItem.checkExpense = workflowInstance.CheckExpense;
        }

        private IEnumerable<ComplimentaryCheckItemViewModel> GetComplimentaryCheckItem(IEnumerable<ComplimentaryCheckItemExt> ItemList)
        {
            var checklist = new List<ComplimentaryCheckItemViewModel>();

            foreach(ComplimentaryCheckItemExt ci in ItemList)
            {                
                checklist.Add(new ComplimentaryCheckItemViewModel()
                {
                    Check = ci.Check,
                    CreatedDate = ci.CreatedDate,                    
                    ExplId = ci.ExplId,
                    Id = ci.Id,
                    CheckName = ci.CheckName,
                    RequestHeaderId = ci.RequestHeaderId
                });
            }

            return checklist;
        }

        private IEnumerable<GuestViewModel> GetGuestVMs(IEnumerable<Guest> guestList)
        {

            var guestVMs = new List<GuestViewModel>();
            GuestViewModel guestVM = null;
            foreach (Guest g in guestList)
            {
                guestVM = new GuestViewModel();
                guestVM.Id = g.Id;
                guestVM.Name = g.Name;
                guestVM.Title = g.Title;
                guestVM.CompanyName = g.CompanyName;
                guestVM.RequestHeaderId = g.RequestHeaderId;
                guestVMs.Add(guestVM);
            }

            return guestVMs;
        }

        private IEnumerable<Guest> GetRequestGuestModel(IEnumerable<GuestViewModel> requestGuestViewlModels)
        {
            var requestGuests = new List<Guest>();
            if (requestGuestViewlModels != null)
            {
                foreach (GuestViewModel guestVM in requestGuestViewlModels)
                {
                    var g = new Guest()
                    {
                        Id = guestVM.Id,
                        Name = guestVM.Name,
                        Title = guestVM.Title,
                        CompanyName = guestVM.CompanyName,
                        RequestHeaderId = guestVM.RequestHeaderId
                    };
                    requestGuests.Add(g);
                }
            }

            return requestGuests;
        }
        
    }
}