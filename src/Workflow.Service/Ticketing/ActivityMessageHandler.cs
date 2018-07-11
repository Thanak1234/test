/**
*@author : Phanny
*/
using System;
using System.Collections.Generic;
using Workflow.Business.Ticketing;
using Workflow.Business.Ticketing.Dto;
using Workflow.Domain.Entities.Ticket;

namespace Workflow.Service.Ticketing
{
    public class ActivityMessageHandler : IActivityMessageHandler , ITicketMessaging
    {
        private Object ticketMessage = null;
        private List<int> activityIds = new List<int>();
        private IDataProcessingProvider dataPsProvider;
        public ActivityMessageHandler(IDataProcessingProvider dataPsProvider)
        {
            this.dataPsProvider = dataPsProvider;
        }


        public void onActivityCreation(TicketActivity activity, AbstractTicketParam tkParam)
        {
            activityIds.Add(activity.Id);
        }

        public void onTicketCreation(Ticket ticket)
        { 
            ticketMessage = new {
                ticketNo = ticket.TicketNo,
                message =""
            };
        }

        public object getMessage()
        {
            if(ticketMessage != null)
            {
                return ticketMessage;
            }
            else
            {
                var actvities = dataPsProvider.getActivity(activityIds);
                //List<FileUploadInfo> fileInfos = null;
                IFillMoreActData fillMoreActData = new FillMoreActData(dataPsProvider);

                actvities.ForEach(t =>
                {

                    fillMoreActData.fill(null, t);
                });

                return actvities;
            }   
        }
    }
}
