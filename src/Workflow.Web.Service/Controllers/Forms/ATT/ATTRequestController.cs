using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Workflow.Domain.Entities.HR;
using Workflow.Domain.Entities.BatchData;
using Workflow.Service;
using Workflow.Service.Interfaces;
using Workflow.Web.Service.Controllers.Common;
using Workflow.Web.Models.HumanResource.ATT;

namespace Workflow.Web.Service.Controllers
{
    public class ATTRequestController : AbstractServiceController<ATTRequestWorkflowInstance, ATTRequestFormViewModel>
    {

        public ATTRequestController() : base(){ }

        protected override ATTRequestFormViewModel CreateNewFormDataViewModel()
        {
            return new ATTRequestFormViewModel();
        }

        protected override void MoreMapDataBC(ATTRequestFormViewModel viewData, ATTRequestWorkflowInstance workflowInstance)
        {
            workflowInstance.TravelDetail = viewData.dataItem.travelDetail.TypeAs<TravelDetail>();

            workflowInstance.Destinations = GetRequestDestinationModel(viewData.dataItem.destinations);
            workflowInstance.AddRequestDestinations = GetRequestDestinationModel(viewData.dataItem.addRequestDestinations);
            workflowInstance.EditRequestDestinations = GetRequestDestinationModel(viewData.dataItem.editRequestDestinations);
            workflowInstance.DelRequestDestinations = GetRequestDestinationModel(viewData.dataItem.delRequestDestinations);

            workflowInstance.FlightDetails = GetRequestFlightDetailModel(viewData.dataItem.flightDetails);
            workflowInstance.AddRequestFlightDetails = GetRequestFlightDetailModel(viewData.dataItem.addRequestflightDetails);
            workflowInstance.EditRequestFlightDetails = GetRequestFlightDetailModel(viewData.dataItem.editRequestflightDetails);
            workflowInstance.DelRequestFlightDetails = GetRequestFlightDetailModel(viewData.dataItem.delRequestflightDetails);
        }

        protected override void MoreMapDataView(ATTRequestWorkflowInstance workflowInstance, ATTRequestFormViewModel viewData)
        {
            viewData.dataItem.travelDetail = workflowInstance.TravelDetail.TypeAs<ATTTravelDetailViewModel>();
            viewData.dataItem.destinations = GetDestinationVMs(workflowInstance.Destinations);
            viewData.dataItem.flightDetails = GetFlightDetailVMs(workflowInstance.FlightDetails);
        }

        private IEnumerable<ATTDestinationViewModel> GetDestinationVMs(IEnumerable<Destination> list)
        {

            var vms = new List<ATTDestinationViewModel>();
            ATTDestinationViewModel vm = null;
            foreach (Destination d in list)
            {
                vm = new ATTDestinationViewModel();
                vm.id = d.Id;
                vm.fromDestination = d.FromDestination;
                vm.toDestination = d.ToDestination;
                vm.date = d.Date;
                vm.time = d.Time;
                vm.requestHeaderId = d.RequestHeaderId;
                vms.Add(vm);
            }

            return vms;
        }

        private IEnumerable<ATTFlightDetailViewModel> GetFlightDetailVMs(IEnumerable<FlightDetail> list)
        {

            var vms = new List<ATTFlightDetailViewModel>();
            ATTFlightDetailViewModel vm = null;
            foreach (FlightDetail d in list)
            {
                vm = new ATTFlightDetailViewModel();
                vm.id = d.Id;
                vm.fromDestination = d.FromDestination;
                vm.toDestination = d.ToDestination;
                vm.date = d.Date;
                vm.time = d.Time;
                vm.requestHeaderId = d.RequestHeaderId;
                vms.Add(vm);
            }

            return vms;
        }


        protected override IRequestFormService<ATTRequestWorkflowInstance> GetRequestformService()
        {
            return new ATTRequestFormService();
        }

        private IEnumerable<Destination> GetRequestDestinationModel(IEnumerable<ATTDestinationViewModel> requestDestinationViewlModels)
        {
            var list = new List<Destination>();
            
            if (requestDestinationViewlModels != null) {
                foreach(ATTDestinationViewModel g in requestDestinationViewlModels)
                {
                    var d = new Destination()
                    {
                        Id = g.id,
                        FromDestination = g.fromDestination,
                        ToDestination = g.toDestination,
                        Date = g.date,
                        Time = g.time,
                        RequestHeaderId = g.requestHeaderId
                    };
                    list.Add(d);
                }
            }

            return list;
        }

        private IEnumerable<FlightDetail> GetRequestFlightDetailModel(IEnumerable<ATTFlightDetailViewModel> requestFlightDetailViewlModels)
        {
            var list = new List<FlightDetail>();

            if (requestFlightDetailViewlModels != null)
            {
                foreach (ATTFlightDetailViewModel g in requestFlightDetailViewlModels)
                {
                    var d = new FlightDetail()
                    {
                        Id = g.id,
                        FromDestination = g.fromDestination,
                        ToDestination = g.toDestination,
                        Date = g.date,
                        Time = g.time,
                        RequestHeaderId = g.requestHeaderId
                    };
                    list.Add(d);
                }
            }

            return list;
        }

        
    }

}